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
    public class SyncOrderSendTroughJob : IJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public SyncOrderSendTroughJob(IServiceFactory serviceFactory)
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
                SyncOrderTroughProcess();
            });
        }
        public void SyncOrderTroughProcess()
        {
            log.Info("==============start process SyncOrderTroughProcess ====================");
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
                         and cvw.TIMEIN > :STARTTIME
                         and cvw.LOADWEIGHTFULL IS NULL
                         and cvw.LOADWEIGHTNULL > 1
                         order by cvw.TIMEIN desc";
                using (OracleConnection connection = new OracleConnection(strConString))
                {
                    OracleCommand Cmd = new OracleCommand(sqlQuery, connection);
                    var startDate = DateTime.Now.AddMinutes(-3);
                    Cmd.Parameters.Add(new OracleParameter("STARTTIME", startDate));
                    connection.Open();
                    //Cmd.Parameters.Add("CodeStore", SqlDbType.NVarChar).Value = objVehicleStoreParam.CodeStore;
                    using (OracleDataReader Rd = Cmd.ExecuteReader())
                    {
                        while (Rd.Read())
                        {
                            log.Info($@"=======Log===Has new order scale in, deliverycode is {Rd["DELIVERY_CODE"].ToString()}");
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
                log.Info($@"Has new order scale in, deliverycode is {orderModel.DELIVERY_CODE}");
                using (var db = new HMXuathangtudong_Entities())
                    {
                        var orderExist = db.tblDeliveryCodeTroughOperatings.FirstOrDefault(x => x.DeliveryCode == orderModel.DELIVERY_CODE);
                        if (orderExist != null)
                        {


                        }
                        else
                        {
                            var configLocation = db.tblConfigOperatings.FirstOrDefault(x => x.Code == "LocationXiGiong")?.ValueString;
                            string TypeProduct = "";
                            if (orderModel.LOCATION_CODE.Contains(".XK"))
                            {
                                TypeProduct = "XK";
                            }
                            else if (configLocation.Contains(orderModel.LOCATION_CODE))
                            {
                                TypeProduct = "XK";
                            }
                            else if (orderModel.ITEM_NAME.ToUpper().Contains("RỜI"))
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
                                TypeProduct = "CLINKER";
                            }
                        if(TypeProduct == "PCB40" || TypeProduct == "PCB30" || TypeProduct == "XK")
                        {
                            var vehicle = orderModel.VEHICLE_CODE.Replace("-", "").Replace("  ", "").Replace(" ", "").Replace("/", "").Replace(".", "").ToUpper();
                            var query = $@"if not exists(SELECT TOP 1 * from dbo.tblDeliveryCodeTroughOnDemandOperating where DeliveryCode = @DeliveryCode)            
                                BEGIN            
                                    INSERT INTO dbo.tblDeliveryCodeTroughOnDemandOperating
                                        ( DeliveryCode ,
                                            Vehicle ,
                                            IsSentTrough ,
                                            CreatedOn ,
                                            ModifiedOn ,
                                            TimeSendTrough ,
                                            LogHistory ,
                                            Flag
                                        )
                                VALUES  ( @DeliveryCode , -- DeliveryCode - varchar(50)
                                            @Vehicle , -- Vehicle - varchar(50)
                                            0 , -- IsSentTrough - bit
                                            GETDATE() , -- CreatedOn - datetime
                                            GETDATE() , -- ModifiedOn - datetime
                                            NULL , -- TimeSendTrough - datetime
                                            N'' , -- LogHistory - nvarchar(500)
                                            0  -- Flag - int
                                        )
                                End ";
                            var InsertResponse = db.Database.ExecuteSqlCommand(query, new SqlParameter("@DeliveryCode", orderModel.DELIVERY_CODE), new SqlParameter("@Vehicle", vehicle));
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
