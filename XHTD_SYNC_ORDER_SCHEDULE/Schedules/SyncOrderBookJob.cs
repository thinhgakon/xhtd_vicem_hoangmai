﻿using HMXHTD.Data.DataEntity;
using HMXHTD.Services.Services;
using Newtonsoft.Json;
using Quartz;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XHTD_SYNC_ORDER_SCHEDULE.Models;

namespace XHTD_SYNC_ORDER_SCHEDULE.Schedules
{
    public class SyncOrderBookJob : IJob
    {
        private static string strToken;
        private static DateTime expireTimeToken;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public SyncOrderBookJob(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            await Task.Run(() =>
            {
                SyncOrderProcess();
            });
        }
        public void SyncOrderProcess()
        {
            log.Info("==============start process SyncOrderBookJob ====================");
            getToken();
            getBillOrder();
        }
        private string getToken()
        {
            if (!String.IsNullOrEmpty(strToken) && expireTimeToken > DateTime.Now.AddMinutes(60 * 5))
            {
                return strToken;
            }


            var requestData = new TokenRequestModel
            {
                userName = ConfigurationManager.AppSettings.Get("userNameAPI").ToString(),
                password = ConfigurationManager.AppSettings.Get("passwordAPI").ToString(),
                OUId = "302"
            };
            try
            {
                var client = new RestClient(ConfigurationManager.AppSettings.Get("LinkAPI_WebSale").ToString() + "/api/get-token");
                var request = new RestRequest(Method.POST);

                request.AddJsonBody(requestData);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.RequestFormat = DataFormat.Json;
                IRestResponse response = client.Execute(request);
                var content = response.Content;

                var responseData = JsonConvert.DeserializeObject<TokenResponseModel>(content);
                strToken = responseData.token;
                expireTimeToken = responseData.expires;

                return responseData.token;
            }
            catch (Exception ex)
            {
                log.Error("getToken" + ex.Message);
                return "";
            }
        }

