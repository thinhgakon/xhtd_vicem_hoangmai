using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using XHTD_Schedules.Models;
using Newtonsoft.Json;
using HMXHTD.Data.DataEntity;
using System.Globalization;
using XHTD_Schedules.SignalRNotification;
using HMXHTD.Services.Services;
using XHTD_Schedules.ApiScales;
using System.Configuration;
using System.Data.SqlClient;
using XHTD_Schedules.Models.UpWebsale;

namespace XHTD_Schedules.Schedules
{
    public class SyncOrderNewWSJob : IJob
    {
        private static string strToken;
        private static DateTime expireTimeToken;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public SyncOrderNewWSJob(IServiceFactory serviceFactory)
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
            var timestart = DateTime.Now;
            log.Info("==============start process SyncOrderProcess ====================");
            GetToken();
            GetBillOrder();
            var timeEnd = DateTime.Now;

        }
        private string GetToken()
        {
            if (!String.IsNullOrEmpty(strToken) && expireTimeToken > DateTime.Now.AddMinutes(60))
            {
                return strToken;
            }


            var requestData = new TokenScaleRequestModel
            {
                client_id = ConfigurationManager.AppSettings.Get("client_id").ToString(),
                client_secret = ConfigurationManager.AppSettings.Get("client_secret_upwebsale").ToString(),
                grant_type = "password",
                username = "mobifone",
                password = "mobi@A123456"
            };
            try
            {
                var client = new RestClient(ConfigurationManager.AppSettings.Get("LinkAPI_UpWebsale").ToString() + "/connect/token");
                var request = new RestRequest(Method.POST);

                request.AddHeader("Accept", "application/json");
                request.AddParameter("application/x-www-form-urlencoded", $"client_id={requestData.client_id}&client_secret={requestData.client_secret}&grant_type={requestData.grant_type}&username={requestData.username}&password={requestData.password}", ParameterType.RequestBody);
                request.RequestFormat = DataFormat.Json;
                IRestResponse response = client.Execute(request);
                var content = response.Content;

                var responseData = JsonConvert.DeserializeObject<TokenScaleResponseModel>(content);
                strToken = responseData.access_token;
                expireTimeToken = DateTime.Now.AddSeconds(responseData.expires_in);
                return responseData.access_token;
            }
            catch (Exception ex)
            {
                log.Error("getToken " + ex.StackTrace);
                return "";
            }
        }

