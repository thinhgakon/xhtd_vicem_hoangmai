using HMXHTD.Core;
using HMXHTD.Data.DataEntity;
using Microsoft.AspNet.SignalR.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Windows.Forms;

namespace HMXHTD
{
    public partial class frmTaskOperating : Form
    {
        #region declare objects
        private IHubProxy HubProxy { get; set; }
        private HubConnection Connection { get; set; }
        private Thread threadArea;
        delegate void SetTextCallback(string text);
        public bool Area = true;
        int TimeSleep = 60000, OrderId = 0, StateId = 0, StepId = -1, IndexOrder = 0;
        BillOrder objBillOrder = new BillOrder();
        Print objPrint = new Print();
        private static int IndexOrderCurrent = 0;
        #endregion

        #region method frmTaskOperating
        public frmTaskOperating()
        {
            InitializeComponent();
            if(frmMain.UserName.ToUpper() != "XHTD_ADMIN")
            {
                this.txtSTT.Visible = false;
                this.btnUpdateSTT.Visible = false;
            }
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
                this.lbNotification.Text = "Mở server windowservice trước khi bật ứng dụng";
                return;
            }
            this.lbNotification.Text = "Đã kết nối với server lúc " + DateTime.Now;
        }
        private void ProcessNotification(string key, string message)
        {
            //if (this.txtSearchKey.Text != "")
            //{
            //    return;
            //}
            switch (key)
            {
                case "New_Order_Sync":
                    this.getData();
                    this.lbNotification.Text = message;
                    break;
                case "ReIndex_Order":
                    this.getData();
                    this.lbNotification.Text = message;
                    break;
                case "Step_1":
                    this.getData();
                    this.lbNotification.Text = message;
                    break;
                case "Step_2":
                    this.getData();
                    this.lbNotification.Text = message;
                    break;
                case "Step_3":
                    this.getData();
                    this.lbNotification.Text = message;
                    break;
                case "Step_4":
                    this.getData();
                    this.lbNotification.Text = message;
                    break;
                case "Step_5":
                    this.getData();
                    this.lbNotification.Text = message;
                    break;
                case "Step_6":
                    this.getData();
                    this.lbNotification.Text = message;
                    break;
                case "Step_7":
                    this.getData();
                    this.lbNotification.Text = message;
                    break;
                case "Step_8":
                    this.getData();
                    this.lbNotification.Text = message;
                    break;
                case "Scale1_Current":
                    {
                        this.lb_scale_cn_current_weight.Text = $@"{message} kg";
                        if (int.Parse(message) <= 1500)
                        {
                            this.lb_scale_cn_current_weight.Text = "";
                            this.lb_scale_cn_balance.Text = "";
                        }
                    }
                    break;
                case "Scale2_Current":
                    this.lb_scale_cc_current_weight.Text = $@"{message} kg";

                    if (int.Parse(message) <= 1500)
                    {
                        this.lb_scale_cc_current_weight.Text = "";
                        this.lb_scale_cc_balance.Text = "";
                    }

                    break;
                case "Scale1_Balance":
                    this.lb_scale_cn_balance.Text = $@"{message} kg";
                    break;
                case "Scale2_Balance":
                    this.lb_scale_cc_balance.Text = $@"{message} kg";
                    break;
                case "Scale_In_CN":
                    this.lb_scale_vehicle_cn.Text = $@"{message}";
                    break;
                case "Scale_Out_CN":
                    this.lb_scale_vehicle_cn.Text = $@"{message}";
                    break;
                case "Scale_In_CC":
                    this.lb_scale_vehicle_cn.Text = $@"{message}";
                    break;
                case "Scale_Out_CC":
                    this.lb_scale_vehicle_cn.Text = $@"{message}";
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region method frmTaskOperating_Load
        private void frmTaskOperating_Load(object sender, EventArgs e)
        {
            this.cbbTypeProduct.SelectedIndex = 0;
            this.cbbStep.SelectedIndex = 0;
            this.btnStart.PerformClick();
        }
        #endregion

        #region method dgvListBillOrder_RowPrePaint
        private void dgvListBillOrder_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
                if (this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderState1"].Value.ToString() == "Đang lấy hàng")
                {
                    this.dgvListBillOrder.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gold;
                }
                //if (this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderWarningNotCall"].Value.ToString() == "True")
                //{
                //    this.dgvListBillOrder.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                //}

            }
            catch
            {
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
            this.Area = true;
            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;
            threadArea = new Thread(t =>
            {
                while (this.Area)
                {
                    this.Invoke((MethodInvoker)delegate
                        {


                            this.getData();
                        });

                    this.SetTextArea(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ", " + this.dgvListBillOrder.RowCount.ToString() + " đơn hàng");
                    Thread.Sleep(this.TimeSleep);
                    //this.Area = false;
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

        #region method dgvListBillOrder_CellClick
        private void dgvListBillOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.txtDayCreate.Text = "";
                this.txtIDOrderSyn.Text = "";
                this.txtVehicle.Text = "";
                this.txtDriverName.Text = "";
                this.txtSTT.Text = "";
                this.txtNameProduct.Text = "";
                this.txtSumNumber.Text = "";
                this.txtNameStore.Text = "";
                this.txtNameDistributor.Text = "";

                #region Lịch sử đơn hàng
                this.txtTimeOrder.Text = "";
                this.txtTimeAccept.Text = "";
                this.txtTimeConfirm.Text = "";
                this.txtTimeEnter.Text = "";
                this.txtTimeIn.Text = "";
                this.txtTimeRelease.Text = "";
                this.txtTimeOut.Text = "";
                this.txtTimeComplete.Text = "";
                this.txtLog.Text = "";
                #endregion

                this.OrderId = 0;
                this.StateId = -1;
                this.IndexOrder = 0;
                this.OrderId = int.Parse(this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderId"].Value.ToString());
                this.StepId = int.Parse(this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderStep"].Value.ToString());
                this.txtDayCreate.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderDayCreate"].Value.ToString();
                this.txtIDOrderSyn.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderIDOrderSyn"].Value.ToString();
                this.txtVehicle.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderVehicle"].Value.ToString().ToUpper();
                this.txtDriverName.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderDriverName"].Value.ToString();
                this.txtNameProduct.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderNameProduct"].Value.ToString();
                //dgvListBillOrderIndexOrder
                this.txtSTT.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderIndexOrder"].Value.ToString();
                Int32.TryParse(this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderIndexOrder"].Value.ToString(), out IndexOrderCurrent);
                this.txtSumNumber.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderSumNumber"].Value.ToString();
                this.txtNameStore.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderNameStore"].Value.ToString();
                this.txtNameDistributor.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderNameDistributor"].Value.ToString();
                if (this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderState1"].Value.ToString() == "Chưa vào cổng")
                {
                }
                else
                {
                }

                if (this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderTIMEIN33"].Value.ToString() != "")
                {
                    string[] TimeIn = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderTIMEIN33"].Value.ToString().Split('-');
                    string[] TimeIn1 = TimeIn[0].Split(':');
                    string[] TimeIn2 = TimeIn[1].Split(':');
                }
                if (this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderIndexOrder"].Value.ToString() != "")
                {
                    this.IndexOrder = int.Parse(this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderIndexOrder"].Value.ToString());
                }
                this.txtTimeOrder.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderDayCreate"].Value.ToString();
                this.txtLog.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderLogProcessOrder"].Value.ToString();

                if (this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderDriverAccept"].Value.ToString() != "")
                {
                    this.txtTimeAccept.Text = DateTime.Parse(this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderDriverAccept"].Value.ToString()).ToString("dd/MM/yyyy HH:mm");
                }

                if (this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderTimeConfirm1"].Value.ToString() != "")
                {
                    var test = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderTimeConfirm1"].Value.ToString();
                    this.txtTimeConfirm.Text = DateTime.Parse(this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderTimeConfirm1"].Value.ToString()).ToString("dd/MM/yyyy HH:mm");
                }

                if (this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderTimeConfirm2"].Value.ToString() != "")
                {
                    this.txtTimeEnter.Text = DateTime.Parse(this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderTimeConfirm2"].Value.ToString()).ToString("dd/MM/yyyy HH:mm");
                }

                if (this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderTimeConfirm3"].Value.ToString() != "")
                {
                    this.txtTimeIn.Text = DateTime.Parse(this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderTimeConfirm3"].Value.ToString()).ToString("dd/MM/yyyy HH:mm");
                }

                if (this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderTimeConfirm5"].Value.ToString() != "")
                {
                    this.txtTimeRelease.Text = DateTime.Parse(this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderTimeConfirm5"].Value.ToString()).ToString("dd/MM/yyyy HH:mm");
                }

                if (this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderTimeConfirm7"].Value.ToString() != "")
                {
                    this.txtTimeOut.Text = DateTime.Parse(this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderTimeConfirm7"].Value.ToString()).ToString("dd/MM/yyyy HH:mm");
                }

                if (this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderTimeConfirm8"].Value.ToString() != "")
                {
                    this.txtTimeComplete.Text = DateTime.Parse(this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderTimeConfirm8"].Value.ToString()).ToString("dd/MM/yyyy HH:mm");
                }

                //if (this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderDriverAccept"].Value.ToString() != "")
                //{
                //    this.txtTimeAccept.Text = DateTime.Parse(this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderDriverAccept"].Value.ToString()).ToString("dd/MM/yyyy HH:mm");
                //}

                //if (this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderDriverAccept"].Value.ToString() != "")
                //{
                //    this.txtTimeAccept.Text = DateTime.Parse(this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderDriverAccept"].Value.ToString()).ToString("dd/MM/yyyy HH:mm");
                //}
            }
            catch (Exception Ex)
            {

            }
        }
        #endregion

        #region method tabControl1_SelectedIndexChanged
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region method btnSetOperating_Click
        private void btnSetOperating_Click(object sender, EventArgs e)
        {
            if (this.OrderId > 0)
            {
                if (this.objBillOrder.setBillOrderOperating(this.OrderId) > 0)
                {
                    MessageBox.Show("Thiết lập thông tin xuất hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thiết lập thông tin xuất hàng thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
           
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            syncStatus();
        }
        private void syncStatus()
        {
          var  threadSyncStatusTrough = new Thread(t =>
            {
                while (1 < 2)
                {
                    if (!ckbAutoSync.Checked) continue;
                    this.Invoke((MethodInvoker)delegate
                    {
                        using (var db = new HMXuathangtudong_Entities())
                        {
                            var troughs  = db.tblTroughs.ToList();
                            foreach (var trough in troughs)
                            {
                                switch (trough.LineCode)
                                {
                                    case "M1":
                                        lb1_status.Text = trough.IsPicking == true ? "Đang lấy hàng" : "Đang nghỉ";
                                        lb1_planquantity.Text = trough.PlanQuantityCurrent?.ToString();
                                        lb1_countquantity.Text = trough.CountQuantityCurrent?.ToString();
                                        lb1_deliverycode.Text = trough.DeliveryCodeCurrent;
                                        lb1_vehicle.Text = trough.TransportNameCurrent;
                                        if (trough.IsPicking == true)
                                        {
                                            lb1_pecent.Text = (Math.Round((double)((trough.CountQuantityCurrent / trough.PlanQuantityCurrent) * 100), 2)).ToString() + " %";
                                        }
                                        else
                                        {
                                            lb1_pecent.Text = "";
                                        }
                                        break;
                                    case "M2":
                                        lb2_status.Text = trough.IsPicking == true ? "Đang lấy hàng" : "Đang nghỉ";
                                        lb2_planquantity.Text = trough.PlanQuantityCurrent?.ToString();
                                        lb2_countquantity.Text = trough.CountQuantityCurrent?.ToString();
                                        lb2_deliverycode.Text = trough.DeliveryCodeCurrent;
                                        lb2_vehicle.Text = trough.TransportNameCurrent;
                                        if (trough.IsPicking == true)
                                        {
                                            lb2_pecent.Text = (Math.Round((double)((trough.CountQuantityCurrent / trough.PlanQuantityCurrent) * 100), 2)).ToString() + " %";
                                        }
                                        else
                                        {
                                            lb2_pecent.Text = "";
                                        }
                                        break;
                                    case "M3":
                                        lb3_status.Text = trough.IsPicking == true ? "Đang lấy hàng" : "Đang nghỉ";
                                        lb3_planquantity.Text = trough.PlanQuantityCurrent?.ToString();
                                        lb3_countquantity.Text = trough.CountQuantityCurrent?.ToString();
                                        lb3_deliverycode.Text = trough.DeliveryCodeCurrent;
                                        lb3_vehicle.Text = trough.TransportNameCurrent;
                                        if (trough.IsPicking == true)
                                        {
                                            lb3_pecent.Text = (Math.Round((double)((trough.CountQuantityCurrent / trough.PlanQuantityCurrent) * 100), 2)).ToString() + " %";
                                        }
                                        else
                                        {
                                            lb3_pecent.Text = "";
                                        }
                                        break;
                                    case "M4":
                                        lb4_status.Text = trough.IsPicking == true ? "Đang lấy hàng" : "Đang nghỉ";
                                        lb4_planquantity.Text = trough.PlanQuantityCurrent?.ToString();
                                        lb4_countquantity.Text = trough.CountQuantityCurrent?.ToString();
                                        lb4_deliverycode.Text = trough.DeliveryCodeCurrent;
                                        lb4_vehicle.Text = trough.TransportNameCurrent;
                                        if (trough.IsPicking == true)
                                        {
                                            lb4_pecent.Text = (Math.Round((double)((trough.CountQuantityCurrent / trough.PlanQuantityCurrent) * 100), 2)).ToString() + " %";
                                        }
                                        else
                                        {
                                            lb4_pecent.Text = "";
                                        }
                                        break;
                                    case "M5":
                                        lb5_status.Text = trough.IsPicking == true ? "Đang lấy hàng" : "Đang nghỉ";
                                        lb5_planquantity.Text = trough.PlanQuantityCurrent?.ToString();
                                        lb5_countquantity.Text = trough.CountQuantityCurrent?.ToString();
                                        lb5_deliverycode.Text = trough.DeliveryCodeCurrent;
                                        lb5_vehicle.Text = trough.TransportNameCurrent;
                                        if (trough.IsPicking == true)
                                        {
                                            lb5_pecent.Text = (Math.Round((double)((trough.CountQuantityCurrent / trough.PlanQuantityCurrent) * 100), 2)).ToString() + " %";
                                        }
                                        else
                                        {
                                            lb5_pecent.Text = "";
                                        }
                                        break;
                                    case "M6":
                                        lb6_status.Text = trough.IsPicking == true ? "Đang lấy hàng" : "Đang nghỉ";
                                        lb6_planquantity.Text = trough.PlanQuantityCurrent?.ToString();
                                        lb6_countquantity.Text = trough.CountQuantityCurrent?.ToString();
                                        lb6_deliverycode.Text = trough.DeliveryCodeCurrent;
                                        lb6_vehicle.Text = trough.TransportNameCurrent;
                                        if (trough.IsPicking == true)
                                        {
                                            lb6_pecent.Text = (Math.Round((double)((trough.CountQuantityCurrent / trough.PlanQuantityCurrent) * 100), 2)).ToString() + " %";
                                        }
                                        else
                                        {
                                            lb6_pecent.Text = "";
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        
                    });
                    Thread.Sleep(10000);
                }
            })
            { IsBackground = true };
            threadSyncStatusTrough.Start();
        }

        private void lb_scale_cn_current_weight_Click(object sender, EventArgs e)
        {

        }

        private void lb_scale_cc_current_weight_Click(object sender, EventArgs e)
        {

        }

        private void txtSearchKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.getData();
            }
        }

        private void txtDeliveryCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.getData();
            }
        }

        private void cbbTypeProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.getData();
        }

        private void cbbStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbbStep.Text == "Đã nhận đơn")
            {
                //this.dgvListBillOrder.Columns["dgvListBillOrderIndexOrder"].Visible = false;
                this.dgvListBillOrder.Columns["dgvListBillOrderDriverAccept"].Visible = true;
            }
            else if (this.cbbStep.Text == "Chưa xác nhận")
            {
                //this.dgvListBillOrder.Columns["dgvListBillOrderIndexOrder"].Visible = false;
                this.dgvListBillOrder.Columns["dgvListBillOrderDriverAccept"].Visible = false;
            }
            else if (this.cbbStep.Text == "Đã xác nhận")
            {
                //this.dgvListBillOrder.Columns["dgvListBillOrderIndexOrder"].Visible = true;
                this.dgvListBillOrder.Columns["dgvListBillOrderDriverAccept"].Visible = false;
            }
            else
            {
                //this.dgvListBillOrder.Columns["dgvListBillOrderIndexOrder"].Visible = false;
                this.dgvListBillOrder.Columns["dgvListBillOrderDriverAccept"].Visible = false;
            }

            this.getData();
        }

        private void btnFinish_Click(object sender, EventArgs e)
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
                DataTable objTable = this.objBillOrder.getBillOrderOperating(this.txtSearchKey.Text, this.cbbTypeProduct.Text, this.cbbStep.Text);
                objTable.DefaultView.Sort = "tmpOrder ASC";
                this.dgvListBillOrder.DataSource = objTable;
                this.tabPage1.Text = "Phương tiện - Đơn hàng: " + this.dgvListBillOrder.RowCount.ToString();

                foreach (DataGridViewRow Myrow in dgvListBillOrder.Rows)
                {
                    if (Myrow.Cells["dgvListBillOrderWarningNotCall"].Value.ToString() == "True")
                    {
                        //this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderWarningNotCall"].Value.ToString() == "True"
                        Myrow.DefaultCellStyle.BackColor = Color.Red;
                    }
                }

            }
            catch
            {

            }
        }
        #endregion

        #region method frmTaskOperating_KeyDown
        private void frmTaskOperating_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtSearchKey_TextChanged(object sender, EventArgs e)
        {

        }

       
        #endregion

        #region method frmTaskOperating_FormClosing
        private void frmTaskOperating_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.threadArea.IsAlive)
                {
                    this.threadArea.Abort();
                }
                Connection.Stop();
            }
            catch
            {
            }
        }

        
        #endregion

        #region method dgvListBillOrder_CellMouseDoubleClick
        // hoi them ham nay de lam gi
        private void dgvListBillOrder_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
            {
                frmVehicleDeliveryCode objVehicleDeliveryCode = new frmVehicleDeliveryCode();
                objVehicleDeliveryCode.Vehicle = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderVehicle"].Value.ToString();                
                objVehicleDeliveryCode.ShowDialog();
                if (objVehicleDeliveryCode.TotalItem > 0)
                {
                    this.getData();
                }
            }
            //else if (e.ColumnIndex == 4)
            //{
            //    frmTrackingSetting objTrackingSetting = new frmTrackingSetting();

            //    objTrackingSetting.OrderId = int.Parse(this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderId"].Value.ToString());
            //    objTrackingSetting.StepId = int.Parse(this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderStep"].Value.ToString());
            //    objTrackingSetting.txtDayCreate.Text = DateTime.Parse(this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderDayCreate"].Value.ToString()).ToString("dd/MM/yyyy");
            //    objTrackingSetting.txtIDOrderSyn.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderIDOrderSyn"].Value.ToString();
            //    objTrackingSetting.txtVehicle.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderVehicle"].Value.ToString();
            //    objTrackingSetting.txtDriverName.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderDriverName"].Value.ToString();
            //    objTrackingSetting.txtNameProduct.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderNameProduct"].Value.ToString();
            //    objTrackingSetting.txtSumNumber.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderSumNumber"].Value.ToString();
            //    objTrackingSetting.txtNameStore.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderNameStore"].Value.ToString();
            //    objTrackingSetting.txtNameDistributor.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderNameDistributor"].Value.ToString();
            //    objTrackingSetting.txtSearch.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderDriverName"].Value.ToString();

            //    objTrackingSetting.ShowDialog();

            //    this.getData();
            //}
            else if (e.ColumnIndex == 6 && this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderDriverUserName"].Value.ToString().Trim() == "")
            {
                FrmDriverFindUserName objFrmDriverFindUserName = new FrmDriverFindUserName();
                objFrmDriverFindUserName.txtSearch.Text = this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderDriverName"].Value.ToString();
                objFrmDriverFindUserName.ShowDialog();
                if (objFrmDriverFindUserName.AccId > 0)
                {
                    this.objBillOrder.updateStoreOrderOperatingDriver(int.Parse(this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderId"].Value.ToString()), objFrmDriverFindUserName.UserName.ToString(), this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderVehicle"].Value.ToString());
                    this.dgvListBillOrder.Rows[e.RowIndex].Cells["dgvListBillOrderDriverUserName"].Value = objFrmDriverFindUserName.UserName.ToString();
                }
            }
        }

        #endregion

        #region method btnBillOrderCancel_Click
        private void btnBillOrderCancel_Click(object sender, EventArgs e)
        {

        }

       
        #endregion

        #region method btnBillOrderFinish_Click
        private void btnBillOrderFinish_Click(object sender, EventArgs e)
        {
            if (this.txtIDOrderSyn.Text.Trim() != "")
            {
                if (MessageBox.Show("Bạn có muốn kết thúc đơn hàng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (this.objBillOrder.FinishBillOrder(this.txtIDOrderSyn.Text.Trim()) > 0)
                    {
                        MessageBox.Show("Kết thúc đơn hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.getData();
                    }
                    else
                    {
                        MessageBox.Show("Kết thúc đơn hàng thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.txtIDOrderSyn.Text.Trim() != "")
            {
                if (MessageBox.Show("Bạn có muốn in phiếu xuất cho đơn này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        var client = new RestClient($"http://tv.ximanghoangmai.vn:8189/api/v1/PrintApi/print-order?deliveryCode={this.txtIDOrderSyn.Text.Trim()}");
                        var request = new RestRequest();
                        request.Method = Method.POST;
                        request.AddHeader("Accept", "application/json");
                        var response = client.Execute(request);
                        string data = response.Content;
                        var jo = Newtonsoft.Json.Linq.JObject.Parse(response.Content);
                        var status = jo["Status"].ToString();
                        if(status == "200")
                        {
                            MessageBox.Show("In đơn hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("In đơn hàng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("In đơn hàng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    //if (objPrint.PrintPDF(this.txtIDOrderSyn.Text.Trim()))
                    //{
                    //    MessageBox.Show("In đơn hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("In đơn hàng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }
            }
        }

        private void btnPrintScale_Click(object sender, EventArgs e)
        {
            if (this.txtIDOrderSyn.Text.Trim() != "")
            {
                if (MessageBox.Show("Bạn có muốn in phiếu cân cho đơn này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        var statusCode = CreateCoupleScale();
                        if (statusCode == 200)
                        {
                            var client = new RestClient($"http://tv.ximanghoangmai.vn:8189/api/v1/PrintApi/print-coupon-scale?deliveryCode={this.txtIDOrderSyn.Text.Trim()}");
                            var request = new RestRequest();
                            request.Method = Method.POST;
                            request.AddHeader("Accept", "application/json");
                            var response = client.Execute(request);
                            string data = response.Content;
                            var jo = Newtonsoft.Json.Linq.JObject.Parse(response.Content);
                            var status = jo["Status"].ToString();
                            if (status == "200")
                            {
                                MessageBox.Show("In đơn hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("In đơn hàng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("In đơn hàng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("In đơn hàng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } 
                }
            }
        }
        private int CreateCoupleScale()
        {
            var statusCode = 500;
            try
            {
                var client = new RestClient($"http://tv.ximanghoangmai.vn:8189/api/v1/CouponScale/create-coupon-scale?deliveryCode={this.txtIDOrderSyn.Text.Trim()}");
                var request = new RestRequest();
                request.Method = Method.POST;
                request.AddHeader("Accept", "application/json");
                var response = client.Execute(request);
                string data = response.Content;
                var jo = Newtonsoft.Json.Linq.JObject.Parse(response.Content);
                var status = jo["StatusCode"].ToString();
                if (status == "200") statusCode = 200;
            }
            catch (Exception ex)
            {

            }
            return statusCode;
        }

        private void btnPushToTrough_Click(object sender, EventArgs e)
        { 
            try
            {
                var client = new RestClient($"http://192.168.158.19/WebCounter/api/Delivery?DeliveryCode={this.txtIDOrderSyn.Text.Trim()}");
                var request = new RestRequest();
                request.Method = Method.POST;
                request.AddHeader("Accept", "application/json");
                var response = client.Execute(request);
                string data = response.Content;
                var jo = Newtonsoft.Json.Linq.JObject.Parse(response.Content);
                var status = jo["Code"].ToString();
                if (status == "200")
                {
                    MessageBox.Show(jo["Mesage"].ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(jo["Mesage"].ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

            } 
        }
        #endregion

        #region method btnBillOrderCancel_Click_1
        private void btnBillOrderCancel_Click_1(object sender, EventArgs e)
        {
            if (this.txtIDOrderSyn.Text.Trim() != "")
            {
                if (MessageBox.Show("Bạn có muốn hủy nhận đơn này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (this.objBillOrder.CancelAcceptBillOrder(this.txtIDOrderSyn.Text.Trim()) > 0)
                    {
                        MessageBox.Show("Hủy nhận đơn hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.getData();
                    }
                    else
                    {
                        MessageBox.Show("Hủy nhận đơn hàng thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        #endregion

        #region method btnBillOrderCancelSTT_Click
        private void btnBillOrderCancelSTT_Click(object sender, EventArgs e)
        {
            if (this.txtIDOrderSyn.Text.Trim() != "")
            {
                if (MessageBox.Show("Bạn có muốn hủy lốt đơn hàng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (this.objBillOrder.CancelSTTBillOrder(this.txtIDOrderSyn.Text.Trim()) > 0)
                    {
                        MessageBox.Show("Hủy lốt thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.getData();
                    }
                    else
                    {
                        MessageBox.Show("Hủy lốt thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        #endregion

        #region method btnBillOrderConfirm_Click
        private void btnBillOrderConfirm_Click(object sender, EventArgs e)
        {
            if (this.txtIDOrderSyn.Text.Trim() != "")
            {
                if (MessageBox.Show("Bạn có muốn xác thực và xếp lốt cho đơn hàng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (this.objBillOrder.ConfirmBillOrder(this.txtIDOrderSyn.Text.Trim()) > 0)
                    {
                        MessageBox.Show("Xác thực và xếp lốt thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.getData();
                    }
                    else
                    {
                        MessageBox.Show("Xác thực và xếp lốt thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        #endregion

        #region method btnBillOrderConfirm_Click
        private void btnUpdateSTT_Click(object sender, EventArgs e)
        {
            if (this.txtIDOrderSyn.Text.Trim() != "" && this.txtSTT.Text.Trim() != "")
            {
                int sttUpdate = 0;
                Int32.TryParse(this.txtSTT.Text.Trim(), out sttUpdate);
                if(sttUpdate < 1)
                {
                    
                    MessageBox.Show("Số thứ tự không đúng định dạng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (MessageBox.Show("Bạn có muốn xếp lại lốt cho đơn hàng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sttUpdate = IndexOrderCurrent < sttUpdate ? sttUpdate + 1 : sttUpdate;
                    if (this.objBillOrder.UpdateIndexBillOrderPriority(this.txtIDOrderSyn.Text.Trim(), sttUpdate) > 0)
                    {
                        MessageBox.Show("Xếp lốt ưu tiên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.getData();
                    }
                    else
                    {
                        MessageBox.Show("Xếp lốt ưu tiên thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đơn hàng đang có lốt để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
