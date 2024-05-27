using Quartz;
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
using Oracle.ManagedDataAccess.Client;

namespace XHTD_Schedules.Schedules
{
    public class SyncScaleJob : IJob
    {
        private static string strToken;
        private static DateTime expireTimeToken;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public SyncScaleJob(IServiceFactory serviceFactory)
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
                SyncScaleWeightProcess();
            });
        }
        public void SyncBAK()
        {
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var time = DateTime.Now.AddHours(-48);
                    var orders = db.tblStoreOrderOperatings.Where(x=>x.WeightIn > 0 && x.WeightOut > 0 && x.TimeConfirm1 > time).ToList();
                    foreach (var order in orders)
                    {
                        try
                        {
                            OrderOracleModel orderModel = new OrderOracleModel();
                            double weightNull = 0;
                            double weightFull = 0;
                            string sqlQuery = "";
                            string strConString = System.Configuration.ConfigurationManager.ConnectionStrings["MbfConnOracle"].ConnectionString.ToString();

                            sqlQuery = $@"select cvw.LOADWEIGHTNULL, cvw.LOADWEIGHTFULL  from sales_orders so
                         ,cx_vehicle_weight cvw 
                         where so.delivery_code = cvw.delivery_code 
                         and so.VEHICLE_CODE IS NOT NULL
                         and so.DELIVERY_CODE = :DELIVERY_CODE";
                            using (OracleConnection connection = new OracleConnection(strConString))
                            {
                                OracleCommand Cmd = new OracleCommand(sqlQuery, connection);

                                Cmd.Parameters.Add(new OracleParameter("DELIVERY_CODE", order.DeliveryCode));
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
                            if (weightNull > 0 && weightFull > 0)
                            {
                                order.WeightIn = (int)(weightNull * 1000);
                                order.WeightOut = (int)(weightFull * 1000);
                                db.SaveChanges();
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("getBillOrder" + ex.Message);
            }
        }
        public void SyncScaleWeightProcess()
        {
            //log.Info("==============start process SyncScaleProcess ====================");
            if (_serviceFactory.ConfigOperating.GetValueByCode(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name) == 0) return;
            try
            {
                var scaleLogs = _serviceFactory.LogScale.GetAllLogByStatus();
                if (scaleLogs.Count < 1) return;
                foreach (var scaleLog in scaleLogs)
                {
                    ProcessScaleOrder(scaleLog.DeliveryCode);
                }
            }
            catch (Exception ex)
            {
                log.Error("getBillOrder" + ex.Message);
            }
        }
        private void ProcessScaleOrder(string deliveryCode)
        {
            try
            {
                OrderOracleModel orderModel = new OrderOracleModel();
                double weightNull = 0;
                double weightFull = 0;
                string sqlQuery = "";
                string strConString = System.Configuration.ConfigurationManager.ConnectionStrings["MbfConnOracle"].ConnectionString.ToString();

                sqlQuery = $@"select cvw.LOADWEIGHTNULL, cvw.LOADWEIGHTFULL  from sales_orders so
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
                if (weightNull > 0 && weightFull > 0)
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        var logScale = db.tblScaleLogOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode && x.IsSynced == false);
                        logScale.LoadWeightNull = weightNull;
                        logScale.LoadWeightFull = weightFull;
                        logScale.IsSynced = true;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("processBillOrderByDistributor " + ex.Message);
                using (var db = new HMXuathangtudong_Entities())
                {
                    var logScale = db.tblScaleLogOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode && x.IsSynced == false);
                    logScale.IsSynced = true;
                    db.SaveChanges();
                }
            }
        }
      
    }
    public class ResponseOrder
    {
        public List<ResponseOrderItem> orders { get; set; }
    }
    public class ResponseOrderItem
    {
        public int ORDER_ID { get; set; }
        public string STATUS { get; set; }
        public string PRINT_STATUS { get; set; }
        public string SO_STATUS { get; set; }
        public double? LOADWEIGHTNULL { get; set; }
        public double? LOADWEIGHTFULL { get; set; }
    }
}
