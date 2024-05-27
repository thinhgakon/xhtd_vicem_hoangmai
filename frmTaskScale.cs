using HMXHTD.BarrierLib;
using HMXHTD.Data.DataEntity;
using Microsoft.AspNet.SignalR.Client;
using PLC_Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;
using Tulpep.NotificationWindow;

namespace HMXHTD
{
    public partial class frmTaskScale : Form
    {
        private PLCConnect PLC_M221;
        private Result PLC_Result;

        public static int scale_cn_balance = 0;
        public static int scale_cc_balance = 0;
        public static int scale_cn_realtime = 0;
        public static DateTime scale_cn_realtime_datetime = DateTime.Now;
        public static int scale_cc_realtime = 0;
        public static DateTime scale_cc_realtime_datetime = DateTime.Now;
        #region declare objects
        public static bool is_cn_auto = true;
        public static bool is_cc_auto = false;

        private IHubProxy hubProxy { get; set; }
        private HubConnection connection { get; set; }
        private Thread threadArea;
        delegate void SetTextCallback(string text);
        int TimeSleep = 60000, OrderId = 0, StepId = -1;
        BillOrder objBillOrder = new BillOrder();
        #endregion

        #region method frmTaskScale
        public frmTaskScale()
        {
            InitializeComponent();
            InitData();
            ConnectAsync();
            resetScaleJob();
            ConnectSensor();
            //for (int i = 0; i < 10; i++)
            //{
            //    LoadScaleWeight(true);
            //}

        }
        #endregion

        private void InitData()
        {
            try
            {

            }
            catch (Exception ex)
            {
                
            }
        }

