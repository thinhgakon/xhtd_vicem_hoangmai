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
using System.Threading;
using System.Runtime.InteropServices;
using System.Text;
using System.Data;
using HMXHTD.Services.Services;
using XHTD_Schedules.LEDControl;
using Autofac;
using XHTD_Schedules.SignalRNotification;

namespace XHTD_Schedules.Schedules
{
    public class GatewayModuleJob : IJob
    {
        private IntPtr h21 = IntPtr.Zero;
        private IntPtr h = IntPtr.Zero;
        private static bool DeviceConnected = false;

        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "Connect")]
        public static extern IntPtr Connect(string Parameters);
        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "PullLastError")]
        public static extern int PullLastError();
        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "GetRTLog")]
        public static extern int GetRTLog(IntPtr h, ref byte buffer, int buffersize);
        //Điều khiển Barie
        [DllImport("plcommpro.dll", EntryPoint = "ControlDevice")]
        public static extern int ControlDevice(IntPtr h, int operationid, int param1, int param2, int param3, int param4, string options);
        private List<string> tmpCardNoIn = new List<string>() { };
        private List<string> tmpCardNoOut = new List<string>() { };
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        int m_nSendType_LED22;
        IntPtr m_pSendParams_LED22;
        int countTryConnect = 0;
        public GatewayModuleJob(IServiceFactory serviceFactory)
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
                AuthenticateGatewayModule();
            });
        }
        public void AuthenticateGatewayModule()
        {
            m_nSendType_LED22 = 0;
            string strParams_LED22 = "192.168.22.18";
            m_pSendParams_LED22 = Marshal.StringToHGlobalUni(strParams_LED22);
            while (!DeviceConnected)
            {
                ConnectGatewayModule();
            }
            AuthenticateCardNoInGatewayModule();
        }
        public bool ConnectGatewayModule()
        {
            try
            {
                string str = "protocol=TCP,ipaddress=192.168.22.16,port=4370,timeout=2000,passwd=";
                int ret = 0;
                if (IntPtr.Zero == h21)
                {
                    h21 = Connect(str);
                    if (h21 != IntPtr.Zero)
                    {
                       // log.Info("--------------connected--------------");
                        DeviceConnected = true;
                    }
                    else
                    {
                        countTryConnect++;
                        log.Info("--------------connected failed--------------");
                        ret = PullLastError();
                        DeviceConnected = false;
                        _serviceFactory.Notification.SendNotificationToTelegram($@"Thiết bị {str} mất kết nối");
                        //if(countTryConnect < 2)
                        //{
                        //    _serviceFactory.Common.SendMail("Xuất hàng tự động", "trungnc.bk@gmail.com", "Trung Nguyễn", "Thông tin từ hệ thống xuất hàng tự động", "", str);
                        //}
                        //Thread.Sleep(1000 * 60 * 5);
                    }
                }
                return DeviceConnected;
            }
            catch (Exception ex)
            {
                log.Error($@"ConnectGatewayModule : {ex.StackTrace}");
                return false;
            }
        }
        public void AuthenticateCardNoInGatewayModule()
        {
            try
            {
                var count = 1;
                if (DeviceConnected)
                {
                        while (true)
                        {
                            //if(count < 100)
                            //{
                            //    var res = ControlDevice(h21, 1, 4, 1, 1, 0, "");
                            //    Console.WriteLine($@"============{res}==========");
                            //    Thread.Sleep(1000* 10);
                            //    continue;
                            //}
                            //count++;
                        int ret = 0, buffersize = 256;
                        string str = "";
                        string[] tmp = null;
                        byte[] buffer = new byte[256];
                        if (IntPtr.Zero != h21)
                        {
                            ret = GetRTLog(h21, ref buffer[0], buffersize);
                            if (ret >= 0)
                            {
                                try
                                {
                                    str = Encoding.Default.GetString(buffer);
                                    tmp = str.Split(',');
                                    if (tmp[2] != "0")
                                    {
                                        log.Info($@"============================card no================================= {tmp[2]}");
                                    }
                                    if (tmp[2] == "0" || tmp[2] == "")
                                    {

                                    }
                                    else
                                    {
                                        if (!_serviceFactory.RFID.CheckRFIDByCardNo(tmp[2].ToString())) continue; // đoạn này sau mở ra để check cardno
                                                                                                                  // check step của đơn hàng hiện tại để xác định đang vào hay ra


                                       // ControlDevice(h21, 1, 1, 1, 1, 0, "");
                                        if (tmp[3].ToString() == "2")
                                        {
                                            if (tmpCardNoIn.FirstOrDefault(x => x.ToString().Equals(tmp[2].ToString())) != null) continue;
                                            
                                            if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm2(tmp[2].ToString()))
                                            {
                                                ControlDevice(h21, 1, 1, 1, 1, 0, "");
                                                var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(tmp[2].ToString());
                                                //nếu vào thì phải cập nhật lại lốt
                                                _serviceFactory.StoreOrderOperating.ReIndexOrderWhenSyncOrderWithEnd((int)orderCurrent.OrderId);
                                                tmpCardNoIn.Add(tmp[2].ToString());
                                                _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, tmp[2].ToString(), 4);
                                               
                                                
                                               
                                                ShowInviteLED(orderCurrent?.Vehicle);
                                                new MyHub().Send("Step_2", String.Format("{0} \n {1}", orderCurrent.Vehicle, DateTime.Now));
                                            }
                                            if (tmpCardNoIn.Count > 5) tmpCardNoIn.RemoveRange(0, 2);
                                        }
                                        else if (tmp[3].ToString() == "3")
                                        {
                                            if (tmpCardNoOut.FirstOrDefault(x => x.ToString().Equals(tmp[2].ToString())) != null) continue;
                                            if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm8(tmp[2].ToString()))
                                            {
                                                ControlDevice(h21, 1, 1, 1, 1, 0, "");
                                                var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(tmp[2].ToString());
                                                tmpCardNoOut.Add(tmp[2].ToString());
                                                _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, tmp[2].ToString(), 8);
                                                new MyHub().Send("Step_8", String.Format("{0} \n {1}", orderCurrent.Vehicle, DateTime.Now));
                                               
                                            }
                                            if (tmpCardNoOut.Count > 5) tmpCardNoOut.RemoveRange(0, 2);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    log.Error(ex.StackTrace);
                                }
                            }
                            else
                            {
                                AuthenticateGatewayModule();
                            }
                        }
                    }
                }
                else
                {
                    AuthenticateGatewayModule();
                }
            }
            catch (Exception Ex)
            {
                log.Error($@"===================gatewayjob======================= {Ex.StackTrace}");
            }
        }
        public void TestBarrierControl()
        {
            try
            {
                    var count = 100;
                for (int i = 0; i < count; i++)
                {
                    count--;
                    var res = ControlDevice(h21, 1, 1, 1, 1, 0, "");
                    Thread.Sleep(15000);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private async void ShowInviteLED(string vehicle)
        {
            try
            {
                AutoFacBootstrapper.Init().Resolve<LEDGetwayFrontControl>().SendLedFrontAllArea(isShowArea1: true, isShowArea2: true, isShowArea3: true, isInviteVehicleComeIn: true, contentComeIn: vehicle);
                Thread.Sleep(1000);
                AutoFacBootstrapper.Init().Resolve<LEDGetwayFrontControl>().SendLedFrontAllArea(isShowArea1: true, isShowArea2: true, isShowArea3: true, isInviteVehicleComeIn: false, contentComeIn: "");
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
            }
        }
    }
}
