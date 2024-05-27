using HMXHTD.Data.DataEntity;
using HMXHTD.Services.Services;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using Quartz;
using RestSharp;
using System;
using System.Net.Http;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace XHTD_Extension_Service.Schedules
{
    public class MonitorStatusJob : IJob
    {
        private HubConnection Connection { get; set; }
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public MonitorStatusJob(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            await Task.Run(async () =>
            {
                await MonitorAllProcessAsync();
            });
        }
        public async Task MonitorAllProcessAsync()
        {
            log.Info("service is running...");
          //  SendNotification("ds");
        }
        public void SendNotification(string cardNo = "")
        {
            while (true)
            {
                try
                {
                    log.Info($@"==========send {cardNo} =========");
                }
                catch (Exception ex)
                {
                    log.Error($@"SendNotification {ex.Message}");
                }
                Thread.Sleep(5000);
            }
            
        }

        public void UpdateData()
        {
            try
            {

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
    }
}
