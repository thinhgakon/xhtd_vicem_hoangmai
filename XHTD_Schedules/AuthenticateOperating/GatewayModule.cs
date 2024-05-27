using HMXHTD.Data.DataEntity;
using HMXHTD.Services.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XHTD_Schedules.AuthenticateOperating
{
    public class GatewayModule
    {
        private IntPtr h21 = IntPtr.Zero;
        private static bool InOut21Connected = false;
        private DataTable objTableInOut21 = new DataTable();

        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "Connect")]
        private static extern IntPtr Connect(string Parameters);
        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "PullLastError")]
        private static extern int PullLastError();
        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "GetRTLog")]
        private static extern int GetRTLog(IntPtr h, ref byte buffer, int buffersize);

        private List<string> tmpCardNoIn = new List<string>() { };
        private List<string> tmpCardNoOut = new List<string>() { };

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IStoreOrderOperatingService _storeOrderOperatingService;
        private ILogStoreOrderOperatingService _logStoreOrderOperatingService;
        public GatewayModule(IStoreOrderOperatingService storeOrderOperatingService, ILogStoreOrderOperatingService logStoreOrderOperatingService)
        {
            _storeOrderOperatingService = storeOrderOperatingService;
            _logStoreOrderOperatingService = logStoreOrderOperatingService;
        }

        public void AuthenticateGatewayModule()
        {
            DataColumn objI11 = new DataColumn("CardNo", typeof(string));
            DataColumn objI12 = new DataColumn("VerifyTime", typeof(string));
            objTableInOut21.Columns.Add(objI11);
            objTableInOut21.Columns.Add(objI12);
            while (!InOut21Connected)
            {
                ConnectGatewayModule();
            }
            AuthenticateCardNoInGatewayModule();
        }
        public bool ConnectGatewayModule()
        {
            try
            {
                string str = "protocol=TCP,ipaddress=192.168.22.16,port=4370,timeout=2000,passwd=";  //this.InOut21Config;
                int ret = 0;
                if (IntPtr.Zero == h21)
                {
                    h21 = Connect(str);
                    if (h21 != IntPtr.Zero)
                    {
                        log.Info("--------------connected--------------");
                        InOut21Connected = true;
                    }
                    else
                    {
                        log.Info("--------------connected failed--------------");
                        ret = PullLastError();
                        InOut21Connected = false;
                    }
                }
                return InOut21Connected;
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
                if (InOut21Connected)
                {
                    while (true)
                    {
                        int ret = 0, buffersize = 256;
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
                                    log.Info($@"============================card no================================= {tmp[2]}");
                                }
                                if (tmp[2] == "0" || tmp[2] == "")
                                {
                                    log.Info($@"============================card no không có================================= {tmp[2]}");
                                }
                                else
                                {
                                    if (tmp[3].ToString() == "2")
                                    {
                                        if (tmpCardNoIn.FirstOrDefault(x => x.ToString().Equals(tmp[2].ToString())) != null) return;
                                        tmpCardNoIn.Add(tmp[2].ToString());
                                        
                                        _logStoreOrderOperatingService.InsertLog(tmp[2].ToString(), 4);

                                        // chiều đi vào lấy hàng
                                        //if (this.objBillOrder.setBillOrderInByCardNo(tmp[2].ToString()) > 0)
                                        //{
                                        //    //log storeOrderOperatingLogs
                                        //    storeOrderOperatingLogs.InsertLogByCardNo(tmp[2].ToString());
                                        //    if (IntPtr.Zero != h)
                                        //    {
                                        //        //ControlDevice(h, 1, 1, 1, 1, 0, "");
                                        //        this.sendContentToLED22("MỜI");
                                        //        Thread.Sleep(15000);
                                        //        this.sendContentToLED22("");
                                        // cần giải phóng objTableInOut21
                                        //    }
                                        //}
                                        if (tmpCardNoIn.Count > 5) tmpCardNoIn.RemoveRange(0, 2);
                                    }
                                    else if (tmp[3].ToString() == "3")
                                    {
                                        if (tmpCardNoOut.FirstOrDefault(x => x.ToString().Equals(tmp[2].ToString())) != null) return;
                                        tmpCardNoOut.Add(tmp[2].ToString());
                                        _logStoreOrderOperatingService.InsertLog(tmp[2].ToString(), 8);
                                        // chiều đi ra, sau khi lấy hàng thì qua cổng này để ra
                                        //if (this.objBillOrder.setBillOrderOutByCardNo(tmp[2].ToString()) > 0)
                                        // tạm thời để check luồng xe ra
                                        //if (this.objBillOrder.setBillOrderOutByCardNo_Temp(tmp[2].ToString()) > 0)
                                        //{
                                        //    //log storeOrderOperatingLogs
                                        //    storeOrderOperatingLogs.InsertLogByCardNo(tmp[2].ToString());
                                        //    if (IntPtr.Zero != h)
                                        //    {
                                        //        //ControlDevice(h, 1, 1, 1, 1, 0, "");
                                        //        this.sendContentToLED21("MỜI XE RA");
                                        //        Thread.Sleep(15000);
                                        //        this.sendContentToLED21("");
                                        // cần giải phóng objTableInOut21
                                        //    }
                                        //}
                                        if (tmpCardNoIn.Count > 5) tmpCardNoOut.RemoveRange(0, 2);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    AuthenticateGatewayModule();
                }
            }
            catch (Exception Ex)
            {
                log.Error($@"===================gatewayjob======================= {Ex.StackTrace}");
            }
        }
    }
}
