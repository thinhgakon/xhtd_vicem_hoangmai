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
    public class ScaleModuleCCJob : IJob
    {
        private IntPtr h21 = IntPtr.Zero;
        private IntPtr h = IntPtr.Zero;
        private static bool DeviceConnectedCC = false;

        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "Connect")]
        public static extern IntPtr Connect(string Parameters);
        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "PullLastError")]
        public static extern int PullLastError();
        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "GetRTLog")]
        public static extern int GetRTLog(IntPtr h, ref byte buffer, int buffersize);
        private List<string> tmpCardNoIn_CC = new List<string>() { };
        private List<string> tmpCardNoOut_CC = new List<string>() { };
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public ScaleModuleCCJob(IServiceFactory serviceFactory)
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
                ScaleModuleCC();
            });
        }
        public void ScaleModuleCC()
        {
            while (!DeviceConnectedCC)
            {
                ConnectScaleModuleCC();
            }
            AuthenticateCardNoInScaleModule();
        }
        public bool ConnectScaleModuleCC()
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
                        log.Info("--------------ScaleModulecc connected--------------");
                        DeviceConnectedCC = true;
                    }
                    else
                    {
                        log.Info("------------- ScaleModulecc -connected failed--------------");
                        ret = PullLastError();
                        DeviceConnectedCC = false;
                    }
                }
                return DeviceConnectedCC;
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
                if (DeviceConnectedCC)
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


                                    if (tmp[3]?.ToString() == "3")  // vào cân chìm
                                    {
                                        new MyHub().Send("Scale_In_CC", orderCurrent.Vehicle);
                                        if (tmpCardNoIn_CC.FirstOrDefault(x => x.ToString().Equals(cardNoCurrent)) != null) continue;

                                        if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm3(cardNoCurrent))
                                        {
                                            //scale job
                                            if (orderCurrent != null)
                                            {
                                                log.Info($@"=========Vào cân chìm====================={orderCurrent.DeliveryCode}");
                                                _serviceFactory.Scale.UpdateWhenConfirmVehicle(scaleCode: "CC", deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, scaleIn: true, scaleOut: false);
                                                _serviceFactory.LogScale.InsertOrUpdateByDeliveryCode(deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, IsInScale: true);
                                            }
                                            //end scale job
                                            tmpCardNoIn_CC.Add(cardNoCurrent);
                                            _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, cardNoCurrent, 3);
                                            // ControlDevice(h21, 1, 1, 1, 1, 0, "");
                                        }
                                        if (tmpCardNoIn_CC.Count > 2) tmpCardNoIn_CC.RemoveRange(0, 2);
                                    }
                                    else if (tmp[3]?.ToString() == "4" && orderCurrent.Step > 4)  // ra cân chìm
                                    {
                                        new MyHub().Send("Scale_Out_CC", orderCurrent.Vehicle);
                                        if (tmpCardNoOut_CC.FirstOrDefault(x => x.ToString().Equals(cardNoCurrent)) != null) continue;

                                        if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm7(cardNoCurrent))
                                        {
                                            //scale job
                                            if (orderCurrent != null)
                                            {
                                                log.Info($@"=========Ra cân chìm====================={orderCurrent.DeliveryCode}");
                                                _serviceFactory.Scale.UpdateWhenConfirmVehicle(scaleCode: "CC", deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, scaleIn: false, scaleOut: true);
                                                _serviceFactory.LogScale.InsertOrUpdateByDeliveryCode(deliveryCode: orderCurrent.DeliveryCode, vehicle: orderCurrent.Vehicle, IsInScale: false);
                                            }
                                            //end scale job

                                            tmpCardNoOut_CC.Add(cardNoCurrent);
                                            _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, cardNoCurrent, 7);
                                            //ControlDevice(h21, 1, 1, 1, 1, 0, "");
                                        }
                                        if (tmpCardNoOut_CC.Count > 2) tmpCardNoOut_CC.RemoveRange(0, 2);
                                    }
                                }
                            }
                            i++;
                        }
                    }
                }
                else
                {
                    ScaleModuleCC();
                }
            }
            catch (Exception Ex)
            {
                log.Error($@"===============ScaleModuleCC=========================== {Ex.Message}");
            }
        }
    }
}
