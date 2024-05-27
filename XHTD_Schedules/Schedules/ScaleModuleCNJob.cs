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
namespace XHTD_Schedules.Schedules
{
    public class ScaleModuleCNJob : IJob
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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public ScaleModuleCNJob(IServiceFactory serviceFactory)
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
                ScaleModule();
            });
        }
        public void ScaleModule()
        {
            while (!DeviceConnected)
            {
                ConnectScaleModule();
            }
            AuthenticateCardNoInScaleModule();
        }
        public bool ConnectScaleModule()
        {
            try
            {
                string str = "protocol=TCP,ipaddress=192.168.22.34,port=4370,timeout=2000,passwd=";
                int ret = 0;
                if (IntPtr.Zero == h21)
                {
                    h21 = Connect(str);
                    if (h21 != IntPtr.Zero)
                    {
                        log.Info("--------------ScaleModulecn connected--------------");
                        DeviceConnected = true;
                    }
                    else
                    {
                        log.Info("------------- ScaleModulecn -connected failed--------------");
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
        public void AuthenticateCardNoInScaleModule()
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
                                str = Encoding.Default.GetString(buffer);
                                tmp = str.Split(',');
                                if (tmp[2] != "0")
                                {
                                    //log.Info($@"============================card no================================= {tmp[2]}   ========= {tmp[3]}");
                                }
                                if (tmp[2] == "0" || tmp[2] == "")
                                {

                                }
                                else
                                {
                                    var cardNoCurrent = tmp[2].ToString();
                                    if (!_serviceFactory.RFID.CheckRFIDByCardNo(cardNoCurrent)) continue;
                                    // check step của đơn hàng hiện tại để xác định đang vào hay ra
                                    var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(cardNoCurrent);

                                    if (tmp[3]?.ToString() == "1" && orderCurrent.Step > 4)  // ra cân nổi
                                    {
                                        new MyHub().Send("Scale_Out_CN", orderCurrent.Vehicle);
                                        if (tmpCardNoOut_CN.FirstOrDefault(x => x.ToString().Equals(cardNoCurrent)) != null) continue;

                                        if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm7(cardNoCurrent))
                                        {
                                            //scale job
                                            if (orderCurrent != null)
                                            {
                                                log.Info($@"=========ra cân nổi===================={orderCurrent.DeliveryCode}");
                                                _serviceFactory.Scale.UpdateWhenConfirmVehicle(scaleCode: "CN", deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, scaleIn: false, scaleOut: true);
                                                _serviceFactory.LogScale.InsertOrUpdateByDeliveryCode(deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, IsInScale: false);

                                            }
                                            //end scale job

                                            tmpCardNoOut_CN.Add(cardNoCurrent);
                                            _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, cardNoCurrent, 7);
                                            //ControlDevice(h21, 1, 1, 1, 1, 0, "");
                                        }
                                        if (tmpCardNoOut_CN.Count > 2) tmpCardNoOut_CN.RemoveRange(0, 2);
                                    }
                                    else if (tmp[3]?.ToString() == "2")//  && orderCurrent.Step == 2)  // vào cân nổi
                                    {
                                        new MyHub().Send("Scale_In_CN", orderCurrent.Vehicle);
                                        if (tmpCardNoIn_CN.FirstOrDefault(x => x.ToString().Equals(cardNoCurrent)) != null) continue;

                                        if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm3(cardNoCurrent))
                                        {
                                            //scale job
                                            if (orderCurrent != null)
                                            {
                                                log.Info($@"=========Vào cân nổi===================={orderCurrent.DeliveryCode}");
                                                _serviceFactory.Scale.UpdateWhenConfirmVehicle(scaleCode: "CN", deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, scaleIn: true, scaleOut: false);
                                                _serviceFactory.LogScale.InsertOrUpdateByDeliveryCode(deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, IsInScale: true);
                                            }
                                            //end scale job
                                            tmpCardNoIn_CN.Add(cardNoCurrent);
                                            _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, cardNoCurrent, 3);
                                            // ControlDevice(h21, 1, 1, 1, 1, 0, "");
                                        }
                                        if (tmpCardNoIn_CN.Count > 2) tmpCardNoIn_CN.RemoveRange(0, 2);
                                    }
                                }
                            }
                            i++;
                        }
                    }
                }
                else
                {
                    ScaleModule();
                }
            }
            catch (Exception Ex)
            {
                log.Error($@"===============ScaleModuleCN=========================== {Ex.Message}");
            }
        }
    }
}
