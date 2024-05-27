using HMXHTD.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace XHTD_Led_Service.LEDControl
{
    public class LEDGetwayBehindControl
    {
        int m_nSendType_LEDFront;
        IntPtr m_pSendParams_LEDFront;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public LEDGetwayBehindControl(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;

            m_nSendType_LEDFront = 0;
            string strParams_LEDFront = "192.168.22.18";
            m_pSendParams_LEDFront = Marshal.StringToHGlobalUni(strParams_LEDFront);
        }
        public void SendLedyBehindAllArea(bool isShowArea1, bool isShowArea2, bool isShowArea3, bool isInviteVehicleComeIn, string contentComeIn)
        {
            // SendLedArea1(); // 96x64
            //128x64
            //SendLedArea3();
            ShowAllProcess(isShowArea1, isShowArea2, isShowArea3, isInviteVehicleComeIn, contentComeIn);
        }
        public void ShowAllProcess(bool isShowArea1, bool isShowArea2, bool isShowArea3, bool isInviteVehicleComeIn, string contentComeIn)
        {
            try
            {
                IntPtr pNULL = new IntPtr(0);

                int nErrorCode = -1;
                int nWidth = 320;
                int nHeight = 64;
                int nColor = 1;
                int nGray = 1;
                int nCardType = 0;

                int nRe = CSDKExport.Hd_CreateScreen(nWidth, nHeight, nColor, nGray, nCardType, pNULL, 0);
                if (nRe != 0)
                {
                    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                    return;
                }

                int nProgramID = CSDKExport.Hd_AddProgram(pNULL, 0, 0, pNULL, 0);
                if (nProgramID == -1)
                {
                    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                    return;
                }

                #region led1
                if (isShowArea1)
                {
                        int nAreaWidth = 96;
                        int nAreaHeight = 16;

                        #region Add Area 0
                        int nAreaHeight0 = 48;
                        int nX1 = 0;
                        int nY1 = 0;

                        int nAreaID_1 = CSDKExport.Hd_AddArea(nProgramID, nX1, nY1, nAreaWidth, nAreaHeight0, pNULL, 0, 0, pNULL, 0);
                        if (nAreaID_1 == -1)
                        {
                            nErrorCode = CSDKExport.Hd_GetSDKLastError();
                            return;
                        }

                        #endregion
                        #region Add Area 2
                        int nX3 = 0;
                        int nY3 = 48;
                        int nAreaID_3 = CSDKExport.Hd_AddArea(nProgramID, nX3, nY3, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
                        if (nAreaID_3 == -1)
                        {
                            nErrorCode = CSDKExport.Hd_GetSDKLastError();
                            return;
                        }
                        #endregion


                        #region DÒNG TIÊU ĐỀ
                        // 4.Add text AreaItem to Area
                        IntPtr pText = Marshal.StringToHGlobalUni("Cổng xe ra vào\n   nhập vật tư".ToUpper());
                        IntPtr pFontName = Marshal.StringToHGlobalUni("Times New Roman");
                        int nTextColor = CSDKExport.Hd_GetColor(255, 255, 255);

                        // center in bold and underline
                        int nTextStyle = 0x0004 | 0x0100; /*| 0x0200 */
                        int nFontHeight = 10;
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


                        #region Show on Area 2
                        nFontHeight = 14;
                        pText = Marshal.StringToHGlobalUni("<======");
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
                }
                #endregion led1

                #region led2

                if (isShowArea2)
                {
                    int nAreaWidth_Main = 128;

                    #region Add Area 0
                    int nAreaHeight0_Main = 28;
                    int nX1_Main = 96;
                    int nY1_Main = 0;

                    int nAreaID_1_Main = CSDKExport.Hd_AddArea(nProgramID, nX1_Main, nY1_Main, nAreaWidth_Main, nAreaHeight0_Main, pNULL, 0, 0, pNULL, 0);
                    if (nAreaID_1_Main == -1)
                    {
                        nErrorCode = CSDKExport.Hd_GetSDKLastError();
                        return;
                    }
                    #endregion

                    #region Add Area 2
                    int nAreaHeight2_Main = 28;
                    int nX3_Main = 100;
                    int nY3_Main = 25;
                    int nAreaID_3_Main = CSDKExport.Hd_AddArea(nProgramID, nX3_Main, nY3_Main, nAreaWidth_Main, nAreaHeight2_Main, pNULL, 0, 0, pNULL, 0);
                    if (nAreaID_3_Main == -1)
                    {
                        nErrorCode = CSDKExport.Hd_GetSDKLastError();
                        return;
                    }
                    #endregion


                    #region DÒNG TIÊU ĐỀ
                    // 4.Add text AreaItem to Area
                    IntPtr pText_Main = Marshal.StringToHGlobalUni("VICEM HOÀNG MAI");
                    IntPtr pFontName_Main = Marshal.StringToHGlobalUni("Times New Roman");
                    int nTextColor_Main = CSDKExport.Hd_GetColor(255, 255, 255);

                    // center in bold and underline
                    int nTextStyle_Main = 0x0004 | 0x0100; /*| 0x0200 */
                    int nFontHeight_Main = 12;
                    int nEffect_Main = 0;
                    #endregion

                    #region Show on Area 0
                    int nAreaItemID_1_Main = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_1_Main, pText_Main, nTextColor_Main, 0, nTextStyle_Main,
                        pFontName_Main, nFontHeight_Main, nEffect_Main, 30, 201, 3, pNULL, 0);
                    if (nAreaItemID_1_Main == -1)
                    {
                        Marshal.FreeHGlobal(pText_Main);
                        Marshal.FreeHGlobal(pFontName_Main);
                        nErrorCode = CSDKExport.Hd_GetSDKLastError();
                        return;
                    }
                    #endregion

                    #region Show on Area 2
                    nFontHeight_Main = 10;
                    pText_Main = Marshal.StringToHGlobalUni("   Bảo vệ\ncổng số 3".ToUpper());
                    nEffect_Main = 0;
                    int nAreaItemID_3_Main = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_3_Main, pText_Main, nTextColor_Main, 0, nTextStyle_Main,
                        pFontName_Main, nFontHeight_Main, nEffect_Main, 30, 201, 3, pNULL, 0);

                    if (nAreaItemID_3_Main == -1)
                    {
                        Marshal.FreeHGlobal(pText_Main);
                        Marshal.FreeHGlobal(pFontName_Main);
                        nErrorCode = CSDKExport.Hd_GetSDKLastError();
                        return;
                    }
                    #endregion
                }

                #endregion led2

                #region led3

                if (isShowArea3)
                {
                    if (isInviteVehicleComeIn)
                    {
                        int nAreaWidth_3 = 96;
                        int nAreaHeight_3 = 16;
                        #region Add Area 0
                        int nAreaHeight0_3 = 48;
                        int nX1_3 = 224;
                        int nY1_3 = 5;

                        int nAreaID_1_3 = CSDKExport.Hd_AddArea(nProgramID, nX1_3, nY1_3, nAreaWidth_3, nAreaHeight0_3, pNULL, 0, 0, pNULL, 0);
                        if (nAreaID_1_3 == -1)
                        {
                            nErrorCode = CSDKExport.Hd_GetSDKLastError();
                            return;
                        }
                        #endregion

                        #region Add Area 2
                        int nX3_3 = 224;
                        int nY3_3 = 48;
                        int nAreaID_3_3 = CSDKExport.Hd_AddArea(nProgramID, nX3_3, nY3_3, nAreaWidth_3, nAreaHeight_3, pNULL, 0, 0, pNULL, 0);
                        if (nAreaID_3_3 == -1)
                        {
                            nErrorCode = CSDKExport.Hd_GetSDKLastError();
                            return;
                        }
                        #endregion


                        #region DÒNG TIÊU ĐỀ
                        // 4.Add text AreaItem to Area
                        IntPtr pText_3 = Marshal.StringToHGlobalUni(($"  Mời xe\n  {contentComeIn} \nra cổng").ToUpper());
                        IntPtr pFontName_3 = Marshal.StringToHGlobalUni("Times New Roman");
                        int nTextColor_3 = CSDKExport.Hd_GetColor(255, 255, 255);

                        // center in bold and underline
                        int nTextStyle_3 = 0x0004 | 0x0100; /*| 0x0200 */
                        int nFontHeight_3 = 10;
                        int nEffect_3 = 10;
                        #endregion

                        #region Show on Area 0
                        int nAreaItemID_1_3 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID: nAreaID_3_3, pText: pText_3, nTextColor: nTextColor_3, nBackGroupColor: 0, nStyle: nTextStyle_3,
                            pFontName: pFontName_3, nFontHeight: nFontHeight_3, nShowEffect: nEffect_3, nShowSpeed: 10, nClearType: 0, nStayTime: 3, pExParamsBuf: pNULL, nBufSize: 0);
                        if (nAreaItemID_1_3 == -1)
                        {
                            Marshal.FreeHGlobal(pText_3);
                            Marshal.FreeHGlobal(pFontName_3);
                            nErrorCode = CSDKExport.Hd_GetSDKLastError();
                            return;
                        }
                        #endregion

                        #region Show on Area 2
                        nFontHeight_3 = 14;
                        pText_3 = Marshal.StringToHGlobalUni("======>");
                        nEffect_3 = 0;
                        int nAreaItemID_3_3 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_3_3, pText_3, nTextColor_3, 0, nTextStyle_3,
                            pFontName_3, nFontHeight_3, nEffect_3, 30, 201, 3, pNULL, 0);
                        if (nAreaItemID_3_3 == -1)
                        {
                            Marshal.FreeHGlobal(pText_3);
                            Marshal.FreeHGlobal(pFontName_3);
                            nErrorCode = CSDKExport.Hd_GetSDKLastError();
                            return;
                        }
                        #endregion
                    }
                    else
                    {
                        int nAreaWidth_3 = 96;
                        int nAreaHeight_3 = 16;
                        #region Add Area 0
                        int nAreaHeight0_3 = 48;
                        int nX1_3 = 224;
                        int nY1_3 = 5;

                        int nAreaID_1_3 = CSDKExport.Hd_AddArea(nProgramID, nX1_3, nY1_3, nAreaWidth_3, nAreaHeight0_3, pNULL, 0, 0, pNULL, 0);
                        if (nAreaID_1_3 == -1)
                        {
                            nErrorCode = CSDKExport.Hd_GetSDKLastError();
                            return;
                        }
                        #endregion

                        #region Add Area 2
                        int nX3_3 = 224;
                        int nY3_3 = 48;
                        int nAreaID_3_3 = CSDKExport.Hd_AddArea(nProgramID, nX3_3, nY3_3, nAreaWidth_3, nAreaHeight_3, pNULL, 0, 0, pNULL, 0);
                        if (nAreaID_3_3 == -1)
                        {
                            nErrorCode = CSDKExport.Hd_GetSDKLastError();
                            return;
                        }
                        #endregion


                        #region DÒNG TIÊU ĐỀ
                        // 4.Add text AreaItem to Area
                        IntPtr pText_3 = Marshal.StringToHGlobalUni("Cổng xe ra vào\n  nhận xi măng\n      Clinker".ToUpper());
                        IntPtr pFontName_3 = Marshal.StringToHGlobalUni("Times New Roman");
                        int nTextColor_3 = CSDKExport.Hd_GetColor(255, 255, 255);

                        // center in bold and underline
                        int nTextStyle_3 = 0x0004 | 0x0100; /*| 0x0200 */
                        int nFontHeight_3 = 10;
                        int nEffect_3 = 0;
                        #endregion

                        #region Show on Area 0
                        int nAreaItemID_1_3 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_1_3, pText_3, nTextColor_3, 0, nTextStyle_3,
                            pFontName_3, nFontHeight_3, nEffect_3, 30, 201, 3, pNULL, 0);
                        if (nAreaItemID_1_3 == -1)
                        {
                            Marshal.FreeHGlobal(pText_3);
                            Marshal.FreeHGlobal(pFontName_3);
                            nErrorCode = CSDKExport.Hd_GetSDKLastError();
                            return;
                        }
                        #endregion

                        #region Show on Area 2
                        nFontHeight_3 = 14;
                        pText_3 = Marshal.StringToHGlobalUni("======>");
                        nEffect_3 = 0;
                        int nAreaItemID_3_3 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_3_3, pText_3, nTextColor_3, 0, nTextStyle_3,
                            pFontName_3, nFontHeight_3, nEffect_3, 30, 201, 3, pNULL, 0);
                        if (nAreaItemID_3_3 == -1)
                        {
                            Marshal.FreeHGlobal(pText_3);
                            Marshal.FreeHGlobal(pFontName_3);
                            nErrorCode = CSDKExport.Hd_GetSDKLastError();
                            return;
                        }
                        #endregion
                    }



                }

                #endregion led3

                nRe = CSDKExport.Hd_SendScreen(m_nSendType_LEDFront, m_pSendParams_LEDFront, pNULL, pNULL, 0);
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
        public void SendLedArea1()
        {
            try
            {
                IntPtr pNULL = new IntPtr(0);

                int nErrorCode = -1;
                // 1. Create a screen
                int nWidth = 320;
                int nHeight = 64;
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
                int nAreaWidth = 96;
                int nAreaHeight = 16;
                #region Add Area 0
                int nAreaHeight0 = 48;
                int nX1 = 0;
                int nY1 = 0;

                int nAreaID_1 = CSDKExport.Hd_AddArea(nProgramID, nX1, nY1, nAreaWidth, nAreaHeight0, pNULL, 0, 0, pNULL, 0);
                if (nAreaID_1 == -1)
                {
                    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                    return;
                }
                #endregion
                #region Add Area 2
                int nX3 = 0;
                int nY3 = 48;
                int nAreaID_3 = CSDKExport.Hd_AddArea(nProgramID, nX3, nY3, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
                if (nAreaID_3 == -1)
                {
                    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                    return;
                }
                #endregion


                #region DÒNG TIÊU ĐỀ
                // 4.Add text AreaItem to Area
                IntPtr pText = Marshal.StringToHGlobalUni("Cổng xe ra vào\n  nhận xi măng\n      Clinker".ToUpper());
                IntPtr pFontName = Marshal.StringToHGlobalUni("Times New Roman");
                int nTextColor = CSDKExport.Hd_GetColor(255, 255, 255);

                // center in bold and underline
                int nTextStyle = 0x0004 | 0x0100; /*| 0x0200 */
                int nFontHeight = 10;
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


                #region Show on Area 2
                nFontHeight = 14;
                pText = Marshal.StringToHGlobalUni("<==========");
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




                #region 3



                #endregion 3


                // 5. Send to device 
                nRe = CSDKExport.Hd_SendScreen(m_nSendType_LEDFront, m_pSendParams_LEDFront, pNULL, pNULL, 0);
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
        public void SendLedArea3()
        {
            try
            {
                IntPtr pNULL = new IntPtr(0);

                int nErrorCode_3 = -1;
                // 1. Create a screen
                int nWidth_3 = 320;
                int nHeight_3 = 64;
                int nColor_3 = 1;
                int nGray_3 = 1;
                int nCardType_3 = 0;

                int nRe_3 = CSDKExport.Hd_CreateScreen(nWidth_3, nHeight_3, nColor_3, nGray_3, nCardType_3, pNULL, 0);
                if (nRe_3 != 0)
                {
                    nErrorCode_3 = CSDKExport.Hd_GetSDKLastError();
                    return;
                }

                // 2. Add program to screen
                int nProgramID_3 = CSDKExport.Hd_AddProgram(pNULL, 0, 0, pNULL, 0);
                if (nProgramID_3 == -1)
                {
                    nErrorCode_3 = CSDKExport.Hd_GetSDKLastError();
                    return;
                }

                int nAreaWidth_3 = 96;
                int nAreaHeight_3 = 16;
                #region Add Area 0
                int nAreaHeight0_3 = 32;
                int nX1 = 224;
                int nY1 = 5;

                int nAreaID_1_3 = CSDKExport.Hd_AddArea(nProgramID_3, nX1, nY1, nAreaWidth_3, nAreaHeight0_3, pNULL, 0, 0, pNULL, 0);
                if (nAreaID_1_3 == -1)
                {
                    nErrorCode_3 = CSDKExport.Hd_GetSDKLastError();
                    return;
                }
                #endregion

                #region Add Area 2
                int nX3_3 = 224;
                int nY3_3 = 40;
                int nAreaID_3_3 = CSDKExport.Hd_AddArea(nProgramID_3, nX3_3, nY3_3, nAreaWidth_3, nAreaHeight_3, pNULL, 0, 0, pNULL, 0);
                if (nAreaID_3_3 == -1)
                {
                    nErrorCode_3 = CSDKExport.Hd_GetSDKLastError();
                    return;
                }
                #endregion


                #region DÒNG TIÊU ĐỀ
                // 4.Add text AreaItem to Area
                IntPtr pText_3 = Marshal.StringToHGlobalUni("Cổng xe ra vào nhập     vật tư");
                IntPtr pFontName_3 = Marshal.StringToHGlobalUni("Times New Roman");
                int nTextColor_3 = CSDKExport.Hd_GetColor(255, 255, 255);

                // center in bold and underline
                int nTextStyle_3 = 0x0004 | 0x0100; /*| 0x0200 */
                int nFontHeight_3 = 10;
                int nEffect_3 = 0;
                #endregion

                #region Show on Area 0
                int nAreaItemID_1_3 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_1_3, pText_3, nTextColor_3, 0, nTextStyle_3,
                    pFontName_3, nFontHeight_3, nEffect_3, 30, 201, 3, pNULL, 0);
                if (nAreaItemID_1_3 == -1)
                {
                    Marshal.FreeHGlobal(pText_3);
                    Marshal.FreeHGlobal(pFontName_3);
                    nErrorCode_3 = CSDKExport.Hd_GetSDKLastError();
                    return;
                }
                #endregion

                #region Show on Area 2
                nFontHeight_3 = 14;
                pText_3 = Marshal.StringToHGlobalUni("==========>");
                nEffect_3 = 0;
                int nAreaItemID_3_3 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_3_3, pText_3, nTextColor_3, 0, nTextStyle_3,
                    pFontName_3, nFontHeight_3, nEffect_3, 30, 201, 3, pNULL, 0);
                if (nAreaItemID_3_3 == -1)
                {
                    Marshal.FreeHGlobal(pText_3);
                    Marshal.FreeHGlobal(pFontName_3);
                    nErrorCode_3 = CSDKExport.Hd_GetSDKLastError();
                    return;
                }
                #endregion


                // 5. Send to device 
                //nRe = CSDKExport.Hd_SendScreen(m_nSendType_LEDFront, m_pSendParams_LEDFront, pNULL, pNULL, 0);
                //if (nRe != 0)
                //{
                //    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                //}
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
    }
}
