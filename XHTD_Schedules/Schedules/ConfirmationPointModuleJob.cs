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
    public class ConfirmationPointModuleJob : IJob
    {
        private IntPtr h21 = IntPtr.Zero;
        private IntPtr h = IntPtr.Zero;
        private static bool DeviceConnected = false;

        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "Connect")]
        public static extern IntPtr Connect(string Parameters);
        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "PullLastError")]
        public static extern int PullLastError();
        [DllImport("plcommpro.dll", EntryPoint = "GetRTLog")]
        public static extern int GetRTLog(IntPtr h, ref byte buffer, int buffersize);
        private List<string> tmpCardNo = new List<string>() { };
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        int m_nSendType_LED11;
        IntPtr m_pSendParams_LED11;
        public ConfirmationPointModuleJob(IServiceFactory serviceFactory)
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
        public void FixBug()
        {
            var cardNo = "1048581";
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
            m_nSendType_LED11 = 0;
            string strParams_LED11 = "192.168.22.6";
            m_pSendParams_LED11 = Marshal.StringToHGlobalUni(strParams_LED11);
            while (!DeviceConnected)
            {
                ConnectConfirmationPointModule();
            }
            AuthenticateCardNoInConfirmationPointModule();
            //sendContentToLED11("    ĐIỂM\nXÁC THỰC", true);
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
                        //log.Info("--------------ConnectConfirmationPointModule connected--------------");
                        DeviceConnected = true;
                    }
                    else
                    {
                        log.Info("------------- ConnectConfirmationPointModule -connected failed--------------");
                        ret = PullLastError();
                        DeviceConnected = false;
                        //_serviceFactory.Common.SendMail("Xuất hàng tự động", "trungnc.bk@gmail.com", "Trung Nguyễn", "Thông tin từ hệ thống xuất hàng tự động", "", str);
                        //Thread.Sleep(1000*60*5);
                        _serviceFactory.Notification.SendNotificationToTelegram($@"Thiết bị {str} mất kết nối");
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
                                   log.Info($@"==================ConnectConfirmationPointModule ==========card no================================= {tmp[2]}");

                                }
                                if (tmp[2] == "0" || tmp[2] == "")
                                {
                                    
                                }
                                else
                                {
                                    if (tmpCardNo.Count > 0 && tmpCardNo.FirstOrDefault(x => x.ToString().Equals(tmp[2].ToString())) != null) continue;
                                    if (!_serviceFactory.RFID.CheckRFIDByCardNo(tmp[2].ToString())) continue;

                                    if (_serviceFactory.StoreOrderOperating.UpdateBillOrderConfirm1(tmp[2].ToString()))
                                    {
                                        tmpCardNo.Add(tmp[2].ToString());
                                        var orderCurrent = _serviceFactory.StoreOrderOperating.GetCurrentOrderByCardNoReceiving(tmp[2].ToString());
                                        _serviceFactory.StoreOrderOperating.UpdateIndexOrderForNewConfirm(tmp[2].ToString());
                                        _serviceFactory.LogStoreOrderOperating.InsertLogOnly(orderCurrent.Vehicle, tmp[2].ToString(), 1);
                                        //new MyHub().Send("Step_1", String.Format("Có xe vừa được xác thực qua cổng số 1. Cập nhật vào lúc : {0}", DateTime.Now));
                                        ProcessLedConfirm(orderCurrent.Vehicle);
                                    }
                                    if (tmpCardNo.Count > 2) tmpCardNo.RemoveRange(0, 2);

                                }
                            }
                            else
                            {
                                ConfirmationPointModule();
                            }
                        }
                    }
                }
                else
                {
                    ConfirmationPointModule();
                }
            }
            catch (Exception Ex)
            {
                log.Error($@"===============ConnectConfirmationPointModule=========================== {Ex.Message}");
            }
        }
        public void ProcessLedConfirm(string vehicle)
        {
            sendContentToLED11(vehicle, true);
            Thread.Sleep(2000);
            sendContentToLED11("    ĐIỂM\nXÁC THỰC", false);
        }
        private void sendContentToLED11(string content, bool isConfirmed)
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
    }
}
