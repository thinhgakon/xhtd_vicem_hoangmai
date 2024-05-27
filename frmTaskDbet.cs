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
    #region method frmTaskDbet
    public partial class frmTaskDbet : Form
    {
        #region declare objects
        Fun objFunc = new Fun();
        #endregion

        #region method frmTaskDbet
        public frmTaskDbet()
        {
            InitializeComponent();
        }
        #endregion

        #region method frmTaskDbet_Load
        private void frmTaskDbet_Load(object sender, EventArgs e)
        {
            this.getData();
        }
        #endregion

        #region method getData
        private void getData()
        {
            this.dgvCustomer.AutoGenerateColumns = false;
            this.dgvCustomer.DataSource = this.objFunc.getCustomers(this.txtSearch.Text);
        }
        #endregion

        #region method frmTaskDbet_KeyDown
        private void frmTaskDbet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region method txtSearch_TextChanged
        private void txtSearch_TextChanged(object sender, EventArgs e)
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

        #region method dgvCustomer_CellDoubleClick
        private void dgvCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                if (this.objFunc.updateCustomerLockInDbet(int.Parse(this.dgvCustomer.Rows[e.RowIndex].Cells["dgvCustomersIDDistributorSyn"].Value.ToString()),bool.Parse(this.dgvCustomer.Rows[e.RowIndex].Cells["dgvCustomersLockInDbet"].Value.ToString())) > 0)
                {
                    MessageBox.Show("Thay đổi trạng thái khóa nhà phân phối thành công!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtSearch.Text = "";
                    this.getData();
                }
                else
                {
                    MessageBox.Show("Lỗi: Thay đổi trạng thái khóa nhà phân phối thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion
    }
#endregion
}
