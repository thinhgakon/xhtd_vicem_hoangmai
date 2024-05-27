using HMXHTD.Services.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using XHTD_Led_Service.LEDControl;
using HMXHTD.Data.DataEntity;
using System.Collections;

namespace XHTD_Led_Service.Schedules
{
    public class ControlLedInTroughJob : IJob
    {
        int m_nSendType_LED;
        IntPtr m_pSendParams_LED;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDictionary<string, string> listParam = new Dictionary<string, string>();
        protected readonly IServiceFactory _serviceFactory;
        public ControlLedInTroughJob(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            listParam.Add(new KeyValuePair<string, string>("M1", "192.168.21.50"));
            listParam.Add(new KeyValuePair<string, string>("M2", "192.168.21.51"));
            listParam.Add(new KeyValuePair<string, string>("M3", "192.168.21.52"));
            listParam.Add(new KeyValuePair<string, string>("M4", "192.168.21.53"));
            listParam.Add(new KeyValuePair<string, string>("M5", "192.168.21.54"));
            listParam.Add(new KeyValuePair<string, string>("M6", "192.168.21.56"));
        }
        public async Task Execute(IJobExecutionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            await Task.Run(() =>
            {
                ProcessShowLedInTrough();
            });
        }
        private void ProcessShowLedInTrough()
        {
            //  log.Info("==============start process ProcessShowLedInTrough ====================");
            if (_serviceFactory.ConfigOperating.GetValueByCode(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name) == 0) return;
            ProcessShowLedInSigleTrough("M1");
            ProcessShowLedInSigleTrough("M2");
            ProcessShowLedInSigleTrough("M3");
            ProcessShowLedInSigleTrough("M4");
            ProcessShowLedInSigleTrough("M5");
            ProcessShowLedInSigleTrough("M6");
        }
        private void ProcessShowLedInSigleTrough(string trough)
        {
            try
            {
                var troughDB = new tblTrough();
                using (var db = new HMXuathangtudong_Entities())
                {
                    troughDB = db.tblTroughs.FirstOrDefault(x=>x.Code == trough);
                }
                 ShowLedProcess(troughDB, (bool)troughDB.IsPicking, (bool)troughDB.IsInviting);
               // ShowLedProcessTest(troughDB, (bool)troughDB.IsPicking, (bool)troughDB.IsInviting);
                //if ((bool)troughDB.IsPicking)
                //{
                //    ShowLedProcess(troughDB);
                //}
                //else if (!(bool)troughDB.IsPicking && (bool)troughDB.IsInviting)
                //{
                //    ShowLedInviting(troughDB);
                //}
                //else
                //{
                //    ShowLedNoContent(troughDB);
                //}
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        private void ShowLedProcess(tblTrough trough, bool isPicking, bool isInviting)
        {
            //isPicking = false; isInviting = false;
            m_nSendType_LED = 0;
            string strParams_LED = listParam[trough.Code];
            m_pSendParams_LED = Marshal.StringToHGlobalUni(strParams_LED);


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

            int plusX = 6;
            int plusY = 6;
            int nAreaWidth = 64;
            int nAreaHeight = 32;

            #region Add Area 0
            int nX1 = 0;
            int nY1 = 0;
            int nAreaID_1 = CSDKExport.Hd_AddArea(nProgramID, nX1, nY1, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
            if (nAreaID_1 == -1)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion
            var vehicleCode = trough.TransportNameCurrent == "MAC DINH" ? trough.Code : trough.TransportNameCurrent;
            var textShow = isPicking ? $"{vehicleCode}" : (isInviting ? $"{vehicleCode}" : $"{trough.Code}".Replace("M", ""));
            IntPtr pText = Marshal.StringToHGlobalUni(textShow.ToUpper());
            IntPtr pFontName = Marshal.StringToHGlobalUni("Times New Roman");
            int nTextColor = CSDKExport.Hd_GetColor(255, 0, 0);

            int nTextStyle = 0x0004 | 0x0100 /*| 0x0200 */;
            int nFontHeight = isPicking ? 14 : (isInviting ? 14 : 25);
            int nEffect = 0, nShowSpeed = 30, nClearType = 201;

            if (!isPicking && isInviting)
            {
                nEffect = 8;
                nShowSpeed =  10; 
                nClearType = 0;
            }

            #region Show on Area 0
            int nAreaItemID_1 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_1, pText, nTextColor, 0, nTextStyle,
                pFontName, nFontHeight, nEffect, nShowSpeed, nClearType, 3, pNULL, 0);
            if (nAreaItemID_1 == -1)
            {
                Marshal.FreeHGlobal(pText);
                Marshal.FreeHGlobal(pFontName);
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion

            nRe = CSDKExport.Hd_SendScreen(m_nSendType_LED, m_pSendParams_LED, pNULL, pNULL, 0);
            if (nRe != 0)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
            }


        }
        private void ShowLedProcessTest(tblTrough trough, bool isPicking, bool isInviting)
        {
            isPicking = false; isInviting = false;
            m_nSendType_LED = 0;
            string strParams_LED = listParam[trough.Code];
            m_pSendParams_LED = Marshal.StringToHGlobalUni(strParams_LED);


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

            int nAreaWidth = 64;
            int nAreaHeight = 32;

            #region Add Area 0
            int nX1 = 0;
            int nY1 = 0;
            int nAreaID_1 = CSDKExport.Hd_AddArea(nProgramID, nX1, nY1, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
            if (nAreaID_1 == -1)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion
            var vehicleCode = trough.TransportNameCurrent == "MAC DINH" ? trough.Code : trough.TransportNameCurrent;
            var textShow ="1";
            IntPtr pText = Marshal.StringToHGlobalUni(textShow.ToUpper());
            IntPtr pFontName = Marshal.StringToHGlobalUni("Times New Roman");
            int nTextColor = CSDKExport.Hd_GetColor(255, 0, 0);

            int nTextStyle = 0x0004 | 0x0100 /*| 0x0200 */;
            int nFontHeight = isPicking ? 14 : (isInviting ? 14 : 25);
            int nEffect = 0, nShowSpeed = 30, nClearType = 201;

            if (!isPicking && isInviting)
            {
                nEffect = 8;
                nShowSpeed = 10;
                nClearType = 0;
            }

            #region Show on Area 0
            int nAreaItemID_1 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_1, pText, nTextColor, 0, nTextStyle,
                pFontName, nFontHeight, nEffect, nShowSpeed, nClearType, 3, pNULL, 0);
            if (nAreaItemID_1 == -1)
            {
                Marshal.FreeHGlobal(pText);
                Marshal.FreeHGlobal(pFontName);
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion

            nRe = CSDKExport.Hd_SendScreen(m_nSendType_LED, m_pSendParams_LED, pNULL, pNULL, 0);
            if (nRe != 0)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
            }

        }
        private void ShowLedInviting(tblTrough trough)
        {
            m_nSendType_LED = 0;
            string strParams_LED = listParam[trough.Code];
            m_pSendParams_LED = Marshal.StringToHGlobalUni(strParams_LED);


            IntPtr pNULL = new IntPtr(0);

            int nErrorCode = -1;
            // 1. Create a screen
            int nWidth = 224;
            int nHeight = 160;
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

            int plusX = 6;
            int plusY = 6;
            int nAreaWidth = 224;
            int nAreaHeight = 16;
            IntPtr pFontName = Marshal.StringToHGlobalUni("Times New Roman");
            int nTextColor = CSDKExport.Hd_GetColor(255, 0, 0);


            #region Add Area 0
            int nX1 = 0;
            int nY1 = 0;
            int nAreaID_1 = CSDKExport.Hd_AddArea(nProgramID, nX1, nY1, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
            if (nAreaID_1 == -1)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion


            IntPtr pText = Marshal.StringToHGlobalUni($"{trough.TransportNameCurrent}");
            
            int nTextStyle = 0x0000 | 0x0100 /*| 0x0200 */;
            int nFontHeight = 14;
            int nEffect = 0;

            #region Show on Area 0
            int nAreaItemID_1 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_1, pText, nTextColor, 0, nTextStyle,
                pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
            if (nAreaItemID_1 == -1)
            {
                Marshal.FreeHGlobal(pText);
                Marshal.FreeHGlobal(pFontName);
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion

            nRe = CSDKExport.Hd_SendScreen(m_nSendType_LED, m_pSendParams_LED, pNULL, pNULL, 0);
            if (nRe != 0)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
            }
        }
        private void ShowLedNoContent(tblTrough trough)
        {
            m_nSendType_LED = 0;
            string strParams_LED = listParam[trough.Code];
            m_pSendParams_LED = Marshal.StringToHGlobalUni(strParams_LED);


            IntPtr pNULL = new IntPtr(0);

            int nErrorCode = -1;
            // 1. Create a screen
            int nWidth = 224;
            int nHeight = 160;
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

            int plusX = 6;
            int plusY = 6;
            int nAreaWidth = 224;
            int nAreaHeight = 16;

            #region Add Area 0
            int nX1 = 0;
            int nY1 = 0;
            int nAreaID_1 = CSDKExport.Hd_AddArea(nProgramID, nX1, nY1, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
            if (nAreaID_1 == -1)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion


            IntPtr pText = Marshal.StringToHGlobalUni("BIEN SO             TRANG THAI");
            IntPtr pFontName = Marshal.StringToHGlobalUni("Times New Roman");
            int nTextColor = CSDKExport.Hd_GetColor(255, 0, 0);

            int nTextStyle = 0x0000 | 0x0100 /*| 0x0200 */;
            int nFontHeight = 14;
            int nEffect = 0;

            #region Show on Area 0
            int nAreaItemID_1 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_1, pText, nTextColor, 0, nTextStyle,
                pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
            if (nAreaItemID_1 == -1)
            {
                Marshal.FreeHGlobal(pText);
                Marshal.FreeHGlobal(pFontName);
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion

            nRe = CSDKExport.Hd_SendScreen(m_nSendType_LED, m_pSendParams_LED, pNULL, pNULL, 0);
            if (nRe != 0)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
            }
        }
    }
}
