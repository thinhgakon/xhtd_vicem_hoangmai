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
using XHTD_ConfirmationPointModule_Service.Models;
using Newtonsoft.Json;
using HMXHTD.Data.DataEntity;
using System.Globalization;
using System.Threading;
using System.Runtime.InteropServices;
using System.Text;
using System.Data;
using HMXHTD.Services.Services;
using XHTD_ConfirmationPointModule_Service.LEDControl;
using Microsoft.AspNet.SignalR.Client.Hubs;
using Microsoft.AspNet.SignalR.Client;
using System.Net.Http;
using PLC_Lib;
using HMXHTD.Data.Models;
using Telegram.Bot;

namespace XHTD_ConfirmationPointModule_Service.Schedules
{
    public class ConfirmationPointModule_Job : IJob
    {
        private IHubProxy HubProxy { get; set; }
       // const string ServerURI = "http://127.0.0.1:8091/signalr"; // chay tren server
        const string ServerURI = "http://192.168.0.10:8091/signalr";//chay local
        private HubConnection Connection { get; set; }

        //light
        private PLCConnect PLC_M221;
        private Result PLC_Result;
        //end light

        private IntPtr h21 = IntPtr.Zero;
        private IntPtr h = IntPtr.Zero;
        private static bool DeviceConnected = false;
        private static int CountTest = 0;
        [DllImport("plcommpro.dll", EntryPoint = "Connect")]
        public static extern IntPtr Connect(string Parameters);
        [DllImport("plcommpro.dll", EntryPoint = "PullLastError")]
        public static extern int PullLastError();
        [DllImport("plcommpro.dll", EntryPoint = "GetRTLog")]
        public static extern int GetRTLog(IntPtr h, ref byte buffer, int buffersize);
        private List<string> tmpCardNo = new List<string>() { };
        private List<CardNoLog> tmpCardNoLst = new List<CardNoLog>();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        int m_nSendType_LED11;
        IntPtr m_pSendParams_LED11;
        public ConfirmationPointModule_Job(IServiceFactory serviceFactory)
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
                 ConfirmationPointModule();
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
            catch (HttpRequestException ex)
            {
                log.Error($@"ConnectAsync {ex.Message}");
            }
        }
        private void Connection_Closed()
        {
        }
        public void TestSignalR()
        {
            ConnectAsync();
            while (true)
            {
                SendNotification("ds");
                Thread.Sleep(2000);
            }
        }
        public void TestTele()
        {
            //_serviceFactory.Notification.SendNotificationToTelegram(DateTime.Now.ToString());
            SetLightStatus(false,false);
        }
        public void FixBug()
        {
            var cardNo = "2130006";
            if (tmpCardNo.Count > 0 && tmpCardNo.FirstOrDefault(x => x.ToString().Equals(cardNo)) != null) return;
            if (!_serviceFactory.RFID.CheckRFIDByCardNo(cardNo)) return;

            if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm1(cardNo))
            {
                tmpCardNo.Add(cardNo);
                var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(cardNo);
                _serviceFactory.StoreOrderOperating.UpdateIndexOrderForNewConfirm(cardNo);
                _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, cardNo, 1);
                //new MyHub().Send("Step_1", String.Format("Có xe vừa được xác thực qua cổng số 1. Cập nhật vào lúc : {0}", DateTime.Now));

            }
        }
        public void ConfirmationPointModule()
        {
            if (_serviceFactory.ConfigOperating.GetValueByCode(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name) == 0) return;
            m_nSendType_LED11 = 0;
            string strParams_LED11 = "192.168.22.6";
            m_pSendParams_LED11 = Marshal.StringToHGlobalUni(strParams_LED11);
            
            while (!DeviceConnected)
            {
              //  log.Info("--------------ConfirmationPointModule reconnect--------------");
               // Thread.Sleep(2000);
                ConnectConfirmationPointModule();
            }
            ConnectDeviceLight();
            AuthenticateCardNoInConfirmationPointModule();
            //sendContentToLED11("    ĐIỂM\nXÁC THỰC", true);
        }
        public void ConnectDeviceLight()
        {
            try
            {
                PLC_M221 = new PLCConnect();
                PLC_M221.Mode = Mode.TCP_IP;
                PLC_M221.ResponseTimeout = 1000;
                PLC_Result = PLC_M221.Connect("192.168.22.8", 502);
            }
            catch (Exception ex)
            {
                log.Error($@"Connect device light failed, ex: {ex.Message}");
            }
        }
        public bool ConnectConfirmationPointModule()
        {
            try
            {
                string str = "protocol=TCP,ipaddress=192.168.22.5,port=4370,timeout=2000,passwd=";
                int ret = 0;
                if (IntPtr.Zero == h21)
                {
                    h21 = Connect(str);
                    if (h21 != IntPtr.Zero)
                    {
                        log.Info("--------------ConnectConfirmationPointModule connected--------------");
                        DeviceConnected = true;
                        _serviceFactory.Notification.SendNotificationToTelegram($@"Thiết bị {str} đã kết nối thành công");
                        // ConnectAsync();
                    }
                    else
                    {
                        log.Info("------------- ConnectConfirmationPointModule -connected failed--------------");
                        ret = PullLastError();
                        DeviceConnected = false;
                        _serviceFactory.Notification.SendNotificationToTelegram($@"Thiết bị {str} mất kết nối");
                        //_serviceFactory.Common.SendMail("Xuất hàng tự động", "trungnc.bk@gmail.com", "Trung Nguyễn", "Thông tin từ hệ thống xuất hàng tự động", "", str);
                        //Thread.Sleep(1000*60*5);
                    }
                }
                return DeviceConnected;
            }
            catch (Exception ex)
            {
                log.Error($@"ConnectConfirmationPointModule : {ex.Message}");
                return false;
            }
        }
        public void AuthenticateCardNoInConfirmationPointModule()
        {
            try
            {
                log.Info("AuthenticateCardNoInConfirmationPointModule start");
                if (DeviceConnected)
                {
                    while (DeviceConnected)
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
                                        log.Info($@"==================ConnectConfirmationPointModule ==========card no================================= {tmp[2]}");
                                        _serviceFactory.Notification.SendNotificationToTelegram($@"{tmp[2]}");
                                        
                                        //SendNotification("ok");
                                    }
                                    if (tmp[2] == "0" || tmp[2] == "")
                                    {
                                        //log.Info($@"==================ConnectConfirmationPointModule ==========no card================================= {tmp[2]}");
                                        ProcessLogFollow();
                                    }
                                    else
                                    {
                                        #region process old
                                        //if (tmpCardNo.Count > 0 && tmpCardNo.FirstOrDefault(x => x.ToString().Equals(tmp[2]?.ToString())) != null) continue;
                                        //if (!_serviceFactory.RFID.CheckRFIDByCardNo(tmp[2]?.ToString())) continue;

                                        //if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm1(tmp[2]?.ToString()))
                                        //{
                                        //    tmpCardNo.Add(tmp[2]?.ToString());
                                        //    var orderCurrent1 = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(tmp[2]?.ToString());
                                        //    _serviceFactory.StoreOrderOperating.UpdateIndexOrderForNewConfirm(tmp[2]?.ToString());
                                        //    _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent1.Vehicle, tmp[2]?.ToString(), 1);
                                        //    //new MyHub().Send("Step_1", String.Format("Có xe vừa được xác thực qua cổng số 1. Cập nhật vào lúc : {0}", DateTime.Now));
                                        //    //_serviceFactory.Notification.SendNotificationToTelegram($@"Xe {orderCurrent.Vehicle} vừa xác thực qua cổng 1");
                                        //    ProcessLedConfirm(orderCurrent1.Vehicle);

                                        //}
                                        //if (tmpCardNo.Count > 2) tmpCardNo.RemoveRange(0, 2);
                                        #endregion

                                        var cardNoCurrent = tmp[2]?.ToString();
                                        log.Info($@"========== confirm ======== {cardNoCurrent}");
                                        if (tmpCardNoLst.Count > 5) tmpCardNoLst.RemoveRange(0, 2);
                                        var cardLogs = String.Join(";", tmpCardNoLst.Select(x => x.LogCat).ToArray());
                                        log.Info($@"========== log list ======== {cardLogs}");
                                        if (tmpCardNoLst.Exists(x => x.CardNo.Equals(cardNoCurrent) && x.DateTime > DateTime.Now.AddMinutes(-1)))
                                        {
                                            log.Warn($@"========== cardno exist on list, ra cn======== {cardNoCurrent}");
                                            continue;
                                        }
                                        var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(cardNoCurrent);
                                        if (orderCurrent == null)
                                        {
                                            log.Info($@"==========no order valid ======== {cardLogs}");
                                           // _serviceFactory.Notification.SendNotificationToTelegram($@"xe có thẻ {cardNoCurrent} chưa nhận đơn");
                                            continue;
                                        }
                                        else
                                        {

                                            log.Warn($@"==========confirm with exist order ======= {cardNoCurrent}");
                                            if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm1(cardNoCurrent))
                                            {
                                                _serviceFactory.Notification.SendNotificationToTelegram($@"xác thực thành công xe {orderCurrent.Vehicle}");
                                                log.Warn($@"==========xac thuc thanh cong, step 1======= {cardNoCurrent}");
                                                _serviceFactory.StoreOrderOperating.UpdateIndexOrderForNewConfirm(tmp[2]?.ToString());
                                                log.Warn($@"==========turn light======= {cardNoCurrent}");
                                                ProcessLedConfirm(orderCurrent.Vehicle);
                                                tmpCardNoLst.Add(new CardNoLog { CardNo = cardNoCurrent, DateTime = DateTime.Now });
                                                _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, tmp[2]?.ToString(), 1);
                                            }

                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    log.Error($@"Error trace {ex.StackTrace} {ex.Message} ");
                                    continue;
                                }
                            }
                            else
                            {
                                log.Warn("Error can not read data, internet connect failed");
                                DeviceConnected = false;
                                h21 = IntPtr.Zero;
                                ConfirmationPointModule();
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                log.Error($@"===============ConnectConfirmationPointModule=========================== {Ex.Message}");
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
        public void SendNotification(string cardNo = "")
        {
            try
            {
                HubProxy.Invoke("ConfirmPointModule", cardNo , DateTime.Now);
            }
            catch (Exception ex)
            {
                log.Error($@"SendNotification {ex.Message}");
            }
        }
        public void ProcessLedConfirm(string vehicle)
        {
           // sendContentToLED11(vehicle, true);
            SetLightStatus(true,false);
            log.Info($@"======Light active with vehicle {vehicle}");
            Thread.Sleep(15000);
            SetLightStatus(false, false);
          //  sendContentToLED11("    ĐIỂM\nXÁC THỰC", false);
        }
        private void sendContentToLED11(string content, bool isConfirmed)
        {
            try
            {
                IntPtr pNULL = new IntPtr(0);

                int nErrorCode = -1;
                // 1. Create a screen
                int nWidth = 64;
                int nHeight = 32;
                int nColor = 1;
                int nGray = 1;
                int nCardType = 0;

                int nRe = CSDKExport.Hd_CreateScreen(nWidth, nHeight, nColor, nGray, nCardType, pNULL, 0);
                if (nRe != 0)
                {
                    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                    return;
                }

                // 2. Add program to screen
                int nProgramID = CSDKExport.Hd_AddProgram(pNULL, 0, 0, pNULL, 0);
                if (nProgramID == -1)
                {
                    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                    return;
                }

                //Add Area 1
                int nX1 = 0;
                int nY1 = 0;
                int nAreaWidth = 64;
                int nAreaHeight = 28;

                // 3. Add Area to program
                int nAreaID_1 = CSDKExport.Hd_AddArea(nProgramID, nX1, nY1, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
                if (nAreaID_1 == -1)
                {
                    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                    return;
                }


                // 4.Add text AreaItem to Area
                IntPtr pText = Marshal.StringToHGlobalUni(content);
                IntPtr pFontName = Marshal.StringToHGlobalUni("Times New Roman");
                int nTextColor = CSDKExport.Hd_GetColor(255, 0, 0);

                // center in bold and underline
                int nTextStyle = 0x0004 | 0x0100/* | 0x0200 */;

                int nFontHeight = 10;
                int nEffect = 0;

                //Show on Area 1
                int nAreaItemID_1 = nAreaItemID_1 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID: nAreaID_1, pText: pText, nTextColor: nTextColor, nBackGroupColor: 0, nStyle: nTextStyle,
                                pFontName: pFontName, nFontHeight: nFontHeight, nShowEffect: nEffect, nShowSpeed: 10, nClearType: 0, nStayTime: 3, pExParamsBuf: pNULL, nBufSize: 0); ;
                if (isConfirmed)
                {
                    nTextColor = CSDKExport.Hd_GetColor(255, 255, 255);
                }
                if (nAreaItemID_1 == -1)
                {
                    Marshal.FreeHGlobal(pText);
                    Marshal.FreeHGlobal(pFontName);
                    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                    return;
                }

                Marshal.FreeHGlobal(pText);
                Marshal.FreeHGlobal(pFontName);

                // 5. Send to device 
                nRe = CSDKExport.Hd_SendScreen(m_nSendType_LED11, m_pSendParams_LED11, pNULL, pNULL, 0);
                if (nRe != 0)
                {
                    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        private void SetLightStatus(bool isGreen, bool reset)
        {
            try
            {
                ConnectDeviceLight();
                if (reset)
                {
                    if (CheckPortIsOn(byte.Parse("1"))){
                        PLC_M221.ShuttleOutputPort((byte.Parse("1")));
                    }
                    if (CheckPortIsOn(byte.Parse("2"))){
                        PLC_M221.ShuttleOutputPort((byte.Parse("2")));
                    }
                    return;
                }
                if (isGreen)
                {
                    if (!CheckPortIsOn(byte.Parse("1")))
                    {
                        PLC_M221.ShuttleOutputPort((byte.Parse("1")));
                    }
                    if (CheckPortIsOn(byte.Parse("2")))
                    {
                        PLC_M221.ShuttleOutputPort((byte.Parse("2")));
                    }
                }
                else
                {
                    if (CheckPortIsOn(byte.Parse("1")))
                    {
                        PLC_M221.ShuttleOutputPort((byte.Parse("1")));
                    }
                    if (!CheckPortIsOn(byte.Parse("2")))
                    {
                        PLC_M221.ShuttleOutputPort((byte.Parse("2")));
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        private bool CheckPortIsOn(byte k)
        {
            bool isOn = false;
            try
            {
                
                bool[] Ports = new bool[24]; //There are 24 input ports on M221 PLC
                PLC_Result = PLC_M221.CheckInputPorts(Ports);
                if (PLC_Result == Result.SUCCESS)
                {
                    if (Ports[k])
                    {
                        isOn = true;
                    }
                }
            }
            catch (Exception)
            {
                
            }
            return isOn;
        }
    }
}
