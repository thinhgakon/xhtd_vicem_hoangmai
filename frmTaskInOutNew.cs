using HMXHTD.Core;
using HMXHTD.Data.DataEntity;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
    public partial class frmTaskInOutNew : Form
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
        private string DeliveryCode = "";
        #endregion
       
        #region method frmTaskInOut
        public frmTaskInOutNew()
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
                //this.ProcessShowDetails(message, this.DeliveryCode, true);
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
                this.ProcessShowDetails(message, this.DeliveryCode, true);
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
                      //  Myrow.DefaultCellStyle.BackColor = Color.Red;
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
            this.txtVehicle.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderVehicle"].Value.ToString();
            this.DeliveryCode = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderIDOrderSyn"].Value.ToString();
            this.ProcessShowDetails(this.txtVehicle.Text, this.DeliveryCode, false);
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

        #region method  process show details order
        private void ProcessShowDetails(string vehicle, string deliveryCode, bool realTime)
        {
            this.ClearDetails();
            var orders = new List<tblStoreOrderOperating>();
            if (realTime)
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var sqlSelect = $@"SELECT TOP 100 * FROM dbo.tblStoreOrderOperating WHERE Step > 0 AND Step < 9 AND ISNULL(DriverUserName, '') != '' AND Vehicle = @Vehicle";
                    orders = db.Database.SqlQuery<tblStoreOrderOperating>(sqlSelect, new SqlParameter("@Vehicle", vehicle)).ToList();
                }
            }
            else
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var temp = db.tblStoreOrderOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                    if (String.IsNullOrEmpty(temp.DeliveryCodeParent))
                    {
                        orders = db.tblStoreOrderOperatings.Where(x => x.DeliveryCode == deliveryCode || x.DeliveryCodeParent == deliveryCode).ToList();
                    }
                    else
                    {
                        orders = db.tblStoreOrderOperatings.Where(x => x.DeliveryCode == deliveryCode || x.DeliveryCode == temp.DeliveryCodeParent).ToList();
                    } 
                }
            }
            if (orders.Count > 0)
            {
                var order = orders[0];
                var orderScale = this.objBillOrder.GetOrderErpDetails(order.DeliveryCode);
                var percent = (orderScale.WEIGHTFULL - orderScale.WEIGHTNULL) / (double)order.SumNumber; percent = -1*(1 - percent);
                var kgDeviated = (orderScale.WEIGHTFULL - orderScale.WEIGHTNULL - (double)order.SumNumber) * 1000;


                this.txtDeliveryCode1.Text = order.DeliveryCode;
                this.txtNameProduct1.Text = order.NameProduct;
                this.txtActualExport1.Text = (orderScale.WEIGHTFULL - orderScale.WEIGHTNULL).ToString();
                this.txtSumNumber1.Text = order.SumNumber.ToString();
                this.txtKgDeviated1.Text = String.Format("{0:0.##} kg", kgDeviated);
                this.txtPercentDeviated1.Text = String.Format("{0:P2}", percent);
                this.txtWarningScale1.Text =  order.AutoScaleOut == true ? "Cân tự động" : "Cân tay";
               
                if (order.Step < 5)
                {
                    this.txtActualExport1.Text = "";
                    this.txtKgDeviated1.Text = "";
                    this.txtPercentDeviated1.Text = String.Format("{0:P2}", 0);
                    this.txtWarningScale1.Text = "";
                }
            }
            if (orders.Count > 1)
            {
                var order = orders[1];
                var orderScale = this.objBillOrder.GetOrderErpDetails(order.DeliveryCode);
                var percent = (orderScale.WEIGHTFULL - orderScale.WEIGHTNULL) / (double)order.SumNumber; percent = -1*(1 - percent);
                var kgDeviated = (orderScale.WEIGHTFULL - orderScale.WEIGHTNULL - (double)order.SumNumber) * 1000;
                this.txtDeliveryCode2.Text = order.DeliveryCode;
                this.txtNameProduct2.Text = order.NameProduct;
                this.txtActualExport2.Text = (orderScale.WEIGHTFULL - orderScale.WEIGHTNULL).ToString();
                this.txtSumNumber2.Text = order.SumNumber.ToString();
                this.txtKgDeviated2.Text = String.Format("{0:0.##} kg", kgDeviated);
                this.txtPercentDeviated2.Text = String.Format("{0:P2}", percent);
                this.txtWarningScale2.Text = order.AutoScaleOut == true ? "Cân tự động" : "Cân tay";
                if (order.Step < 5)
                {
                    this.txtActualExport2.Text = "";
                    this.txtKgDeviated2.Text = $" ";
                    this.txtPercentDeviated2.Text = String.Format("{0:P2}", 0);
                    this.txtWarningScale2.Text = "";
                }
            } 
        }
        private void ClearDetails()
        {
            this.txtDeliveryCode1.Text = "";
            this.txtNameProduct1.Text = "";
            this.txtActualExport1.Text = "";
            this.txtSumNumber1.Text = "";
            this.txtPercentDeviated1.Text = "";
            this.txtKgDeviated1.Text = "";
            this.txtWarningScale1.Text = "";

            this.txtDeliveryCode2.Text = "";
            this.txtNameProduct2.Text = "";
            this.txtActualExport2.Text = "";
            this.txtSumNumber2.Text = "";
            this.txtPercentDeviated2.Text = "";
            this.txtKgDeviated2.Text = "";
            this.txtWarningScale2.Text = "";
        }
        #endregion
    }
}
