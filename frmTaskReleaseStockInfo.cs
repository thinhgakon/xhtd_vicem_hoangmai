using HMXHTD.Core;
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
    public partial class frmTaskReleaseStockInfo : Form
    {
        Account objAccount = new Account();
        int dgvAccount_Curr_Index = -1;

        public frmTaskReleaseStockInfo()
        {
            InitializeComponent();
        }

        private void frmTaskReleaseStockInfo_Load(object sender, EventArgs e)
        {
            this.dgvAccount.AutoGenerateColumns = false;
            this.dgvAccount.DataSource = this.objAccount.getDataStockInfo("");

            this.dgvTrough.AutoGenerateColumns = false;
            this.dgvTrough.DataSource = this.objAccount.getDataTrough();
        }

        private void frmTaskReleaseStockInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int totalItem = 0;
            this.objAccount.delAccountOrderTypeProduct(this.dgvAccount.Rows[this.dgvAccount_Curr_Index].Cells["dgvAccountUserName"].Value.ToString());
            if (this.ckbPCB30.Checked)
            {
                totalItem += this.objAccount.setAccountOrderTypeProduct(this.dgvAccount.Rows[this.dgvAccount_Curr_Index].Cells["dgvAccountUserName"].Value.ToString(), "PCB30");
            }

            if (this.ckbPCB40.Checked)
            {
                totalItem += this.objAccount.setAccountOrderTypeProduct(this.dgvAccount.Rows[this.dgvAccount_Curr_Index].Cells["dgvAccountUserName"].Value.ToString(), "PCB40");
            }

            if (this.ckbROI.Checked)
            {
                totalItem += this.objAccount.setAccountOrderTypeProduct(this.dgvAccount.Rows[this.dgvAccount_Curr_Index].Cells["dgvAccountUserName"].Value.ToString(), "ROI");
            }

            if (this.ckbClinker.Checked)
            {
                totalItem += this.objAccount.setAccountOrderTypeProduct(this.dgvAccount.Rows[this.dgvAccount_Curr_Index].Cells["dgvAccountUserName"].Value.ToString(), "CLINKER");
            }

            if (this.ckbXuatKhau.Checked)
            {
                totalItem += this.objAccount.setAccountOrderTypeProduct(this.dgvAccount.Rows[this.dgvAccount_Curr_Index].Cells["dgvAccountUserName"].Value.ToString(), "XK");
            }

            this.objAccount.delAccountOrderLineCode(this.dgvAccount.Rows[this.dgvAccount_Curr_Index].Cells["dgvAccountUserName"].Value.ToString());
           
            for (int i = 0; i < this.dgvTrough.RowCount; i++)
            {
                if (this.dgvTrough.Rows[i].Cells["dgvTroughSelect"].Value.ToString() == "1")
                {
                    totalItem += this.objAccount.setAccountOrderLineCode(this.dgvAccount.Rows[this.dgvAccount_Curr_Index].Cells["dgvAccountUserName"].Value.ToString(), this.dgvTrough.Rows[i].Cells["dgvTroughLineCode"].Value.ToString());
                }
            }
            if (totalItem > 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi xảy ra khi cập nhật thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvTrough_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvAccount_Curr_Index = e.RowIndex;

            this.ckbPCB30.Checked = this.objAccount.getAccountOrderTypeProduct(this.dgvAccount.Rows[e.RowIndex].Cells["dgvAccountUserName"].Value.ToString(), "PCB30");
            this.ckbPCB40.Checked = this.objAccount.getAccountOrderTypeProduct(this.dgvAccount.Rows[e.RowIndex].Cells["dgvAccountUserName"].Value.ToString(), "PCB40");
            this.ckbROI.Checked = this.objAccount.getAccountOrderTypeProduct(this.dgvAccount.Rows[e.RowIndex].Cells["dgvAccountUserName"].Value.ToString(), "ROI");
            this.ckbClinker.Checked = this.objAccount.getAccountOrderTypeProduct(this.dgvAccount.Rows[e.RowIndex].Cells["dgvAccountUserName"].Value.ToString(), "CLINKER");
            this.ckbXuatKhau.Checked = this.objAccount.getAccountOrderTypeProduct(this.dgvAccount.Rows[e.RowIndex].Cells["dgvAccountUserName"].Value.ToString(), "XK");

            for (int i = 0; i < this.dgvTrough.RowCount; i++)
            {
                this.dgvTrough.Rows[i].Cells["dgvTroughSelect"].Value = 0;
                this.dgvTrough.Rows[i].Cells["dgvTroughSelect"].Value = this.objAccount.getAccountOrderLineCode(this.dgvAccount.Rows[e.RowIndex].Cells["dgvAccountUserName"].Value.ToString(), this.dgvTrough.Rows[i].Cells["dgvTroughLineCode"].Value.ToString());
            }
        }
    }
}
