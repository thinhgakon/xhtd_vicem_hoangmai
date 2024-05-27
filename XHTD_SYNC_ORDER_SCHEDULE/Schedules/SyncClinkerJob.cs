using HMXHTD.Data.DataEntity;
using HMXHTD.Services.Services;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
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
    public class SyncClinkerJob : IJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public SyncClinkerJob(IServiceFactory serviceFactory)
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
            log.Info("==============start process SyncOrderVoidedJob ====================");
            // GetDataFromDb();
            //SyncRFID();
            //UnladenWeight();
        }
        public void SyncRFID()
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var datetime = DateTime.Now.AddDays(-1);
                var vehicles = db.tblRFIDs.Where(x => x.DayCreate > datetime).ToList();
                foreach (var vehicle in vehicles)
                {
                    var sqlUpdate = $@"UPDATE dbo.tblStoreOrderOperating SET CardNo = @CardNo WHERE Vehicle = @Vehicle and ISNULL(CardNo, '') = ''  ";
                    var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@CardNo", vehicle.Code), new SqlParameter("@Vehicle", vehicle.Vehicle));
                }
            }
        }
        public void UnladenWeight()
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var sqlSelect = "SELECT DISTINCT Vehicle FROM dbo.tblStoreOrderOperating WHERE  DriverUserName != '' AND Step = 9";
                var vehicles = db.Database.SqlQuery<string>(sqlSelect).ToList();
                foreach (var vehicle in vehicles)
                {
                    var sqlItem = $@"SELECT TOP 1 * FROM tblStoreOrderOperating WHERE DriverUserName != '' AND Step = 9 AND Vehicle = '{vehicle}'";
                    var order = db.Database.SqlQuery<tblStoreOrderOperating>(sqlItem).SingleOrDefault();
                    var weightNull = GetWeightNull(order.DeliveryCode);
                    if(weightNull > 0)
                    {
                        var sqlUpdate = "UPDATE dbo.tblVehicle SET TonnageDefault = @TonnageDefault WHERE Vehicle = @Vehicle";
                        var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@TonnageDefault", weightNull), new SqlParameter("@Vehicle", order.Vehicle));
                    }

                }
            }
            
        }
        public int GetWeightNull(string deliveryCode)
        {
            double weight = 0;
            try
            {
                #region Oracle
                string sqlQuery = "";
                string strConString = System.Configuration.ConfigurationManager.ConnectionStrings["MbfConnOracle"].ConnectionString.ToString();
               
                sqlQuery = $@"select cvw.* from sales_orders so
                         ,cx_vehicle_weight cvw 
                         where so.delivery_code = cvw.delivery_code 
                         and so.VEHICLE_CODE IS NOT NULL
                         and so.DELIVERY_CODE = :DELIVERY_CODE";
                using (OracleConnection connection = new OracleConnection(strConString))
                {
                    OracleCommand Cmd = new OracleCommand(sqlQuery, connection);
                   
                    Cmd.Parameters.Add(new OracleParameter("DELIVERY_CODE", deliveryCode));
                    connection.Open();
                    using (OracleDataReader Rd = Cmd.ExecuteReader())
                    {
                        while (Rd.Read())
                        {
                            Double.TryParse(Rd["LOADWEIGHTNULL"]?.ToString(), out weight);
                            break;
                        }
                    }
                }
                
                #endregion
            }
            catch (Exception ex)
            {

            }
            return (int)(weight * 1000);
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
                                cust.CUSTOMER_NAME,
                                c2.name_store,
                                c2.code_store
                                from sales_orders so
                                 ,dev_om_item_list_v item
                                 ,dev_customers_all_v cust
                                 , ORDER_C2 c2
                                where 
                                so.customer_id = cust.customer_id
                                and so.inventory_item_id = item.inventory_item_id 
                                and so.delivery_code = c2.delivery_code(+) 
                                and so.STATUS = 'RECEIVED'
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
                    var startDate = DateTime.Now.AddDays(-4).ToString("dd/MM/yyyy");
                    var endDate = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
                    Cmd.Parameters.Add(new OracleParameter("startDate", startDate));
                    Cmd.Parameters.Add(new OracleParameter("endDate", endDate));
                    connection.Open();
                    //Cmd.Parameters.Add("CodeStore", SqlDbType.NVarChar).Value = objVehicleStoreParam.CodeStore;
                    using (OracleDataReader Rd = Cmd.ExecuteReader())
                    {
                        while (Rd.Read())
                        {
                            var dateOrder = DateTime.Now;
                            DateTime.TryParse(Rd["ORDER_DATE"]?.ToString(), out dateOrder);
                            orderModel = new OrderOracleModel();
                            orderModel.ORDER_ID = Int32.Parse(Rd["ORDER_ID"].ToString());
                            orderModel.ORDER_DATE = dateOrder;
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
                            orderModel.NAME_STORE = Rd["NAME_STORE"]?.ToString();
                            orderModel.CODE_STORE = Rd["CODE_STORE"]?.ToString();
                            orderModel.INVENTORY_ITEM_ID = Int32.Parse(Rd["INVENTORY_ITEM_ID"]?.ToString());
                            objList.Add(orderModel);
                        }
                    }
                }
                if (objList.Count > 0)
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
                if (stateId == 6)
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        var sqlUpdate = "UPDATE dbo.tblStoreOrderOperating SET Step = 8, Confirm1 = 1, Confirm2 = 1,  Confirm3 = 1, Confirm4 = 1, Confirm5 = 1, Confirm6 = 1, Confirm7 = 1, Confirm8 = 1,IndexOrder = 0 WHERE ISNULL(DriverUserName, '') = '' AND Step < 8 AND DeliveryCode = @DeliveryCode";
                        var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@DeliveryCode", orderModel.DELIVERY_CODE));
                    }
                }
                else if (stateId == 2)
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
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