        #region method SignalR
        private async Task<bool> ConnectAsync()
        {
            bool connected = false;
            try
            {
                connection = new HubConnection(ConfigurationManager.AppSettings.Get("singalRHost").ToString());
                hubProxy = connection.CreateHubProxy(ConfigurationManager.AppSettings.Get("hubSignalR").ToString());
                //hubProxy.On<string, string>(ConfigurationManager.AppSettings.Get("methodSendMessage").ToString(), (name, message) =>
                //    this.Invoke((Action)(() =>
                //        ProcessNotification(name, message)
                //    ))
                //);
            }
            catch
            {

            }
            try
            {
                await connection.Start();
                if (connection.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected)
                {
                    hubProxy.On<string, string>(ConfigurationManager.AppSettings.Get("methodSendMessage").ToString(), (name, message) =>
                    this.Invoke((Action)(() =>
                        ProcessNotification(name, message)
                    ))
                );

                    connection.Error += (ex) =>
                    {
                    //    Console.WriteLine("Connection error: " + ex.ToString());
                    };
                    connection.Closed += () =>
                    {
                      //  Console.WriteLine("Connection closed");
                    };
                    connection.StateChanged += Connection_StateChanged;
                   // Console.WriteLine("Server for Current is started.");
                    connected = true;
                }
            }
            catch (HttpRequestException ex)
            {
            }
            return connected;
        }
        private async void Connection_StateChanged(StateChange obj)
        {
            if (obj.NewState == Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected)
            {
                await RestartConnection();
            }
        }
        public async Task RestartConnection()
        {
            while (true)
            {
                bool connected = await ConnectAsync();
                if (connected)
                    return;
            }
        }
        private void ProcessNotification(string key, string message)
        {
            switch (key)
            {
                case "New_Order_Sync":
                    break;
                case "Step_1":
                    break;
                case "Step_2":
                   // this.getData();
                    break;
                case "Step_3":
                  //  this.getData();
                    break;
                case "Step_4":
                  //  this.getData();
                    break;
                case "Step_5":
                    break;
                case "Step_6":
                  //  this.getData();
                    break;
                case "Step_7":
                    break;
                case "Step_8":
                    break;
                case "Scale1_Current":
                    this.lbNoti_CN.Text = $@"{message}";
                    int weightCN = int.Parse(message);
                    scale_cn_realtime = weightCN;
                    scale_cn_realtime_datetime = DateTime.Now;
                    if (weightCN > 500 && weightCN < 2000)
                    {
                        PlayDingDing();
                    }
                    if(scale_cn_balance > 0 && ((scale_cn_balance - weightCN) > 500))
                    {
                        this.resetControl_CN();
                        this.ResetScaleWeight(true);
                        //this.lblScaleMsg_CN.Text = $@"";
                    }
                    break;
                case "Scale2_Current":
                    this.lbNoti_CC.Text = $@"{message}";
                    int weightCC = int.Parse(message);
                    scale_cc_realtime = weightCC;
                    scale_cc_realtime_datetime = DateTime.Now;
                    if (weightCC > 500 && weightCC < 3000)
                    {
                        PlayDingDing();
                    }
                    if (scale_cc_balance > 0 && ((scale_cc_balance - weightCC) > 500))
                    {
                        this.resetControl_CC();
                        this.ResetScaleWeight(false);
                        //this.lblScaleMsg_CC.Text = $@"";
                    }
                    break;
                case "Scale_In_CN":
                    this.lblVehicle_CN.Text = $@"{message}";
                    this.lblScaleState_CN.Text = "ĐANG CÂN VÀO";
                    this.lblScaleMsg_CN.Text = "XE ĐANG VÀO CÂN";
                    this.LoadOrdersVehicleByVehicle(message, true);
                    break;
                case "Scale_Out_CN":
                    this.lblVehicle_CN.Text = $@"{message}";
                    this.lblScaleState_CN.Text = "ĐANG CÂN RA";
                    this.lblScaleMsg_CN.Text = "XE ĐANG VÀO CÂN";
                    this.LoadOrdersVehicleByVehicle(message, true);
                    break;
                case "Scale_In_CC":
                    this.lblVehicle_CC.Text = $@"{message}";
                    this.lblScaleState_CC.Text = "ĐANG CÂN VÀ0";
                    this.lblScaleMsg_CC.Text = "XE ĐANG VÀO CÂN";
                    this.LoadOrdersVehicleByVehicle(message, false);
                    break;
                case "Scale_Out_CC":
                    this.lblVehicle_CC.Text = $@"{message}";
                    this.lblScaleState_CC.Text = "ĐANG CÂN RA";
                    this.lblScaleMsg_CC.Text = "XE ĐANG VÀO CÂN";
                    this.LoadOrdersVehicleByVehicle(message, false);
                    break;
                case "Scale1_Balance":
                    scale_cn_balance = int.Parse(message);
                    this.lblScaleMsg_CN.Text = "SẴN SÀNG CÂN";
                    break;
                case "Scale2_Balance":
                    scale_cc_balance = int.Parse(message);
                    this.lblScaleMsg_CN.Text = "SẴN SÀNG CÂN";
                    break;
                case "Scale_CN_Warning":
                    this.lblScaleMsg_CN.Text = $@"{message}";
                    break;
                case "Scale_CC_Warning":
                    this.lblScaleMsg_CC.Text = $@"{message}";
                    break;
                case "Scale_CN_IN_Desision":
                    this.lblScaleMsg_CN.Text = $@"ĐÃ CÂN VÀO";
                    scale_cn_realtime_datetime = DateTime.Now;
                    this.LoadScaleWeight(true, this.lblVehicle_CN.Text.Trim());
                    break;
                case "Scale_CN_OUT_Desision":
                    this.lblScaleMsg_CN.Text = $@"ĐÃ CÂN RA";
                    scale_cn_realtime_datetime = DateTime.Now;
                    this.LoadScaleWeight(true, this.lblVehicle_CN.Text.Trim());
                    break;
                case "Scale_CC_IN_Desision":
                    this.lblScaleMsg_CC.Text = $@"ĐÃ CÂN VÀO";
                    scale_cc_realtime_datetime = DateTime.Now;
                    this.LoadScaleWeight(false, this.lblVehicle_CC.Text.Trim());
                    break;
                case "Scale_CC_OUT_Desision":
                    this.lblScaleMsg_CC.Text = $@"ĐÃ CÂN RA";
                    scale_cc_realtime_datetime = DateTime.Now;
                    this.LoadScaleWeight(false, this.lblVehicle_CC.Text.Trim());
                    break;
                case "Scale_Send_Successed":
                    //MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowPopupNotification("Thông báo", message, "INFO");
                    break;
                case "Scale_Send_Failed":
                    MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   // this.ShowPopupNotification("Thông báo", message, "ERROR");
                    break;
                //case "Scale_CN_Vehicle_Down":
                //    this.lblScaleMsg_CN.Text = $@"Xe ra";
                //    this.lblScaleMsg_CN.Refresh();
                //    break;
                //case "Scale_CC_Vehicle_Down":
                //    this.lblScaleMsg_CC.Text = $@"Xe ra";
                //    break;
                default:
                    break;
            }
        }
        #endregion
        private void ShowPopupNotification(string title, string message, string status)
        {
            PopupNotifier popup = new PopupNotifier();
            popup.TitleText = title;
            popup.ContentText = message;
            popup.TitleFont = new Font("Tahoma", 12F);
            popup.TitlePadding = new Padding(150, 0, 0, 0);
            popup.ContentFont = new Font("Tahoma", 10F);
            popup.ContentColor = Color.White;
            popup.TitleColor = Color.White;
            popup.Delay = 2000;
            popup.AnimationDuration = 1000;
            switch (status)
            {
               case "INFO":
                    popup.HeaderColor = Color.GreenYellow;
                    popup.BodyColor = Color.Green;
                    break;
                case "WARNING":
                    popup.HeaderColor = Color.OrangeRed;
                    popup.BodyColor = Color.Red;
                    break;
                case "ERROR":
                    popup.HeaderColor = Color.OrangeRed;
                    popup.BodyColor = Color.Red;
                    break;
                default:
                    break;
            }
            popup.Popup();
        }
        private void LoadScaleWeight(bool isCN, string vehicle)
        {
            try
            {
                int lb_trongluongbi = 0; // can va0
                int lb_trongluongtong = 0; // can ra
                int lb_trongluonghang = 0; // trong luong hang thuc te
                int lb_trongluongdat = 0; // trong luong dat hàng qua phiếu
                if (isCN)
                {
                    DataTable objTable = this.objBillOrder.getBillOrderByVehicleV2(vehicle);
                    if (objTable.Rows.Count > 0)
                    {
                        Int32.TryParse(objTable.Rows[0]["WeightIn"].ToString(), out lb_trongluongbi);
                        for (int i = 0; i < objTable.Rows.Count; i++)
                        {
                            var weightOutItem = 0;
                            var sumNumberItem = 0;
                            Int32.TryParse(objTable.Rows[i]["WeightOut"].ToString(), out weightOutItem);
                            Int32.TryParse(objTable.Rows[i]["SumNumber"].ToString().Replace(".0", ""), out sumNumberItem);

                            lb_trongluongtong += weightOutItem;
                            lb_trongluongdat += sumNumberItem * 1000;
                        }
                        lb_trongluonghang = lb_trongluongtong - lb_trongluongbi;
                    }
                    this.lb_trongluongbi_cn.Text = lb_trongluongbi.ToString();
                    this.lb_trongluongtong_cn.Text = lb_trongluongtong.ToString();
                    this.lb_trongluonghang_cn.Text = lb_trongluonghang.ToString();
                    this.lb_trongluongdat_cn.Text = lb_trongluongdat.ToString();
                }
                else
                {
                    DataTable objTableCN = this.objBillOrder.getBillOrderByVehicleV2(vehicle);
                    if (objTableCN.Rows.Count > 0)
                    {
                        Int32.TryParse(objTableCN.Rows[0]["WeightIn"].ToString(), out lb_trongluongbi);
                        for (int i = 0; i < objTableCN.Rows.Count; i++)
                        {
                            var weightOutItem = 0;
                            var sumNumberItem = 0;
                            Int32.TryParse(objTableCN.Rows[i]["WeightOut"].ToString(), out weightOutItem);
                            Int32.TryParse(objTableCN.Rows[i]["SumNumber"].ToString().Replace(".0", ""), out sumNumberItem);
                            lb_trongluongtong += weightOutItem;
                            lb_trongluongdat += sumNumberItem * 1000;
                        }
                        lb_trongluonghang = lb_trongluongtong - lb_trongluongbi;
                    }
                    this.lb_trongluongbi_cc.Text = lb_trongluongbi.ToString();
                    this.lb_trongluongtong_cc.Text = lb_trongluongtong.ToString();
                    this.lb_trongluonghang_cc.Text = lb_trongluonghang.ToString();
                    this.lb_trongluongdat_cc.Text = lb_trongluongdat.ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void ResetScaleWeight(bool isCN)
        {
            if (isCN)
            {
                this.lb_trongluongbi_cn.Text = "0";
                this.lb_trongluongtong_cn.Text = "0";
                this.lb_trongluonghang_cn.Text = "0";
                this.lb_trongluongdat_cn.Text = "0";
            }
            else
            {
                this.lb_trongluongbi_cc.Text = "0";
                this.lb_trongluongtong_cc.Text = "0";
                this.lb_trongluonghang_cc.Text = "0";
                this.lb_trongluongdat_cc.Text = "0";
            }
        }

        #region connect sensor
        public void ConnectSensor()
        {
            try
            {
                PLC_M221 = new PLCConnect();
                PLC_M221.Mode = Mode.TCP_IP;
                PLC_M221.ResponseTimeout = 1000;
                PLC_Result = PLC_M221.Connect("192.168.22.38", 502);
                if (PLC_Result == Result.SUCCESS)
                {
                    ProcessSensor();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connect Sensor Failed", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion connect sensor


        #region call ting ting
        public void WaitPlay(WMPLib.WindowsMediaPlayer wplayer, int duration)
        {
            Thread.Yield();
            while (wplayer.playState == WMPLib.WMPPlayState.wmppsTransitioning)
            {
                Application.DoEvents();
                Thread.Yield();
            }
            Thread.Sleep(duration);
        }
        public void PlayDingDing()
        {
            try
            {
               // var PathAudioLib = $@"./AudioNormal";
                var PathAudioLib = $@"D://AudioNormal";
                string dingpath = $@"{PathAudioLib}/audio_generer/ding.wav";
                WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                wplayer.URL = dingpath;
                wplayer.controls.play();
                WaitPlay(wplayer, 1000);
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region remove when vehicle out scale
        private void resetScaleJob()
        {
            try
            {
                var threadReset = new Thread(t =>
                {
                    while (1 < 2)
                    {
                        while (!this.IsHandleCreated)
                            System.Threading.Thread.Sleep(1000);
                        this.Invoke((MethodInvoker)delegate
                        {

                            if (scale_cn_realtime_datetime.AddSeconds(10) < DateTime.Now )//&& this.lblVehicle_CN.Text?.Trim() == "")
                            {
                                this.lbNoti_CN.Text = "0";
                                this.lblScaleMsg_CN.Text = $@"";
                                this.ResetScaleWeight(true);
                            }
                            if (scale_cc_realtime_datetime.AddSeconds(10) < DateTime.Now) // && this.lblVehicle_CC.Text?.Trim() == "")
                            {
                                this.lbNoti_CC.Text = "0";
                                this.lblScaleMsg_CC.Text = $@"";
                                this.ResetScaleWeight(false);
                            }
                        });
                        Thread.Sleep(1000);
                    }
                })
                { IsBackground = true };
                threadReset.Start();
            }
            catch (Exception)
            {
            }
        }
        private void resetScaleJobProcess()
        {
            try
            {
                while (true)
                {
                    this.Invoke((MethodInvoker)delegate
                    {

                        if (scale_cn_realtime_datetime.AddSeconds(3) < DateTime.Now && this.lblVehicle_CN.Text.Trim() == "")
                        {
                            this.lbNoti_CN.Text = "0";
                            this.lblScaleMsg_CN.Text = $@"";
                        }
                        if (scale_cc_realtime_datetime.AddSeconds(3) < DateTime.Now && this.lblVehicle_CC.Text.Trim() == "")
                        {
                            this.lbNoti_CC.Text = "0";
                            this.lblScaleMsg_CC.Text = $@"";
                        }
                    });
                    Thread.Sleep(1000);
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region process connect sensor
        private async Task ProcessSensor()
        {
            try
            {
                var threadSensor = new Thread(t =>
                {
                    while (1 < 2)
                    {
                        while (!this.IsHandleCreated)
                            System.Threading.Thread.Sleep(3000);
                        this.Invoke((MethodInvoker)delegate
                        {
                            if (!CheckPortIsOn(byte.Parse("0")))
                            {
                                this.lbsensor_cc_in.BackColor = Color.Red;
                            }
                            else
                            {
                                this.lbsensor_cc_in.BackColor = Color.White;
                            }

                            if (!CheckPortIsOn(byte.Parse("1")))
                            {
                                this.lbsensor_cc_vertical1.BackColor = Color.Red;
                            }
                            else
                            {
                                this.lbsensor_cc_vertical1.BackColor = Color.White;
                            }

                            if (!CheckPortIsOn(byte.Parse("3")))
                            {
                                this.lbsensor_cc_vertical2.BackColor = Color.Red;
                            }
                            else
                            {
                                this.lbsensor_cc_vertical2.BackColor = Color.White;
                            }

                            if (!CheckPortIsOn(byte.Parse("2")))
                            {
                                this.lbsensor_cc_out.BackColor = Color.Red;
                            }
                            else
                            {
                                this.lbsensor_cc_out.BackColor = Color.White;
                            }

                            if (!CheckPortIsOn(byte.Parse("4")))
                            {
                                this.lbsensor_cn_in.BackColor = Color.Red;
                            }
                            else
                            {
                                this.lbsensor_cn_in.BackColor = Color.White;
                            }

                            if (!CheckPortIsOn(byte.Parse("5")))
                            {
                                this.lbsensor_cn_out.BackColor = Color.Red;
                            }
                            else
                            {
                                this.lbsensor_cn_out.BackColor = Color.White;
                            }
                            if (CheckPortIsOn(byte.Parse("8"))) // đóng barrier cn
                            {
                                this.lblCN_Barie_Vao.BackColor = Color.Red;
                            }
                            else
                            {
                                this.lblCN_Barie_Vao.BackColor = Color.Orange;
                            }
                            if (CheckPortIsOn(byte.Parse("9"))) // đóng barrier cn
                            {
                                this.lblCN_Barie_Ra.BackColor = Color.Red;
                            }
                            else
                            {
                                this.lblCN_Barie_Ra.BackColor = Color.Orange;
                            }

                            if (CheckPortIsOn(byte.Parse("6"))) // đóng barrier cc
                            {
                                this.lblCC_Barie_Vao.BackColor = Color.Red;
                            }
                            else
                            {
                                this.lblCC_Barie_Vao.BackColor = Color.Orange;
                            }

                            if (CheckPortIsOn(byte.Parse("7"))) // đóng barrier cc
                            {
                                this.lblCC_Barie_Ra.BackColor = Color.Red;
                            }
                            else
                            {
                                this.lblCC_Barie_Ra.BackColor = Color.Orange;
                            }

                        });
                        Thread.Sleep(1000);
                    }
                })
                { IsBackground = true };
                threadSensor.Start();
            }
            catch (Exception)
            {
            }
        }
        private bool CheckPortIsOn(byte k)
        {
            bool isOn = false;
            try
            {

                bool[] Ports = new bool[24];
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
        #endregion


        public void LoadOrdersVehicleByVehicle(string vehicle, bool isScaleCN)
        {
            try
            {
                DataTable objTable = this.objBillOrder.getBillOrderByVehicleV2(vehicle.Trim());
                DataTable objTableTonnageDefault = this.objBillOrder.getTonnageDefaultByDeliveryCode(vehicle.Trim());
               
                if (isScaleCN)
                {
                    ResetScaleAuto(true);
                    ClearOrderCN();
                    if (objTable.Rows.Count > 0)
                    {
                        this.lblMooc_CN.Text = objTable.Rows[0]["MoocCode"].ToString();
                        this.txtIDOrderSyn_CN.Text = objTable.Rows[0]["DeliveryCode"].ToString();
                        this.txtNameProduct_CN.Text = objTable.Rows[0]["NameProduct"].ToString();
                        this.txtSumNumber_CN.Text = objTable.Rows[0]["SumNumber"].ToString();
                        this.txtKhoiLuongBi_CN.Text = objTableTonnageDefault.Rows.Count > 0 ? objTableTonnageDefault.Rows[0]["TonnageDefault"]?.ToString() : "";
                        this.txtDriverName_CN.Text = objTable.Rows[0]["DriverName"].ToString();

                        if (objTable.Rows.Count > 1)
                        {
                            this.txtIDOrderSyn1_CN.Text = objTable.Rows[1]["DeliveryCode"].ToString();
                            this.txtNameProduct1_CN.Text = objTable.Rows[1]["NameProduct"].ToString();
                            this.txtSumNumber1_CN.Text = objTable.Rows[1]["SumNumber"].ToString();
                        }

                        if (objTable.Rows.Count > 2)
                        {
                            this.txtIDOrderSyn2_CN.Text = objTable.Rows[2]["DeliveryCode"].ToString();
                            this.txtNameProduct2_CN.Text = objTable.Rows[2]["NameProduct"].ToString();
                            this.txtSumNumber2_CN.Text = objTable.Rows[2]["SumNumber"].ToString();
                        }
                    }
                }
                else
                {
                    ResetScaleAuto(false);
                    ClearOrderCC();
                    if (objTable.Rows.Count > 0)
                    {
                        this.lblMooc_CC.Text = objTable.Rows[0]["MoocCode"].ToString();
                        this.txtIDOrderSyn_CC.Text = objTable.Rows[0]["DeliveryCode"].ToString();
                        this.txtNameProduct_CC.Text = objTable.Rows[0]["NameProduct"].ToString();
                        this.txtSumNumber_CC.Text = objTable.Rows[0]["SumNumber"].ToString();
                        this.txtKhoiLuongBi_CC.Text = objTableTonnageDefault.Rows.Count > 0 ? objTableTonnageDefault.Rows[0]["TonnageDefault"]?.ToString() : "";
                        this.txtDriverName_CC.Text = objTable.Rows[0]["DriverName"].ToString();

                        if (objTable.Rows.Count > 1)
                        {
                            this.txtIDOrderSyn1_CC.Text = objTable.Rows[1]["DeliveryCode"].ToString();
                            this.txtNameProduct1_CC.Text = objTable.Rows[1]["NameProduct"].ToString();
                            this.txtSumNumber1_CC.Text = objTable.Rows[1]["SumNumber"].ToString();
                        }

                        if (objTable.Rows.Count > 2)
                        {
                            this.txtIDOrderSyn2_CC.Text = objTable.Rows[2]["DeliveryCode"].ToString();
                            this.txtNameProduct2_CC.Text = objTable.Rows[2]["NameProduct"].ToString();
                            this.txtSumNumber2_CC.Text = objTable.Rows[2]["SumNumber"].ToString();
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("Xử lý lấy đơn hàng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ClearOrderCN()
        {
            try
            {

                this.lblMooc_CN.Text = "";
                this.txtIDOrderSyn_CN.Text = "";
                this.txtNameProduct_CN.Text = "";
                this.txtSumNumber_CN.Text = "";
                this.txtKhoiLuongBi_CN.Text = "";
                this.txtDriverName_CN.Text = "";

                 this.txtIDOrderSyn1_CN.Text = "";
                this.txtNameProduct1_CN.Text = "";
                this.txtSumNumber1_CN.Text = "";

                this.txtIDOrderSyn2_CN.Text = "";
                this.txtNameProduct2_CN.Text = "";
                this.txtSumNumber2_CN.Text = "";

            }
            catch (Exception ex)
            {

            }
        }
        public void ClearOrderCC()
        {
            try
            {

                this.lblMooc_CC.Text = "";
                this.txtIDOrderSyn_CC.Text = "";
                this.txtNameProduct_CC.Text = "";
                this.txtSumNumber_CC.Text = "";
                this.txtKhoiLuongBi_CC.Text = "";
                this.txtDriverName_CC.Text = "";

                this.txtIDOrderSyn1_CC.Text = "";
                this.txtNameProduct1_CC.Text = "";
                this.txtSumNumber1_CC.Text = "";

                this.txtIDOrderSyn2_CC.Text = "";
                this.txtNameProduct2_CC.Text = "";
                this.txtSumNumber2_CC.Text = "";

            }
            catch (Exception ex)
            {

            }
        }
        #region method SetTextArea
        private void SetTextArea(string text)
        {
            if (this.label1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTextArea);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.label1.Text = text;
            }
        }
        #endregion

        #region method btnStart_Click
        private void btnStart_Click(object sender, EventArgs e)
        {
            //this.btnStart.Enabled = false;
            //this.btnStop.Enabled = true;
            //threadArea = new Thread(t =>
            //{
            //    while (1 > 0)
            //    {
            //        this.Invoke((MethodInvoker)delegate
            //        {
            //           this.getData();

            //        });

            //        this.SetTextArea(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ", " + this.dgvListBillOrder.RowCount.ToString() + " đơn hàng");
            //        Thread.Sleep(this.TimeSleep);
            //    }
            //})
            //{ IsBackground = true };
            //threadArea.Start();
        }
        #endregion

        #region method btnStop_Click
        private void btnStop_Click(object sender, EventArgs e)
        {
            this.threadArea.Abort();
            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;
        }
        #endregion

        #region method frmTaskScale_Load
        private void frmTaskScale_Load(object sender, EventArgs e)
        {
            this.cbbTypeProduct.SelectedIndex = 0;
            this.cbbStep.SelectedIndex = 0;
            this.btnStart.PerformClick();
            this.txtSearchKey.Focus();

            //this.LoadInit();
        }
        #endregion

        private void LoadInit()
        {
            try
            {
                var orderScales = new List<tblScaleOperating>();
                using (var db = new HMXuathangtudong_Entities())
                {
                    orderScales = db.tblScaleOperatings.ToList();
                }
                foreach (var orderScale in orderScales)
                {
                    if (String.IsNullOrEmpty(orderScale.Vehicle)) continue;
                    if (orderScale.ScaleCode == "CC")
                    {
                        this.lblVehicle_CC.Text = $@"{orderScale.Vehicle}"; 
                        this.LoadOrdersVehicleByVehicle(orderScale.Vehicle, false);
                        LoadScaleWeight(false, orderScale.Vehicle);
                    }
                    else
                    {
                        this.lblVehicle_CC.Text = $@"{orderScale.Vehicle}"; 
                        this.LoadOrdersVehicleByVehicle(orderScale.Vehicle, true);
                        LoadScaleWeight(true, orderScale.Vehicle);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        #region method getData
        private void getData()
        {
            this.dgvListBillOrder.AutoGenerateColumns = false;
            this.dgvListBillOrder.EnableHeadersVisualStyles = false;
            this.dgvListBillOrder.DataSource = this.objBillOrder.getBillOrderScale(this.txtSearchKey.Text, this.cbbTypeProduct.Text, this.cbbStep.Text);

            this.lblCountItem.Text = this.dgvListBillOrder.RowCount.ToString();
            this.txtSearchKey.Focus();
        }
        #endregion

        #region method frmTaskScale_KeyDown
        private void frmTaskScale_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region method frmTaskScale_FormClosing
        private void frmTaskScale_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.threadArea.IsAlive)
                {
                    this.threadArea.Abort();
                }
            }
            catch
            {

            }
        }
        #endregion

        #region method btnConfirmByQRCode_Click
        private void btnConfirmByQRCode_Click(object sender, EventArgs e)
        {
            //if (this.txtQRCode.Text.Trim() != "")
            //{
            //    if (this.objBillOrder.setBillOrderOperatingByDeliveryCode(this.txtQRCode.Text.Trim(), 2, 2) > 0)
            //    {
            //        this.getData();
            //        this.txtQRCode.Text = "";
            //        this.txtQRCode.Focus();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Xác thực thất bại, vui lòng kiểm tra lại thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        this.txtQRCode.Text = "";
            //        this.txtQRCode.Focus();
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Bạn chưa đọc mã QR-Code", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.txtQRCode.Focus();
            //}
        }
        #endregion

        #region method txtQRCode_Enter
        private void txtQRCode_Enter(object sender, EventArgs e)
        {
            this.txtQRCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(157)))), ((int)(((byte)(107)))));
        }

        private void btnBarier_Click(object sender, EventArgs e)
        {
            frmTaskScaleBarie objfrmTaskScaleBarie = new frmTaskScaleBarie();
            objfrmTaskScaleBarie.ShowDialog();
        }
        #endregion

        #region method txtQRCode_Leave
        private void txtQRCode_Leave(object sender, EventArgs e)
        {
            this.txtQRCode.BackColor = Color.White;
        }
        #endregion

        #region method txtQRCode_KeyDown
        private void txtQRCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnConfirmByQRCode.PerformClick();
            }
        }
        #endregion

        #region method cbbTypeProduct_SelectedIndexChanged
        private void cbbTypeProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.getData();
        }
        #endregion

        #region method cbbStep_SelectedIndexChanged
        private void cbbStep_SelectedIndexChanged(object sender, EventArgs e)
        {
           // this.getData();
        }
        #endregion

        #region method txtSearchKey_KeyDown
        private void txtSearchKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               // this.getData();
            }
        }
        #endregion

        #region method txtVehicle_TextChanged
        private void txtVehicle_TextChanged(object sender, EventArgs e)
        {
            //if (this.lblVehicle_CN.Text.Trim() != "")
            //{
            //    if (this.txtIDOrderSyn_CN.Text.Trim() != "")
            //    {
            //        return;
            //    }
            //    DataTable objTable = this.objBillOrder.getBillOrderByVehicleV2(this.lblVehicle_CN.Text.Trim());
            //    if (objTable.Rows.Count > 0)
            //    {
            //        this.txtIDOrderSyn_CN.Text = objTable.Rows[0]["DeliveryCode"].ToString();
            //        this.txtNameProduct_CN.Text = objTable.Rows[0]["NameProduct"].ToString();
            //        this.txtSumNumber_CN.Text = objTable.Rows[0]["SumNumber"].ToString();

            //        this.txtDriverName_CN.Text = objTable.Rows[0]["DriverName"].ToString();

            //        if (objTable.Rows.Count > 1)
            //        {
            //            this.txtIDOrderSyn1_CN.Text = objTable.Rows[1]["DeliveryCode"].ToString();
            //            this.txtNameProduct1_CN.Text = objTable.Rows[1]["NameProduct"].ToString();
            //            this.txtSumNumber1_CN.Text = objTable.Rows[1]["SumNumber"].ToString();
            //        }

            //        if (objTable.Rows.Count > 2)
            //        {
            //            this.txtIDOrderSyn2_CN.Text = objTable.Rows[2]["DeliveryCode"].ToString();
            //            this.txtNameProduct2_CN.Text = objTable.Rows[2]["NameProduct"].ToString();
            //            this.txtSumNumber2_CN.Text = objTable.Rows[2]["SumNumber"].ToString();
            //        }
            //    }
            //}
            //else
            //{
            //    this.txtIDOrderSyn_CN.Text = "";
            //    this.txtNameProduct_CN.Text = "";
            //    this.txtSumNumber_CN.Text = "";

            //    this.txtIDOrderSyn1_CN.Text = "";
            //    this.txtNameProduct1_CN.Text = "";
            //    this.txtSumNumber1_CN.Text = "";

            //    this.txtIDOrderSyn2_CN.Text = "";
            //    this.txtNameProduct2_CN.Text = "";
            //    this.txtSumNumber2_CN.Text = "";
            //}
        }
        #endregion

        private void txtVehicle_CN_TextChanged(object sender, EventArgs e)
        {
            //if (this.txtVehicle_CN.Text.Trim() != "")
            //{
            //    if (this.lblScaleMsg_CN.Text == "Không xác định được biển số")
            //    {
            //        this.lblScaleMsg_CN.Text = "";
            //    }
            //    if (this.txtIDOrderSyn_CN.Text.Trim() != "")
            //    {
            //        return;
            //    }
            //    DataTable objTable = this.objBillOrder.getBillOrderByVehicleV2(this.txtVehicle_CN.Text.Trim());
            //    if (objTable.Rows.Count > 0)
            //    {
            //        this.txtIDOrderSyn_CN.Text = objTable.Rows[0]["DeliveryCode"].ToString();
            //        this.txtNameProduct_CN.Text = objTable.Rows[0]["NameProduct"].ToString();
            //        this.txtSumNumber_CN.Text = objTable.Rows[0]["SumNumber"].ToString();

            //        this.txtDriverName_CN.Text = objTable.Rows[0]["DriverName"].ToString();

            //        if (objTable.Rows.Count > 1)
            //        {
            //            this.txtIDOrderSyn1_CN.Text = objTable.Rows[1]["DeliveryCode"].ToString();
            //            this.txtNameProduct1_CN.Text = objTable.Rows[1]["NameProduct"].ToString();
            //            this.txtSumNumber1_CN.Text = objTable.Rows[1]["SumNumber"].ToString();
            //        }

            //        if (objTable.Rows.Count > 2)
            //        {
            //            this.txtIDOrderSyn2_CN.Text = objTable.Rows[2]["DeliveryCode"].ToString();
            //            this.txtNameProduct2_CN.Text = objTable.Rows[2]["NameProduct"].ToString();
            //            this.txtSumNumber2_CN.Text = objTable.Rows[2]["SumNumber"].ToString();
            //        }
            //    }
            //}
            //else
            //{
            //    this.txtIDOrderSyn_CN.Text = "";
            //    this.txtNameProduct_CN.Text = "";
            //    this.txtSumNumber_CN.Text = "";

            //    this.txtIDOrderSyn1_CN.Text = "";
            //    this.txtNameProduct1_CN.Text = "";
            //    this.txtSumNumber1_CN.Text = "";

            //    this.txtIDOrderSyn2_CN.Text = "";
            //    this.txtNameProduct2_CN.Text = "";
            //    this.txtSumNumber2_CN.Text = "";
            //}
        }

        private void resetControl_CN()
        {
            this.txtDriverName_CN.Text = "";
            this.txtKhoiLuongBi_CN.Text = "";
            this.txtKhoiLuongHang_CN.Text = "";
            this.txtKhoiLuongBi_CN.Text = "";

            this.txtIDOrderSyn_CN.Text = "";
            this.txtNameProduct_CN.Text = "";
            this.txtSumNumber_CN.Text = "";

            this.txtIDOrderSyn1_CN.Text = "";
            this.txtNameProduct1_CN.Text = "";
            this.txtSumNumber1_CN.Text = "";

            this.txtIDOrderSyn2_CN.Text = "";
            this.txtNameProduct2_CN.Text = "";
            this.txtSumNumber2_CN.Text = "";

            //this.lblScaleMsg_CN.Text = "";
            this.lblScaleState_CN.Text = "";

            this.lbNoti_CN.Text = "";
            this.lblVehicle_CN.Text = "";
            this.lblMooc_CN.Text = "";
            scale_cn_balance = 0;
        }
        private void resetControl_CC()
        {
            this.txtDriverName_CC.Text = "";
            this.txtKhoiLuongBi_CC.Text = "";
            this.txtKhoiLuongHang_CC.Text = "";
            this.txtKhoiLuongBi_CN.Text = "";

            this.txtIDOrderSyn_CC.Text = "";
            this.txtNameProduct_CC.Text = "";
            this.txtSumNumber_CC.Text = "";

            this.txtIDOrderSyn1_CC.Text = "";
            this.txtNameProduct1_CC.Text = "";
            this.txtSumNumber1_CC.Text = "";

            this.txtIDOrderSyn2_CC.Text = "";
            this.txtNameProduct2_CC.Text = "";
            this.txtSumNumber2_CC.Text = "";

           // this.lblScaleMsg_CC.Text = "";
            this.lblScaleState_CC.Text = "";

            this.lbNoti_CC.Text = "";
            this.lblVehicle_CC.Text = "";
            this.lblMooc_CC.Text = "";
            scale_cc_balance = 0;
        }

        private void tabControl1_SizeChanged(object sender, EventArgs e)
        {
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
        private void ResetScaleAuto(bool isCN)
        {
            if (isCN)
            {
                is_cn_auto = true;
                this.lb_scale_auto_cn.Text = "Đang cân tự động";
            }
            else
            {
                is_cc_auto = true;
                this.lb_scale_auto_cc.Text = "Đang cân tự động";
            }
        }
        private bool SetScaleAuto(bool isCN, bool isAuto)
        {
            bool isSet = false;
            try
            {
                var vehicle = isCN ? this.lblVehicle_CN.Text.Trim() : this.lblVehicle_CC.Text.Trim();
                if (this.objBillOrder.UpdateAutoScale(vehicle, isAuto) > 0)
                {
                    MessageBox.Show("Đã ngắt cân tự động cho đơn hàng hiện tại trên xe", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isSet = true;
                }
                else
                {
                    MessageBox.Show("Ngắt cân tự động không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isSet = false;
                }
            }
            catch (Exception ex)
            {

            }
            return isSet;
        }
        private void btnScale_CN_Auto_Click(object sender, EventArgs e)
        {
            if(this.txtDriverName_CN.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhận diện được xe vào để cấu hình cân tự động", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (is_cn_auto)
            {
                if (this.SetScaleAuto(true, false))
                {
                    is_cn_auto = false;
                    this.lb_scale_auto_cn.Text = "Đang hủy cân tự động";
                }
            }
            else
            {
                if (this.SetScaleAuto(true, true))
                {
                    is_cn_auto = true;
                    this.lb_scale_auto_cn.Text = "Đang cân tự động";
                }
                
            }
            
        }

        private void btnScale_CC_Auto_Click(object sender, EventArgs e)
        {
            if (this.txtDriverName_CC.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhận diện được xe vào để cấu hình cân tự động", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (is_cc_auto)
            {
                if (this.SetScaleAuto(false, false))
                {
                    is_cc_auto = false;
                    this.lb_scale_auto_cc.Text = "Đang hủy cân tự động";
                }
               
            }
            else
            {
                if (this.SetScaleAuto(false, true))
                {
                    is_cc_auto = true;
                    this.lb_scale_auto_cc.Text = "Đang cân tự động";
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void btnScale1_Auto_Click(object sender, EventArgs e)
        {
           // this.ShowPopupNotification("Thông báo","Cân thành công", "INFO");
        }

        private void btnScale2_Auto_Click(object sender, EventArgs e)
        {
            //this.ShowPopupNotification("Thông báo", "Không thể cân bì lần 2", "ERROR");
        }


        private void btnBarrierCN_Open_Click(object sender, EventArgs e)
        {
            new BarrierScaleBusiness().OpenBarrierScale(true);
        }

        private void btnBarrierCC_Open_Click(object sender, EventArgs e)
        {
            new BarrierScaleBusiness().OpenBarrierScale(false);
        }

        private void btnFilterScaleIn_Click(object sender, EventArgs e)
        {
            this.dgvListBillScale.AutoGenerateColumns = false;
            this.dgvListBillScale.EnableHeadersVisualStyles = false;
            this.dgvListBillScale.DataSource = this.objBillOrder.getBillOrderScaleLastest("scale_in");
        }

        private void btnFilterScaleOut_Click(object sender, EventArgs e)
        {
            this.dgvListBillScale.AutoGenerateColumns = false;
            this.dgvListBillScale.EnableHeadersVisualStyles = false;
            this.dgvListBillScale.DataSource = this.objBillOrder.getBillOrderScaleLastest("scale_out");
        }

        private void tabPage1_Resize(object sender, EventArgs e)
        {
        }

        private void frmTaskScale_Resize(object sender, EventArgs e)
        {
        }
    }
}
