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
    public partial class frmReportInOut : Form
    {
        #region declare
        private Report objReport = new Report();
        #endregion

        #region method frmReportInOut
        public frmReportInOut()
        {
            InitializeComponent();
        }
        #endregion

        #region method frmReportInOut_KeyDown
        private void frmReportInOut_KeyDown(object sender, KeyEventArgs e)
        {

        }
        #endregion

        #region method frmReportInOut_Shown
        private void frmReportInOut_Shown(object sender, EventArgs e)
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
            this.dgvReportConfirm.DataSource = this.objReport.getInOut(this.dtpFromDay.Value, this.dtpToDay.Value, this.txtQRCode.Text);
            this.lblTotalItem.Text = this.dgvReportConfirm.RowCount.ToString();
        }
        #endregion

        private void txtQRCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.getData();
            }
        }
    }
}
