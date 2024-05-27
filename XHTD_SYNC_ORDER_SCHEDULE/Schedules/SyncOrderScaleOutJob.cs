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
    public class SyncOrderScaleOutJob : IJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public SyncOrderScaleOutJob(IServiceFactory serviceFactory)
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
                SyncOrderScaleOutProcess();
            });
        }
        public void SyncOrderScaleOutProcess()
        {
            log.Info("==============start process SyncOrderScaleOutProcess ====================");
            GetDataFromDb();
        }
        public void GetDataFromDb()
        {
            try
            {
                #region Oracle
                string sqlQuery = "";
                string strConString = System.Configuration.ConfigurationManager.ConnectionStrings["MbfConnOracle"].ConnectionString.ToString();
               
                double weightNull = 0;
                double weightFull = 0;
                DateTime timeIn;
                DateTime timeOut;
                List<OrderOracleModel> objList = new List<OrderOracleModel>();
                sqlQuery = $@"select so.*, cvw.LOADWEIGHTNULL, cvw.LOADWEIGHTFULL,cvw.ITEMNAME as ITEM_NAME, cvw.TIMEIN, cvw.TIMEOUT  from sales_orders so
                         ,cx_vehicle_weight cvw 
                         where so.delivery_code = cvw.delivery_code 
                         and so.VEHICLE_CODE IS NOT NULL
                         and cvw.TIMEOUT > :STARTTIME
                         and cvw.LOADWEIGHTFULL > 1
                         order by cvw.TIMEOUT desc";
                //cvw.TIMEOUT > TO_DATE('07/26/2021 13:22:52', 'MM/DD/YYYY HH24:MI:SS')
                using (OracleConnection connection = new OracleConnection(strConString))
                {
                    OracleCommand Cmd = new OracleCommand(sqlQuery, connection);
                    var startDate = DateTime.Now.AddMinutes(-5);
                    Cmd.Parameters.Add(new OracleParameter("STARTTIME", startDate));
                    connection.Open();
                    //Cmd.Parameters.Add("CodeStore", SqlDbType.NVarChar).Value = objVehicleStoreParam.CodeStore;
                    using (OracleDataReader Rd = Cmd.ExecuteReader())
                    {
                        while (Rd.Read())
                        {
                            OrderOracleModel orderModel = new OrderOracleModel();
                            Double.TryParse(Rd["LOADWEIGHTNULL"]?.ToString(), out weightNull);
                            Double.TryParse(Rd["LOADWEIGHTFULL"]?.ToString(), out weightFull);
                            DateTime.TryParse(Rd["TIMEIN"]?.ToString(), out timeIn);
                            DateTime.TryParse(Rd["TIMEOUT"]?.ToString(), out timeOut);
                            orderModel.ORDER_ID = Int32.Parse(Rd["ORDER_ID"].ToString());
                            orderModel.STATUS = Rd["STATUS"].ToString();
                            orderModel.ORDER_QUANTITY = Double.Parse(Rd["ORDER_QUANTITY"].ToString());
                            orderModel.DRIVER_NAME = Rd["DRIVER_NAME"].ToString();
                            orderModel.VEHICLE_CODE = Rd["VEHICLE_CODE"].ToString();
                            orderModel.MOOC_CODE = Rd["MOOC_CODE"].ToString();
                            orderModel.DELIVERY_CODE = Rd["DELIVERY_CODE"].ToString();
                            orderModel.CUSTOMER_ID = Int32.Parse(Rd["CUSTOMER_ID"].ToString());
                            orderModel.BOOK_QUANTITY = Double.Parse(Rd["BOOK_QUANTITY"].ToString());
                            orderModel.PRINT_STATUS = Rd["PRINT_STATUS"].ToString();
                            orderModel.LOCATION_CODE = Rd["LOCATION_CODE"].ToString();
                            orderModel.AREA_ID = Int32.Parse(Rd["AREA_ID"].ToString());
                            orderModel.ITEM_NAME = Rd["ITEM_NAME"]?.ToString();
                            orderModel.TIMEIN = timeIn;
                            orderModel.TIMEOUT = timeOut;
                            orderModel.WEIGHTNULL = weightNull;
                            orderModel.WEIGHTFULL = weightFull;
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
                log.Info($@"Has new order scale out, deliverycode is {orderModel.DELIVERY_CODE}");
                using (var db = new HMXuathangtudong_Entities())
                {
                    var query = $@"UPDATE  dbo.tblStoreOrderOperating
                                SET     Step = 9 ,
                                        Confirm1 = 1 ,
                                        TimeConfirm1 = GETDATE() ,
                                        Confirm2 = 1 ,
                                        TimeConfirm2 = GETDATE() ,
                                        Confirm3 = 1 ,
                                        TimeConfirm3 = GETDATE() ,
                                        Confirm4 = 1 ,
                                        TimeConfirm4 = GETDATE() ,
                                        Confirm5 = 1 ,
                                        TimeConfirm5 = GETDATE() ,
                                        Confirm6 = 1 ,
                                        TimeConfirm6 = GETDATE() ,
                                        Confirm7 = 1 ,
                                        TimeConfirm7 = GETDATE() ,
                                        Confirm8 = 1 ,
                                        TimeConfirm8 = GETDATE() ,
                                        Confirm9 = 1 ,
                                        TimeConfirm9 = GETDATE() ,
		                                Confirm9Note = N'Hệ thống kết thúc tự động',
		                                LogProcessOrder = CONCAT(LogProcessOrder, N'#Kết thúc đơn hàng tự động do ngoại lệ', FORMAT(getdate(), 'dd/MM/yyyy HH:mm:ss')),
                                        IndexOrder = 0
                                WHERE   DeliveryCode = @DeliveryCode
                                AND Step = 0";
                    var InsertResponse = db.Database.ExecuteSqlCommand(query, new SqlParameter("@DeliveryCode", orderModel.DELIVERY_CODE));
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
    }
}
