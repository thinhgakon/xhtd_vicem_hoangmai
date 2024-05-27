using HMXHTD.Data.DataEntity;
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
    public class ConfirmationPointModule
    {
        private IntPtr h21 = IntPtr.Zero;
        private static bool InOut21Connected = false;
        private DataTable objTableInOut21 = new DataTable();

        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "Connect")]
        private static extern IntPtr Connect(string Parameters);
        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "PullLastError")]
        private static extern int PullLastError();
        [DllImport("plcommpro.dll", EntryPoint = "GetRTLog")]
        private static extern int GetRTLog(IntPtr h, ref byte buffer, int buffersize);

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
                string str = "protocol=TCP,ipaddress=192.168.22.5,port=4370,timeout=2000,passwd=";
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
                                    log.Info($@"============================xác thực cổng 1. card no================================= {tmp[2]}");
                                }
                                if (tmp[2] == "0" || tmp[2] == "")
                                {

                                }
                                else
                                {
                                    bool findItem = false;
                                    for (int j = 0; j < this.objTableInOut21.Rows.Count; j++)
                                    {
                                        if (this.objTableInOut21.Rows[j]["CardNo"].ToString() == tmp[2].ToString())
                                        {
                                            this.objTableInOut21.Rows[j]["VerifyTime"] = tmp[0].ToString();
                                            findItem = true;
                                        }
                                    }
                                    if (!findItem && tmp[3].ToString() == "2")
                                    {
                                        using (var db = new HMXuathangtudong_Entities())
                                        {
                                            var vehicle = db.tblStoreOrderOperatings.FirstOrDefault(x => x.CardNo == tmp[2].ToString());
                                            var newLog = new LogStoreOrderOperating
                                            {
                                                CardNo = tmp[2].ToString(),
                                                Step = 4,
                                                Vehicle = vehicle?.Vehicle,
                                                CreatedOn = DateTime.Now,
                                                ModifiedOn = DateTime.Now
                                            };
                                            db.LogStoreOrderOperatings.Add(newLog);
                                            db.SaveChanges();
                                        }
                                        // chiều đi vào lấy hàng
                                        //this.objTableInOut21.Rows.Add(tmp[2].ToString(), tmp[0].ToString());
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
                                    }
                                    else if (!findItem && tmp[3].ToString() == "3")
                                    {
                                        using (var db = new HMXuathangtudong_Entities())
                                        {
                                            var vehicle = db.tblStoreOrderOperatings.FirstOrDefault(x => x.CardNo == tmp[2].ToString());
                                            var newLog = new LogStoreOrderOperating
                                            {
                                                CardNo = tmp[2].ToString(),
                                                Step = 8,
                                                Vehicle = vehicle?.Vehicle,
                                                CreatedOn = DateTime.Now,
                                                ModifiedOn = DateTime.Now
                                            };
                                            db.LogStoreOrderOperatings.Add(newLog);
                                            db.SaveChanges();
                                        }
                                        // chiều đi ra, sau khi lấy hàng thì qua cổng này để ra
                                        //this.objTableInOut21.Rows.Add(tmp[2].ToString(), tmp[0].ToString());
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
                                    }
                                }
                            }
                            i++;
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
