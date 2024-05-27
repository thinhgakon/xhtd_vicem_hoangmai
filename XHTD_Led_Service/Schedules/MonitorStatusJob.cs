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

namespace XHTD_Led_Service.Schedules
{
    public class MonitorStatusJob : IJob
    {
        private IHubProxy HubProxy { get; set; }
        // const string ServerURI = "http://127.0.0.1:8091/signalr"; // chay tren server
        const string ServerURI = "http://192.168.0.10:8091/signalr";//chay local
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
        private async Task<bool> ConnectAsync()
        {
            bool connected = false;
            try
            {
                Connection = new HubConnection(ServerURI);
                Connection.Closed += Connection_Closed;
                HubProxy = Connection.CreateHubProxy("MyHub");
                await Connection.Start();
                if (Connection.State == ConnectionState.Connected)
                {
                    connected = true;
                    Connection.Closed += Connection_Closed;
                }
            }
            catch (HttpRequestException ex)
            {
                log.Error($@"ConnectAsync {ex.Message}");
            }
            return connected;
        }
        private async void Connection_Closed()
        {
            TimeSpan retryDuration = TimeSpan.FromSeconds(30);
            DateTime retryTill = DateTime.UtcNow.Add(retryDuration);

            while (DateTime.UtcNow < retryTill)
            {
                bool connected = await ConnectAsync();
                if (connected)
                    return;
            }
        }
        public async Task MonitorAllProcessAsync()
        {
            await ConnectAsync();
            log.Info("service is running...");
            SendNotification("ds");
        }
        public void SendNotification(string cardNo = "")
        {
            while (true)
            {
                try
                {
                    HubProxy.Invoke("ConfirmPointModule", cardNo, DateTime.Now);
                    log.Info($@"==========send {cardNo} =========");
                }
                catch (Exception ex)
                {
                    log.Error($@"SendNotification {ex.Message}");
                }
                Thread.Sleep(5000);
            }
            
        }
    }
}