        private void GetBillOrder()
        {
            try
            {
                var distributors = _serviceFactory.Distributor.GetListDistributors();
                distributors.Add(new tblDistributor { IDDistributorSyn = 56231 });
                distributors.Add(new tblDistributor { IDDistributorSyn = 97231 });
                //  distributors.Add(new tblDistributor { IDDistributorSyn = 2121 });

                if (distributors.Count < 1) return;
                foreach (var distributor in distributors)
                {
                  //  if (distributor.IDDistributorSyn != 2164) continue;
                    ProcessBillOrderByDistributor(distributor);
                }

            }
            catch (Exception ex)
            {
                log.Error("getBillOrder" + ex.Message);
            }
        }
        private void ProcessBillOrderByDistributor(tblDistributor distributor)
        {
            try
            {
                var tokenString = GetToken();
                var requestData = new UpWebsale_OrderSearchRequestModel
                {
                    limit = 200,
                    offset = 1,
                    from = DateTime.Now.AddHours(-2).ToString("dd/MM/yyyy"),
                    to = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"),
                    customerId = (int)distributor.IDDistributorSyn
                };

                var client = new RestClient(ConfigurationManager.AppSettings.Get("LinkAPI_UpWebsale").ToString() + "/api/order/search");
                var request = new RestRequest(Method.POST);

                request.AddJsonBody(requestData);
                request.AddHeader("Authorization", "Bearer " + tokenString);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.RequestFormat = DataFormat.Json;
                IRestResponse response = client.Execute(request);
                var content = response.Content;

                var responseData = JsonConvert.DeserializeObject<UpWebsale_OrderSearchResponseModel>(content);
                if (responseData.total < 1) return;
                var orderList = responseData.collection.OrderBy(x => x.id).ToList();

                // nên tạo 1 điểm neo orderId lớn nhất để check có cần kéo về nữa hay không để tránh tốn tài nguyên
                int? maxOrderId = 0;
                using (var db = new HMXuathangtudong_Entities())
                {
                    var maxTemp = db.tblStoreOrderOperatings.Where(x => x.IDDistributorSyn == distributor.IDDistributorSyn).Max(x => x.OrderId);
                    maxOrderId = maxTemp != null ? maxTemp.Value : 0;
                }
                maxOrderId = 0;// chỗ này sau bỏ đi, để điểm neo hoạt động
                foreach (var orderItem in responseData.collection)
                {
                    if (orderItem.id > maxOrderId)
                        InsertOrUpdateOrderOperating(orderItem, (int)distributor.IDDistributorSyn);
                }
            }
            catch (Exception ex)
            {
                log.Error("processBillOrderByDistributor" + ex.Message);
            }
        }
        private void InsertOrUpdateOrderOperating(UpWebsale_OrderItemResponse orderModel, int idDistributeSync)
        {
            try
            {
                var stateId = 0;
                var statusName = "";
                switch (orderModel.status.ToUpper())
                {
                    case "BOOKED":
                        switch (orderModel.orderPrintStatus.ToUpper())
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
                var status = orderModel.status.ToUpper();
                var statusPrint = orderModel.orderPrintStatus.ToUpper();
                if (stateId != 2 && stateId != 6)
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.OrderId == orderModel.id);
                        if (orderExist != null)
                        {


                        }
                        else
                        {
                            string TypeProduct = "";
                            if (orderModel.productName.ToUpper().Contains("RỜI"))
                            {
                                TypeProduct = "ROI";
                            }
                            else if (orderModel.productName.ToUpper().Contains("PCB30") || orderModel.productName.ToUpper().Contains("MAX PRO"))
                            {
                                TypeProduct = "PCB30";
                            }
                            else if (orderModel.productName.ToUpper().Contains("PCB40"))
                            {
                                TypeProduct = "PCB40";
                            }
                            var vehicle = orderModel.vehicleCode.Replace("-", "").Replace("  ", "").Replace(" ", "").Replace("/", "").Replace(".", "").ToUpper();
                            var rfidItem = db.tblRFIDs.FirstOrDefault(x => x.Vehicle.Contains(vehicle));
                            var cardNo = rfidItem?.Code ?? "";
                            DateTime orderDate = DateTime.ParseExact(orderModel?.orderDate?.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            //cardNo = ""; // dành cho test
                            if (stateId == 1 || stateId == 4 || stateId == 5) // sau này chỉ if stateId == 1
                            {
                                var newOrderOperating = new tblStoreOrderOperating
                                {
                                    OrderId = orderModel.id,
                                    Vehicle = vehicle,
                                    DriverName = orderModel.driverName,
                                    NameDistributor = orderModel.customerName,
                                    IDDistributorSyn = idDistributeSync,
                                    //ItemId = orderModel.INVENTORY_ITEM_ID,
                                    NameProduct = orderModel.productName,
                                    //CatId = orderModel.ITEM_CATEGORY,
                                    CardNo = cardNo,
                                    SumNumber = (decimal?)orderModel.orderQuantity,
                                    DeliveryCode = orderModel.deliveryCode,
                                    TypeProduct = TypeProduct,
                                    OrderDate = orderDate,
                                    State = orderModel.status,
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
                                    IsVoiced = false,
                                    IsSyncedByNewWS = true
                                };
                                db.tblStoreOrderOperatings.Add(newOrderOperating);
                               // db.SaveChanges();
                            }
                            else
                            {
                                var newOrderOperating = new tblStoreOrderOperating
                                {
                                    OrderId = orderModel.id,
                                    Vehicle = vehicle,
                                    DriverName = orderModel.driverName,
                                    NameDistributor = orderModel.customerName,
                                    IDDistributorSyn = idDistributeSync,
                                    //ItemId = orderModel.INVENTORY_ITEM_ID,
                                    NameProduct = orderModel.productName,
                                    //CatId = orderModel.ITEM_CATEGORY,
                                    CardNo = cardNo,
                                    SumNumber = (decimal?)orderModel.orderQuantity,
                                    DeliveryCode = orderModel.deliveryCode,
                                    TypeProduct = TypeProduct,
                                    OrderDate = orderDate,
                                    State = orderModel.status,
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
                                    Step = 9,
                                    IsSyncedByNewWS = true
                                };
                                db.tblStoreOrderOperatings.Add(newOrderOperating);
                               // db.SaveChanges();
                              //  _serviceFactory.StoreOrderOperating.ReIndexOrderWhenSyncOrderWithEnd((int)newOrderOperating.OrderId);
                            }

                            new MyHub().Send("New_Order_Sync", String.Format("Có đơn hàng mới, orderId {0} . Cập nhật lúc : {1}", orderModel.id, DateTime.Now));
                        }
                    }
                }
                else if (stateId == 6)
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        var timeAfter = DateTime.Now.AddHours(-1);
                        var sqlUpdate = "UPDATE dbo.tblStoreOrderOperating SET Step = 8, Confirm1 = 1, Confirm2 = 1,  Confirm3 = 1, Confirm4 = 1, Confirm5 = 1, Confirm6 = 1, Confirm7 = 1, Confirm8 = 1,  IndexOrder = 0 WHERE TimeConfirm6 < @TimeAfter AND Step < 8 AND Step > 5 AND DeliveryCode = @DeliveryCode";
                        var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@TimeAfter", timeAfter), new SqlParameter("@DeliveryCode", orderModel.deliveryCode));
                    }
                }
                else if (stateId == 2)
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        var timeAfter = DateTime.Now.AddHours(-48);
                        var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == orderModel.deliveryCode && (x.Step != 8 && x.Step != 9));
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
                            orderExist.IsSyncedByNewWS = true;
                         //   db.SaveChanges();
                        //    _serviceFactory.StoreOrderOperating.ReIndexOrderWhenSyncOrderWithEnd((int)orderExist.OrderId);
                         //   new MyHub().Send("ReIndex_Order", String.Format("Cập nhật lốt xe trong bãi chờ. Cập nhật vào lúc : {0}", DateTime.Now));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error($@"InsertOrUpdateOrderOperating + {orderModel?.deliveryCode} :" + ex.Message);
            }
        }

    }
}
