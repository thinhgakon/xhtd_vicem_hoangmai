using HMXHTD.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace XHTD_Schedules.LEDControl
{
    public class LED12Control
    {
        int m_nSendType_LED12;
        IntPtr m_pSendParams_LED12;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IStoreOrderOperatingService _storeOrderOperatingService;
        private ILogStoreOrderOperatingService _logStoreOrderOperatingService;
        public LED12Control(IStoreOrderOperatingService storeOrderOperatingService, ILogStoreOrderOperatingService logStoreOrderOperatingService)
        {
            _storeOrderOperatingService = storeOrderOperatingService;
            _logStoreOrderOperatingService = logStoreOrderOperatingService;

            m_nSendType_LED12 = 0;
            string strParams_LED12 = "192.168.22.19";
            m_pSendParams_LED12 = Marshal.StringToHGlobalUni(strParams_LED12);
        }
        private void sendToLED12()
        {
            var orderShows = _storeOrderOperatingService.GetStoreOrderForLED12();
            if (orderShows.Count == 0)
            {
                SetLED12NoContent();
                return;
            }

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

            #region Add Area 0
            int nX1 = 0;
            int nY1 = 0;
            int nAreaWidth = 224;
            int nAreaHeight = 16;

            int nAreaID_1 = CSDKExport.Hd_AddArea(nProgramID, nX1, nY1, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
            if (nAreaID_1 == -1)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion

            #region Add Area 1
            int nX2 = 0;
            int nY2 = 16;


            int nAreaID_2 = CSDKExport.Hd_AddArea(nProgramID, nX2, nY2, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
            if (nAreaID_2 == -1)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion

            #region Add Area 2
            int nX3 = 0;
            int nY3 = 32;

            int nAreaID_3 = CSDKExport.Hd_AddArea(nProgramID, nX3, nY3, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
            if (nAreaID_3 == -1)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion

            #region Add Area 3
            int nX4 = 0;
            int nY4 = 48;

            int nAreaID_4 = CSDKExport.Hd_AddArea(nProgramID, nX4, nY4, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
            if (nAreaID_4 == -1)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion

            #region Add Area 4
            int nX5 = 0;
            int nY5 = 64;

            int nAreaID_5 = CSDKExport.Hd_AddArea(nProgramID, nX5, nY5, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
            if (nAreaID_5 == -1)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion

            #region Add Area 5
            int nX6 = 0;
            int nY6 = 80;

            int nAreaID_6 = CSDKExport.Hd_AddArea(nProgramID, nX6, nY6, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
            if (nAreaID_6 == -1)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion

            #region Add Area 6
            int nX7 = 0;
            int nY7 = 96;

            int nAreaID_7 = CSDKExport.Hd_AddArea(nProgramID, nX7, nY7, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
            if (nAreaID_7 == -1)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion

            //  mới thêm
            #region Add Area 7
            int nX8 = 0;
            int nY8 = 112;

            int nAreaID_8 = CSDKExport.Hd_AddArea(nProgramID, nX8, nY8, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
            if (nAreaID_8 == -1)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion
            #region Add Area 8
            int nX9 = 0;
            int nY9 = 128;

            int nAreaID_9 = CSDKExport.Hd_AddArea(nProgramID, nX9, nY9, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
            if (nAreaID_9 == -1)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion
            #region Add Area 9
            int nX10 = 0;
            int nY10 = 144;

            int nAreaID_10 = CSDKExport.Hd_AddArea(nProgramID, nX10, nY10, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
            if (nAreaID_10 == -1)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion

            #region DÒNG TIÊU ĐỀ
            // 4.Add text AreaItem to Area
            IntPtr pText = Marshal.StringToHGlobalUni(" BIEN SO             TRANG THAI");
            IntPtr pFontName = Marshal.StringToHGlobalUni("Times New Roman");
            int nTextColor = CSDKExport.Hd_GetColor(255, 0, 0);

            // center in bold and underline
            int nTextStyle = 0x0000 | 0x0100 /*| 0x0200 */;
            int nFontHeight = 14;
            int nEffect = 0;
            #endregion

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

            #region Show on Area 1
            nFontHeight = 14;
            if (orderShows.Count > 0)
            {
                pText = Marshal.StringToHGlobalUni(" 37C00000" + "            " + orderShows[0].State1.ToUpper());
            }
            else
            {
                pText = Marshal.StringToHGlobalUni("");
            }
            nEffect = 0;
            int nAreaItemID_2 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_2, pText, nTextColor, 0, nTextStyle,
                pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
            if (nAreaItemID_2 == -1)
            {
                Marshal.FreeHGlobal(pText);
                Marshal.FreeHGlobal(pFontName);
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion

            #region Show on Area 2
            nFontHeight = 14;
            if (orderShows.Count > 1)
            {
                pText = Marshal.StringToHGlobalUni(" 37C00000" + "            " + orderShows[1].State1.ToUpper());
            }
            else
            {
                pText = Marshal.StringToHGlobalUni("");
            }
            nEffect = 0;
            int nAreaItemID_3 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_3, pText, nTextColor, 0, nTextStyle,
                pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
            if (nAreaItemID_3 == -1)
            {
                Marshal.FreeHGlobal(pText);
                Marshal.FreeHGlobal(pFontName);
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion

            #region Show on Area 3
            nFontHeight = 14;
            if (orderShows.Count > 2)
            {
                pText = Marshal.StringToHGlobalUni(" 37C00000" + "            " + orderShows[2].State1.ToUpper());
            }
            else
            {
                pText = Marshal.StringToHGlobalUni("");
            }
            nEffect = 0;
            int nAreaItemID_4 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_4, pText, nTextColor, 0, nTextStyle,
                pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
            if (nAreaItemID_4 == -1)
            {
                Marshal.FreeHGlobal(pText);
                Marshal.FreeHGlobal(pFontName);
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion

            #region Show on Area 4
            nFontHeight = 14;
            if (orderShows.Count > 3)
            {
                pText = Marshal.StringToHGlobalUni(" 37C00000" + "            " + orderShows[3].State1.ToUpper());
            }
            else
            {
                pText = Marshal.StringToHGlobalUni("");
            }
            nEffect = 0;
            int nAreaItemID_5 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_5, pText, nTextColor, 0, nTextStyle,
                pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
            if (nAreaItemID_5 == -1)
            {
                Marshal.FreeHGlobal(pText);
                Marshal.FreeHGlobal(pFontName);
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion

            #region Show on Area 5
            nFontHeight = 14;
            if (orderShows.Count > 4)
            {
                pText = Marshal.StringToHGlobalUni(" 37C00000" + "            " + orderShows[4].State1.ToUpper());
            }
            else
            {
                pText = Marshal.StringToHGlobalUni("");
            }
            nEffect = 0;
            int nAreaItemID_6 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_6, pText, nTextColor, 0, nTextStyle,
                pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
            if (nAreaItemID_6 == -1)
            {
                Marshal.FreeHGlobal(pText);
                Marshal.FreeHGlobal(pFontName);
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion

            #region Show on Area 6
            nFontHeight = 14;
            if (orderShows.Count > 5)
            {
                pText = Marshal.StringToHGlobalUni(" 37C00000" + "            " + orderShows[5].State1.ToUpper());
            }
            else
            {
                pText = Marshal.StringToHGlobalUni("");
            }
            nEffect = 0;
            int nAreaItemID_7 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_7, pText, nTextColor, 0, nTextStyle,
                pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
            if (nAreaItemID_7 == -1)
            {
                Marshal.FreeHGlobal(pText);
                Marshal.FreeHGlobal(pFontName);
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion
            // mới thêm
            #region Show on Area 7
            nFontHeight = 14;
            if (orderShows.Count > 6)
            {
                pText = Marshal.StringToHGlobalUni(" 37C00000" + "            " + orderShows[6].State1.ToUpper());
            }
            else
            {
                pText = Marshal.StringToHGlobalUni("");
            }
            nEffect = 0;
            int nAreaItemID_8 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_8, pText, nTextColor, 0, nTextStyle,
                pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
            if (nAreaItemID_8 == -1)
            {
                Marshal.FreeHGlobal(pText);
                Marshal.FreeHGlobal(pFontName);
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion

            #region Show on Area 8
            nFontHeight = 14;
            if (orderShows.Count > 7)
            {
                pText = Marshal.StringToHGlobalUni(" 37C00000" + "            " + orderShows[7].State1.ToUpper());
            }
            else
            {
                pText = Marshal.StringToHGlobalUni("");
            }
            nEffect = 0;
            int nAreaItemID_9 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_9, pText, nTextColor, 0, nTextStyle,
                pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
            if (nAreaItemID_9 == -1)
            {
                Marshal.FreeHGlobal(pText);
                Marshal.FreeHGlobal(pFontName);
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion

            #region Show on Area 9
            nFontHeight = 14;
            if (orderShows.Count > 8)
            {
                pText = Marshal.StringToHGlobalUni(" 37C00000" + "            " + orderShows[8].State1.ToUpper());
            }
            else
            {
                pText = Marshal.StringToHGlobalUni("");
            }
            nEffect = 0;
            int nAreaItemID_10 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_10, pText, nTextColor, 0, nTextStyle,
                pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
            if (nAreaItemID_10 == -1)
            {
                Marshal.FreeHGlobal(pText);
                Marshal.FreeHGlobal(pFontName);
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
                return;
            }
            #endregion

           

            Marshal.FreeHGlobal(pText);
            Marshal.FreeHGlobal(pFontName);

            // 5. Send to device 
            nRe = CSDKExport.Hd_SendScreen(m_nSendType_LED12, m_pSendParams_LED12, pNULL, pNULL, 0);
            if (nRe != 0)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
            }
        }
        private void SetLED12NoContent()
        {
            try
            {
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

                #region Add Area 0
                int nX1 = 0;
                int nY1 = 40;
                int nAreaWidth = 224;
                int nAreaHeight = 20;

                int nAreaID_1 = CSDKExport.Hd_AddArea(nProgramID, nX1, nY1, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
                if (nAreaID_1 == -1)
                {
                    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                    return;
                }
                #endregion

                #region Add Area 1
                int nX2 = 0;
                int nY2 = 70;


                int nAreaID_2 = CSDKExport.Hd_AddArea(nProgramID, nX2, nY2, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
                if (nAreaID_2 == -1)
                {
                    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                    return;
                }
                #endregion

                #region Add Area 2
                int nX3 = 0;
                int nY3 = 90;

                int nAreaID_3 = CSDKExport.Hd_AddArea(nProgramID, nX3, nY3, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
                if (nAreaID_3 == -1)
                {
                    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                    return;
                }
                #endregion



                #region DÒNG TIÊU ĐỀ
                // 4.Add text AreaItem to Area
                IntPtr pText = Marshal.StringToHGlobalUni("VICEM HOÀNG MAI");
                IntPtr pFontName = Marshal.StringToHGlobalUni("Times New Roman");
                int nTextColor = CSDKExport.Hd_GetColor(255, 255, 255);

                // center in bold and underline
                int nTextStyle = 0x0004 | 0x0100; /*| 0x0200 */
                int nFontHeight = 18;
                int nEffect = 0;
                #endregion

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

                #region Show on Area 1
                nFontHeight = 14;
                pText = Marshal.StringToHGlobalUni("HỆ THỐNG");
                nEffect = 0;
                int nAreaItemID_2 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_2, pText, nTextColor, 0, nTextStyle,
                    pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
                if (nAreaItemID_2 == -1)
                {
                    Marshal.FreeHGlobal(pText);
                    Marshal.FreeHGlobal(pFontName);
                    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                    return;
                }
                #endregion

                #region Show on Area 2
                nFontHeight = 14;
                pText = Marshal.StringToHGlobalUni("XUẤT HÀNG TỰ ĐỘNG");
                nEffect = 0;
                int nAreaItemID_3 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_3, pText, nTextColor, 0, nTextStyle,
                    pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
                if (nAreaItemID_3 == -1)
                {
                    Marshal.FreeHGlobal(pText);
                    Marshal.FreeHGlobal(pFontName);
                    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                    return;
                }
                #endregion



                Marshal.FreeHGlobal(pText);
                Marshal.FreeHGlobal(pFontName);

                // 5. Send to device 
                nRe = CSDKExport.Hd_SendScreen(m_nSendType_LED12, m_pSendParams_LED12, pNULL, pNULL, 0);
                if (nRe != 0)
                {
                    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
