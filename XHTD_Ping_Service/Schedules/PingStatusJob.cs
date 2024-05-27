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
    public class PingStatusJob : IJob
    {
        private HubConnection Connection { get; set; }
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public PingStatusJob(IServiceFactory serviceFactory)
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
            log.Info("service is running...");
            Console.WriteLine(DateTime.Now);
            var devices = _serviceFactory.DeviceOperating.GetAllDeviceOperating();
            var message = "";
            foreach (var device in devices)
            {
                var pingStatus = PingHostDevice(device.IpAddress);
                if (!pingStatus)
                {
                    _serviceFactory.DeviceOperating.UpdateStatus(device.Id);
                    if(!message.Contains(device.IpAddress))
                    message += $@"{device.IpAddress} is not working" + "\n";
                }
            }
            Console.WriteLine(DateTime.Now);
            //messsage += PingHost("192.168.22.16", "Thiết bị cổng 3"); //thiết bị ở cổng số 3 
            //messsage += PingHost("192.168.22.18", "Led trước bãi chờ"); //192.168.22.18 (led trước bãi chờ)
            //messsage += PingHost("192.168.22.17", "Led trước cổng 3"); //192.168.22.17 (led trước cổng 3)
            //messsage += PingHost("192.168.22.18", "Led sau cổng 3"); //192.168.22.18 (led sau cổng 3)
            //messsage += PingHost("192.168.22.6", "Led nhỏ cổng 1"); //192.168.22.6 (Led ở cổng 1 xác thực (led nhỏ))
            //messsage += PingHost("192.168.22.5", "Xác thực cổng 1"); //192.168.22.5 (rfid cổng 1)
            //messsage += PingHost("192.168.22.34", "C3-400 trạm cân"); //192.168.22.34 (thiết bị ở trạm cân)
            //messsage += PingHost("192.168.21.57", "Led to ở máng"); //192.168.21.57 (led to ở máng)

            //messsage += PingHost("192.168.21.41", "PC Xi Rời"); //192.168.21.57 (led to ở máng)

            _serviceFactory.Notification.SendNotificationToTelegramMonitor(message);
           
        }

        public string PingHost(string nameOrAddress, string deviceName)
        {
            log.Info($@"ping {nameOrAddress}");
            bool pingable = false;
            Ping pinger = null;
            var res = "";
            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
                if (pingable)
                {
                    res = $@"{deviceName} ({nameOrAddress}) is working" + "\n";
                }
                else
                {
                    res = $@"{deviceName} ({nameOrAddress}) { reply.Status}" + "\n";
                }
                    
            }
            catch (PingException ex)
            {
                log.Info($@"ping {nameOrAddress} {ex.Message}");
                // Discard PingExceptions and return false;
                res = $@"{deviceName} ({nameOrAddress}) is not working" + "\n";
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }
            return res;
        }

        public bool PingHostDevice(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;
            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException ex)
            {
                log.Info($@"ping {nameOrAddress} {ex.Message}");
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }
            return pingable;
        }

    }
}
