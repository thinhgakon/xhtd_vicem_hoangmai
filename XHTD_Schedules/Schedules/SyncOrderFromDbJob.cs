﻿using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.IO.Compression;
using RestSharp;
using XHTD_Schedules.Models;
using Newtonsoft.Json;
using HMXHTD.Data.DataEntity;
using System.Globalization;
using XHTD_Schedules.SignalRNotification;
using HMXHTD.Services.Services;
using System.Configuration;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;

namespace XHTD_Schedules.Schedules
{
    public class SyncOrderFromDbJob : IJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public SyncOrderFromDbJob(IServiceFactory serviceFactory)
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
            GetDataFromDb();
        }
        public void GetDataFromDb()
        {
            try
            {
                #region Oracle
                string sqlQuery = "";
                string strConString = System.Configuration.ConfigurationManager.ConnectionStrings["MbfConnOracle"].ConnectionString.ToString();
                OrderOracleModel orderModel;
                List<OrderOracleModel> objList = new List<OrderOracleModel>();
                sqlQuery = $@"select so.ORDER_ID
                                , so.ORDER_DATE, 
                                so.STATUS, 
                                so.ORDER_QUANTITY, 
                                so.DRIVER_NAME, 
                                so.VEHICLE_CODE, 
                                so.DELIVERY_CODE,
                                so.CUSTOMER_ID,
                                so.BOOK_QUANTITY,
                                so.PRINT_STATUS,
                                so.LOCATION_CODE,
                                so.AREA_ID,
                                so.INVENTORY_ITEM_ID,
                                item.description ITEM_NAME, 
                                cust.CUSTOMER_NAME
                                from sales_orders so
                                 ,dev_om_item_list_v item
                                 ,dev_customers_all_v cust
                                where
                                1 = 1
                                and so.customer_id = cust.customer_id
                                and so.inventory_item_id = item.inventory_item_id
                                and so.STATUS = 'BOOKED' 
                                and so.CREATION_DATE BETWEEN TO_DATE(:startDate,'dd/MM/yyyy')  AND TO_DATE(:endDate,'dd/MM/yyyy') + INTERVAL '86399' second
                                order by so.ORDER_ID desc";
                //sqlQuery = $@"select * 
                //                from sales_orders so
                //                where
                //                1 = 1
                //                and STATUS = 'BOOKED' 
                //                and CREATION_DATE BETWEEN TO_DATE(:startDate,'dd/MM/yyyy')  AND TO_DATE(:endDate,'dd/MM/yyyy') + INTERVAL '86399' second
                //                order by so.ORDER_ID desc";
                using (OracleConnection connection = new OracleConnection(strConString))
                {
                    OracleCommand Cmd = new OracleCommand(sqlQuery, connection);
                    var startDate = DateTime.Now.ToString("dd/MM/yyyy");
                    var endDate = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
                    Cmd.Parameters.Add(new OracleParameter("startDate", startDate));
                    Cmd.Parameters.Add(new OracleParameter("endDate", endDate));
                    connection.Open();
                    //Cmd.Parameters.Add("CodeStore", SqlDbType.NVarChar).Value = objVehicleStoreParam.CodeStore;
                    using (OracleDataReader Rd = Cmd.ExecuteReader())
                    {
                        while (Rd.Read())
                        {
                            log.Info(Rd["ORDER_DATE"].ToString());
                            orderModel = new OrderOracleModel();
                            orderModel.ORDER_ID = Int32.Parse(Rd["ORDER_ID"].ToString());
                            orderModel.ORDER_DATE = DateTime.ParseExact(Rd["ORDER_DATE"]?.ToString() , "M/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                            orderModel.STATUS = Rd["STATUS"].ToString();
                            orderModel.ORDER_QUANTITY = Double.Parse(Rd["ORDER_QUANTITY"].ToString());
                            orderModel.DRIVER_NAME = Rd["DRIVER_NAME"].ToString();
                            orderModel.VEHICLE_CODE = Rd["VEHICLE_CODE"].ToString();
                            orderModel.DELIVERY_CODE = Rd["DELIVERY_CODE"].ToString();
                            orderModel.CUSTOMER_ID = Int32.Parse(Rd["CUSTOMER_ID"].ToString());
                            orderModel.BOOK_QUANTITY = Double.Parse(Rd["BOOK_QUANTITY"].ToString());
                            orderModel.PRINT_STATUS = Rd["PRINT_STATUS"].ToString();
                            orderModel.LOCATION_CODE = Rd["LOCATION_CODE"].ToString();
                            orderModel.AREA_ID = Int32.Parse(Rd["AREA_ID"].ToString());
                            orderModel.ITEM_NAME = Rd["ITEM_NAME"]?.ToString();
                            orderModel.CUSTOMER_NAME = Rd["CUSTOMER_NAME"]?.ToString();
                            orderModel.INVENTORY_ITEM_ID = Int32.Parse(Rd["INVENTORY_ITEM_ID"]?.ToString());
                            objList.Add(orderModel);
                        }
                    }
                }
                if(objList.Count > 0)
                {
                    foreach (var order in objList)
                    {
                        ProcessSyncOrderItem(order);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
            }
        }
        public void ProcessSyncOrderItem(OrderOracleModel orderModel)
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
                            else if (orderModel.ITEM_NAME.ToUpper().Contains("Clinker".ToUpper()))
                            {
                                return;
                            }
                            var vehicle = orderModel.VEHICLE_CODE.Replace("-", "").Replace("  ", "").Replace(" ", "").Replace("/", "").Replace(".", "").ToUpper();
                            var rfidItem = db.tblRFIDs.FirstOrDefault(x => x.Vehicle.Contains(vehicle));
                            var cardNo = rfidItem?.Code ?? "";
                            DateTime orderDate = (DateTime)orderModel.ORDER_DATE;
                            //cardNo = ""; // dành cho test
                            if (stateId == 1 || stateId == 4 || stateId == 5) // sau này chỉ if stateId == 1
                            {
                                var newOrderOperating = new tblStoreOrderOperating
                                {
                                    OrderId = orderModel.ORDER_ID,
                                    Vehicle = vehicle,
                                    DriverName = orderModel.DRIVER_NAME,
                                    NameDistributor = orderModel.CUSTOMER_NAME,
                                    IDDistributorSyn = orderModel.CUSTOMER_ID,
                                    ItemId = orderModel.INVENTORY_ITEM_ID,
                                    NameProduct = orderModel.ITEM_NAME,
                                    //CatId = orderModel.ITEM_CATEGORY,
                                    CardNo = cardNo,
                                    SumNumber = (decimal?)orderModel.BOOK_QUANTITY,
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
                                    IsVoiced = false,
                                    LogJobAttach = $@"SyncOrderJob stateId in (1,4,5) # ",
                                    IsSyncedByNewWS = true
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
                                    IDDistributorSyn = orderModel.CUSTOMER_ID,
                                    ItemId = orderModel.INVENTORY_ITEM_ID,
                                    NameProduct = orderModel.ITEM_NAME,
                                    //CatId = orderModel.ITEM_CATEGORY,
                                    CardNo = cardNo,
                                    SumNumber = (decimal?)orderModel.BOOK_QUANTITY,
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
                                    Step = 9,
                                    LogJobAttach = $@"SyncOrderJob stateId not in (1,4,5) # ",
                                    IsSyncedByNewWS = true
                                };
                                db.tblStoreOrderOperatings.Add(newOrderOperating);
                                db.SaveChanges();
                                _serviceFactory.StoreOrderOperating.ReIndexOrderWhenSyncOrderWithEnd((int)newOrderOperating.OrderId);
                            }

                            new MyHub().Send("New_Order_Sync", String.Format("Có đơn hàng mới, orderId {0} . Cập nhật lúc : {1}", orderModel.ORDER_ID, DateTime.Now));
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
                            orderExist.LogJobAttach = $@"{orderExist.LogJobAttach} # syncorder stateId 2 ";
                            db.SaveChanges();
                            _serviceFactory.StoreOrderOperating.ReIndexOrderWhenSyncOrderWithEnd((int)orderExist.OrderId);
                            new MyHub().Send("ReIndex_Order", String.Format("Cập nhật lốt xe trong bãi chờ. Cập nhật vào lúc : {0}", DateTime.Now));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
    }
}