        private void getBillOrder()
        {
            try
            {
                var distributors = _serviceFactory.Distributor.GetListDistributors();
                distributors.Add(new tblDistributor { IDDistributorSyn = 56231 });
                distributors.Add(new tblDistributor { IDDistributorSyn = 97231 });
                //56231
                //97231
                //  distributors.Add(new tblDistributor { IDDistributorSyn = 2121 });

                if (distributors.Count < 1) return;
                foreach (var distributor in distributors)
                {
                    //if (distributor.IDDistributorSyn != 2173) continue;
                    processBillOrderByDistributor(distributor);
                }

            }
            catch (Exception ex)
            {
                log.Error("getBillOrder" + ex.Message);
            }
        }
        private void processBillOrderByDistributor(tblDistributor distributor)
        {
            try
            {
                var requestData = new OrderSearchRequestModel
                {
                    pageSize = 200,
                    pageIndex = 1,
                    deliveryCode = "",
                    customerId = distributor.IDDistributorSyn?.ToString(),
                    status = "APPROVED",
                    fromDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    toDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    orderType = 0,
                    inventoryItemId = 0,
                    shippointId = 0,
                    vehicleCode = "",
                    docNum = ""
                };
                var client = new RestClient(ConfigurationManager.AppSettings.Get("LinkAPI_WebSale").ToString() + "/api/search-orders");
                var request = new RestRequest(Method.POST);

                request.AddJsonBody(requestData);
                request.AddHeader("Authorization", "Bearer " + strToken);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.RequestFormat = DataFormat.Json;
                IRestResponse response = client.Execute(request);
                if (response.StatusDescription.Equals("Unauthorized"))
                {
                    strToken = "";
                    SyncOrderProcess();
                }
                var content = response.Content;

                var responseData = JsonConvert.DeserializeObject<OrderSearchResponseModel>(content);
                if (responseData.total < 1) return;
                var orderList = responseData.datas.OrderBy(x => x.ORDER_ID).ToList();

                // nên tạo 1 điểm neo orderId lớn nhất để check có cần kéo về nữa hay không để tránh tốn tài nguyên
                int? maxOrderId = 0;
                using (var db = new HMXuathangtudong_Entities())
                {
                    var maxTemp = db.tblStoreOrderOperatings.Where(x => x.IDDistributorSyn == distributor.IDDistributorSyn).Max(x => x.OrderId);
                    maxOrderId = maxTemp != null ? maxTemp.Value : 0;
                }
                maxOrderId = 0;// chỗ này sau bỏ đi, để điểm neo hoạt động
                foreach (var orderItem in responseData.datas)
                {
                    if (orderItem.ORDER_ID > maxOrderId)
                        //if (orderItem.DELIVERY_CODE != "389944-21") continue;
                        //InsertOrUpdateOrderOperating(orderItem);
                        // chỉ insert các order thôi, không care luồng update
                        //InsertOrderOperating(orderItem, (int)distributor.IDDistributorSyn);
                        InsertOrUpdateOrderOperating(orderItem, (int)distributor.IDDistributorSyn);
                }
            }
            catch (Exception ex)
            {
                log.Error("processBillOrderByDistributor" + ex.Message);
            }
        }
        private void InsertOrUpdateOrderOperating(OrderItemResponse orderModel, int idDistributeSync)
        {
            try
            {
                var stateId = 0;
                var statusName = "";
                switch (orderModel.STATUS.ToUpper())
                {
                    case "BOOKED":
                        switch (orderModel.PRINT_STATUS.ToUpper())
                        {
                            case "BOOKED":
                            case "APPROVED":
                            case "PENDING":
                                stateId = 1;
                                statusName = "Đã xác nhận";
                                break;
                            case "PRINTED":
                                stateId = 4;
                                statusName = "Đã in phiếu";
                                break;
                        }
                        break;
                    case "PRINTED":
                        stateId = 4;
                        statusName = "Đã in phiếu";
                        break;
                    case "VOIDED":
                        stateId = 2;
                        statusName = "Đã hủy";
                        break;
                    case "RECEIVING":
                        stateId = 5;
                        statusName = "Đang lấy hàng";
                        break;
                    case "RECEIVED":
                        stateId = 6;
                        statusName = "Đã xuất hàng";
                        break;
                }
                var status = orderModel.STATUS.ToUpper();
                var statusPrint = orderModel.PRINT_STATUS.ToUpper();
                if (stateId != 2 && stateId != 6)
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.OrderId == orderModel.ORDER_ID);
                        if (orderExist != null)
                        {


                        }
                        else
                        {
                            string TypeProduct = "";
                            if (orderModel.ITEM_NAME.ToUpper().Contains("RỜI"))
                            {
                                TypeProduct = "ROI";
                            }
                            else if (orderModel.ITEM_NAME.ToUpper().Contains("PCB30") || orderModel.ITEM_NAME.ToUpper().Contains("MAX PRO"))
                            {
                                TypeProduct = "PCB30";
                            }
                            else if (orderModel.ITEM_NAME.ToUpper().Contains("PCB40"))
                            {
                                TypeProduct = "PCB40";
                            }
                            var vehicle = orderModel.VEHICLE_CODE.Replace("-", "").Replace("  ", "").Replace(" ", "").Replace("/", "").Replace(".", "").ToUpper();
                            var rfidItem = db.tblRFIDs.FirstOrDefault(x => x.Vehicle.Contains(vehicle));
                            var cardNo = rfidItem?.Code ?? "";
                            DateTime orderDate = DateTime.ParseExact(orderModel?.ORDER_DATE, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            //cardNo = ""; // dành cho test
                            if (stateId == 1 || stateId == 4 || stateId == 5) // sau này chỉ if stateId == 1
                            {
                                var newOrderOperating = new tblStoreOrderOperating
                                {
                                    OrderId = orderModel.ORDER_ID,
                                    Vehicle = vehicle,
                                    DriverName = orderModel.DRIVER_NAME,
                                    NameDistributor = orderModel.CUSTOMER_NAME,
                                    IDDistributorSyn = idDistributeSync,
                                    ItemId = orderModel.INVENTORY_ITEM_ID,
                                    NameProduct = orderModel.ITEM_NAME,
                                    CatId = orderModel.ITEM_CATEGORY,
                                    CardNo = cardNo,
                                    SumNumber = orderModel.ORDER_QUANTITY,
                                    DeliveryCode = orderModel.DELIVERY_CODE,
                                    TypeProduct = TypeProduct,
                                    LocationCode = orderModel.LOCATION_CODE,
                                    OrderDate = orderDate,
                                    State = orderModel.STATUS,
                                    Confirm1 = 0,
                                    Confirm2 = 0,
                                    Confirm3 = 0,
                                    Confirm4 = 0,
                                    Confirm5 = 0,
                                    Confirm6 = 0,
                                    Confirm7 = 0,
                                    Confirm8 = 0,
                                    IndexOrder = 0,
                                    IndexOrder2 = 0,
                                    Step = 0,
                                    IsVoiced = false
                                };
                                db.tblStoreOrderOperatings.Add(newOrderOperating);
                                db.SaveChanges();
                            }
                            else
                            {
                                var newOrderOperating = new tblStoreOrderOperating
                                {
                                    OrderId = orderModel.ORDER_ID,
                                    Vehicle = vehicle,
                                    DriverName = orderModel.DRIVER_NAME,
                                    NameDistributor = orderModel.CUSTOMER_NAME,
                                    IDDistributorSyn = idDistributeSync,
                                    ItemId = orderModel.INVENTORY_ITEM_ID,
                                    NameProduct = orderModel.ITEM_NAME,
                                    CatId = orderModel.ITEM_CATEGORY,
                                    CardNo = cardNo,
                                    SumNumber = orderModel.ORDER_QUANTITY,
                                    DeliveryCode = orderModel.DELIVERY_CODE,
                                    TypeProduct = TypeProduct,
                                    LocationCode = orderModel.LOCATION_CODE,
                                    OrderDate = orderDate,
                                    State = orderModel.STATUS,
                                    IsVoiced = false,
                                    Confirm1 = 1,
                                    Confirm2 = 1,
                                    Confirm3 = 1,
                                    Confirm4 = 1,
                                    Confirm5 = 1,
                                    Confirm6 = 1,
                                    Confirm7 = 1,
                                    IndexOrder = 0,
                                    IndexOrder2 = 0,
                                    TimeConfirm8 = DateTime.Now,
                                    Confirm8 = 1,
                                    TimeConfirm9 = DateTime.Now,
                                    Confirm9 = 1,
                                    Step = 9
                                };
                                db.tblStoreOrderOperatings.Add(newOrderOperating);
                                db.SaveChanges();
                                _serviceFactory.StoreOrderOperating.ReIndexOrderWhenSyncOrderWithEnd((int)newOrderOperating.OrderId);
                            }

                        }
                    }
                }
                else if (stateId == 6)
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        var timeAfter = DateTime.Now.AddHours(-1);
                        var sqlUpdate = "UPDATE dbo.tblStoreOrderOperating SET Step = 8, Confirm1 = 1, Confirm2 = 1,  Confirm3 = 1, Confirm4 = 1, Confirm5 = 1, Confirm6 = 1, Confirm7 = 1, Confirm8 = 1,  IndexOrder = 0 WHERE TimeConfirm6 < @TimeAfter AND Step < 8 AND Step > 5 AND DeliveryCode = @DeliveryCode";
                        var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@TimeAfter", timeAfter), new SqlParameter("@DeliveryCode", orderModel.DELIVERY_CODE));
                    }
                }
                else if (stateId == 2)
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        var timeAfter = DateTime.Now.AddHours(-48);
                        var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == orderModel.DELIVERY_CODE && (x.Step != 8 && x.Step != 9));
                        if (orderExist != null)
                        {

                            orderExist.Confirm1 = 1;
                            orderExist.Confirm2 = 1;
                            orderExist.Confirm3 = 1;
                            orderExist.Confirm4 = 1;
                            orderExist.Confirm5 = 1;
                            orderExist.Confirm6 = 1;
                            orderExist.Confirm7 = 1;
                            orderExist.Confirm8 = 1;
                            orderExist.TimeConfirm8 = DateTime.Now;
                            orderExist.Confirm9 = 1;
                            orderExist.TimeConfirm9 = DateTime.Now;
                            orderExist.IndexOrder = 0;
                            orderExist.Step = 9;
                            orderExist.IsVoiced = true;
                            db.SaveChanges();
                            _serviceFactory.StoreOrderOperating.ReIndexOrderWhenSyncOrderWithEnd((int)orderExist.OrderId);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error($@"InsertOrUpdateOrderOperating + {orderModel?.ORDER_DATE} :" + ex.Message);
            }
        }

    }
}
