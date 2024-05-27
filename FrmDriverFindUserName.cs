using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMXHTD
{
    public partial class FrmDriverFindUserName : Form
    {
        #region declare objects
        HMXHTD.Core.Driver objDriver = new Core.Driver();
        public string UserName = "", FullName;
        public int AccId = 0;
        #endregion

        public FrmDriverFindUserName()
        {
            InitializeComponent();
        }

        private void FrmDriverFindUserName_Shown(object sender, EventArgs e)
        {
            this.dgvAccount.AutoGenerateColumns = false;
            this.dgvAccount.EnableHeadersVisualStyles = false;
            this.dgvAccount.DataSource = this.objDriver.getDataAsDriver(this.txtSearch.Text.Trim());
            this.txtSearch.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.AccId = 0;
            this.UserName = "";
            this.Close();
        }

        private void FrmDriverFindUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.AccId = 0;
                this.UserName = "";
                this.Close();
            }
        }

        private void dgvAccount_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.AccId = int.Parse(this.dgvAccount.Rows[e.RowIndex].Cells["dgvAccountId"].Value.ToString());
                this.UserName = this.dgvAccount.Rows[e.RowIndex].Cells["dgvAccountUserName"].Value.ToString();
                this.FullName = this.dgvAccount.Rows[e.RowIndex].Cells["dgvAccountFullName"].Value.ToString();
                this.Close();
            }
            catch
            {

            }
        }

        private void btnSearchVehicle_Click(object sender, EventArgs e)
        {
            this.dgvAccount.AutoGenerateColumns = false;
            this.dgvAccount.EnableHeadersVisualStyles = false;
            this.dgvAccount.DataSource = this.objDriver.getDataAsDriver(this.txtSearch.Text.Trim());
            this.txtSearch.Focus();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                this.dgvAccount.Focus();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.dgvAccount.AutoGenerateColumns = false;
            this.dgvAccount.EnableHeadersVisualStyles = false;
            this.dgvAccount.DataSource = this.objDriver.getDataAsDriver(this.txtSearch.Text.Trim());
            this.txtSearch.Focus();
        }
    }
}
