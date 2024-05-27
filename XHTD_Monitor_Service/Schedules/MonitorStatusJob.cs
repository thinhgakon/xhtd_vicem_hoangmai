using HMXHTD.Data.DataEntity;
using HMXHTD.Services.Services;
using Newtonsoft.Json;
using Quartz;
using RestSharp;
using System;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace XHTD_Monitor_Service.Schedules
{
    public class MonitorStatusJob : IJob
    {
        private static bool IsStop_XHTD_SYNC = false;
        private static bool IsSent_XHTD_SYNC = false;
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
            await Task.Run(() =>
            {
                MonitorAllProcess();
            });
        }
        public void TestAsync()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            
             _serviceFactory.Notification.SendNotificationToTelegram("Hello");
            
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
        }
       
        public void MonitorAllProcess()
        {
            MonitorServiceProcess("XHTD_Trough_Service");
            MonitorServiceProcess("XHTD_SYNC");
            MonitorServiceProcess("XHTD_Schedules");
            MonitorServiceProcess("XHTD_Getway_Service");
            MonitorServiceProcess("XHTD_ConfirmationPointModule_Service");
        }
        public void MonitorServiceProcess(string serviceName)
        {
            string contentStart = $@"<h1>Thông báo của hệ thống xuất hàng tự động</h1>
                            Dear anh,
                            <br />
                            <br />
                            Service {serviceName} đang tắt vì lý do đặc biệt";
            try
            {
                ServiceController sc = new ServiceController(serviceName);

                switch (sc.Status)
                {
                    case ServiceControllerStatus.Running:
                      //  SetIsRunningService(serviceName);
                        // return "Running";
                        break;
                    case ServiceControllerStatus.Stopped:
                        _serviceFactory.Common.SendMail("Xuất hàng tự động", "trungnc.bk@gmail.com", "Admin Xuất hàng tự động", "Thông tin từ hệ thống service của xuất hàng tự động", contentStart, "");
                        _serviceFactory.Common.SendMail("Xuất hàng tự động", "khoanait@gmail.com", "Admin Xuất hàng tự động", "Thông tin từ hệ thống service của xuất hàng tự động", contentStart, "");
                        // ProcessServiceStop(serviceName);
                        // return "Stopped";
                        break;
                    case ServiceControllerStatus.Paused:
                        // return "Paused";
                        break;
                    case ServiceControllerStatus.StopPending:
                        break;
                    //return "Stopping";
                    case ServiceControllerStatus.StartPending:
                      
                        break;
                    //return "Starting";
                    default:
                        // return "Status Changing";
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error($@"Lỗi xẩy ra : {ex.Message}");
            }
        }
        public void SetIsRunningService(string serviceName)
        {
            try
            {
                string contentStart = $@"<h1>Thông báo của hệ thống xuất hàng tự động</h1>
                            Dear anh Trung,
                            <br />
                            <br />
                            Service {serviceName} đang bật";
                switch (serviceName)
                {
                    case "XHTD_SYNC":
                        if (!IsStop_XHTD_SYNC)
                        {
                            _serviceFactory.Common.SendMail("Xuất hàng tự động", "trungnc.bk@gmail.com", "Trung Nguyễn", "Thông tin từ hệ thống service của xuất hàng tự động", contentStart, "");
                            IsStop_XHTD_SYNC = false;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        public void ProcessServiceStop(string serviceName)
        {
            try
            {
                string contentStart = $@"<h1>Thông báo của hệ thống xuất hàng tự động</h1>
                            Dear anh Trung,
                            <br />
                            <br />
                            Service {serviceName} đang tắt vì lý do đặc biệt";
                switch (serviceName)
                {
                    case "XHTD_SYNC":
                        _serviceFactory.Common.SendMail("Xuất hàng tự động", "trungnc.bk@gmail.com", "Trung Nguyễn", "Thông tin từ hệ thống service của xuất hàng tự động", contentStart, "");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
    }
}
