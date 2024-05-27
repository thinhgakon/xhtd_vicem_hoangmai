using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HMXHTD.Core;

namespace HMXHTD
{
    public partial class frmReportConfirm : Form
    {
        #region declare
        private Report objReport = new Report();
        #endregion

        #region method frmReportConfirm
        public frmReportConfirm()
        {
            InitializeComponent();
        }
        #endregion

        #region method frmReportConfirm_KeyDown
        private void frmReportConfirm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region method frmReportConfirm_Shown
        private void frmReportConfirm_Shown(object sender, EventArgs e)
        {
            this.getData();
        }
        #endregion

        #region method btnSearch_Click
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.getData();
        }
        #endregion

        #region method btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region method getData
        private void getData()
        {
            this.dgvReportConfirm.AutoGenerateColumns = false;
            this.dgvReportConfirm.DataSource = this.objReport.getConfirm(this.dtpFromDay.Value, this.dtpToDay.Value, this.txtQRCode.Text.Trim());
            this.lblTotalItem.Text = this.dgvReportConfirm.RowCount.ToString();
        }
        #endregion

        #region method dgvReportConfirm_CellClick
        private void dgvReportConfirm_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               var Id = Int32.Parse(this.dgvReportConfirm.Rows[e.RowIndex].Cells["dgvListBillOrderId"].Value.ToString());
                var logText =  new BillOrder().getLogOrderById(Id).Trim();
                this.rtxtHistory.Text = logText?.Replace("#","\n") ?? "Chưa ghi nhận lịch sử đơn này";
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region method txtQRCode_KeyDown
        private void txtQRCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.getData();
            }
        }
        #endregion

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {

        }
    }
}