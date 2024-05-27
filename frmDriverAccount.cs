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
    public partial class frmDriverAccount : Form
    {
        #region declare objects
        HMXHTD.Core.Driver objDriver = new Core.Driver();
        Account objAccount = new Account();
        #endregion

        public frmDriverAccount()
        {
            InitializeComponent();
        }

        private void frmDriverAccount_Shown(object sender, EventArgs e)
        {
            this.getData();
            ActiveEditControl(false);
            this.txtSearch.Focus();
        }
        private void ActiveEditControl(bool active)
        {
            if (active)
            {
                this.btnSave.Enabled = true;
                this.txtVehicleList.Enabled = true;
            }
            else
            {
                this.btnSave.Enabled = false;
                this.txtVehicleList.Enabled = false;
            }
        }

        private void getData()
        {
            this.dgvAccount.AutoGenerateColumns = false;
            this.dgvAccount.EnableHeadersVisualStyles = false;
            this.dgvAccount.DataSource = this.objDriver.getDataAsDriver(this.txtSearch.Text.Trim());
            this.lblCountItem.Text = this.dgvAccount.RowCount.ToString();
        }

        private void dgvAccount_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.txtUserName.Text = this.dgvAccount.Rows[e.RowIndex].Cells["dgvAccountUserName"].Value.ToString();
            this.txtVehicleList.Text = this.dgvAccount.Rows[e.RowIndex].Cells["dgvAccountVehicleList"].Value.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.dgvAccount.AutoGenerateColumns = false;
            this.dgvAccount.EnableHeadersVisualStyles = false;
            this.dgvAccount.DataSource = this.objDriver.getDataAsDriver(this.txtSearch.Text.Trim());

            this.lblCountItem.Text = this.dgvAccount.RowCount.ToString();
            this.txtSearch.Focus();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                this.dgvAccount.Focus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtVehicleList.Text.Trim() != "")
            {
                if (this.objAccount.setDataDriverVehicle(this.txtUserName.Text, this.txtVehicleList.Text) > 0)
                {
                    MessageBox.Show("Cập nhật thông tin thành công!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.dgvAccount.AutoGenerateColumns = false;
                    this.dgvAccount.EnableHeadersVisualStyles = false;
                    this.dgvAccount.DataSource = this.objDriver.getDataAsDriver(this.txtSearch.Text.Trim());
                    this.lblCountItem.Text = this.dgvAccount.RowCount.ToString();
                    this.txtSearch.Focus();
                    this.ActiveEditControl(false);
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin lỗi, vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void frmDriverAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearchVehicle_Click(object sender, EventArgs e)
        {
            this.getData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ActiveEditControl(true);
        }
    }
}
