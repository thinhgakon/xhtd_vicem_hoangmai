using HMXHTD.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace HMXHTD
{
    public partial class frmMainControl : Form
    {

        #region declare objects

        private string LinkAPI_WebSale = "http://api.ximanghoangmai.vn:8099";
        private string userNameAPI = "mobifone";
        private string passwordAPI = "mobi@A123456";
        private string strToken;
        private Thread threadBillOrder;
        private Customer objCustomer = new Customer();
        private StoreOrderOperatingLogs storeOrderOperatingLogs = new StoreOrderOperatingLogs();

        private BillOrder objBillOrder = new BillOrder();
        private DataTable BillOrderRelease = new DataTable();
        private Thread threadVoice1;
        private Thread threadLED12;
        private Trough objTrough = new Trough();
        private Fun objFun = new Fun();
        private HMXHTD.Core.Device objDevice = new Core.Device();
        private IntPtr h = IntPtr.Zero;

        //Xác thực cổng bảo vệ số 1, RFID11
        private Thread threadConfirm;
        private string ConfirmConfig = "";
        public bool Confirm = true;
        int TimeSleepConfirm = 1000;
        private DataTable objTableConfirm = new DataTable();

        //Xác thực cổng bảo vệ số 3, vào cổng - RFID21
        private Thread threadInOut21;
        private string InOut21Config = "";
        public bool InOut21 = true;
        int TimeSleepInOut21 = 2000;
        private DataTable objTableInOut21 = new DataTable();
        private IntPtr h21 = IntPtr.Zero;

        //Xác thực cổng bảo vệ số 3, ra cổng - RFID22
        private Thread threadInOut22;
        private string InOut22Config = "";
        public bool InOut22 = true;
        int TimeSleepInOut22 = 2000;
        private DataTable objTableInOut22 = new DataTable();

        //Cân vào, cân số 1 - RFID31
        private Thread threadScale31;
        private string Scale31Config = "";
        public bool Scale31 = true;
        int TimeSleepScale31 = 3000;
        private DataTable objTableScale31 = new DataTable();

        //Cân vào, cân số 1 - RFID32
        private Thread threadScale32;
        private string Scale32Config = "";
        public bool Scale32 = true;
        int TimeSleepScale32 = 4000;
        private DataTable objTableScale32 = new DataTable();

        //Cân vào, cân số 2 - RFID33
        private Thread threadScale33;
        private string Scale33Config = "";
        public bool Scale33 = true;
        int TimeSleepScale33 = 3000;
        private DataTable objTableScale33 = new DataTable();

        //Cân vào, cân số 2 - RFID33
        private Thread threadScale34;
        private string Scale34Config = "";
        public bool Scale34 = true;
        int TimeSleepScale34 = 4000;
        private DataTable objTableScale34 = new DataTable();

        //Điều khiển bảng LED
        int m_nSendType_LED11;
        IntPtr m_pSendParams_LED11;

        int m_nSendType_LED12;
        IntPtr m_pSendParams_LED12;

        int m_nSendType_LED21;
        IntPtr m_pSendParams_LED21;

        int m_nSendType_LED22;
        IntPtr m_pSendParams_LED22;

        //Điều khiển Barie
        [DllImport("plcommpro.dll", EntryPoint = "ControlDevice")]
        public static extern int ControlDevice(IntPtr h, int operationid, int param1, int param2, int param3, int param4, string options);
        #endregion

        #region Declare Connect
        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "Connect")]
        public static extern IntPtr Connect(string Parameters);
        [DllImport("plcommpro.dll", EntryPoint = "PullLastError")]
        public static extern int PullLastError();
        #endregion

        #region Declare GetRTLog
        [DllImport("plcommpro.dll", EntryPoint = "GetRTLog")]
        public static extern int GetRTLog(IntPtr h, ref byte buffer, int buffersize);
        #endregion

        #region Declare Disconnect
        [DllImport("plcommpro.dll", EntryPoint = "Disconnect")]
        public static extern void Disconnect(IntPtr h);
        #endregion

        #region method frmMainControl
        public frmMainControl()
        {
            InitializeComponent();

            if (frmMain.SysOperating)
            {
                //this.strToken = this.getToken();

                #region Declare LED
                m_nSendType_LED11 = 0;
                //string strParams_LED11 = this.objDevice.getIpAddressByCode("LED11"); //"192.168.2.200";
                string strParams_LED11 = "192.168.22.6";
                m_pSendParams_LED11 = Marshal.StringToHGlobalUni(strParams_LED11);

                m_nSendType_LED12 = 0;
                //string strParams_LED12 = this.objDevice.getIpAddressByCode("LED12"); //"192.168.2.200";
                string strParams_LED12 = "192.168.22.19";
                m_pSendParams_LED12 = Marshal.StringToHGlobalUni(strParams_LED12);

                m_nSendType_LED21 = 0;
                //string strParams_LED21 = this.objDevice.getIpAddressByCode("LED21"); //"192.168.2.200";
                string strParams_LED21 = "192.168.22.17";
                m_pSendParams_LED21 = Marshal.StringToHGlobalUni(strParams_LED21);

                m_nSendType_LED22 = 0;
                //string strParams_LED22 = this.objDevice.getIpAddressByCode("LED22"); //"192.168.2.200";
                string strParams_LED22 = "192.168.22.18";
                m_pSendParams_LED22 = Marshal.StringToHGlobalUni(strParams_LED22);

                #endregion

                #region DataTable Confirm
                DataColumn objC1 = new DataColumn("CardNo", typeof(string));
                DataColumn objC2 = new DataColumn("VerifyTime", typeof(string));
                this.objTableConfirm.Columns.Add(objC1);
                this.objTableConfirm.Columns.Add(objC2);
                #endregion

                #region DataTable objTableInOut21
                DataColumn objI11 = new DataColumn("CardNo", typeof(string));
                DataColumn objI12 = new DataColumn("VerifyTime", typeof(string));
                this.objTableInOut21.Columns.Add(objI11);
                this.objTableInOut21.Columns.Add(objI12);
                #endregion

                #region DataTable objTableInOut22
                DataColumn objI21 = new DataColumn("CardNo", typeof(string));
                DataColumn objI22 = new DataColumn("VerifyTime", typeof(string));
                this.objTableInOut22.Columns.Add(objI21);
                this.objTableInOut22.Columns.Add(objI22);
                #endregion

                #region Devices Config
                ConfirmConfig = this.objDevice.getConfigByCode("RFID11");
                InOut21Config = this.objDevice.getConfigByCode("RFID21");
                InOut22Config = this.objDevice.getConfigByCode("RFID22");
                Scale31Config = this.objDevice.getConfigByCode("RFID31");
                Scale32Config = this.objDevice.getConfigByCode("RFID32");
                Scale33Config = this.objDevice.getConfigByCode("RFID33");
                Scale34Config = this.objDevice.getConfigByCode("RFID34");
                #endregion
            }
        }
        #endregion

        #region method ~frmMainControl
        ~frmMainControl()
        {
            Marshal.FreeHGlobal(m_pSendParams_LED11);
        }
        #endregion

        #region method frmMainControl_Shown
        private void frmMainControl_Shown(object sender, EventArgs e)
        {
            //#region get token
            //this.strToken = this.getToken();
            //#endregion

            #region Module Điểm xác nhận

            //#region Kết nối thiết bị
            //bool ConfirmConnected = false;
            //try
            //{
            //    this.lblDevice1.Visible = false;
            //    string str = "protocol=TCP,ipaddress=192.168.22.5,port=4370,timeout=2000,passwd=";  //this.ConfirmConfig;
            //    int ret = 0; // Error ID number
            //    if (IntPtr.Zero == h)
            //    {
            //        h = Connect(str);
            //        if (h != IntPtr.Zero)
            //        {
            //            this.lblDevice1.Visible = true;
            //            ConfirmConnected = true;
            //        }
            //        else
            //        {
            //            ret = PullLastError();
            //            this.lblDevice1.Visible = false;
            //            ConfirmConnected = false;
            //        }
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    MessageBox.Show(Ex.Message);
            //}
            //#endregion

            //#region Lấy tín hiệu
            //try
            //{
            //    if (ConfirmConnected)
            //    {
            //        this.Confirm = true;
            //        threadConfirm = new Thread(t =>
            //        {
            //            while (this.Confirm)
            //            {
            //                this.Invoke((MethodInvoker)delegate
            //                {
            //                    int ret = 0, i = 0, buffersize = 256;
            //                    string str = "";
            //                    string[] tmp = null;
            //                    byte[] buffer = new byte[256];
            //                    if (IntPtr.Zero != h)
            //                    {
            //                        ret = GetRTLog(h, ref buffer[0], buffersize);
            //                        if (ret >= 0)
            //                        {
            //                            str = Encoding.Default.GetString(buffer);
            //                            tmp = str.Split(',');
            //                            if(tmp[2] != "0")
            //                            {
            //                                var test = "ok";
            //                            }
            //                            if (tmp[2] == "0" || tmp[2] == "")
            //                            {
            //                                this.lblDiemxacthuc.Visible = false;
            //                                this.lblDiemxacthuc.Refresh();
            //                            }
            //                            else
            //                            {
            //                                this.lblDiemxacthuc.Visible = true;
            //                                this.lblDiemxacthuc.Refresh();
            //                                bool findItem = false;
            //                                for (int j = 0; j < this.objTableConfirm.Rows.Count; j++)
            //                                {
            //                                    if (this.objTableConfirm.Rows[j]["CardNo"].ToString() == tmp[2].ToString())
            //                                    {
            //                                        this.objTableConfirm.Rows[j]["VerifyTime"] = tmp[0].ToString();
            //                                        findItem = true;
            //                                    }
            //                                }
            //                                if (!findItem)
            //                                {
            //                                    this.objTableConfirm.Rows.Add(tmp[2].ToString(), tmp[0].ToString());
            //                                    if (this.objBillOrder.setBillOrderConfirmByCardNo(tmp[2].ToString()) > 0)
            //                                    {
            //                                        this.sendContentToLED11("ĐÃ");
            //                                        Thread.Sleep(15000);
            //                                        this.sendContentToLED11("");
            //                                    }
            //                                }
            //                                this.dgvDataConfirm.AutoGenerateColumns = false;
            //                                this.dgvDataConfirm.DataSource = this.objTableConfirm;
            //                            }
            //                        }
            //                        i++;
            //                    }
            //                    else
            //                    {
            //                        MessageBox.Show("Connect device failed!");
            //                        return;
            //                    }

            //                });

            //                Thread.Sleep(this.TimeSleepConfirm);
            //            }
            //        })
            //        { IsBackground = true };
            //        threadConfirm.Start();
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    MessageBox.Show(Ex.Message);
            //}
            //#endregion

            #endregion

            #region Module Cổng Vào

            //#region Kết nối thiết bị
            //bool InOut21Connected = false;
            //try
            //{
            //    this.lblDevice21.Visible = false;
            //    string str = "protocol=TCP,ipaddress=192.168.22.16,port=4370,timeout=2000,passwd=";  //this.InOut21Config;
            //    int ret = 0; // Error ID number
            //    if (IntPtr.Zero == h21)
            //    {
            //        h21 = Connect(str);
            //        if (h21 != IntPtr.Zero)
            //        {
            //            this.lblDevice21.Visible = true;
            //            InOut21Connected = true;
            //        }
            //        else
            //        {
            //            ret = PullLastError();
            //            this.lblDevice21.Visible = false;
            //            InOut21Connected = false;
            //        }
            //    }
            //}
            //catch
            //{
            //}
            //#endregion

            //#region Lấy tín hiệu
            //try
            //{
            //    if (InOut21Connected)
            //    {
            //        this.InOut21 = true;
            //        threadInOut21 = new Thread(t =>
            //        {
            //            while (this.InOut21)
            //            {
            //                this.Invoke((MethodInvoker)delegate
            //                {
            //                    this.lblInOut1.Visible = !this.lblInOut1.Visible;
            //                    int ret = 0, i = 0, buffersize = 256;
            //                    string str = "";
            //                    string[] tmp = null;
            //                    byte[] buffer = new byte[256];
            //                    if (IntPtr.Zero != h21)
            //                    {
            //                        ret = GetRTLog(h21, ref buffer[0], buffersize);
            //                        if (ret >= 0)
            //                        {
            //                            str = Encoding.Default.GetString(buffer);
            //                            tmp = str.Split(',');

            //                            if (tmp[2] != "0")
            //                            {
            //                                var ok = "vao";
            //                            }
            //                            if (tmp[2] == "0" || tmp[2] == "")
            //                            {
            //                                this.lblInOut1.Visible = false;
            //                                this.lblInOut1.Refresh();
            //                            }
            //                            else
            //                            {
            //                                this.lblInOut1.Visible = true;
            //                                this.lblInOut1.Refresh();
            //                                bool findItem = false;
            //                                for (int j = 0; j < this.objTableInOut21.Rows.Count; j++)
            //                                {
            //                                    if (this.objTableInOut21.Rows[j]["CardNo"].ToString() == tmp[2].ToString())
            //                                    {
            //                                        this.objTableInOut21.Rows[j]["VerifyTime"] = tmp[0].ToString();
            //                                        findItem = true;
            //                                    }
            //                                }
            //                                if (!findItem && tmp[3].ToString() == "2")
            //                                {
            //                                    // chiều đi vào lấy hàng
            //                                    this.objTableInOut21.Rows.Add(tmp[2].ToString(), tmp[0].ToString());
            //                                    if (this.objBillOrder.setBillOrderInByCardNo(tmp[2].ToString()) > 0)
            //                                    {
            //                                        //log storeOrderOperatingLogs
            //                                        storeOrderOperatingLogs.InsertLogByCardNo(tmp[2].ToString());
            //                                        if (IntPtr.Zero != h)
            //                                        {
            //                                            //ControlDevice(h, 1, 1, 1, 1, 0, "");
            //                                            this.sendContentToLED22("MỜI");
            //                                            Thread.Sleep(15000);
            //                                            this.sendContentToLED22("");
            //                                        }
            //                                    }
            //                                }
            //                                else if (!findItem && tmp[3].ToString() == "3")
            //                                {
            //                                    // chiều đi ra, sau khi lấy hàng thì qua cổng này để ra
            //                                    this.objTableInOut21.Rows.Add(tmp[2].ToString(), tmp[0].ToString());
            //                                    //if (this.objBillOrder.setBillOrderOutByCardNo(tmp[2].ToString()) > 0)
            //                                    // tạm thời để check luồng xe ra
            //                                    if (this.objBillOrder.setBillOrderOutByCardNo_Temp(tmp[2].ToString()) > 0)
            //                                    {
            //                                        //log storeOrderOperatingLogs
            //                                        storeOrderOperatingLogs.InsertLogByCardNo(tmp[2].ToString());
            //                                        if (IntPtr.Zero != h)
            //                                        {
            //                                            //ControlDevice(h, 1, 1, 1, 1, 0, "");
            //                                            this.sendContentToLED21("MỜI XE RA");
            //                                            Thread.Sleep(15000);
            //                                            this.sendContentToLED21("");
            //                                        }
            //                                    }
            //                                }
            //                                this.dgvDataInOut21.AutoGenerateColumns = false;
            //                                this.dgvDataInOut21.DataSource = this.objTableInOut21;
            //                            }
            //                        }
            //                        i++;
            //                    }
            //                    else
            //                    {
            //                        MessageBox.Show("Connect device failed!");
            //                        return;
            //                    }

            //                });

            //                Thread.Sleep(this.TimeSleepInOut21);
            //            }
            //        })
            //        { IsBackground = true };
            //        threadInOut21.Start();
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    MessageBox.Show(Ex.Message);
            //}
            //#endregion

            #endregion

            #region Module Cổng Ra

            //#region Kết nối thiết bị
            //bool InOut22Connected = false;
            //try
            //{
            //    this.lblDevice22.Visible = false;
            //    string str = this.InOut22Config;
            //    int ret = 0; // Error ID number
            //    if (IntPtr.Zero == h)
            //    {
            //        h = Connect(str);
            //        if (h != IntPtr.Zero)
            //        {
            //            this.lblDevice22.Visible = true;
            //            InOut22Connected = true;
            //        }
            //        else
            //        {
            //            ret = PullLastError();
            //            this.lblDevice22.Visible = false;
            //            InOut22Connected = false;
            //        }
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    MessageBox.Show(Ex.Message);
            //}
            //#endregion

            //#region Lấy tín hiệu
            //try
            //{
            //    if (InOut22Connected)
            //    {
            //        this.InOut22 = true;
            //        threadInOut22 = new Thread(t =>
            //        {
            //            while (this.InOut22)
            //            {
            //                this.Invoke((MethodInvoker)delegate
            //                {
            //                    this.lblInOut1.Visible = !this.lblInOut1.Visible;
            //                    int ret = 0, i = 0, buffersize = 256;
            //                    string str = "";
            //                    string[] tmp = null;
            //                    byte[] buffer = new byte[256];
            //                    if (IntPtr.Zero != h)
            //                    {
            //                        ret = GetRTLog(h, ref buffer[0], buffersize);
            //                        if (ret >= 0)
            //                        {
            //                            str = Encoding.Default.GetString(buffer);
            //                            tmp = str.Split(',');
            //                            if (tmp[2] == "0" || tmp[2] == "")
            //                            {
            //                                this.lblInOut1.Visible = false;
            //                                this.lblInOut1.Refresh();
            //                            }
            //                            else
            //                            {
            //                                this.lblInOut1.Visible = true;
            //                                this.lblInOut1.Refresh();
            //                                bool findItem = false;
            //                                for (int j = 0; j < this.objTableInOut22.Rows.Count; j++)
            //                                {
            //                                    if (this.objTableInOut22.Rows[j]["CardNo"].ToString() == tmp[2].ToString())
            //                                    {
            //                                        this.objTableInOut22.Rows[j]["VerifyTime"] = tmp[0].ToString();
            //                                        findItem = true;
            //                                    }
            //                                }
            //                                if (!findItem)
            //                                {
            //                                    this.objTableInOut22.Rows.Add(tmp[2].ToString(), tmp[0].ToString());
            //                                    if (this.objBillOrder.setBillOrderOutByCardNo(tmp[2].ToString()) > 0)
            //                                    {
            //                                        if (IntPtr.Zero != h)
            //                                        {
            //                                            ControlDevice(h, 1, 1, 1, 1, 0, "");
            //                                            this.sendContentToLED22("MỜI XE QUA");
            //                                            Thread.Sleep(5000);
            //                                            this.sendContentToLED22("");
            //                                        }
            //                                    }
            //                                }
            //                                this.dgvDataInOut22.AutoGenerateColumns = false;
            //                                this.dgvDataInOut22.DataSource = this.objTableInOut22;
            //                            }
            //                        }
            //                        i++;
            //                    }
            //                    else
            //                    {
            //                        MessageBox.Show("Connect device failed!");
            //                        return;
            //                    }

            //                });

            //                Thread.Sleep(this.TimeSleepInOut22);
            //            }
            //        }) { IsBackground = true };
            //        threadInOut22.Start();
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    MessageBox.Show(Ex.Message);
            //}
            //#endregion

            #endregion

            #region Module Cân số 1

            //#region Kết nối thiết bị
            //bool Scale31Connected = false;
            //try
            //{
            //    string str = this.Scale31Config;
            //    int ret = 0; // Error ID number
            //    if (IntPtr.Zero == h)
            //    {
            //        h = Connect(str);
            //        Cursor = Cursors.Default;
            //        if (h != IntPtr.Zero)
            //        {
            //            //this.lblDevice2.Visible = true;
            //            Scale31Connected = true;
            //        }
            //        else
            //        {
            //            ret = PullLastError();
            //            //this.lblDevice2.Visible = false;
            //            Scale31Connected = false;
            //        }
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    MessageBox.Show(Ex.Message);
            //}
            //#endregion

            //#region Lấy tín hiệu
            //try
            //{
            //    if (Scale31Connected)
            //    {
            //        this.Scale31 = true;
            //        threadScale31 = new Thread(t =>
            //        {
            //            while (this.Scale31)
            //            {
            //                this.Invoke((MethodInvoker)delegate
            //                {
            //                    this.lblCanso1.Visible = !this.lblCanso1.Visible;

            //                    //int ret = 0, i = 0, buffersize = 256;
            //                    //string str = "";
            //                    //string[] tmp = null;
            //                    //byte[] buffer = new byte[256];
            //                    //if (IntPtr.Zero != h)
            //                    //{
            //                    //    ret = GetRTLog(h, ref buffer[0], buffersize);
            //                    //    if (ret >= 0)
            //                    //    {
            //                    //        str = Encoding.Default.GetString(buffer);
            //                    //        tmp = str.Split(',');
            //                    //        if (tmp[2] == "0" || tmp[2] == "")
            //                    //        {
            //                    //            this.lblDiemxacthuc.Visible = false;
            //                    //            this.lblDiemxacthuc.Refresh();
            //                    //        }
            //                    //        else
            //                    //        {
            //                    //            this.lblDiemxacthuc.Visible = true;
            //                    //            this.lblDiemxacthuc.Refresh();
            //                    //            bool findItem = false;
            //                    //            for (int j = 0; j < this.objTableConfirm.Rows.Count; j++)
            //                    //            {
            //                    //                if (this.objTableConfirm.Rows[j]["CardNo"].ToString() == tmp[2].ToString())
            //                    //                {
            //                    //                    this.objTableConfirm.Rows[j]["VerifyTime"] = tmp[0].ToString();
            //                    //                    findItem = true;
            //                    //                }
            //                    //            }
            //                    //            if (!findItem)
            //                    //            {
            //                    //                this.objTableConfirm.Rows.Add(tmp[2].ToString(), tmp[0].ToString());
            //                    //                this.objBillOrder.setBillOrderConfirmByCardNo(tmp[2].ToString());
            //                    //            }
            //                    //            this.dgvDataConfirm.AutoGenerateColumns = false;
            //                    //            this.dgvDataConfirm.DataSource = this.objTableConfirm;
            //                    //        }
            //                    //    }
            //                    //    i++;
            //                    //}
            //                    //else
            //                    //{
            //                    //    MessageBox.Show("Connect device failed!");
            //                    //    return;
            //                    //}

            //                });

            //                Thread.Sleep(this.TimeSleepScale31);
            //            }
            //        })
            //        { IsBackground = true };
            //        threadScale31.Start();
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    MessageBox.Show(Ex.Message);
            //}
            //#endregion

            #endregion

            #region Module Cân số 2

            //#region Kết nối thiết bị
            //bool Scale32Connected = false;
            //try
            //{
            //    string str = this.Scale31Config;
            //    int ret = 0; // Error ID number
            //    if (IntPtr.Zero == h)
            //    {
            //        h = Connect(str);
            //        Cursor = Cursors.Default;
            //        if (h != IntPtr.Zero)
            //        {
            //            //this.lblDevice2.Visible = true;
            //            Scale32Connected = true;
            //        }
            //        else
            //        {
            //            ret = PullLastError();
            //            //this.lblDevice2.Visible = true;
            //            Scale32Connected = false;
            //        }
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    MessageBox.Show(Ex.Message);
            //}
            //#endregion

            //#region Lấy tín hiệu
            //try
            //{
            //    if (Scale32Connected)
            //    {
            //        this.Scale32 = true;
            //        threadScale32 = new Thread(t =>
            //        {
            //            while (this.Scale32)
            //            {
            //                this.Invoke((MethodInvoker)delegate
            //                {
            //                    this.lblCanso2.Visible = !this.lblCanso2.Visible;

            //                    //int ret = 0, i = 0, buffersize = 256;
            //                    //string str = "";
            //                    //string[] tmp = null;
            //                    //byte[] buffer = new byte[256];
            //                    //if (IntPtr.Zero != h)
            //                    //{
            //                    //    ret = GetRTLog(h, ref buffer[0], buffersize);
            //                    //    if (ret >= 0)
            //                    //    {
            //                    //        str = Encoding.Default.GetString(buffer);
            //                    //        tmp = str.Split(',');
            //                    //        if (tmp[2] == "0" || tmp[2] == "")
            //                    //        {
            //                    //            this.lblDiemxacthuc.Visible = false;
            //                    //            this.lblDiemxacthuc.Refresh();
            //                    //        }
            //                    //        else
            //                    //        {
            //                    //            this.lblDiemxacthuc.Visible = true;
            //                    //            this.lblDiemxacthuc.Refresh();
            //                    //            bool findItem = false;
            //                    //            for (int j = 0; j < this.objTableConfirm.Rows.Count; j++)
            //                    //            {
            //                    //                if (this.objTableConfirm.Rows[j]["CardNo"].ToString() == tmp[2].ToString())
            //                    //                {
            //                    //                    this.objTableConfirm.Rows[j]["VerifyTime"] = tmp[0].ToString();
            //                    //                    findItem = true;
            //                    //                }
            //                    //            }
            //                    //            if (!findItem)
            //                    //            {
            //                    //                this.objTableConfirm.Rows.Add(tmp[2].ToString(), tmp[0].ToString());
            //                    //                this.objBillOrder.setBillOrderConfirmByCardNo(tmp[2].ToString());
            //                    //            }
            //                    //            this.dgvDataConfirm.AutoGenerateColumns = false;
            //                    //            this.dgvDataConfirm.DataSource = this.objTableConfirm;
            //                    //        }
            //                    //    }
            //                    //    i++;
            //                    //}
            //                    //else
            //                    //{
            //                    //    MessageBox.Show("Connect device failed!");
            //                    //    return;
            //                    //}

            //                });

            //                Thread.Sleep(this.TimeSleepScale32);
            //            }
            //        }) { IsBackground = true };
            //        threadScale32.Start();
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    MessageBox.Show(Ex.Message);
            //}
            //#endregion

            #endregion

            //#region Mời xe vào
            //this.CallVehicle();
            //#endregion

            #region Lấy đơn hàng, mở ra để lấy đơn hàng về test trung
            // this.getBillOrder();
            #endregion

            #region bảng led12
           // this.sendContentToLED12();
            #endregion
        }
        #endregion

        #region method frmMainControl_FormClosing
        private void frmMainControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            #region Đóng kết nối

            try
            {
                if (this.threadBillOrder.IsAlive)
                {
                    this.threadBillOrder.Abort();
                }
            }
            catch
            {

            }

            try
            {
                if (this.threadConfirm.IsAlive)
                {
                    this.threadConfirm.Abort();
                }
            }
            catch
            {

            }

            try
            {
                if (this.threadInOut21.IsAlive)
                {
                    this.threadInOut21.Abort();
                }
            }
            catch
            {

            }

            try
            {
                if (this.threadScale31.IsAlive)
                {
                    this.threadScale31.Abort();
                }
            }
            catch
            {

            }

            try
            {
                if (this.threadScale32.IsAlive)
                {
                    this.threadScale32.Abort();
                }
            }
            catch
            {

            }

            try
            {
                if (this.threadVoice1.IsAlive)
                {
                    this.threadVoice1.Abort();
                }
            }
            catch
            {

            }

            try
            {
                if (this.threadLED12.IsAlive)
                {
                    this.threadLED12.Abort();
                }
            }
            catch
            {

            }
            #endregion
        }
        #endregion

        //#region method LED11

        //#region method sendContentToLED11
        //private void sendContentToLED11(string content)
        //{
        //    IntPtr pNULL = new IntPtr(0);

        //    int nErrorCode = -1;
        //    // 1. Create a screen
        //    int nWidth = 64;
        //    int nHeight = 32;
        //    int nColor = 1;
        //    int nGray = 1;
        //    int nCardType = 0;

        //    int nRe = CSDKExport.Hd_CreateScreen(nWidth, nHeight, nColor, nGray, nCardType, pNULL, 0);
        //    if (nRe != 0)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }

        //    // 2. Add program to screen
        //    int nProgramID = CSDKExport.Hd_AddProgram(pNULL, 0, 0, pNULL, 0);
        //    if (nProgramID == -1)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }

        //    //Add Area 1
        //    int nX1 = 0;
        //    int nY1 = 0;
        //    int nAreaWidth = 64;
        //    int nAreaHeight = 16;

        //    // 3. Add Area to program
        //    int nAreaID_1 = CSDKExport.Hd_AddArea(nProgramID, nX1, nY1, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
        //    if (nAreaID_1 == -1)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }

        //    //Add Area 2
        //    int nX2 = 0;
        //    int nY2 = 14;


        //    // 3. Add Area to program
        //    int nAreaID_2 = CSDKExport.Hd_AddArea(nProgramID, nX2, nY2, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
        //    if (nAreaID_2 == -1)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }

        //    // 4.Add text AreaItem to Area
        //    IntPtr pText = Marshal.StringToHGlobalUni(content);
        //    IntPtr pFontName = Marshal.StringToHGlobalUni("Times New Roman");
        //    int nTextColor = CSDKExport.Hd_GetColor(255, 0, 0);

        //    // center in bold and underline
        //    int nTextStyle = 0x0004 | 0x0100 /*| 0x0200 */;
        //    int nFontHeight = 12;
        //    int nEffect = 0;

        //    //Show on Area 1
        //    int nAreaItemID_1 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_1, pText, nTextColor, 0, nTextStyle,
        //        pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
        //    if (nAreaItemID_1 == -1)
        //    {
        //        Marshal.FreeHGlobal(pText);
        //        Marshal.FreeHGlobal(pFontName);
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }

        //    //Show on Area 2
        //    nFontHeight = 12;
        //    if (content.Trim() != "")
        //    {
        //        pText = Marshal.StringToHGlobalUni("XÁC THỰC");
        //    }
        //    else
        //    {
        //        pText = Marshal.StringToHGlobalUni("");
        //    }
        //    nEffect = 0;
        //    int nAreaItemID_2 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_2, pText, nTextColor, 0, nTextStyle,
        //        pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
        //    if (nAreaItemID_2 == -1)
        //    {
        //        Marshal.FreeHGlobal(pText);
        //        Marshal.FreeHGlobal(pFontName);
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }

        //    Marshal.FreeHGlobal(pText);
        //    Marshal.FreeHGlobal(pFontName);

        //    // 5. Send to device 
        //    nRe = CSDKExport.Hd_SendScreen(m_nSendType_LED11, m_pSendParams_LED11, pNULL, pNULL, 0);
        //    if (nRe != 0)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //    }
        //}
        //#endregion

        //#region method clearScreenLED11
        //private void clearScreenLED11()
        //{
        //    IntPtr pNULL = new IntPtr(0);

        //    // 1 check is card online.
        //    int nRe = CSDKExport.Cmd_IsCardOnline(m_nSendType_LED11, m_pSendParams_LED11, pNULL);

        //    // 2 clear screen
        //    nRe = CSDKExport.Cmd_ClearScreen(m_nSendType_LED11, m_pSendParams_LED11, pNULL);
        //}
        //#endregion

        //#endregion

        //#region method LED12

        //#region method sendToLED12
        //private void sendToLED12()
        //{
        //    DataTable objTable = this.objBillOrder.getVehicleInfo();
        //    if (objTable.Rows.Count == 0)
        //    {
        //        SetLED12NoContent();
        //        return;
        //    }

        //    IntPtr pNULL = new IntPtr(0);

        //    int nErrorCode = -1;
        //    // 1. Create a screen
        //    int nWidth = 224;
        //    int nHeight = 160;
        //    int nColor = 1;
        //    int nGray = 1;
        //    int nCardType = 0;

        //    int nRe = CSDKExport.Hd_CreateScreen(nWidth, nHeight, nColor, nGray, nCardType, pNULL, 0);
        //    if (nRe != 0)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }

        //    // 2. Add program to screen
        //    int nProgramID = CSDKExport.Hd_AddProgram(pNULL, 0, 0, pNULL, 0);
        //    if (nProgramID == -1)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }

        //    #region Add Area 0
        //    int nX1 = 0;
        //    int nY1 = 0;
        //    int nAreaWidth = 224;
        //    int nAreaHeight = 16;

        //    int nAreaID_1 = CSDKExport.Hd_AddArea(nProgramID, nX1, nY1, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
        //    if (nAreaID_1 == -1)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }
        //    #endregion

        //    #region Add Area 1
        //    int nX2 = 0;
        //    int nY2 = 16;


        //    int nAreaID_2 = CSDKExport.Hd_AddArea(nProgramID, nX2, nY2, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
        //    if (nAreaID_2 == -1)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }
        //    #endregion

        //    #region Add Area 2
        //    int nX3 = 0;
        //    int nY3 = 32;

        //    int nAreaID_3 = CSDKExport.Hd_AddArea(nProgramID, nX3, nY3, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
        //    if (nAreaID_3 == -1)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }
        //    #endregion

        //    #region Add Area 3
        //    int nX4 = 0;
        //    int nY4 = 48;

        //    int nAreaID_4 = CSDKExport.Hd_AddArea(nProgramID, nX4, nY4, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
        //    if (nAreaID_4 == -1)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }
        //    #endregion

        //    #region Add Area 4
        //    int nX5 = 0;
        //    int nY5 = 64;

        //    int nAreaID_5 = CSDKExport.Hd_AddArea(nProgramID, nX5, nY5, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
        //    if (nAreaID_5 == -1)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }
        //    #endregion

        //    #region Add Area 5
        //    int nX6 = 0;
        //    int nY6 = 80;

        //    int nAreaID_6 = CSDKExport.Hd_AddArea(nProgramID, nX6, nY6, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
        //    if (nAreaID_6 == -1)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }
        //    #endregion

        //    #region Add Area 6
        //    int nX7 = 0;
        //    int nY7 = 96;

        //    int nAreaID_7 = CSDKExport.Hd_AddArea(nProgramID, nX7, nY7, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
        //    if (nAreaID_7 == -1)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }
        //    #endregion

        //  //  mới thêm
        //    #region Add Area 7
        //    int nX8 = 0;
        //    int nY8 = 112;

        //    int nAreaID_8 = CSDKExport.Hd_AddArea(nProgramID, nX8, nY8, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
        //    if (nAreaID_8 == -1)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }
        //    #endregion
        //    #region Add Area 8
        //    int nX9 = 0;
        //    int nY9 = 128;

        //    int nAreaID_9 = CSDKExport.Hd_AddArea(nProgramID, nX9, nY9, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
        //    if (nAreaID_9 == -1)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }
        //    #endregion
        //    #region Add Area 9
        //    int nX10 = 0;
        //    int nY10 = 144;

        //    int nAreaID_10 = CSDKExport.Hd_AddArea(nProgramID, nX10, nY10, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
        //    if (nAreaID_10 == -1)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }
        //    #endregion

        //    //#region Add Area 10
        //    //int nX11 = 0;
        //    //int nY11 = 160;

        //    //int nAreaID_11 = CSDKExport.Hd_AddArea(nProgramID, nX11, nY11, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
        //    //if (nAreaID_11 == -1)
        //    //{
        //    //    nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //    //    return;
        //    //}
        //    //#endregion
        //    //#region Add Area 11
        //    //int nX12 = 0;
        //    //int nY12 = 176;

        //    //int nAreaID_12 = CSDKExport.Hd_AddArea(nProgramID, nX12, nY12, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
        //    //if (nAreaID_12 == -1)
        //    //{
        //    //    nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //    //    return;
        //    //}
        //    //#endregion

        //    #region DÒNG TIÊU ĐỀ
        //    // 4.Add text AreaItem to Area
        //    IntPtr pText = Marshal.StringToHGlobalUni(" BIEN SO             TRANG THAI");
        //    IntPtr pFontName = Marshal.StringToHGlobalUni("Times New Roman");
        //    int nTextColor = CSDKExport.Hd_GetColor(255, 0, 0);

        //    // center in bold and underline
        //    int nTextStyle = 0x0000 | 0x0100 /*| 0x0200 */;
        //    int nFontHeight = 14;
        //    int nEffect = 0;
        //    #endregion

        //    #region Show on Area 0
        //    int nAreaItemID_1 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_1, pText, nTextColor, 0, nTextStyle,
        //        pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
        //    if (nAreaItemID_1 == -1)
        //    {
        //        Marshal.FreeHGlobal(pText);
        //        Marshal.FreeHGlobal(pFontName);
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }
        //    #endregion

        //    #region Show on Area 1
        //    nFontHeight = 14;
        //    if (objTable.Rows.Count > 0)
        //    {
        //        pText = Marshal.StringToHGlobalUni(" 37C00000" + "            " + objTable.Rows[0]["State1"].ToString().ToUpper());
        //    }
        //    else
        //    {
        //        pText = Marshal.StringToHGlobalUni("");
        //    }
        //    nEffect = 0;
        //    int nAreaItemID_2 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_2, pText, nTextColor, 0, nTextStyle,
        //        pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
        //    if (nAreaItemID_2 == -1)
        //    {
        //        Marshal.FreeHGlobal(pText);
        //        Marshal.FreeHGlobal(pFontName);
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }
        //    #endregion

        //    #region Show on Area 2
        //    nFontHeight = 14;
        //    if (objTable.Rows.Count > 1)
        //    {
        //        pText = Marshal.StringToHGlobalUni(" 37C00000" + "            " + objTable.Rows[1]["State1"].ToString().ToUpper());
        //    }
        //    else
        //    {
        //        pText = Marshal.StringToHGlobalUni("");
        //    }
        //    nEffect = 0;
        //    int nAreaItemID_3 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_3, pText, nTextColor, 0, nTextStyle,
        //        pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
        //    if (nAreaItemID_3 == -1)
        //    {
        //        Marshal.FreeHGlobal(pText);
        //        Marshal.FreeHGlobal(pFontName);
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }
        //    #endregion

        //    #region Show on Area 3
        //    nFontHeight = 14;
        //    if (objTable.Rows.Count > 2)
        //    {
        //        pText = Marshal.StringToHGlobalUni(" 37C00000" + "            " + objTable.Rows[2]["State1"].ToString().ToUpper());
        //    }
        //    else
        //    {
        //        pText = Marshal.StringToHGlobalUni("");
        //    }
        //    nEffect = 0;
        //    int nAreaItemID_4 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_4, pText, nTextColor, 0, nTextStyle,
        //        pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
        //    if (nAreaItemID_4 == -1)
        //    {
        //        Marshal.FreeHGlobal(pText);
        //        Marshal.FreeHGlobal(pFontName);
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }
        //    #endregion

        //    #region Show on Area 4
        //    nFontHeight = 14;
        //    if (objTable.Rows.Count > 3)
        //    {
        //        pText = Marshal.StringToHGlobalUni(" 37C00000" + "            " + objTable.Rows[3]["State1"].ToString().ToUpper());
        //    }
        //    else
        //    {
        //        pText = Marshal.StringToHGlobalUni("");
        //    }
        //    nEffect = 0;
        //    int nAreaItemID_5 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_5, pText, nTextColor, 0, nTextStyle,
        //        pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
        //    if (nAreaItemID_5 == -1)
        //    {
        //        Marshal.FreeHGlobal(pText);
        //        Marshal.FreeHGlobal(pFontName);
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }
        //    #endregion

        //    #region Show on Area 5
        //    nFontHeight = 14;
        //    if (objTable.Rows.Count > 4)
        //    {
        //        pText = Marshal.StringToHGlobalUni(" 37C00000" + "            " + objTable.Rows[4]["State1"].ToString().ToUpper());
        //    }
        //    else
        //    {
        //        pText = Marshal.StringToHGlobalUni("");
        //    }
        //    nEffect = 0;
        //    int nAreaItemID_6 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_6, pText, nTextColor, 0, nTextStyle,
        //        pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
        //    if (nAreaItemID_6 == -1)
        //    {
        //        Marshal.FreeHGlobal(pText);
        //        Marshal.FreeHGlobal(pFontName);
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }
        //    #endregion

        //    #region Show on Area 6
        //    nFontHeight = 14;
        //    if (objTable.Rows.Count > 5)
        //    {
        //        pText = Marshal.StringToHGlobalUni(" 37C00000" + "            " + objTable.Rows[5]["State1"].ToString().ToUpper());
        //    }
        //    else
        //    {
        //        pText = Marshal.StringToHGlobalUni("");
        //    }
        //    nEffect = 0;
        //    int nAreaItemID_7 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_7, pText, nTextColor, 0, nTextStyle,
        //        pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
        //    if (nAreaItemID_7 == -1)
        //    {
        //        Marshal.FreeHGlobal(pText);
        //        Marshal.FreeHGlobal(pFontName);
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }
        //    #endregion
        //    // mới thêm
        //    #region Show on Area 7
        //    nFontHeight = 14;
        //    if (objTable.Rows.Count > 6)
        //    {
        //        pText = Marshal.StringToHGlobalUni(" 37C00000" + "            " + objTable.Rows[6]["State1"].ToString().ToUpper());
        //    }
        //    else
        //    {
        //        pText = Marshal.StringToHGlobalUni("");
        //    }
        //    nEffect = 0;
        //    int nAreaItemID_8 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_8, pText, nTextColor, 0, nTextStyle,
        //        pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
        //    if (nAreaItemID_8 == -1)
        //    {
        //        Marshal.FreeHGlobal(pText);
        //        Marshal.FreeHGlobal(pFontName);
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }
        //    #endregion

        //    #region Show on Area 8
        //    nFontHeight = 14;
        //    if (objTable.Rows.Count > 7)
        //    {
        //        pText = Marshal.StringToHGlobalUni(" 37C00000" + "            " + objTable.Rows[7]["State1"].ToString().ToUpper());
        //    }
        //    else
        //    {
        //        pText = Marshal.StringToHGlobalUni("");
        //    }
        //    nEffect = 0;
        //    int nAreaItemID_9 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_9, pText, nTextColor, 0, nTextStyle,
        //        pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
        //    if (nAreaItemID_9 == -1)
        //    {
        //        Marshal.FreeHGlobal(pText);
        //        Marshal.FreeHGlobal(pFontName);
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }
        //    #endregion

        //    #region Show on Area 9
        //    nFontHeight = 14;
        //    if (objTable.Rows.Count > 8)
        //    {
        //        pText = Marshal.StringToHGlobalUni(" 37C00000" + "            " + objTable.Rows[8]["State1"].ToString().ToUpper());
        //    }
        //    else
        //    {
        //        pText = Marshal.StringToHGlobalUni("");
        //    }
        //    nEffect = 0;
        //    int nAreaItemID_10 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_10, pText, nTextColor, 0, nTextStyle,
        //        pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
        //    if (nAreaItemID_10 == -1)
        //    {
        //        Marshal.FreeHGlobal(pText);
        //        Marshal.FreeHGlobal(pFontName);
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }
        //    #endregion

        //    //#region Show on Area 10
        //    //nFontHeight = 14;
        //    //if (objTable.Rows.Count > 9)
        //    //{
        //    //    pText = Marshal.StringToHGlobalUni(" 37C00000" + "            " + objTable.Rows[9]["State1"].ToString().ToUpper());
        //    //}
        //    //else
        //    //{
        //    //    pText = Marshal.StringToHGlobalUni("");
        //    //}
        //    //nEffect = 0;
        //    //int nAreaItemID_11 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_11, pText, nTextColor, 0, nTextStyle,
        //    //    pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
        //    //if (nAreaItemID_11 == -1)
        //    //{
        //    //    Marshal.FreeHGlobal(pText);
        //    //    Marshal.FreeHGlobal(pFontName);
        //    //    nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //    //    return;
        //    //}
        //    //#endregion

        //    //#region Show on Area 11
        //    //nFontHeight = 14;
        //    //if (objTable.Rows.Count > 10)
        //    //{
        //    //    pText = Marshal.StringToHGlobalUni(" 37C00000" + "            " + objTable.Rows[10]["State1"].ToString().ToUpper());
        //    //}
        //    //else
        //    //{
        //    //    pText = Marshal.StringToHGlobalUni("");
        //    //}
        //    //nEffect = 0;
        //    //int nAreaItemID_12 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_12, pText, nTextColor, 0, nTextStyle,
        //    //    pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
        //    //if (nAreaItemID_12 == -1)
        //    //{
        //    //    Marshal.FreeHGlobal(pText);
        //    //    Marshal.FreeHGlobal(pFontName);
        //    //    nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //    //    return;
        //    //}
        //    //#endregion

        //    Marshal.FreeHGlobal(pText);
        //    Marshal.FreeHGlobal(pFontName);

        //    // 5. Send to device 
        //    nRe = CSDKExport.Hd_SendScreen(m_nSendType_LED12, m_pSendParams_LED12, pNULL, pNULL, 0);
        //    if (nRe != 0)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //    }
        //}
        //private void SetLED12NoContent()
        //{
        //    try
        //    {
        //        IntPtr pNULL = new IntPtr(0);

        //        int nErrorCode = -1;
        //        // 1. Create a screen
        //        int nWidth = 224;
        //        int nHeight = 160;
        //        int nColor = 1;
        //        int nGray = 1;
        //        int nCardType = 0;

        //        int nRe = CSDKExport.Hd_CreateScreen(nWidth, nHeight, nColor, nGray, nCardType, pNULL, 0);
        //        if (nRe != 0)
        //        {
        //            nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //            return;
        //        }

        //        // 2. Add program to screen
        //        int nProgramID = CSDKExport.Hd_AddProgram(pNULL, 0, 0, pNULL, 0);
        //        if (nProgramID == -1)
        //        {
        //            nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //            return;
        //        }

        //        #region Add Area 0
        //        int nX1 = 0;
        //        int nY1 = 40;
        //        int nAreaWidth = 224;
        //        int nAreaHeight = 20;

        //        int nAreaID_1 = CSDKExport.Hd_AddArea(nProgramID, nX1, nY1, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
        //        if (nAreaID_1 == -1)
        //        {
        //            nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //            return;
        //        }
        //        #endregion

        //        #region Add Area 1
        //        int nX2 = 0;
        //        int nY2 = 70;


        //        int nAreaID_2 = CSDKExport.Hd_AddArea(nProgramID, nX2, nY2, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
        //        if (nAreaID_2 == -1)
        //        {
        //            nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //            return;
        //        }
        //        #endregion

        //        #region Add Area 2
        //        int nX3 = 0;
        //        int nY3 = 90;

        //        int nAreaID_3 = CSDKExport.Hd_AddArea(nProgramID, nX3, nY3, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
        //        if (nAreaID_3 == -1)
        //        {
        //            nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //            return;
        //        }
        //        #endregion

               

        //        #region DÒNG TIÊU ĐỀ
        //        // 4.Add text AreaItem to Area
        //        IntPtr pText = Marshal.StringToHGlobalUni("VICEM HOÀNG MAI");
        //        IntPtr pFontName = Marshal.StringToHGlobalUni("Times New Roman");
        //        int nTextColor = CSDKExport.Hd_GetColor(255, 255, 255);

        //        // center in bold and underline
        //        int nTextStyle = 0x0004 | 0x0100; /*| 0x0200 */
        //        int nFontHeight = 18;
        //        int nEffect = 0;
        //        #endregion

        //        #region Show on Area 0
        //        int nAreaItemID_1 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_1, pText, nTextColor, 0, nTextStyle,
        //            pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
        //        if (nAreaItemID_1 == -1)
        //        {
        //            Marshal.FreeHGlobal(pText);
        //            Marshal.FreeHGlobal(pFontName);
        //            nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //            return;
        //        }
        //        #endregion

        //        #region Show on Area 1
        //        nFontHeight = 14;
        //        pText = Marshal.StringToHGlobalUni("HỆ THỐNG");
        //        nEffect = 0;
        //        int nAreaItemID_2 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_2, pText, nTextColor, 0, nTextStyle,
        //            pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
        //        if (nAreaItemID_2 == -1)
        //        {
        //            Marshal.FreeHGlobal(pText);
        //            Marshal.FreeHGlobal(pFontName);
        //            nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //            return;
        //        }
        //        #endregion

        //        #region Show on Area 2
        //        nFontHeight = 14;
        //        pText = Marshal.StringToHGlobalUni("XUẤT HÀNG TỰ ĐỘNG");
        //        nEffect = 0;
        //        int nAreaItemID_3 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_3, pText, nTextColor, 0, nTextStyle,
        //            pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
        //        if (nAreaItemID_3 == -1)
        //        {
        //            Marshal.FreeHGlobal(pText);
        //            Marshal.FreeHGlobal(pFontName);
        //            nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //            return;
        //        }
        //        #endregion



        //        Marshal.FreeHGlobal(pText);
        //        Marshal.FreeHGlobal(pFontName);

        //        // 5. Send to device 
        //        nRe = CSDKExport.Hd_SendScreen(m_nSendType_LED12, m_pSendParams_LED12, pNULL, pNULL, 0);
        //        if (nRe != 0)
        //        {
        //            nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        //#endregion

        //#region method clearScreenLED12
        //private void clearScreenLED12()
        //{
        //    IntPtr pNULL = new IntPtr(0);

        //    // 1 check is card online.
        //    int nRe = CSDKExport.Cmd_IsCardOnline(m_nSendType_LED12, m_pSendParams_LED12, pNULL);

        //    // 2 clear screen
        //    nRe = CSDKExport.Cmd_ClearScreen(m_nSendType_LED12, m_pSendParams_LED12, pNULL);
        //}
        //#endregion

        //#endregion

        //#region method LED21

        //#region method sendContentToLED21
        //private void sendContentToLED21(string content)
        //{
        //    IntPtr pNULL = new IntPtr(0);

        //    int nErrorCode = -1;
        //    // 1. Create a screen
        //    int nWidth = 64;
        //    int nHeight = 32;
        //    int nColor = 1;
        //    int nGray = 1;
        //    int nCardType = 0;

        //    int nRe = CSDKExport.Hd_CreateScreen(nWidth, nHeight, nColor, nGray, nCardType, pNULL, 0);
        //    if (nRe != 0)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }


        //    // 2. Add program to screen

        //    int nProgramID = CSDKExport.Hd_AddProgram(pNULL, 0, 0, pNULL, 0);
        //    if (nProgramID == -1)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }

        //    //Add Area 1
        //    int nX1 = 0;
        //    int nY1 = 8;
        //    int nAreaWidth = 64;
        //    int nAreaHeight = 16;

        //    // 3. Add Area to program
        //    int nAreaID_1 = CSDKExport.Hd_AddArea(nProgramID, nX1, nY1, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
        //    if (nAreaID_1 == -1)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }

        //    //Add Area 2
        //    int nX2 = 0;
        //    int nY2 = 16;


        //    // 3. Add Area to program
        //    int nAreaID_2 = CSDKExport.Hd_AddArea(nProgramID, nX2, nY2, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
        //    if (nAreaID_2 == -1)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }

        //    // 4.Add text AreaItem to Area
        //    IntPtr pText = Marshal.StringToHGlobalUni(content);
        //    IntPtr pFontName = Marshal.StringToHGlobalUni("Arial");
        //    int nTextColor = CSDKExport.Hd_GetColor(255, 0, 0);

        //    // center in bold and underline
        //    int nTextStyle = 0x0004 | 0x0100 /*| 0x0200 */;
        //    int nFontHeight = 10;
        //    int nEffect = 0;

        //    //Show on Area 1
        //    int nAreaItemID_1 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_1, pText, nTextColor, 0, nTextStyle,
        //        pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
        //    if (nAreaItemID_1 == -1)
        //    {
        //        Marshal.FreeHGlobal(pText);
        //        Marshal.FreeHGlobal(pFontName);
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }

        //    //Show on Area 2
        //    //nFontHeight = 10;
        //    //pText = Marshal.StringToHGlobalUni("MỜI XE VÀO");
        //    //nEffect = 0;
        //    //int nAreaItemID_2 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_2, pText, nTextColor, 0, nTextStyle,
        //    //    pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
        //    //if (nAreaItemID_2 == -1)
        //    //{
        //    //    Marshal.FreeHGlobal(pText);
        //    //    Marshal.FreeHGlobal(pFontName);
        //    //    nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //    //    return;
        //    //}

        //    Marshal.FreeHGlobal(pText);
        //    Marshal.FreeHGlobal(pFontName);

        //    // 5. Send to device 
        //    nRe = CSDKExport.Hd_SendScreen(m_nSendType_LED21, m_pSendParams_LED11, pNULL, pNULL, 0);
        //    if (nRe != 0)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //    }
        //}
        //#endregion

        //#region method clearScreenLED21
        //private void clearScreenLED21()
        //{
        //    IntPtr pNULL = new IntPtr(0);

        //    // 1 check is card online.
        //    int nRe = CSDKExport.Cmd_IsCardOnline(m_nSendType_LED21, m_pSendParams_LED21, pNULL);

        //    // 2 clear screen
        //    nRe = CSDKExport.Cmd_ClearScreen(m_nSendType_LED21, m_pSendParams_LED21, pNULL);
        //}
        //#endregion

        //#endregion

        //#region method LED22

        //#region method sendContentToLED22
        //private void sendContentToLED22(string content)
        //{
        //    IntPtr pNULL = new IntPtr(0);

        //    int nErrorCode = -1;
        //    // 1. Create a screen
        //    int nWidth = 64;
        //    int nHeight = 32;
        //    int nColor = 1;
        //    int nGray = 1;
        //    int nCardType = 0;

        //    int nRe = CSDKExport.Hd_CreateScreen(nWidth, nHeight, nColor, nGray, nCardType, pNULL, 0);
        //    if (nRe != 0)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }

        //    // 2. Add program to screen
        //    int nProgramID = CSDKExport.Hd_AddProgram(pNULL, 0, 0, pNULL, 0);
        //    if (nProgramID == -1)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }

        //    //Add Area 1
        //    int nX1 = 0;
        //    int nY1 = 0;
        //    int nAreaWidth = 64;
        //    int nAreaHeight = 16;

        //    // 3. Add Area to program
        //    int nAreaID_1 = CSDKExport.Hd_AddArea(nProgramID, nX1, nY1, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
        //    if (nAreaID_1 == -1)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }

        //    //Add Area 2
        //    int nX2 = 0;
        //    int nY2 = 14;


        //    // 3. Add Area to program
        //    int nAreaID_2 = CSDKExport.Hd_AddArea(nProgramID, nX2, nY2, nAreaWidth, nAreaHeight, pNULL, 0, 0, pNULL, 0);
        //    if (nAreaID_2 == -1)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }

        //    // 4.Add text AreaItem to Area
        //    IntPtr pText = Marshal.StringToHGlobalUni(content);
        //    IntPtr pFontName = Marshal.StringToHGlobalUni("Times New Roman");
        //    int nTextColor = CSDKExport.Hd_GetColor(255, 0, 0);

        //    // center in bold and underline
        //    int nTextStyle = 0x0004 | 0x0100 /*| 0x0200 */;
        //    int nFontHeight = 12;
        //    int nEffect = 0;

        //    //Show on Area 1
        //    int nAreaItemID_1 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_1, pText, nTextColor, 0, nTextStyle,
        //        pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
        //    if (nAreaItemID_1 == -1)
        //    {
        //        Marshal.FreeHGlobal(pText);
        //        Marshal.FreeHGlobal(pFontName);
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }

        //    //Show on Area 2
        //    nFontHeight = 12;
        //    if (content.Trim() != "")
        //    {
        //        pText = Marshal.StringToHGlobalUni("XE VÀO");
        //    }
        //    else
        //    {
        //        pText = Marshal.StringToHGlobalUni("");
        //    }
        //    nEffect = 0;
        //    int nAreaItemID_2 = CSDKExport.Hd_AddSimpleTextAreaItem(nAreaID_2, pText, nTextColor, 0, nTextStyle,
        //        pFontName, nFontHeight, nEffect, 30, 201, 3, pNULL, 0);
        //    if (nAreaItemID_2 == -1)
        //    {
        //        Marshal.FreeHGlobal(pText);
        //        Marshal.FreeHGlobal(pFontName);
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //        return;
        //    }

        //    Marshal.FreeHGlobal(pText);
        //    Marshal.FreeHGlobal(pFontName);

        //    // 5. Send to device 
        //    nRe = CSDKExport.Hd_SendScreen(m_nSendType_LED22, m_pSendParams_LED22, pNULL, pNULL, 0);
        //    if (nRe != 0)
        //    {
        //        nErrorCode = CSDKExport.Hd_GetSDKLastError();
        //    }
        //}
        //#endregion

        //#region method clearScreenLED22
        //private void clearScreenLED22()
        //{
        //    IntPtr pNULL = new IntPtr(0);

        //    // 1 check is card online.
        //    int nRe = CSDKExport.Cmd_IsCardOnline(m_nSendType_LED22, m_pSendParams_LED22, pNULL);

        //    // 2 clear screen
        //    nRe = CSDKExport.Cmd_ClearScreen(m_nSendType_LED22, m_pSendParams_LED22, pNULL);
        //}
        //#endregion

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //this.sendContentToLED22("MỜI");
        //    //this.sendToLED12();
        //    //Thread.Sleep(5000);
        //    //this.sendContentToLED22("VICEM");
        //}

        //#endregion

        #region method setText
        private void setText()
        {
            //BIỂN SỐ      SỐ LƯỢNG   BAO   RỜI   XK   K   NH           TRẠNG THÁI
            //37C12345   20                   1                                                   Mời vào lấy hàng   
            string Line1 = "BIỂN SỐ      SỐ LƯỢNG   BAO   RỜI   XK   K   NH           TRẠNG THÁI" + Environment.NewLine + Environment.NewLine;
            #region Dong thu hai
            string Line2 = "", BienSo2 = "37C12345", KhoiLuong2 = "20", ThuTu2 = "01", TrangThai2 = "Mời vào lấy hàng";
            if (BienSo2.Trim().Length == 8)
            {
                Line2 += BienSo2.Trim() + "   ";
            }
            else if (BienSo2.Trim().Length == 7)
            {
                Line2 += BienSo2.Trim() + "    ";
            }

            if (KhoiLuong2.Trim().Length == 3)
            {
                Line2 += KhoiLuong2.Trim() + "                ";
            }
            else if (KhoiLuong2.Trim().Length == 2)
            {
                Line2 += KhoiLuong2.Trim() + "                   ";
            }
            else if (KhoiLuong2.Trim().Length == 1)
            {
                Line2 += KhoiLuong2.Trim() + "                    ";
            }

            if (ThuTu2.Trim().Length == 2)
            {
                Line2 += ThuTu2.Trim() + "                                                ";
            }
            else if (ThuTu2.Trim().Length == 1)
            {
                Line2 += ThuTu2.Trim() + "                                               ";
            }
            Line2 += TrangThai2.Trim() + "                   ";
            Line2 += Environment.NewLine;
            #endregion

            #region Dong thu hai
            string Line3 = "", BienSo3 = "37C12345", KhoiLuong3 = "150", ThuTu3 = "02", TrangThai3 = "Chờ lấy hàng";
            if (BienSo3.Trim().Length == 8)
            {
                Line3 += BienSo3.Trim() + "   ";
            }
            else if (BienSo3.Trim().Length == 7)
            {
                Line3 += BienSo3.Trim() + "    ";
            }

            if (KhoiLuong3.Trim().Length == 3)
            {
                Line3 += KhoiLuong3.Trim() + "                ";
            }
            else if (KhoiLuong3.Trim().Length == 2)
            {
                Line3 += KhoiLuong3.Trim() + "                   ";
            }
            else if (KhoiLuong3.Trim().Length == 1)
            {
                Line3 += KhoiLuong3.Trim() + "                    ";
            }

            if (ThuTu3.Trim().Length == 2)
            {
                Line3 += ThuTu3.Trim() + "                                                ";
            }
            else if (ThuTu3.Trim().Length == 1)
            {
                Line3 += ThuTu3.Trim() + "                                               ";
            }
            Line3 += TrangThai3.Trim() + "                   ";
            Line3 += Environment.NewLine;
            #endregion

            string Line4 = "37C12345   20                   3                                                   Mời vào lấy hàng" + Environment.NewLine;
            string Line5 = "37C12345   20                   4                                                   Mời vào lấy hàng" + Environment.NewLine;
            //textBox1.Text = Line1 + Line2 + Line3 + Line4 + Line5;
        }
        #endregion

        #region method CallVehicle
        private void CallVehicle()
        {
            //threadVoice1 = new Thread(t =>
            //{
            //    while (1 < 2)
            //    {
            //        this.Invoke((MethodInvoker)delegate
            //        {
            //            this.BillOrderRelease = this.objBillOrder.getBillOrderRelease();
            //            int Action = 0;
            //            for (int i = 0; i < this.BillOrderRelease.Rows.Count; i++)
            //            {
            //                if (this.BillOrderRelease.Rows[i]["Step"].ToString() == "1" || this.BillOrderRelease.Rows[i]["Step"].ToString() == "2" || this.BillOrderRelease.Rows[i]["Step"].ToString() == "3" || this.BillOrderRelease.Rows[i]["Step"].ToString() == "4")
            //                {
            //                    if (this.objBillOrder.setReleaseVoice(this.BillOrderRelease.Rows[i]["IDOrderSyn"].ToString(), "Mời xe " + this.BillOrderRelease.Rows[i]["Vehicle"].ToString().ToUpper() + " vào lấy hàng") > 0)
            //                    {
            //                        Action = Action + 1;
            //                        //this.VoiceVehicle();
            //                        //Thread.Sleep(10000);
            //                    }
            //                }
            //            }
            //            if (Action > 0)
            //            {
            //                this.BillOrderRelease = this.objBillOrder.getBillOrderRelease();
            //            }
            //        });
            //        Thread.Sleep(30000);
            //    }
            //}) { IsBackground = true };
            //threadVoice1.Start();
        }
        #endregion

        #region method getBillOrder
        private void getBillOrder()
        {
            //try
            //{
            //    DataTable tblDistributor = new DataTable();
            //    tblDistributor = this.objCustomer.getDistributorData();

            //    if (this.strToken == "")
            //    {
            //        this.strToken = this.getToken();
            //    }

            //    threadBillOrder = new Thread(t =>
            //    {
            //        while (1 < 2)
            //        {
            //            foreach (DataRow distributor in tblDistributor.Rows)
            //            {
            //                var client = new RestClient(this.LinkAPI_WebSale + "/api/search-orders");
            //                var request = new RestRequest(Method.POST);
            //                request.AddHeader("Authorization", "Bearer " + strToken);
            //                request.AddHeader("Content-Type", "application/json");

            //                string strBody = "{";
            //                strBody += "\r\n \"pageSize\": " + 200 + ",";
            //                strBody += "\r\n \"pageIndex\": " + 1 + ",";
            //                strBody += "\r\n \"deliveryCode\": \"" + "" + "\",";
            //                strBody += "\r\n \"customerId\": " + distributor["IDDistributorSyn"].ToString() + ",";
            //                strBody += "\r\n \"status\": \"" + "" + "\",";
            //                strBody += "\r\n \"fromDate\": \"" + DateTime.Now.AddHours(-48).ToString("dd/MM/yyyy") + "\",";
            //                strBody += "\r\n \"toDate\": \"" + DateTime.Now.AddDays(1).ToString("dd/MM/yyyy") + "\",";
            //                strBody += "\r\n \"orderType\": " + 1 + ",";
            //                strBody += "\r\n \"inventoryItemId\": " + 0 + ",";
            //                strBody += "\r\n \"shippointId\": " + 0 + ",";
            //                strBody += "\r\n \"vehicleCode\": \"" + "" + "\",";
            //                strBody += "\r\n \"docNum\": \"" + "" + "\",";
            //                strBody += "\r\n}";

            //                request.AddParameter("application/json", strBody, ParameterType.RequestBody);
            //                IRestResponse response = client.Execute(request);
            //                string data = response.Content;

            //                dynamic jsonData = JsonConvert.DeserializeObject(data);

            //                if (jsonData != null)
            //                {
            //                    string Status = "", Order_date = "", Vehice_code = "", Drive_name = "", Customer_name = "", Inventory_item_id = "", Item_name = "", Item_category = "", Order_quantity = "", Delivery_code = "";
            //                    int Order_Id = 0;
            //                    foreach (var iBill in jsonData.datas)
            //                    {
            //                        if (iBill.STATUS != null)
            //                        {
            //                            Status = iBill.STATUS.ToString();
            //                        }

            //                        try
            //                        {
            //                            if (iBill.ORDER_ID != null)
            //                            {
            //                                Order_Id = int.Parse(iBill.ORDER_ID.ToString());
            //                            }
            //                        }
            //                        catch
            //                        {

            //                        }

            //                        try
            //                        {
            //                            if (iBill.ORDER_DATE != null)
            //                            {
            //                                Order_date = iBill.ORDER_DATE.ToString();
            //                            }
            //                        }
            //                        catch
            //                        {

            //                        }

            //                        try
            //                        {
            //                            if (iBill.VEHICLE_CODE != null)
            //                            {
            //                                Vehice_code = iBill.VEHICLE_CODE.ToString();
            //                            }
            //                        }
            //                        catch
            //                        {

            //                        }

            //                        try
            //                        {
            //                            if (iBill.DRIVER_NAME != null)
            //                            {
            //                                Drive_name = iBill.DRIVER_NAME.ToString();
            //                            }
            //                        }
            //                        catch
            //                        {

            //                        }

            //                        try
            //                        {
            //                            if (iBill.CUSTOMER_NAME != null)
            //                            {
            //                                Customer_name = iBill.CUSTOMER_NAME.ToString();
            //                            }
            //                        }
            //                        catch
            //                        {

            //                        }

            //                        try
            //                        {
            //                            if (iBill.INVENTORY_ITEM_ID != null)
            //                            {
            //                                Inventory_item_id = iBill.INVENTORY_ITEM_ID.ToString();
            //                            }
            //                        }
            //                        catch
            //                        {

            //                        }
                                    
            //                        try
            //                        {
            //                            if (iBill.ITEM_NAME != null)
            //                            {
            //                                Item_name = iBill.ITEM_NAME.ToString();
            //                            }
            //                        }
            //                        catch
            //                        {

            //                        }

            //                        try
            //                        {
            //                            if (iBill.ITEM_CATEGORY != null)
            //                            {
            //                                Item_category = iBill.ITEM_CATEGORY.ToString();
            //                            }
            //                        }
            //                        catch
            //                        {

            //                        }

            //                        try
            //                        {
            //                            if (iBill.ORDER_QUANTITY != null)
            //                            {
            //                                Order_quantity = iBill.ORDER_QUANTITY.ToString();
            //                            }
            //                        }
            //                        catch
            //                        {

            //                        }

            //                        try
            //                        {
            //                            if (iBill.DELIVERY_CODE != null)
            //                            {
            //                                Delivery_code = iBill.DELIVERY_CODE.ToString();
            //                            }
            //                        }
            //                        catch
            //                        {

            //                        }
                                    
            //                        if (Status.ToUpper() != "RECEIVED" && Status.ToUpper() != "VOIDED")
            //                        {
            //                            this.objBillOrder.setDataBillOrderOperating(Order_Id, DateTime.Parse(Order_date), Vehice_code, Drive_name, Customer_name, Inventory_item_id, Item_name, Item_category, decimal.Parse(Order_quantity), Delivery_code, Status);
            //                        }
            //                        else
            //                        {
            //                            //this.objBillOrder.setBillOrderOperatingByDeliveryCode(billEntity.Delivery_code, 2, 0);
            //                            this.objBillOrder.UpdateFinishTMP(Delivery_code);
            //                        }
            //                    }
            //                }
            //            }
            //            Thread.Sleep(1 * 10000);
            //        }
            //    }) { IsBackground = true };
            //    threadBillOrder.Start();
            //}
            //catch (Exception Ex)
            //{
            //    MessageBox.Show(Ex.Message);
            //}
        }
        #endregion

        #region method getToken
        private string getToken()
        {
            //try
            //{
            //    var client0 = new RestClient(this.LinkAPI_WebSale + "/api/get-token");
            //    var request0 = new RestRequest();
            //    request0.Method = Method.POST;
            //    request0.AddHeader("Accept", "application/json");
            //    request0.Parameters.Clear();
            //    request0.AddParameter("application/json", "{\"userName\":\"" + this.userNameAPI + "\",\"password\":\"" + this.passwordAPI + "\",\"OUId\":\"302\"}", ParameterType.RequestBody);

            //    var response0 = client0.Execute(request0);
            //    string data0 = response0.Content;
            //    var jo0 = JObject.Parse(response0.Content);
            //    var id0 = jo0["token"].ToString();
            //    var expires0 = jo0["expires"].ToString();
            //    return id0;
            //}
            //catch (Exception)
            //{
            //    return "";
            //}
            return "";
        }
        #endregion

        #region method sendContentToLED12
        private void sendContentToLED12()
        {
            //threadLED12 = new Thread(t =>
            //{
            //    while (1 < 2)
            //    {
            //        this.Invoke((MethodInvoker)delegate
            //        {
            //            this.sendToLED12();
            //        });
            //        Thread.Sleep(30000);
            //    }
            //}) { IsBackground = true };
            //threadLED12.Start();
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dgvDataConfirm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
