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
    public partial class frmReportBarie : Form
    {
        #region declare
        private Report objReport = new Report();
        #endregion

        #region method frmReportBarie
        public frmReportBarie()
        {
            InitializeComponent();
        }
        #endregion

        #region method frmReportBarie_KeyDown
        private void frmReportBarie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region method frmReportBarie_Shown
        private void frmReportBarie_Shown(object sender, EventArgs e)
        {
            this.cbbBarie.SelectedIndex = 0;

            this.dtpFromDay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,0,0,0);
            this.dtpToDay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            this.getData();
        }
        #endregion

        #region method cbbBarie_SelectedIndexChanged
        private void cbbBarie_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.getData();
        }
        #endregion

        #region method getData
        private void getData()
        {
            this.dgvReportBarie.AutoGenerateColumns = false;
            this.dgvReportBarie.DataSource = this.objReport.getBarieLog(this.dtpFromDay.Value, this.dtpToDay.Value, this.cbbBarie.Text);
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
    }
}
