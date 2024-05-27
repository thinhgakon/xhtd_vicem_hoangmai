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
    public partial class frmTrough : Form
    {
        #region declare objects
        Trough objTrough = new Trough();
        int Id = 0;
        #endregion

        #region method frmTrough
        public frmTrough()
        {
            InitializeComponent();
        }
        #endregion

        #region method frmTrough_Load
        private void frmTrough_Load(object sender, EventArgs e)
        {
            this.dgvTrough.AutoGenerateColumns = false;
            this.dgvTrough.EnableHeadersVisualStyles = false;
            this.dgvTrough.DataSource = this.objTrough.getTrough();

            this.dgvProduct.AutoGenerateColumns = false;
            this.dgvProduct.EnableHeadersVisualStyles = false;
            this.dgvProduct.DataSource = this.objTrough.getProducts();
        }
        #endregion

        #region method dgvTrough_CellClick
        private void dgvTrough_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Id = 0;
            this.Id = int.Parse(this.dgvTrough.Rows[e.RowIndex].Cells["dgvTroughId"].Value.ToString());
            this.txtName.Text = this.dgvTrough.Rows[e.RowIndex].Cells["dgvTroughName"].Value.ToString();
            this.txtH.Text = this.dgvTrough.Rows[e.RowIndex].Cells["dgvTroughH"].Value.ToString();
            this.txtW.Text = this.dgvTrough.Rows[e.RowIndex].Cells["dgvTroughW"].Value.ToString();
            this.txtL.Text = this.dgvTrough.Rows[e.RowIndex].Cells["dgvTroughL"].Value.ToString();
            this.ckbState.Checked = bool.Parse(this.dgvTrough.Rows[e.RowIndex].Cells["dgvTroughState"].Value.ToString());

            for (int i = 0; i < this.dgvProduct.RowCount; i++)
            {
                if (this.dgvTrough.Rows[e.RowIndex].Cells["dgvTroughNameProductId"].Value.ToString().Contains("\""+this.dgvProduct.Rows[i].Cells["dgvProductIDProductSyn"].Value.ToString().ToUpper() + "\""))
                {
                    this.dgvProduct.Rows[i].Cells["dgvProductSelect"].Value = 1;
                }
                else
                {
                    this.dgvProduct.Rows[i].Cells["dgvProductSelect"].Value = 0;
                }
            }
        }
        #endregion

        #region method btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtName.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập tên máng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtName.Focus();
                return;
            }

            if (this.Id == 0)
            {
                MessageBox.Show("Chưa xác định thông tin máng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtName.Focus();
                return;
            }

            string ProductId = "";

            for (int i = 0; i < this.dgvProduct.RowCount; i++)
            {
                if (this.dgvProduct.Rows[i].Cells["dgvProductSelect"].Value.ToString().ToUpper() == "1")
                {
                    ProductId += "\"" + this.dgvProduct.Rows[i].Cells["dgvProductIDProductSyn"].Value.ToString() + "\";";
                }
            }

            if (ProductId.Trim() == "")
            {
                MessageBox.Show("Chưa xác định thông tin sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtName.Focus();
                return;
            }

            if (this.objTrough.setTrough(this.Id, this.txtName.Text, ProductId, this.ckbState.Checked, txtH.Text.Trim(), txtW.Text.Trim(),txtL.Text.Trim()) == 1)
            {
                MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.dgvTrough.AutoGenerateColumns = false;
                this.dgvTrough.EnableHeadersVisualStyles = false;
                this.dgvTrough.DataSource = this.objTrough.getTrough();
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region method btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region method ckbSelectAll_CheckedChanged
        private void ckbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dgvProduct.RowCount; i++)
            {
                this.dgvProduct.Rows[i].Cells["dgvProductSelect"].Value = this.ckbSelectAll.Checked;
            }
        }
        #endregion

        #region dgvTrough_CellEnter
        private void dgvTrough_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.Id = 0;
            this.Id = int.Parse(this.dgvTrough.Rows[e.RowIndex].Cells["dgvTroughId"].Value.ToString());
            this.txtName.Text = this.dgvTrough.Rows[e.RowIndex].Cells["dgvTroughName"].Value.ToString();
            this.txtH.Text = this.dgvTrough.Rows[e.RowIndex].Cells["dgvTroughH"].Value.ToString();
            this.txtW.Text = this.dgvTrough.Rows[e.RowIndex].Cells["dgvTroughW"].Value.ToString();
            this.txtL.Text = this.dgvTrough.Rows[e.RowIndex].Cells["dgvTroughL"].Value.ToString();
            this.ckbState.Checked = bool.Parse(this.dgvTrough.Rows[e.RowIndex].Cells["dgvTroughState"].Value.ToString());

            for (int i = 0; i < this.dgvProduct.RowCount; i++)
            {
                if (this.dgvTrough.Rows[e.RowIndex].Cells["dgvTroughNameProductId"].Value.ToString().Contains("\"" + this.dgvProduct.Rows[i].Cells["dgvProductIDProductSyn"].Value.ToString().ToUpper() + "\""))
                {
                    this.dgvProduct.Rows[i].Cells["dgvProductSelect"].Value = 1;
                }
                else
                {
                    this.dgvProduct.Rows[i].Cells["dgvProductSelect"].Value = 0;
                }
            }
        } 
        #endregion

        #region method frmTrough_KeyDown
        private void frmTrough_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion
    }
}
