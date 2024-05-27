using HMXHTD.Services.Services;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using XHTD_Led_Service.LEDControl;

namespace XHTD_Led_Service.Schedules
{
    public class LEDMainTroughControlJob : IJob
    {
        int m_nSendType_LED12;
        IntPtr m_pSendParams_LED12;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public LEDMainTroughControlJob(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            m_nSendType_LED12 = 0;
            string strParams_LED12 = "192.168.21.57";
            m_pSendParams_LED12 = Marshal.StringToHGlobalUni(strParams_LED12);
        }
        public async Task Execute(IJobExecutionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            await Task.Run(() =>
            {
                ShowLEDMainTroughProcess();
            });
        }
        public string GetContent(string vehicle, int? step)
        {
            var content = "";
            if(step == 5)
            {
                content = vehicle.ToUpper() + "          " + "Đang lấy hàng".ToUpper();
            }
            else
            {
                content = vehicle.ToUpper() + "          " + "Chờ lấy hàng".ToUpper();
            }
            return content;
        }
        public void ShowLEDMainTroughProcess()
        {
            if (_serviceFactory.ConfigOperating.GetValueByCode(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name) == 0) return;
            var orderShows = _serviceFactory.StoreOrderOperating.GetStoreOrderForLEDMainTrough();
            if (orderShows.Count <= 1)
            {
                SetLED12NoContent();
                return;
            }
            else
            {
                var vehicleNewList = String.Join(",", orderShows.Select(x => $"{x.Vehicle}").ToList());
                if (vehicleNewList == Program.LedMainTroughPoint) return;
                Program.LedMainTroughPoint = vehicleNewList;
            }

            try
            {
                IntPtr pNULL = new IntPtr(0);

                int nErrorCode = -1;
                // 1. Create a screen
                int nWidth = 704;
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
                #region Cau hinh chung
                // 4.Add text AreaItem to Area
                IntPtr pText = Marshal.StringToHGlobalUni("");
                IntPtr pFontName = Marshal.StringToHGlobalUni("Times New Roman");
                int nTextColor = CSDKExport.Hd_GetColor(255, 255, 255);
                int nTextStyle = 0x0000 | 0x0100 /*| 0x0200 */;
                int nFontHeight = 14;
                int nEffect = 0;
                #endregion
                int plusX = 12;
                int plusY = 3;
                int nAreaWidth = 704 - plusX;
                int nAreaHeight = 16;
                //#region Add Area 1
                //int nX1 = 12;
                //int nY1 = 6;


                //int nAreaID_1 = CSDKExport.Hd_AddArea(nProgramID, nX1, nY1, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
                //if (nAreaID_1 == -1)
                //{
                //    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                //    return;
                //}
                //////Show on Area 1
                //nFontHeight = 14;
                //if (orderShows.Count > 0)
                //{
                //    pText = Marshal.StringToHGlobalUni(GetContent(orderShows[0].Vehicle, orderShows[0].Step));
                //}
                //else
                //{
                //    pText = Marshal.StringToHGlobalUni("");
                //}
                //nEffect = 0;
                //int nAreaItemID_1 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_1, pText, nTextColor, 0, nTextStyle,
                //    pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
                //if (nAreaItemID_1 == -1)
                //{
                //    Marshal.FreeHGlobal(pText);
                //    Marshal.FreeHGlobal(pFontName);
                //    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                //    return;
                //}
                //#endregion



                //#region Add Area 2
                //int nX2 = 0 + plusX;
                //int nY2 = 16 + plusY;


                //int nAreaID_2 = CSDKExport.Hd_AddArea(nProgramID, nX2, nY2, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
                //if (nAreaID_2 == -1)
                //{
                //    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                //    return;
                //}
                ////// Show on Area 2
                //nFontHeight = 14;
                //if (orderShows.Count > 1)
                //{
                //    // pText = Marshal.StringToHGlobalUni("37C00000" + "       " + orderShows[1].State1.ToUpper());
                //    pText = Marshal.StringToHGlobalUni(GetContent(orderShows[1].Vehicle, orderShows[1].Step));
                //}
                //else
                //{
                //    pText = Marshal.StringToHGlobalUni("");
                //}
                //nEffect = 0;
                //int nAreaItemID_2 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_2, pText, nTextColor, 0, nTextStyle,
                //    pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
                //if (nAreaItemID_2 == -1)
                //{
                //    Marshal.FreeHGlobal(pText);
                //    Marshal.FreeHGlobal(pFontName);
                //    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                //    return;
                //}
                //#endregion

                for (int i = 0; i < 9; i++)
                {
                    #region Add Area 
                    int nX0 = 0 + plusX;
                    int nY0 = 17 * i + plusY;


                    int nAreaID_0 = CSDKExport.Hd_AddArea(nProgramID, nX0, nY0, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
                    if (nAreaID_0 == -1)
                    {
                        nErrorCode = CSDKExport.Hd_GetSDKLastError();
                        return;
                    }
                    //// Show on Area
                    nFontHeight = 14;
                    if (orderShows.Count > i)
                    {
                        pText = Marshal.StringToHGlobalUni(GetContent(orderShows[i].Vehicle, orderShows[i].Step));
                    }
                    else
                    {
                        pText = Marshal.StringToHGlobalUni("");
                    }
                    nEffect = 0;
                    int nAreaItemID_0 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_0, pText, nTextColor, 0, nTextStyle,
                        pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
                    if (nAreaItemID_0 == -1)
                    {
                        Marshal.FreeHGlobal(pText);
                        Marshal.FreeHGlobal(pFontName);
                        nErrorCode = CSDKExport.Hd_GetSDKLastError();
                        continue;
                    }
                    #endregion
                }

                for (int i = 9; i < 18; i++)
                {
                    nAreaWidth = 704 - 352;
                    #region Add Area 
                    int nX0 = 340 + plusX;
                    int nY0 = 17 * (i-9) + plusY;


                    int nAreaID_0 = CSDKExport.Hd_AddArea(nProgramID, nX0, nY0, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
                    if (nAreaID_0 == -1)
                    {
                        nErrorCode = CSDKExport.Hd_GetSDKLastError();
                        return;
                    }
                    //// Show on Area
                    nFontHeight = 14;
                    if (orderShows.Count > i)
                    {
                        pText = Marshal.StringToHGlobalUni(GetContent(orderShows[i].Vehicle, orderShows[i].Step));
                    }
                    else
                    {
                        pText = Marshal.StringToHGlobalUni("");
                    }
                    nEffect = 0;
                    int nAreaItemID_0 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_0, pText, nTextColor, 0, nTextStyle,
                        pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
                    if (nAreaItemID_0 == -1)
                    {
                        Marshal.FreeHGlobal(pText);
                        Marshal.FreeHGlobal(pFontName);
                        nErrorCode = CSDKExport.Hd_GetSDKLastError();
                        continue;
                    }
                    #endregion
                }


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
        private void SetLED12NoContent()
        {
            try
            {
                IntPtr pNULL = new IntPtr(0);

                int nErrorCode = -1;
                // 1. Create a screen
                int nWidth = 704;
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
                int nAreaWidth = 704;
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
