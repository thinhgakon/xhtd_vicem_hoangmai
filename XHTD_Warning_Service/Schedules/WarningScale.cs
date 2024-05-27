using HMXHTD.Data.DataEntity;
using HMXHTD.Services.Services;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using Quartz;
using RestSharp;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace XHTD_Warning_Service.Schedules
{
    public class WarningScale : IJob
    {
        private IHubProxy hubProxy { get; set; }
        private HubConnection connection { get; set; }
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public WarningScale(IServiceFactory serviceFactory)
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
                WarningScaleProcessAsync();
            });
        } 
        #region method SignalR
        public async Task<bool> WarningScaleProcessAsync()
        {
            bool connected = false;
            try
            {
                connection = new HubConnection(ConfigurationManager.AppSettings.Get("singalRHost").ToString());
                hubProxy = connection.CreateHubProxy(ConfigurationManager.AppSettings.Get("hubSignalR").ToString()); 
            }
            catch
            {

            }
            try
            {
                await connection.Start();
                if (connection.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected)
                {
                    hubProxy.On<string, string>(ConfigurationManager.AppSettings.Get("methodSendMessage").ToString(), (name, message) =>
                    ProcessNotification(name, message)
                );

                    connection.Error += (ex) =>
                    { 
                    };
                    connection.Closed += () =>
                    { 
                    };
                    connection.StateChanged += Connection_StateChanged; 
                    connected = true;
                }
            }
            catch (HttpRequestException ex)
            {
            }
            return connected;
        }
        private async void Connection_StateChanged(StateChange obj)
        {
            if (obj.NewState == Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected)
            {
                await RestartConnection();
            }
        }
        public async Task RestartConnection()
        {
            while (true)
            {
                bool connected = await WarningScaleProcessAsync();
                if (connected)
                    return;
            }
        }
        private void ProcessNotification(string key, string message)
        {
            var weightDing = 0; Int32.TryParse(ConfigurationManager.AppSettings.Get("weight_max_ding_ding").ToString(), out weightDing);
            switch (key)
            {
                case "Scale1_Current": 
                    int weightCN = int.Parse(message);
                    
                    if (weightCN > 500 && weightCN < weightDing)
                    {
                        PlayVoice("ding");
                    } 
                    break;
                case "Scale2_Current": 
                    int weightCC = int.Parse(message);
                    if (weightCC > 500 && weightCC < weightDing)
                    {
                        PlayVoice("ding");
                    }  
                    break;
                case "Scale_In_CN":
                    PlayVoice("xelenbancan");
                    break;
                case "Scale_Out_CN":
                    PlayVoice("xelenbancan");
                    break;
                case "Scale_In_CC":
                    PlayVoice("xelenbancan");
                    break;
                case "Scale_Out_CC":
                    PlayVoice("xelenbancan");
                    break;
                case "Scale1_Balance":
                    break;
                case "Scale2_Balance":
                    break;
                case "Scale_CN_Warning": 
                    PlayVoice("canhbaokhonghople");
                    break;
                case "Scale_CC_Warning":
                    PlayVoice("canhbaokhonghople");
                    break;
                case "Scale_CN_IN_Desision": 
                    break;
                case "Scale_CN_OUT_Desision":
                    break;
                case "Scale_CC_IN_Desision": 
                    break;
                case "Scale_CC_OUT_Desision": 
                    break;
                case "Scale_Send_Successed":
                    PlayVoice("canthanhcong");
                    break;
                case "Scale_Send_Failed":
                    PlayVoice("canthatbai");
                    break; 
                default:
                    break;
            }
        }
        #endregion
        public void PlayVoice(string voiceName)
        { 
            try
            { 
                string dingpath = $@"D://AudioWarningScale/{voiceName}.wav";
                WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                wplayer.URL = dingpath;
                wplayer.controls.play(); 
            }
            catch (Exception ex)
            {
            }
        } 
    }
}
