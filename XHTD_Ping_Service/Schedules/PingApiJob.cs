using HMXHTD.Data.DataEntity;
using HMXHTD.Services.Services;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using Quartz;
using RestSharp;
using System;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace XHTD_Ping_Service.Schedules
{
    public class PingApiJob : IJob
    {
        private HubConnection Connection { get; set; }
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public PingApiJob(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            await Task.Run( () =>
            {
                PingAllDevicesProcessAsync();
            });
        }
        public void PingAllDevicesProcessAsync()
        {

            try
            {
                log.Info($"{DateTime.Now}=====pingapi");
                var client = new RestClient($@"http://tv.ximanghoangmai.vn:8189/api/v1/time/now");
                var request = new RestRequest(Method.GET);

                IRestResponse response = client.Execute(request);
                string data = response.Content;
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    log.Info($"{DateTime.Now}=====pingapi======die");
                }
            }
            catch (Exception ex)
            {
                log.Error("========ex======" + ex.Message);
            }
        }
    }
}
