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
using XHTD_Getway_In_Service.Models;
using Newtonsoft.Json;
using HMXHTD.Data.DataEntity;
using System.Globalization;
using System.Threading;
using System.Runtime.InteropServices;
using System.Text;
using System.Data;
using HMXHTD.Services.Services;
using XHTD_Getway_In_Service.LEDControl;
using Autofac;
using HMXHTD.Data.Models;
using Microsoft.AspNet.SignalR.Client;

namespace XHTD_Getway_In_Service.Schedules
{
    public class GatewayModuleJob : IJob
    {
        private IntPtr h21 = IntPtr.Zero;
        private IntPtr h = IntPtr.Zero;
        private static bool DeviceConnected = false;
        private IHubProxy HubProxy { get; set; }
        const string ServerURI = "http://192.168.0.10:8091/signalr";
        private HubConnection Connection { get; set; }

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
        //private List<CardNoLog> tmpCardNoLst_In = new List<CardNoLog>();
        //private List<CardNoLog> tmpCardNoLst_Out = new List<CardNoLog>();
        private List<CardNoLog> tmpCardNoLst = new List<CardNoLog>();
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
                ConnectAsync();
                AuthenticateGatewayModule();
            });
        }
        private async void ConnectAsync()
        {
            Connection = new HubConnection(ServerURI);
            Connection.Closed += Connection_Closed;
            HubProxy = Connection.CreateHubProxy("MyHub");
            try
            {
                await Connection.Start();
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
            }

        }
        private void Connection_Closed()
        {
        }
        public void AuthenticateGatewayModule()
        {
            if (_serviceFactory.ConfigOperating.GetValueByCode(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name) == 0) return;
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
                string str = "protocol=TCP,ipaddress=192.168.22.20,port=4370,timeout=2000,passwd=";
                int ret = 0;
                if (IntPtr.Zero == h21)
                {
                    h21 = Connect(str);
                    if (h21 != IntPtr.Zero)
                    {
                        log.Info("--------------connected--------------");
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
                if (DeviceConnected)
                {
                        while (DeviceConnected)
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
                                        _serviceFactory.Notification.SendNotificationToTelegram($@"---- {tmp[2]}------{tmp[3]}");
                                    }
                                    if (tmp[2] == "0" || tmp[2] == "")
                                    {
                                        ProcessLogFollow();
                                    }
                                    else
                                    {
                                        var cardNoCurrent = tmp[2]?.ToString();
                                        if (!_serviceFactory.RFID.CheckRFIDByCardNo(cardNoCurrent)) continue; // đoạn này sau mở ra để check cardno
                                                                                                              // check step của đơn hàng hiện tại để xác định đang vào hay ra
                                        #region process old
                                        //if (tmp[3].ToString() == "2") // vào
                                        //{
                                        //    if (tmpCardNoIn.FirstOrDefault(x => x.ToString().Equals(tmp[2]?.ToString())) != null) continue;

                                        //    if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm2(tmp[2]?.ToString()))
                                        //    {

                                        //        var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(tmp[2]?.ToString());
                                        //        if (orderCurrent == null) continue;
                                        //        ProcessLedAndBarrier(true, false, orderCurrent?.Vehicle);
                                        //        //nếu vào thì phải cập nhật lại lốt
                                        //        _serviceFactory.StoreOrderOperating.ReIndexOrderWhenSyncOrderWithEnd((int)orderCurrent.OrderId);
                                        //        tmpCardNoIn.Add(tmp[2]?.ToString());
                                        //        _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, tmp[2]?.ToString(), 4);
                                        //        ShowInviteLED(orderCurrent?.Vehicle);
                                        //    }
                                        //    if (tmpCardNoIn.Count > 5) tmpCardNoIn.RemoveRange(0, 2);
                                        //}
                                        //else if (tmp[3].ToString() == "3") // ra
                                        //{
                                        //    if (tmpCardNoOut.FirstOrDefault(x => x.ToString().Equals(tmp[2]?.ToString())) != null) continue;
                                        //    if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm8(tmp[2]?.ToString()))
                                        //    {
                                        //        var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(tmp[2]?.ToString());
                                        //        if (orderCurrent == null) continue;
                                        //        ProcessLedAndBarrier(false, false, orderCurrent?.Vehicle);
                                        //        tmpCardNoOut.Add(tmp[2]?.ToString());
                                        //        _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, tmp[2]?.ToString(), 8);
                                        //    }
                                        //    if (tmpCardNoOut.Count > 5) tmpCardNoOut.RemoveRange(0, 2);
                                        //}
                                        #endregion

                                        #region code old
                                        //if (tmp[3].ToString() == "2") // vào
                                        //{
                                        //    log.Info($@"==========in getway3 ======== {cardNoCurrent}");
                                        //    if (tmpCardNoLst_In.Count > 5) tmpCardNoLst_In.RemoveRange(0, 2);
                                        //    var cardLogs = String.Join(";", tmpCardNoLst_In.Select(x => x.LogCat).ToArray());
                                        //    log.Info($@"========== log list in ======== {cardLogs}");
                                        //    if (tmpCardNoLst_In.Exists(x => x.CardNo.Equals(cardNoCurrent) && x.DateTime > DateTime.Now.AddMinutes(-1)))
                                        //    {
                                        //        log.Warn($@"========== cardno exist on list, vao======== {cardNoCurrent}");
                                        //        continue;
                                        //    }

                                        //    if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm2(tmp[2]?.ToString()))
                                        //    {
                                        //        //ProcessLedAndBarrier(true, true, orderCurrent?.Vehicle);
                                        //        log.Info($@"==========xac thuc vao cong ======== {cardNoCurrent}");
                                        //        tmpCardNoLst_In.Add(new CardNoLog { CardNo = cardNoCurrent, DateTime = DateTime.Now });
                                        //        var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(tmp[2]?.ToString());
                                        //        if (orderCurrent == null) continue;
                                        //        log.Info($@"==========open barrier, comein ======== {cardNoCurrent}");
                                        //        ProcessLedAndBarrier(true, true, orderCurrent?.Vehicle);
                                        //        log.Info($@"==========reindex, comein ======== {cardNoCurrent}");
                                        //        _serviceFactory.StoreOrderOperating.ReIndexOrderWhenSyncOrderWithEnd((int)orderCurrent.OrderId);
                                        //        _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, tmp[2]?.ToString(), 4);
                                        //    }
                                        //}
                                        //else if (tmp[3].ToString() == "3") // ra
                                        //{
                                        //    log.Info($@"==========out getway3 ======== {cardNoCurrent}");
                                        //    if (tmpCardNoLst_Out.Count > 5) tmpCardNoLst_Out.RemoveRange(0, 2);
                                        //    var cardLogs = String.Join(";", tmpCardNoLst_Out.Select(x => x.LogCat).ToArray());
                                        //    log.Info($@"========== log list out======== {cardLogs}");
                                        //    if (tmpCardNoLst_Out.Exists(x => x.CardNo.Equals(cardNoCurrent) && x.DateTime > DateTime.Now.AddMinutes(-1)))
                                        //    {
                                        //        log.Warn($@"========== cardno exist on list, out======== {cardNoCurrent}");
                                        //        continue;
                                        //    }
                                        //    if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm8(tmp[2]?.ToString()))
                                        //    {
                                        //        ProcessLedAndBarrier(false, true, "");
                                        //        log.Info($@"==========xac thuc ra cong ======== {cardNoCurrent}");
                                        //        tmpCardNoLst_Out.Add(new CardNoLog { CardNo = cardNoCurrent, DateTime = DateTime.Now });
                                        //        var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(tmp[2]?.ToString());
                                        //        if (orderCurrent == null) continue;
                                        //        log.Info($@"==========open barrier, out ======== {cardNoCurrent}");
                                        //        //ProcessLedAndBarrier(false, true, orderCurrent?.Vehicle);
                                        //        _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, tmp[2]?.ToString(), 8);
                                        //    }
                                        //}
                                        #endregion


                                        if (tmp[3].ToString() == "2" || tmp[3].ToString() == "3") // vào
                                        {
                                            log.Info($@"========== getway ======== {cardNoCurrent}");
                                            if(cardNoCurrent == "1048598")
                                            {
                                                //ControlDevice(h21, 1, 1, 1, 1, 0, "");
                                                //Thread.Sleep(5000);
                                            }
                                            if (tmpCardNoLst.Count > 5) tmpCardNoLst.RemoveRange(0, 4);
                                            var cardLogs = String.Join(";", tmpCardNoLst.Select(x => x.LogCat).ToArray());
                                            log.Info($@"========== log list  ======== {cardLogs}");
                                            if (tmpCardNoLst.Exists(x => x.CardNo.Equals(cardNoCurrent) && x.DateTime > DateTime.Now.AddMinutes(-1)))
                                            {
                                                log.Warn($@"========== cardno exist on list, vao======== {cardNoCurrent}");
                                                continue;
                                            }
                                            var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(cardNoCurrent);
                                            if (orderCurrent == null) continue;
                                            if(orderCurrent.Step < 6)
                                            {
                                                log.Info($@"========== cho xac thuc vao cong ======== {cardNoCurrent}");
                                                if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm2(tmp[2]?.ToString()))
                                                {
                                                    //ProcessLedAndBarrier(true, true, orderCurrent?.Vehicle);
                                                    log.Info($@"==========xac thuc vao cong ======== {cardNoCurrent}");
                                                    tmpCardNoLst.Add(new CardNoLog { CardNo = cardNoCurrent, DateTime = DateTime.Now });
                                                    //if (orderCurrent == null) continue;
                                                    log.Info($@"==========open barrier, comein ======== {cardNoCurrent}");
                                                    ProcessLedAndBarrier(true, true, orderCurrent?.Vehicle);
                                                    log.Info($@"==========reindex, comein ======== {cardNoCurrent}");
                                                    _serviceFactory.StoreOrderOperating.ReIndexOrderWhenSyncOrderWithEnd((int)orderCurrent.OrderId);
                                                    _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, tmp[2]?.ToString(), 4);
                                                    SendStepSignalR(orderCurrent.Vehicle);
                                                }
                                            }
                                            else
                                            {
                                                //log.Info($@"========== cho xac thuc ra cong ======== {cardNoCurrent}");
                                                //if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm8(tmp[2]?.ToString()))
                                                //{
                                                //    ProcessLedAndBarrier(false, true, "");
                                                //    log.Info($@"==========xac thuc ra cong ======== {cardNoCurrent}");
                                                //    tmpCardNoLst.Add(new CardNoLog { CardNo = cardNoCurrent, DateTime = DateTime.Now });
                                                //   // if (orderCurrent == null) continue;
                                                //    log.Info($@"==========open barrier, out ======== {cardNoCurrent}");
                                                //    //ProcessLedAndBarrier(false, true, orderCurrent?.Vehicle);
                                                //    _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, tmp[2]?.ToString(), 8);
                                                //}
                                            }
                                            
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    log.Error($@"Lỗi xẩy ra {ex.StackTrace} {ex.Message} ");
                                    continue;
                                }
                            }
                            else
                            {
                                log.Warn("Lỗi không đọc được dữ liệu, có thể do mất kết nối");
                                DeviceConnected = false;
                                h21 = IntPtr.Zero;
                                AuthenticateGatewayModule();
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                log.Error($@"===================gatewayjob======================= {Ex.StackTrace}");
            }
        }
        private void SendStepSignalR(string vehicle)
        {
            try
            {
                log.Info("=============sent step 2=========");
                HubProxy.Invoke("Send", "Step_2", vehicle).Wait();
            }
            catch (Exception ex)
            {

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
        private void ProcessLedAndBarrier(bool actionLed, bool actionBarrier, string vehicle)
        {
            try
            {
                //if (actionLed)
                //{
                //    AutoFacBootstrapper.Init().Resolve<LEDGetwayFrontControl>().SendLedFrontAllArea(isShowArea1: true, isShowArea2: true, isShowArea3: true, isInviteVehicleComeIn: true, contentComeIn: vehicle);
                //}
               // Thread.Sleep(15000);
                if (actionBarrier)
                {
                    if (_serviceFactory.ConfigOperating.GetValueByCode("ActiveBarrierGateway") == 1)
                    {
                        ControlDevice(h21, 1, 1, 1, 1, 0, "");
                    }
                }
                
                if (actionLed)
                {
                    AutoFacBootstrapper.Init().Resolve<LEDGetwayFrontControl>().SendLedFrontAllArea(isShowArea1: true, isShowArea2: true, isShowArea3: true, isInviteVehicleComeIn: false, contentComeIn: "");
                }
            }
            catch (Exception ex)
            {
                log.Error($@"ProcessLedAndBarrier, {ex.Message}");
            }
        }
    }
}
