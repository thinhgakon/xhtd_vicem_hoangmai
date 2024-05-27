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
    public class SyncOrderVoidedQuickJob : IJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public SyncOrderVoidedQuickJob(IServiceFactory serviceFactory)
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
                SyncOrderVoidedQuickProcess();
            });
        }
        public void SyncOrderVoidedQuickProcess()
        {
            log.Info("==============start process SyncOrderVoidedJob ====================");
            if (_serviceFactory.ConfigOperating.GetValueByCode(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name) == 0) return;
            GetDataFromDbQuick();
        }
        public void GetDataFromDbQuick()
        {
            try
            {
                #region Oracle
                string sqlQuery = "";
                string strConString = System.Configuration.ConfigurationManager.ConnectionStrings["MbfConnOracle"].ConnectionString.ToString();
                OrderOracleModel orderModel;
                List<OrderOracleModel> objList = new List<OrderOracleModel>();
                sqlQuery = $@"select so.ORDER_ID, 
                                so.STATUS, 
                                so.DELIVERY_CODE,
                                so.PRINT_STATUS
                                from sales_orders so
                                where
                                so.STATUS = 'VOIDED' 
                                and so.CREATION_DATE BETWEEN TO_DATE(:startDate,'dd/MM/yyyy')  AND TO_DATE(:endDate,'dd/MM/yyyy') + INTERVAL '86399' second
                                order by so.ORDER_ID desc";
                using (OracleConnection connection = new OracleConnection(strConString))
                {
                    OracleCommand Cmd = new OracleCommand(sqlQuery, connection);
                    var startDate = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                    var endDate = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
                    Cmd.Parameters.Add(new OracleParameter("startDate", startDate));
                    Cmd.Parameters.Add(new OracleParameter("endDate", endDate));
                    connection.Open();
                    //Cmd.Parameters.Add("CodeStore", SqlDbType.NVarChar).Value = objVehicleStoreParam.CodeStore;
                    using (OracleDataReader Rd = Cmd.ExecuteReader())
                    {
                        while (Rd.Read())
                        {
                            orderModel = new OrderOracleModel();
                            orderModel.ORDER_ID = Int32.Parse(Rd["ORDER_ID"].ToString());
                            orderModel.STATUS = Rd["STATUS"].ToString();
                            orderModel.DELIVERY_CODE = Rd["DELIVERY_CODE"].ToString();
                            orderModel.PRINT_STATUS = Rd["PRINT_STATUS"].ToString();
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
                _serviceFactory.StoreOrderOperating.UpdateIsVoicedWhenSyncOrderByDeliverycode(orderModel.DELIVERY_CODE);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
    }
}
