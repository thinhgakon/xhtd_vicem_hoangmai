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
using XHTD_Schedules.SignalRNotification;
using XHTD_Schedules.ApiTroughs;
using Autofac;

namespace XHTD_Schedules.Schedules
{
    public class BarrierScaleModuleJob : IJob
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
        private List<string> tmpCardNoIn_CN = new List<string>() { };
        private List<string> tmpCardNoOut_CN = new List<string>() { };
        private List<string> tmpCardNoIn_CC = new List<string>() { };
        private List<string> tmpCardNoOut_CC = new List<string>() { };
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public BarrierScaleModuleJob(IServiceFactory serviceFactory)
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
                BarrierScaleModuleProcess();
            });
        }
        public void BarrierScaleModuleProcess()
        {
            while (!DeviceConnected)
            {
                ConnectBarrierScale();
            }
            ControlBarrierScale();
        }
        public bool ConnectBarrierScale()
        {
            try
            {
                string str = "protocol=TCP,ipaddress=192.168.22.321,port=4370,timeout=2000,passwd=";
                int ret = 0;
                if (IntPtr.Zero == h21)
                {
                    h21 = Connect(str);
                    if (h21 != IntPtr.Zero)
                    {
                        log.Info("--------------ConnectBarrierScale connected--------------");
                        DeviceConnected = true;
                    }
                    else
                    {
                        log.Info("------------- ConnectBarrierScale -connected failed--------------");
                        ret = PullLastError();
                        DeviceConnected = false;
                    }
                }
                return DeviceConnected;
            }
            catch (Exception ex)
            {
                log.Error($@"ScaleModule : {ex.StackTrace}");
                return false;
            }
        }
        public void ControlBarrierScale()
        {
            try
            {
                if (DeviceConnected)
                {
                    while (true)
                    {
                        int ret = 0, i = 0, buffersize = 256;
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
                                        //    log.Info($@"============================tramcan_cardno================================= {tmp[2]?.ToString()}   ========= {tmp[3].ToString()}");
                                    }
                                    if (tmp[2] == "0" || tmp[2] == "")
                                    {
                                        ProcessLogFollow();
                                    }
                                    else
                                    {
                                        var cardNoCurrent = tmp[2]?.ToString();
                                        if (!_serviceFactory.RFID.CheckRFIDByCardNo(cardNoCurrent)) continue;
                                       // new MyHub().Send("TramCan_CardNo", $@"{tmp[2]?.ToString()} --- {tmp[3]?.ToString()}");
                                        // check step của đơn hàng hiện tại để xác định đang vào hay ra



                                        if (tmp[3].ToString() == "1")  // ra cân nổi
                                        {

                                            if (tmpCardNoOut_CN.FirstOrDefault(x => x.ToString().Equals(cardNoCurrent)) != null) continue;
                                            var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(cardNoCurrent);
                                            if (orderCurrent == null) continue;
                                            if (orderCurrent.Step >= 5)
                                            {
                                                //new MyHub().Send("Scale_Out_CN", orderCurrent.Vehicle);
                                                // mở barrier ra cân nổi

                                                tmpCardNoOut_CN.Add(cardNoCurrent);
                                                _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, cardNoCurrent, 7);
                                                //ControlDevice(h21, 1, 1, 1, 1, 0, "");
                                            }
                                            if (tmpCardNoOut_CN.Count > 5) tmpCardNoOut_CN.RemoveRange(0, 2);
                                        }
                                        else if (tmp[3].ToString() == "2")//  && orderCurrent.Step == 2)  // vào cân nổi
                                        {

                                            if (tmpCardNoIn_CN.FirstOrDefault(x => x.ToString().Equals(cardNoCurrent)) != null) continue;
                                            var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(cardNoCurrent);
                                            if (orderCurrent == null) continue;
                                            if (orderCurrent.Step == 1 || orderCurrent.Step == 2 || orderCurrent.Step == 4)
                                            {
                                                //new MyHub().Send("Scale_In_CN", orderCurrent.Vehicle);
                                                // mở barrier vào cân nổi

                                                tmpCardNoIn_CN.Add(cardNoCurrent);
                                                _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, cardNoCurrent, 3);
                                                // ControlDevice(h21, 1, 1, 1, 1, 0, "");
                                            }
                                            if (tmpCardNoIn_CN.Count > 5) tmpCardNoIn_CN.RemoveRange(0, 2);
                                        }
                                        else if (tmp[3].ToString() == "3")  // vào cân chìm
                                        {

                                            if (tmpCardNoIn_CC.FirstOrDefault(x => x.ToString().Equals(cardNoCurrent)) != null) continue;
                                            var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(cardNoCurrent);
                                            if (orderCurrent == null) continue;
                                            if (orderCurrent.Step == 1 || orderCurrent.Step == 2 || orderCurrent.Step == 4)
                                            {
                                                //new MyHub().Send("Scale_In_CC", orderCurrent.Vehicle);
                                                // mở barrier vào cân chìm

                                                tmpCardNoIn_CC.Add(cardNoCurrent);
                                                _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, cardNoCurrent, 3);
                                                // ControlDevice(h21, 1, 1, 1, 1, 0, "");
                                            }
                                            if (tmpCardNoIn_CC.Count > 5) tmpCardNoIn_CC.RemoveRange(0, 2);
                                        }
                                        else if (tmp[3].ToString() == "4")  // ra cân chìm
                                        {

                                            if (tmpCardNoOut_CC.FirstOrDefault(x => x.ToString().Equals(cardNoCurrent)) != null) continue;
                                            var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(cardNoCurrent);
                                            if (orderCurrent == null) continue;
                                            if (orderCurrent.Step >= 5)
                                            {
                                                //new MyHub().Send("Scale_Out_CC", orderCurrent.Vehicle);
                                                // mở barrier ra cân chìm

                                                tmpCardNoOut_CC.Add(cardNoCurrent);
                                                _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, cardNoCurrent, 7);
                                                //ControlDevice(h21, 1, 1, 1, 1, 0, "");
                                            }
                                            if (tmpCardNoOut_CC.Count > 5) tmpCardNoOut_CC.RemoveRange(0, 2);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    log.Error($@"Lỗi xẩy ra {ex.StackTrace}");
                                    ConnectBarrierScale();
                                    continue;
                                }
                            }
                            else
                            {
                                log.Warn("Lỗi không đọc được dữ liệu, có thể do mất kết nối");
                                ConnectBarrierScale();
                                continue;
                            }
                        }
                    }
                }
                else
                {
                    BarrierScaleModuleProcess();
                }
            }
            catch (Exception Ex)
            {
                log.Error($@"===============ScaleModule=========================== {Ex.StackTrace}");
            }
        }
        public void ProcessLogFollow()
        {
            try
            {
                var unixTimestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
                if (unixTimestamp % 300000 < 2)
                {
                    log.Info("====================Log status is running==========================");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
