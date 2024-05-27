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
    public class SyncPackageNumberOrderJob : IJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public SyncPackageNumberOrderJob(IServiceFactory serviceFactory)
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
                SyncPackageNumberOrderProcess();
            });
        }
        public void SyncPackageNumberOrderProcess()
        {
            log.Info("==============start process SyncPackageNumberOrderProcess ====================");
            var orders = GetBillOrderXiRoi();
            foreach (var order in orders)
            {
                UpdatePackageNumber(order.DeliveryCode);
            }
        }
        private List<StoreOrderModel> GetBillOrderXiRoi()
        {
            var orders = new List<StoreOrderModel>();
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var sqlQuery = $"SELECT B.Id, B.DeliveryCode FROM tblStoreOrderOperating B WHERE B.TimeConfirm7 BETWEEN DATEADD(HOUR, -48 , GETDATE()) AND GETDATE() AND B.Step > 5 AND ISNULL(B.IsVoiced,0) = 0 AND B.TypeProduct = N'ROI' AND ISNULL(B.PackageNumber,'') = ''";
                    orders = db.Database.SqlQuery<StoreOrderModel>(sqlQuery).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("getToken" + ex.Message);
            }
            return orders;
        }
        private void UpdatePackageNumber(string deliveryCode)
        {
            try
            {
                var packageNumber = "";
                string strConString = System.Configuration.ConfigurationManager.ConnectionStrings["MbfConnOracle"].ConnectionString.ToString();

                string sqlQuery = $@"select  cvw.lot_number as PACKAGE_NUMBER, cvw.delivery_code from cx_vehicle_weight cvw  where cvw.delivery_code = :DELIVERY_CODE";
                using (OracleConnection connection = new OracleConnection(strConString))
                {
                    OracleCommand Cmd = new OracleCommand(sqlQuery, connection);

                    Cmd.Parameters.Add(new OracleParameter("DELIVERY_CODE", deliveryCode));
                    connection.Open();
                    using (OracleDataReader Rd = Cmd.ExecuteReader())
                    {
                        while (Rd.Read())
                        {
                            packageNumber = Rd["PACKAGE_NUMBER"].ToString();
                            break;
                        }
                    }
                }
                if (!String.IsNullOrEmpty(packageNumber))
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        var pdfFile = $"http://tv.ximanghoangmai.vn:8189/pdf/xiroi/kcs-{packageNumber}.pdf";
                        var sqlUpdate = "UPDATE dbo.tblStoreOrderOperating SET PackageNumber = @PackageNumber, XiRoiAttatchmentFile = @XiRoiAttatchmentFile WHERE DeliveryCode = @DeliveryCode And PackageNumber IS NULL";
                        var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@PackageNumber", packageNumber), new SqlParameter("@XiRoiAttatchmentFile", pdfFile), new SqlParameter("@DeliveryCode", deliveryCode));
                    }
                    InsertOrUpdatetblKcsOperating(packageNumber);
                }
            }
            catch (Exception ex)
            {
                log.Error("===UpdatePackageNumber==== " + ex.Message);
            }
        }
        public void InsertOrUpdatetblKcsOperating(string packageNumber)
        {
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var sql = @"IF NOT EXISTS
                                (
                                    SELECT TOP 1
                                           *
                                    FROM dbo.tblKCSOperating k
                                    WHERE k.KCSCode = @KCSCode
                                )
                                BEGIN
                                    INSERT INTO dbo.tblKCSOperating
                                    (
                                        CreatedOn,
                                        ModifiedOn,
                                        KCSCode,
                                        LinkKCS,
                                        Flag
                                    )
                                    VALUES
                                    (GETDATE(), GETDATE(), @KCSCode, NULL, 1);
                                END;";
                    db.Database.ExecuteSqlCommand(sql, new SqlParameter("@KCSCode", packageNumber));
                }
            }
            catch (Exception ex)
            {
                log.Error("======InsertOrUpdatetblKcs======== " + ex.Message);
            }
        }

    }
    public class StoreOrderModel
    {
        public int Id { get; set; }
        public string DeliveryCode { get; set; }
    }
}
