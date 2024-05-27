using Microsoft.AspNet.SignalR.Client;
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


namespace HMXHTD
{
    public partial class frmTaskConfirm : Form
    {
        #region declare objects
        private IHubProxy HubProxy { get; set; }
        private HubConnection Connection { get; set; }
        private Thread threadArea;
        delegate void SetTextCallback(string text);
        int TimeSleep = 30000, OrderId = 0, StepId = -1;
        BillOrder objBillOrder = new BillOrder();
        #endregion

        #region method frmTaskConfirm
        public frmTaskConfirm()
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
                    this.getData();
                    break;
                case "Step_1":
                    this.getData();
                    break;
                case "Step_2":
                    break;
                case "Step_3":
                    break;
                case "Step_4":
                    this.getData();
                    break;
                case "Step_5":
                    break;
                case "Step_6":
                    break;
                case "Step_7":
                    break;
                case "Step_8":
                    break;
                default:
                    break;
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
            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;
            threadArea = new Thread(t =>
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
            threadArea.Start();
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

        #region method frmTaskConfirm_Load
        private void frmTaskConfirm_Load(object sender, EventArgs e)
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
            try
            {
                this.txtIDOrderSyn.Text = "";
                this.txtVehicle.Text = "";
                this.txtDriverName.Text = "";
                this.txtNameProduct.Text = "";
                this.txtSumNumber.Text = "";
                this.txtNameStore.Text = "";
                this.txtNameDistributor.Text = "";

                this.OrderId = 0;
                this.OrderId = int.Parse(this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderId"].Value.ToString());
                this.StepId = int.Parse(this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderStep"].Value.ToString());
                this.txtIDOrderSyn.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderIDOrderSyn"].Value.ToString();
                this.txtVehicle.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderVehicle"].Value.ToString().ToUpper();
                this.txtRFID.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderCardNo"].Value.ToString().ToUpper(); 
                this.txtDriverName.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderDriverName"].Value.ToString();
                this.txtNameProduct.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderNameProduct"].Value.ToString();
                this.txtSumNumber.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderSumNumber"].Value.ToString();
                this.txtNameStore.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderNameStore"].Value.ToString();
                this.txtNameDistributor.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderNameDistributor"].Value.ToString();
            }
            catch
            {

            }
        }
        #endregion

        #region method getData
        private void getData()
        {
            this.dgvListBillOrder.AutoGenerateColumns = false;
            this.dgvListBillOrder.EnableHeadersVisualStyles = false;
            DataTable objTable = this.objBillOrder.getBillOrderConfirm(this.txtSearch_DeliveryCode.Text, this.txtSearch_Vehicle.Text, this.txtSearch_NameProduct.Text, this.cbbTypeProduct.Text, this.cbbStep.Text);
            objTable.DefaultView.Sort = "DayCreate DESC";
            this.dgvListBillOrder.DataSource = objTable;

            this.lblCountItem.Text = this.dgvListBillOrder.RowCount.ToString();
        }
        #endregion

        #region method frmTaskConfirm_KeyDown
        private void frmTaskConfirm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region method frmTaskConfirm_FormClosing
        private void frmTaskConfirm_FormClosing(object sender, FormClosingEventArgs e)
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
            if (this.txtQRCode.Text.Trim() != "")
            {
                if (MessageBox.Show("Bạn muốn xác thực đơn hàng \"" + this.txtQRCode.Text.Trim() + "\"", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string MSG = "";
                    //if (this.objBillOrder.setBillOrderOperatingByDeliveryCode(this.objBillOrder.getDeliveryCodeParentByDeliveryCode(this.txtQRCode.Text.Trim()), 2, 0) > 0)
                    if (MSG == "OK")
                    {
                        HubProxy.Invoke("Send", "Step_1", String.Format("Xác thực đơn hàng {0} qua cổng xác thực 1", this.txtQRCode.Text.Trim()));
                        this.getData();
                        this.txtQRCode.Text = "";
                        this.txtQRCode.Focus();
                    }
                    else
                    {
                        MessageBox.Show(MSG, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.txtQRCode.Text = "";
                        this.txtQRCode.Focus();
                    }
                }
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

        private void txtSearch_DeliveryCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.getData();
            }
        }

        private void txtSearch_Vehicle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.getData();
            }
        }

        private void txtSearch_DriverName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.getData();
            }
        }

        private void txtSearch_NameProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.getData();
            }
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

        private void cbbStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.getData();
        }
        #endregion

        #region method btnRFID_Click
        private void btnRFID_Click(object sender, EventArgs e)
        {
            frmRFIDRegister objRFIDRegister = new frmRFIDRegister();
            objRFIDRegister.ShowDialog();
        }
        #endregion

        #region method cbbTypeProduct_SelectedIndexChanged
        private void cbbTypeProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.getData();
        }
        #endregion
    }
}
