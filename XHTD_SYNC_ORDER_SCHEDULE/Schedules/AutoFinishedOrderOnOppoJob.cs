using HMXHTD.Data.DataEntity;
using HMXHTD.Services.Services;
using Quartz;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace XHTD_SYNC_ORDER_SCHEDULE.Schedules
{
    public class AutoFinishedOrderOnOppoJob : IJob
    {
        private static string strToken;
        private static DateTime expireTimeToken;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public AutoFinishedOrderOnOppoJob(IServiceFactory serviceFactory)
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
                AutoFinishedOrderOnOppoProcess();
            });
        }
        public void AutoFinishedOrderOnOppoProcess()
        {
            if (_serviceFactory.ConfigOperating.GetValueByCode(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name) == 0) return;
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var sqlSelectVehicle = "SELECT TOP 100 Vehicle FROM dbo.tblVehicleWithDeviceOppoOperating";
                    var listVehicles = db.Database.SqlQuery<string>(sqlSelectVehicle).ToListAsync().GetAwaiter().GetResult();
                    foreach (var vehicle in listVehicles)
                    {
                        var timeAfter = DateTime.Now.AddMinutes(-30);
                        var sqlUpdate = "UPDATE dbo.tblStoreOrderOperating SET Step = 9, TimeConfirm9 = GETDATE(), NoteFinish = N'kết thúc tự động cho máy oppo' WHERE TimeConfirm8 < @TimeAfter AND Step = 8 AND Vehicle = @Vehicle";
                        db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@TimeAfter", timeAfter), new SqlParameter("@Vehicle", vehicle));
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
