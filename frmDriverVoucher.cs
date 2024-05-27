using HMXHTD.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMXHTD
{
    public partial class frmDriverVoucher : Form
    {
        Voucher objVoucher = new Voucher();
        int Shifts1 = 0, Shifts2 = 0, Shifts3 = 0;
        int VoucherShifts1 = 0, VoucherShifts2 = 0, VoucherShifts3 = 0;
        int VoucherUsedShifts1 = 0, VoucherUsedShifts2 = 0, VoucherUsedShifts3 = 0;
        int TimeCancel = 0;
        private Thread threadVoucher;
        delegate void SetTextCallback(string text);

        public frmDriverVoucher()
        {
            InitializeComponent();
        }

        private void frmDriverVoucher_Load(object sender, EventArgs e)
        {
            this.cbbUsed.SelectedIndex = 0;

            DataTable objTable = this.objVoucher.getDataConfig();
            if (objTable.Rows.Count > 0)
            {
                //this.btnCreate.Enabled = !bool.Parse(objTable.Rows[0]["AutoRelease"].ToString());
                this.Shifts1 = int.Parse(objTable.Rows[0]["Shifts1"].ToString());
                this.Shifts2 = int.Parse(objTable.Rows[0]["Shifts2"].ToString());
                this.Shifts3 = int.Parse(objTable.Rows[0]["Shifts3"].ToString());
                this.TimeCancel = int.Parse(objTable.Rows[0]["TimeCancel"].ToString());

                this.txtShifts1.Text = this.Shifts1.ToString()+"h - "+ (this.Shifts1+1).ToString()+"h";
                this.txtShifts2.Text = this.Shifts2.ToString() + "h - " + (this.Shifts2 + 1).ToString() + "h";
                this.txtShifts3.Text = this.Shifts3.ToString() + "h - " + (this.Shifts3 + 1).ToString() + "h";
                this.txtCancel.Text = "Sau "+this.TimeCancel.ToString()+" kể từ giờ cấp mà không sử dụng";
            }
            this.getDataVoucher();
            this.getData();
            //this.AutoCreateVoucher();
        }

        private void frmDriverVoucher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void getData()
        {
            this.dgvBillToVoucher.AutoGenerateColumns = false;
            this.dgvBillToVoucher.DataSource = this.objVoucher.getBillToVoucher(this.txtSearchKey.Text);
        }

        private void getDataVoucher()
        {
            this.VoucherShifts1 = 0;
            this.VoucherShifts2 = 0;
            this.VoucherShifts3 = 0;
            this.VoucherUsedShifts1 = 0;
            this.VoucherUsedShifts2 = 0;
            this.VoucherUsedShifts3 = 0;

            this.dgvVoucher.AutoGenerateColumns = false;
            this.dgvVoucher.DataSource = this.objVoucher.getVoucher(this.dtpFromDay.Value, this.dtpToDay.Value, this.cbbUsed.SelectedIndex, this.txtSearchKey_Voucher.Text);

            for (int i = 0; i < this.dgvVoucher.Rows.Count; i++)
            {
                int timeNow = int.Parse(DateTime.Parse(this.dgvVoucher.Rows[i].Cells["dgvVoucherDayCreate"].Value.ToString()).ToString("HH"));
                int timeUsed = 0;
                if (this.dgvVoucher.Rows[i].Cells["dgvVoucherDayUsed"].Value.ToString() != "") 
                {
                    timeUsed = int.Parse(DateTime.Parse(this.dgvVoucher.Rows[i].Cells["dgvVoucherDayUsed"].Value.ToString()).ToString("HH"));
                }
                var StateUsed = this.dgvVoucher.Rows[i].Cells["dgvVoucherStateUsed"].Value.ToString(); 

                if (timeNow == Shifts1 || timeNow == Shifts1 + 1 || timeNow == Shifts1 + 2)
                {
                    VoucherShifts1 += 1;
                }

                if (timeUsed == Shifts1 || timeUsed == Shifts1 + 1 || timeUsed == Shifts1 + 2)
                {
                    if (this.dgvVoucher.Rows[i].Cells["dgvVoucherStateUsed"].Value.ToString() == "True")
                    {
                        VoucherUsedShifts1 += 1;
                    }
                }

                if (timeNow == Shifts2 || timeNow == Shifts2 + 1 || timeNow == Shifts2 + 2)
                {
                    VoucherShifts2 += 1;
                }

                if (timeUsed == Shifts2 || timeUsed == Shifts2 + 1 || timeUsed == Shifts2 + 2)
                {
                    if (this.dgvVoucher.Rows[i].Cells["dgvVoucherStateUsed"].Value.ToString() == "True")
                    {
                        VoucherUsedShifts2 += 1;
                    }
                }

                if (timeNow == Shifts3 || timeNow == Shifts3 + 1 || timeNow == Shifts3 + 2)
                {
                    VoucherShifts3 += 1;
                }

                if (timeUsed == Shifts3 || timeUsed == Shifts3 + 1 || timeUsed == Shifts3 + 2)
                {
                    if (this.dgvVoucher.Rows[i].Cells["dgvVoucherStateUsed"].Value.ToString() == "True")
                    {
                        VoucherUsedShifts3 += 1;
                    }
                }

                //int timeNow = int.Parse(DateTime.Parse(this.dgvVoucher.Rows[i].Cells["dgvVoucherDayCreate"].Value.ToString()).ToString("HH"));
                //double timeSpan = (DateTime.Parse(this.dgvVoucher.Rows[i].Cells["dgvVoucherDayCreate"].Value.ToString()) - DateTime.Now).TotalHours;
                //double timeSpan1 = (DateTime.Now- DateTime.Parse(this.dgvVoucher.Rows[i].Cells["dgvVoucherDayCreate"].Value.ToString())).TotalHours;

                //DateTime startTime = DateTime.Parse(this.dgvVoucher.Rows[i].Cells["dgvVoucherDayCreate"].Value.ToString());
                //DateTime endTime = DateTime.Now;
                //TimeSpan span = endTime.Subtract(startTime);
                //int timeSpan2 = (int)span.Hours;

                ////if (timeSpan2 > this.TimeCancel)
                ////{
                ////    var StateUsed = this.dgvVoucher.Rows[i].Cells["dgvVoucherStateUsed"].Value.ToString().Trim();
                ////    var Cancel = this.dgvVoucher.Rows[i].Cells["dgvVoucherCancel"].Value.ToString().Trim();
                ////    if (StateUsed == "False" && Cancel == "False")
                ////    {
                ////        this.objVoucher.setDataCancel(int.Parse(this.dgvVoucher.Rows[i].Cells["dgvVoucherId"].Value.ToString())); ;
                ////    }
                ////}

                //if (timeNow == this.Shifts1 || timeNow == this.Shifts1 + 1)
                //{
                //    this.VoucherShifts1 += 1;
                //    if (this.dgvVoucher.Rows[i].Cells["dgvVoucherStateUsed"].Value.ToString() == "1")
                //    {
                //        this.VoucherUsedShifts1 += 1;
                //    }
                //}

                //if (timeNow == this.Shifts2 || timeNow == this.Shifts2 + 1)
                //{
                //    this.VoucherShifts2 += 1;
                //    if (this.dgvVoucher.Rows[i].Cells["dgvVoucherStateUsed"].Value.ToString() == "1")
                //    {
                //        this.VoucherUsedShifts2 += 1;
                //    }
                //}

                //if (timeNow == this.Shifts3 || timeNow == this.Shifts3 + 1)
                //{
                //    this.VoucherShifts3 += 1;
                //    if (this.dgvVoucher.Rows[i].Cells["dgvVoucherStateUsed"].Value.ToString() == "1")
                //    {
                //        this.VoucherUsedShifts3 += 1;
                //    }
                //}
            }

            this.txtVoucherShifts1.Text = this.VoucherShifts1.ToString();
            this.txtVoucherUsedShifts1.Text = this.VoucherUsedShifts1.ToString();
            this.txtVoucherShifts2.Text = this.VoucherShifts2.ToString();
            this.txtVoucherUsedShifts2.Text = this.VoucherUsedShifts2.ToString();
            this.txtVoucherShifts3.Text = this.VoucherShifts3.ToString();
            this.txtVoucherUsedShifts3.Text = this.VoucherUsedShifts3.ToString();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                int tmpValue = 0;
                for (int i = 0; i < this.dgvBillToVoucher.RowCount; i++)
                {
                    if (this.dgvBillToVoucher.Rows[i].Cells["dgvBillToVoucherSelect"].Value.ToString().ToUpper() == "1")
                    {
                        tmpValue += this.objVoucher.setData(this.dgvBillToVoucher.Rows[i].Cells["dgvBillToVoucherDriverUserName"].Value.ToString(), this.dgvBillToVoucher.Rows[i].Cells["dgvBillToVoucherDriverName"].Value.ToString(), this.dgvBillToVoucher.Rows[i].Cells["dgvBillToVoucherDeliveryCode"].Value.ToString(), this.dgvBillToVoucher.Rows[i].Cells["dgvBillToVoucherVehicle"].Value.ToString());
                    }
                }
                if (tmpValue > 0)
                {
                    MessageBox.Show("Cấp phát phiếu ăn thành công!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.getData();
                    this.getDataVoucher();
                }
                else
                {
                    MessageBox.Show("Cấp phát phiếu ăn thất bại, vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Ex)
            {

            }
            finally
            {

            }
        }

        private void AutoCreateVoucher()
        {
            //threadVoucher = new Thread(t =>
            //{
            //    while (!this.btnCreate.Enabled)
            //    {
            //        this.Invoke((MethodInvoker)delegate
            //        {
            //            int tmpValue = 0;
            //            int timeNow = int.Parse(DateTime.Now.ToString("HH"));
            //            if (timeNow == this.Shifts1 || timeNow == this.Shifts1+1 || timeNow == this.Shifts2 || timeNow == this.Shifts2 + 1 || timeNow == this.Shifts3 || timeNow == this.Shifts3 + 1)
            //            {
            //                this.getData();
            //                for (int i = 0; i < this.dgvBillToVoucher.RowCount; i++)
            //                {
            //                    tmpValue += this.objVoucher.setDataAuto(this.dgvBillToVoucher.Rows[i].Cells["dgvBillToVoucherDriverUserName"].Value.ToString(), this.dgvBillToVoucher.Rows[i].Cells["dgvBillToVoucherDriverName"].Value.ToString(), this.dgvBillToVoucher.Rows[i].Cells["dgvBillToVoucherDeliveryCode"].Value.ToString(), this.dgvBillToVoucher.Rows[i].Cells["dgvBillToVoucherVehicle"].Value.ToString());
            //                }
            //                this.getDataVoucher();
            //            }
            //        });

            //        this.SetTextVoucher("Lần tạo phiếu gần nhất: "+DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            //        Thread.Sleep(60000);
            //    }
            //})
            //{ IsBackground = true };
            //threadVoucher.Start();
        }

        #region method SetTextVoucher
        private void SetTextVoucher(string text)
        {
            if (this.lblTimeCreate.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTextVoucher);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.lblTimeCreate.Text = text;
            }
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                int tmpValue = 0;
                for (int i = 0; i < this.dgvVoucher.RowCount; i++)
                {
                    if (this.dgvVoucher.Rows[i].Cells["dgvVoucherSelect"].Value.ToString().ToUpper() == "1")
                    {
                        tmpValue += this.objVoucher.setDataCancel(int.Parse(this.dgvVoucher.Rows[i].Cells["dgvVoucherId"].Value.ToString()));
                    }
                }
                if (tmpValue > 0)
                {
                    MessageBox.Show("Hủy phiếu ăn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.getData();
                    this.getDataVoucher();
                }
                else
                {
                    MessageBox.Show("Hủy phiếu ăn thất bại, vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Ex)
            {

            }
            finally
            {

            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void cbbStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.getDataVoucher();
        }

        private void btnFindVoucher_Click(object sender, EventArgs e)
        {
            this.getDataVoucher();
        }

        private void btnClose1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.getData();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            frmDriverVoucherReport objfrmDriverVoucherReport = new frmDriverVoucherReport();
            objfrmDriverVoucherReport.Pr_Time = "Từ ngày "+this.dtpFromDay.Value.ToString("dd/MM/yyyy")+" đến "+this.dtpToDay.Value.ToString("dd/MM/yyyy");
            objfrmDriverVoucherReport.objTableData = (DataTable)this.dgvVoucher.DataSource;
            if (this.cbbUsed.SelectedIndex == 0)
            {
                objfrmDriverVoucherReport.Pr_State = "Trạng thái: Tất cả";
            }
            else if (this.cbbUsed.SelectedIndex == 1)
            {
                objfrmDriverVoucherReport.Pr_State = "Trạng thái: Chưa sử dụng";
            }
            else if (this.cbbUsed.SelectedIndex == 2)
            {
                objfrmDriverVoucherReport.Pr_State = "Trạng thái: Đã sử dụng";
            }
            else if (this.cbbUsed.SelectedIndex == 3)
            {
                objfrmDriverVoucherReport.Pr_State = "Trạng thái: Đã hủy phiếu";
            }
            objfrmDriverVoucherReport.ShowDialog();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            frmDriverVoucherConfig objfrmDriverVoucherConfig = new frmDriverVoucherConfig();
            objfrmDriverVoucherConfig.ShowDialog();
            DataTable objTable = this.objVoucher.getDataConfig();
            if (objTable.Rows.Count > 0)
            {
                //this.btnCreate.Enabled = !bool.Parse(objTable.Rows[0]["AutoRelease"].ToString());
                this.Shifts1 = int.Parse(objTable.Rows[0]["Shifts1"].ToString());
                this.Shifts2 = int.Parse(objTable.Rows[0]["Shifts2"].ToString());
                this.Shifts3 = int.Parse(objTable.Rows[0]["Shifts3"].ToString());
                this.TimeCancel = int.Parse(objTable.Rows[0]["TimeCancel"].ToString());
            }
            this.getDataVoucher();
            this.getData();
            this.AutoCreateVoucher();
        }
    }
}
