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
    public partial class frmReportRelease : Form
    {
        #region declare
        private Report objReport = new Report();
        #endregion

        #region method frmReportScale
        public frmReportRelease()
        {
            InitializeComponent();
        }
        #endregion

        #region method frmReportScale_KeyDown
        private void frmReportScale_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region method frmReportScale_Shown
        private void frmReportScale_Shown(object sender, EventArgs e)
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
            this.dgvReportConfirm.DataSource = this.objReport.getRelease(this.dtpFromDay.Value, this.dtpToDay.Value, this.txtQRCode.Text);
            this.lblTotalItem.Text = this.dgvReportConfirm.RowCount.ToString();
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
    }
}
