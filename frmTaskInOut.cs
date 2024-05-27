using HMXHTD.Core;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMXHTD
{
    public partial class frmTaskInOut : Form
    {
        #region declare objects
        private IHubProxy HubProxy { get; set; }
        private HubConnection Connection { get; set; }
        private Thread threadData;
        private Thread threadVoice;
        private Fun objFun = new Fun();
        delegate void SetTextCallback(string text);
        private int TimeSleep = 30000, TimeSleepVoice1 = 10000, TimeSleepVoice2 = 5000, TimeSleepVoice3 = 8000, OrderId = 0, StepId = -1;
        private BillOrder objBillOrder = new BillOrder();
        private IntPtr h = IntPtr.Zero;
        private DataTable TableInfo;
        private int operID, doorOrAuxoutID, outputAddrType, doorAction;
        private string ipaddress = "", port = "";

        //Xác thực cổng bảo vệ số 3, vào cổng - RFID21
        private Thread threadInOut21;
        private string InOut21Config = "";
        public bool InOut21 = true;
        int TimeSleepInOut21 = 2000;
        private DataTable objTableInOut21 = new DataTable();
        private IntPtr h21 = IntPtr.Zero;

        private Dictionary<string, string> objListCardVehicle = new Dictionary<string, string>();
        private RFID objRfid = new RFID();
        #endregion
       
        #region method frmTaskInOut
        public frmTaskInOut()
        {
            InitializeComponent();            
            ConnectAsync();
        }
        #endregion

        #region method SignalR
        private async void ConnectAsync()
        {
            Connection = new HubConnection(ConfigurationManager.AppSettings.Get("singalRHost").ToString());
            HubProxy = Connection.CreateHubProxy(ConfigurationManager.AppSettings.Get("hubSignalR").ToString());
            HubProxy.On<string, string>(ConfigurationManager.AppSettings.Get("methodSendMessage").ToString(), (name, message) =>
                this.Invoke((Action)(() =>
                    ProcessNotification(name, message)
                ))
            );
            try
            {
                await Connection.Start();
            }
            catch (HttpRequestException ex)
            {
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
                    this.getData();
                    break;
                case "Step_2":
                    this.showNotificationAsync(1, message);
                    this.getData();
                    
                    break;
                case "Step_3":
                    this.getData();
                    break;
                case "Step_4":
                    this.getData();
                    break;
                case "Step_5":
                    break;
                case "Step_6":
                    break;
                case "Step_7":
                    this.getData();
                    break;
                case "Step_8":
                    this.showNotificationAsync(2, message);
                    this.getData();
                    
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region method showNotificationAsync
        public async Task showNotificationAsync(int type, string message)
        {
            if(type == 1)
            {
                //this.btnBarier.Visible = true;
                //this.dgvListBillOrder.Enabled = false;
                this.lblVehicleIn.Text = message;
                await Task.Delay(30000);
                this.lblVehicleIn.Text = "CỔNG VÀO";
                //this.dgvListBillOrder.Enabled = true;
                //this.btnBarier.Visible = false;
            }
            else
            {
                //this.btnBarier.Visible = true;
                //this.dgvListBillOrder.Enabled = false;
                this.lblVehicleOut.Text = message;
                await Task.Delay(30000);
                this.lblVehicleOut.Text = "CỔNG RA";
                //this.dgvListBillOrder.Enabled = true;
                //this.btnBarier.Visible = false;
            }
        }
        #endregion

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
            try
            {
                this.btnStart.Enabled = false;
                this.btnStop.Enabled = true;
                threadData = new Thread(t =>
                {
                    while (1 > 0)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.getData();
                        });

                        this.SetTextArea(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ", " + this.dgvListBillOrder.RowCount.ToString() + " đơn hàng");
                        Thread.Sleep(this.TimeSleep);
                    }
                }) { IsBackground = true };
                threadData.Start();
            }
            catch
            {

            }
        }
        #endregion

        #region method btnStop_Click
        private void btnStop_Click(object sender, EventArgs e)
        {
            this.threadData.Abort();
            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;
        }
        #endregion

        #region method frmTaskInOut_Load
        private void frmTaskInOut_Load(object sender, EventArgs e)
        {
            this.cbbTypeProduct.SelectedIndex = 0;
            this.cbbStep.SelectedIndex = 0;
            this.btnStart.PerformClick();
            this.txtQRCode.Focus();
        }
        #endregion

        #region method dgvListBillOrder_CellClick
        private void dgvListBillOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        #endregion

        #region method getData
        private void getData()
        {
            try
            {
                this.dgvListBillOrder.AutoGenerateColumns = false;
                this.dgvListBillOrder.EnableHeadersVisualStyles = false;

                DataTable objTable = this.objBillOrder.getBillOrderInOut(this.txtSearchKey.Text, this.cbbTypeProduct.Text, this.cbbStep.Text);
                objTable.DefaultView.Sort = "tmpOrder ASC";
                this.dgvListBillOrder.DataSource = objTable;

                this.lblCountItem.Text = this.dgvListBillOrder.RowCount.ToString();
                foreach (DataGridViewRow Myrow in dgvListBillOrder.Rows)
                {
                    if (Myrow.Cells["dgvListBillOrderWarningNotCall"].Value.ToString() == "True")
                    {
                        Myrow.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
            catch
            {
                
            }
            finally
            {

            }
        }
        #endregion

        #region method dgvListBillOrder_CellMouseDoubleClick
        private void dgvListBillOrder_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 3 || e.ColumnIndex == 4)
            {
                frmVehicleBillOrder objfrmVehicleBillOrder = new frmVehicleBillOrder();
                objfrmVehicleBillOrder.Vehicle = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderVehicle"].Value.ToString();
                objfrmVehicleBillOrder.ShowDialog();
                if (objfrmVehicleBillOrder.TotalItem > 0)
                {
                    this.getData();
                }
            }
        }
        #endregion

        #region method frmTaskInOut_KeyDown
        private void frmTaskInOut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region method frmTaskInOut_FormClosing
        private void frmTaskInOut_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.threadData.IsAlive)
                {
                    this.threadData.Abort();
                }
            }
            catch
            {

            }

            try
            {
                if (this.threadVoice.IsAlive)
                {
                    this.threadVoice.Abort();
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
            if (this.txtQRCode.Text.Trim() != "")
            {
                //if (this.objBillOrder.setBillOrderOperatingByDeliveryCode(this.txtQRCode.Text.Trim(), 2, 1) > 0)
                //{
                //    this.getData();
                //    this.txtQRCode.Text = "";
                //    this.txtQRCode.Focus();
                //}
                //else
                //{
                //    MessageBox.Show("Xác thực thất bại, vui lòng kiểm tra lại thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    this.txtQRCode.Text = "";
                //    this.txtQRCode.Focus();
                //}
            }
            else
            {
                MessageBox.Show("Bạn chưa đọc mã QR-Code", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtQRCode.Focus();
            }
        }
        #endregion

        #region method txtQRCode_Enter
        private void txtQRCode_Enter(object sender, EventArgs e)
        {
            this.txtQRCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(157)))), ((int)(((byte)(107)))));
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

        #region method btnCallDriver_Click
        private void btnCallDriver_Click(object sender, EventArgs e)
        {
            if (this.txtVehicle.Text.Trim() == "")
            {
                return;
            }
            this.CallBySystem(this.txtVehicle.Text);
            //this.CallVehicle(this.txtVehicle.Text);
        }

        private void dgvListBillOrder_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
                //if (this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderState1"].Value.ToString() == "Đang lấy hàng")
                //{
                //    this.dgvListBillOrder.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gold;
                //}

            }
            catch
            {
            }
        }
        #endregion

        #region method btnBarier_Click
        private void btnBarier_Click(object sender, EventArgs e)
        {
            frmTaskInOutBarie objfrmTaskInOutBarie = new frmTaskInOutBarie();
            objfrmTaskInOutBarie.ShowDialog();
            
        }
        #endregion

        #region method btnVehicle_Click
        private void btnVehicle_Click(object sender, EventArgs e)
        {
            frmVehicleAdd objVehicleAdd = new frmVehicleAdd();
            objVehicleAdd.ShowDialog();
        }
        #endregion

        #region method CallBySystem
        public void WaitPlay(WMPLib.WindowsMediaPlayer wplayer, int duration)
        {
            Thread.Yield();
            while (wplayer.playState == WMPLib.WMPPlayState.wmppsTransitioning)
            {
                Application.DoEvents();
                Thread.Yield();
            }
            //int duration = Convert.ToInt32(wplayer.currentMedia.duration * 1000);
            Thread.Sleep(duration);
        }
        public void CallBySystem(string vehicle)
        {
            try
            {
               // var PathAudioLib = $@"D:/Projects/VICEM/HOANGMAI/XHTD-FULL/xuathangtudong/bin/Debug/AudioNormal";
                var PathAudioLib = $@"./AudioNormal";
                string VoiceFileStart = $@"{PathAudioLib}/audio_generer/VicemBegin.wav";
                string VoiceFileInvite = $@"{PathAudioLib}/audio_generer/moixe.wav";
                string VoiceFileInOut = $@"{PathAudioLib}/audio_generer/vaonhanhang.wav";
                string VoiceFileEnd = $@"{PathAudioLib}/audio_generer/VicemEnd.wav";
                WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                wplayer.URL = VoiceFileStart;
                wplayer.controls.play();
                WaitPlay(wplayer, 5000);

                wplayer.URL = VoiceFileInvite;
                wplayer.controls.play();
                WaitPlay(wplayer, 1000);
                var count = 0;
                foreach (char c in vehicle)
                {
                    count++;
                    wplayer.URL = $@"{PathAudioLib}/{c}.wav";
                    wplayer.controls.play();
                    if (count < 3)
                    {
                        WaitPlay(wplayer, 500);
                    }
                    else if (count == 3)
                    {
                        WaitPlay(wplayer, 1200);
                    }
                    else
                    {
                        WaitPlay(wplayer, 500);
                    }
                }

                wplayer.URL = VoiceFileInOut;
                wplayer.controls.play();
                WaitPlay(wplayer, 2000);

                wplayer.URL = VoiceFileEnd;
                wplayer.controls.play();

            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region method frmTaskInOut_Shown
        private void frmTaskInOut_Shown(object sender, EventArgs e)
        {
            this.lblVehicleIn.Width = (this.Width / 2) - 10;

            this.lblVehicleOut.Location = new Point( (this.Width / 2) + 10, this.lblVehicleOut.Location.Y);
            this.lblVehicleOut.Width = (this.Width / 2) - 16;
        }
        #endregion

        #region method txtSearchKey_KeyDown
        private void txtSearchKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.getData();
            }
        }
        #endregion

        #region method cbbTypeProduct_SelectedIndexChanged
        private void cbbTypeProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.getData();
        }
        #endregion

        #region method cbbStep_SelectedIndexChanged
        private void cbbStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.getData();
        }
        #endregion

        #region method txtVehicle_TextChanged
        private void txtVehicle_TextChanged(object sender, EventArgs e)
        {
            if (this.txtVehicle.Text.Trim() != "")
            {
                if (this.txtIDOrderSyn.Text.Trim() != "")
                {
                    return;
                }
                DataTable objTable = this.objBillOrder.getBillOrderByVehicleV2(this.txtVehicle.Text.Trim());
                if (objTable.Rows.Count > 0)
                {
                    this.txtIDOrderSyn.Text = objTable.Rows[0]["DeliveryCode"].ToString();
                    this.txtNameProduct.Text = objTable.Rows[0]["NameProduct"].ToString();
                    this.txtSumNumber.Text = objTable.Rows[0]["SumNumber"].ToString();

                    this.txtDriverName.Text = objTable.Rows[0]["DriverName"].ToString();

                    if (objTable.Rows.Count > 1)
                    {
                        this.txtIDOrderSyn1.Text = objTable.Rows[1]["DeliveryCode"].ToString();
                        this.txtNameProduct1.Text = objTable.Rows[1]["NameProduct"].ToString();
                        this.txtSumNumber1.Text = objTable.Rows[1]["SumNumber"].ToString();
                    }

                    if (objTable.Rows.Count > 2)
                    {
                        this.txtIDOrderSyn2.Text = objTable.Rows[2]["DeliveryCode"].ToString();
                        this.txtNameProduct2.Text = objTable.Rows[2]["NameProduct"].ToString();
                        this.txtSumNumber2.Text = objTable.Rows[2]["SumNumber"].ToString();
                    }
                }
            }
            else
            {
                this.txtIDOrderSyn.Text = "";
                this.txtNameProduct.Text = "";
                this.txtSumNumber.Text = "";

                this.txtIDOrderSyn1.Text = "";
                this.txtNameProduct1.Text = "";
                this.txtSumNumber1.Text = "";

                this.txtIDOrderSyn2.Text = "";
                this.txtNameProduct2.Text = "";
                this.txtSumNumber2.Text = "";
            }
        }
        #endregion
    }
}
