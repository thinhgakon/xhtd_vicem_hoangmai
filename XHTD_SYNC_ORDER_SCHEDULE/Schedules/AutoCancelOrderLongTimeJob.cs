using HMXHTD.Data.DataEntity;
using HMXHTD.Services.Services;
using Newtonsoft.Json;
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
    public class AutoCancelOrderLongTimeJob : IJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public AutoCancelOrderLongTimeJob(IServiceFactory serviceFactory)
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
                SyncCancelOrderProcess();
            });
        }
        public void SyncCancelOrderProcess()
        {
            log.Info("==============start process SyncCancelOrderProcess ====================");
            ProcessCancelOrderByTime();
        }
        public void ProcessCancelOrderByTime()
        {
            try
            {
                _serviceFactory.StoreOrderOperating.CancelOrderOverTime();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
    }
}
