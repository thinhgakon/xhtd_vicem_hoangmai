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
    public class AutoRemoveCallJob : IJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public AutoRemoveCallJob(IServiceFactory serviceFactory)
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
                AutoRemoveCallProcess();
            });
        }
        public void AutoRemoveCallProcess()
        {
            if (_serviceFactory.ConfigOperating.GetValueByCode(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name) == 0) return;
            SyncOrderCallVoice();
        }

        private void SyncOrderCallVoice()
        {
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var orders = db.tblStoreOrderOperatings.Where(x => (x.Step == 4 || x.Step == 1) && (x.DriverUserName ?? "") != "").ToList();

                    if (orders.Count < 1) return;
                    foreach (var order in orders)
                    {
                        ProcessOrder(order.DeliveryCode);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("SyncOrderCallVoice" + ex.Message);
            }
        }
        private void ProcessOrder(string deliveryCode)
        {
            try
            {
                var isScaleIn = CheckIsScaleInByDeliveryCode(deliveryCode);
                if (isScaleIn)
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                        orderExist.Confirm2 = 1;
                        orderExist.TimeConfirm2 = DateTime.Now;
                        orderExist.Confirm3 = 1;
                        orderExist.Step = 3;
                        orderExist.TimeConfirm3 = orderExist?.TimeConfirm3 ?? DateTime.Now;
                        orderExist.IndexOrder = 0;
                        orderExist.LogJobAttach = $@"{orderExist.LogJobAttach} # autoremovecalljob check is scaled ";
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        public bool CheckIsScaleInByDeliveryCode(string deliveryCode)
        {
            double weightNull = 0;
            double weightFull = 0;
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
                            Double.TryParse(Rd["LOADWEIGHTNULL"]?.ToString(), out weightNull);
                            Double.TryParse(Rd["LOADWEIGHTFULL"]?.ToString(), out weightFull);
                            break;
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {

            }
            if (weightNull > 0)
            {
                return true;
            }
            return false;
        }
    }
}
