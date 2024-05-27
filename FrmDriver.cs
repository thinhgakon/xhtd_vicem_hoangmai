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
    public partial class FrmDriver : Form
    {
        #region declate objects
        private Core.Driver objDriver = new Core.Driver();
        private int CurrId = 0;
        #endregion

        #region method FrmDriver
        public FrmDriver()
        {
            InitializeComponent();
        }
        #endregion

        #region method getData
        private void getData()
        {
            this.dgvDrive.AutoGenerateColumns = false;
            this.dgvDrive.EnableHeadersVisualStyles = false;
            this.dgvDrive.DataSource = this.objDriver.getData(this.txtSearch.Text);
        }
        #endregion

        #region method FrmDriver_KeyDown
        private void FrmDriver_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
         #endregion

        #region method FrmDriver_FormClosing
        private void FrmDriver_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
         #endregion

        #region method FrmDriver_Shown
        private void FrmDriver_Shown(object sender, EventArgs e)
        {
            this.getData();
        }
         #endregion

        #region method dgvDrive_CellClick

        #endregion

        #region method btnAdd_Click
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.btnDel.Enabled = false;
            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;

            this.txtFullName.ReadOnly = false;
            this.txtPhone.ReadOnly = false;
            this.txtAddress.ReadOnly = false;
            this.txtIdCard.ReadOnly = false;

            this.txtDriverUserName.ReadOnly = false;

            this.CurrId = 0;
            this.txtFullName.Text = "";
            this.txtPhone.Text = "";
            this.txtAccId.Text = "";
            this.txtAccount.Text = "";
            this.txtAddress.Text = "";
            this.txtIdCard.Text = "";
            this.txtDriverUserName.Text = "";
            this.txtFullName.Focus();
        }
        #endregion

        #region method btnEdit_Click
        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.btnDel.Enabled = false;
            this.txtFullName.ReadOnly = false;
            this.txtPhone.ReadOnly = false;
            this.txtAddress.ReadOnly = false;
            this.txtIdCard.ReadOnly = false;

            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;

            this.txtFullName.Focus();
        }
        #endregion

        #region method btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtFullName.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập họ, tên lái xe","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtFullName.Focus();
                return;
            }

            if (this.txtPhone.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại của lái xe", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtPhone.Focus();
                return;
            }

            int AccId = 0;

            if (this.txtAccId.Text.Trim() != "")
            {
                try
                {
                    AccId = int.Parse(this.txtAccId.Text.Trim());
                }
                catch
                {
                    AccId = 0;
                }
            }
            var responseId = this.objDriver.setData(this.CurrId, this.txtFullName.Text.Trim(), this.txtPhone.Text.Trim(), this.txtIdCard.Text.Trim(), this.txtAddress.Text.Trim(), this.txtDriverUserName.Text.Trim());
            if (responseId == -2)
            {
                MessageBox.Show("Tài khoản đã tồn tại, vui lòng thử lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (responseId == 1)
            {
                this.getData();
                this.btnAdd.Enabled = true;
                this.btnEdit.Enabled = true;
                this.btnSave.Enabled = false;
                this.btnCancel.Enabled = false;

                this.txtFullName.ReadOnly = true;
                this.txtPhone.ReadOnly = true;
                this.txtAddress.ReadOnly = true;
                this.txtIdCard.ReadOnly = true;
            }
            else if(responseId == -1)
            {
                MessageBox.Show("Lỗi xảy ra khi cập nhật thông tin vào oracle, vui lòng thử lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Lỗi xảy ra khi cập nhật thông tin","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region method btnCancel_Click
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.btnAdd.Enabled = true;
            this.btnEdit.Enabled = true;
            this.btnSave.Enabled = false;
            this.btnCancel.Enabled = false;

            this.txtFullName.ReadOnly = true;
            this.txtPhone.ReadOnly = true;
            this.txtAddress.ReadOnly = true;
            this.txtIdCard.ReadOnly = true;

            this.txtFullName.Text = "";
            this.txtPhone.Text = "";
            this.txtIdCard.Text = "";
            this.txtAddress.Text = "";
            this.txtDriverUserName.Text = "";
        }
        #endregion

        #region method btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region method dgvDrive_CellEnter
        private void dgvDrive_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.CurrId = int.Parse(this.dgvDrive.Rows[e.RowIndex].Cells["dgvDriveId"].Value.ToString());
                this.txtFullName.Text = this.dgvDrive.Rows[e.RowIndex].Cells["dgvDriveFullName"].Value.ToString();
                this.txtPhone.Text = this.dgvDrive.Rows[e.RowIndex].Cells["dgvDrivePhone"].Value.ToString();
                this.txtAccId.Text = this.dgvDrive.Rows[e.RowIndex].Cells["dgvDriveAccId"].Value.ToString();
                this.txtAccount.Text = this.dgvDrive.Rows[e.RowIndex].Cells["dgvDriveUserName"].Value.ToString();
                this.txtAddress.Text = this.dgvDrive.Rows[e.RowIndex].Cells["dgvDriveAddress"].Value.ToString();
                this.txtDriverUserName.Text = this.dgvDrive.Rows[e.RowIndex].Cells["dgvDriveUserName"].Value.ToString();
                this.btnDel.Enabled = true;
            }
            catch
            {

            }
        }
        #endregion

        #region method btnSearch_Click
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.getData();
        }
        #endregion

        #region method txtSearch_TextChanged
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.getData();
        }
        #endregion

        #region method btnSearchVehicle_Click
        private void btnSearchVehicle_Click(object sender, EventArgs e)
        {
            FrmDriverFindUserName objFrmDriverFindUserName = new FrmDriverFindUserName();
            objFrmDriverFindUserName.ShowDialog();
            if (objFrmDriverFindUserName.AccId > 0)
            {
                this.txtAccId.Text = objFrmDriverFindUserName.AccId.ToString();
                this.txtAccount.Text = objFrmDriverFindUserName.UserName;
            }
        }
        #endregion

        #region method btnDel_Click
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (this.CurrId > 0)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa thông tin này không?","Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (this.objDriver.delData(this.CurrId) > 0)
                    {
                        this.txtFullName.Text = "";
                        this.txtPhone.Text = "";
                        this.txtIdCard.Text = "";
                        this.txtAddress.Text = "";
                        this.txtDriverUserName.Text = "";

                        this.getData();
                    }
                }
            }
        }
        #endregion

        #region method dgvDrive_MouseDoubleClick
        #endregion

        #region method dgvDrive_CellDoubleClick

        private void dgvDrive_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                FrmDriverFindUserName objFrmDriverFindUserName = new FrmDriverFindUserName();
                objFrmDriverFindUserName.txtSearch.Text = this.dgvDrive.Rows[e.RowIndex].Cells["dgvDriveFullName"].Value.ToString();
                objFrmDriverFindUserName.ShowDialog();
                if (objFrmDriverFindUserName.AccId > 0)
                {
                    this.objDriver.updateDriverUserName(int.Parse(this.dgvDrive.Rows[e.RowIndex].Cells["dgvDriveId"].Value.ToString()), objFrmDriverFindUserName.AccId, objFrmDriverFindUserName.UserName.ToString(), objFrmDriverFindUserName.FullName.ToString());
                    this.dgvDrive.Rows[e.RowIndex].Cells["dgvDriveAccId"].Value = objFrmDriverFindUserName.AccId.ToString();
                    this.dgvDrive.Rows[e.RowIndex].Cells["dgvDriveUserName"].Value = objFrmDriverFindUserName.UserName.ToString();
                    this.dgvDrive.Rows[e.RowIndex].Cells["dgvDriveFullName"].Value = objFrmDriverFindUserName.FullName.ToString();
                }
            }
        }
        #endregion
    }
}
