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
    public partial class frmReportScale : Form
    {
        #region declare
        private Report objReport = new Report();
        #endregion

        #region method frmReportScale
        public frmReportScale()
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
            this.dgvReportConfirm.DataSource = this.objReport.getScale(this.dtpFromDay.Value, this.dtpToDay.Value, this.txtSearch.Text);
            this.lblTotalItem.Text = this.dgvReportConfirm.RowCount.ToString();
        }
        #endregion

        #region method txtSearch_KeyDown

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.getData();
            }
        }
        #endregion
    }
}
