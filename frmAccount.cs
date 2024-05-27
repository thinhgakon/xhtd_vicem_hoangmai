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
    public partial class frmAccount : Form
    {
        #region declare objects
        private Account objAccount = new Account();
        private string Curr_UserName = "";
        #endregion

        #region method frmAccount
        public frmAccount()
        {
            InitializeComponent();
        }
        #endregion

        #region method getData
        private void getData()
        {
            this.dgvAccount.AutoGenerateColumns = false;
            this.dgvAccount.EnableHeadersVisualStyles = false;
            this.dgvAccount.DataSource = this.objAccount.getData(this.txtSearch.Text);
        }
        #endregion

        #region method frmAccount_Shown
        private void frmAccount_Shown(object sender, EventArgs e)
        {
            this.getData();
        }
        #endregion

        #region method frmAccount_KeyDown
        private void frmAccount_KeyDown(object sender, KeyEventArgs e)
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

        #region method txtSearch_KeyDown
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {

        }
        #endregion

        #region method btnSearch_Click
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.getData();
        }
        #endregion

        #region method dgvAccount_CellEnter
        private void dgvAccount_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.Curr_UserName = "";
            
            this.ckbSystem.Checked = false;
            this.ckbAccount.Checked = false;

            this.ckbTaskOperating.Checked = false;
            this.ckbTaskConfirm.Checked = false;
            this.ckbTaskInOut.Checked = false;
            this.ckbTaskScale.Checked = false;
            this.ckbTaskRelease.Checked = false;
            this.ckbTaskRelease2.Checked = false;
            this.ckbTaskDbet.Checked = false; 

            this.ckbReportConfirm.Checked = false;
            this.ckbReportInOut.Checked = false;
            this.ckbReportScale.Checked = false;
            this.ckbReportRelease.Checked = false;

            this.ckbTrough.Checked = false;
            this.ckbRFID.Checked = false;
            this.ckbDevice.Checked = false;
            this.ckbVehicle.Checked = false;
            this.ckbDriver.Checked = false;
            this.ckbDriverAccount.Checked = false;

            this.cbbHomePage.SelectedIndex = 0;

            try
            {
                this.Curr_UserName = this.dgvAccount.Rows[e.RowIndex].Cells["dgvAccountUserName"].Value.ToString();

                DataTable objTable = this.objAccount.getDataByUserName(this.Curr_UserName);
                if (objTable.Rows.Count > 0)
                {
                    //kcs
                    this.ckbAdminKCS.Checked = bool.Parse(objTable.Rows[0]["AdminKCS"].ToString());
                    this.ckbViewKCS.Checked = bool.Parse(objTable.Rows[0]["ViewKCS"].ToString());
                    //end kcs

                    this.ckbSystem.Checked = bool.Parse(objTable.Rows[0]["SysOperating"].ToString());
                    this.ckbAccount.Checked = bool.Parse(objTable.Rows[0]["SysAccount"].ToString());

                    this.ckbTaskOperating.Checked = bool.Parse(objTable.Rows[0]["TaskOperating"].ToString());
                    this.ckbTaskConfirm.Checked = bool.Parse(objTable.Rows[0]["TaskConfirm"].ToString());
                    this.ckbTaskInOut.Checked = bool.Parse(objTable.Rows[0]["TaskInOut"].ToString());
                    this.ckbTaskScale.Checked = bool.Parse(objTable.Rows[0]["TaskScale"].ToString());
                    this.ckbTaskRelease.Checked = bool.Parse(objTable.Rows[0]["TaskRelease"].ToString());
                    this.ckbTaskRelease2.Checked = bool.Parse(objTable.Rows[0]["TaskRelease2"].ToString());
                    this.ckbTaskDbet.Checked = bool.Parse(objTable.Rows[0]["TaskDbet"].ToString());

                    this.ckbReportConfirm.Checked = bool.Parse(objTable.Rows[0]["ReportConfirm"].ToString());
                    this.ckbReportInOut.Checked = bool.Parse(objTable.Rows[0]["ReportInOut"].ToString());
                    this.ckbReportScale.Checked = bool.Parse(objTable.Rows[0]["ReportScale"].ToString());
                    this.ckbReportRelease.Checked = bool.Parse(objTable.Rows[0]["ReportRelease"].ToString());

                    this.ckbTrough.Checked = bool.Parse(objTable.Rows[0]["DirTrough"].ToString());
                    this.ckbRFID.Checked = bool.Parse(objTable.Rows[0]["DirRFID"].ToString());
                    this.ckbDevice.Checked = bool.Parse(objTable.Rows[0]["DirDevice"].ToString());
                    this.ckbVehicle.Checked = bool.Parse(objTable.Rows[0]["DirVehicle"].ToString());
                    this.ckbDriver.Checked = bool.Parse(objTable.Rows[0]["DirDriver"].ToString());
                    this.ckbDriverAccount.Checked = bool.Parse(objTable.Rows[0]["DirDriverAccount"].ToString());

                    this.cbbHomePage.SelectedIndex = int.Parse(objTable.Rows[0]["HomePage"].ToString());
                }

                this.setControlState(false);
                this.btnEdit.Enabled = true;
            }
            catch
            {
                this.Curr_UserName = "";
                this.btnSave.Enabled = false;
            }
        }
        #endregion

        #region method btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.Curr_UserName.Trim() != "")
            {
                int HomePage = 0;
                try
                {
                    HomePage = int.Parse(this.cbbHomePage.SelectedIndex.ToString());
                }
                catch
                {
                    HomePage = 0;
                }
                if (this.objAccount.setData(this.Curr_UserName, this.ckbSystem.Checked, this.ckbAccount.Checked,this.ckbTaskOperating.Checked,this.ckbTaskConfirm.Checked,this.ckbTaskInOut.Checked,
                    this.ckbTaskScale.Checked,this.ckbTaskRelease.Checked, this.ckbTaskRelease2.Checked, this.ckbTaskDbet.Checked, this.ckbReportConfirm.Checked,this.ckbReportInOut.Checked, this.ckbReportScale.Checked, this.ckbReportRelease.Checked,
                    this.ckbTrough.Checked, this.ckbRFID.Checked, this.ckbDevice.Checked, this.ckbVehicle.Checked, this.ckbDriver.Checked, this.ckbDriverAccount.Checked, HomePage, this.ckbAdminKCS.Checked, this.ckbViewKCS.Checked) == 1)
                {
                    MessageBox.Show("Cập nhật thông tin thành công!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.btnEdit.Enabled = true;
                    this.btnSave.Enabled = false;
                    this.btnCancel.Enabled = false;

                    this.setControlState(false);
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa xác định thông tin tài khoản","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region method btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region method btnEdit_Click
        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.btnEdit.Enabled = false;
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.setControlState(true);
        }
        #endregion

        #region method btnCancel_Click
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.btnEdit.Enabled = true;
            this.btnSave.Enabled = false;
            this.btnCancel.Enabled = false;

            this.setControlState(false);
        }
        #endregion

        #region method setControlState
        private void setControlState(bool State)
        {
            this.ckbSystem.Enabled = State;
            this.ckbAccount.Enabled = State;

            this.ckbTaskOperating.Enabled = State;
            this.ckbTaskConfirm.Enabled = State;
            this.ckbTaskInOut.Enabled = State;
            this.ckbTaskScale.Enabled = State;
            this.ckbTaskRelease.Enabled = State;
            this.ckbTaskRelease2.Enabled = State;
            this.ckbTaskDbet.Enabled = State;

            this.ckbReportConfirm.Enabled = State;
            this.ckbReportInOut.Enabled = State;
            this.ckbReportScale.Enabled = State;
            this.ckbReportRelease.Enabled = State;

            this.ckbTrough.Enabled = State;
            this.ckbRFID.Enabled = State;
            this.ckbDevice.Enabled = State;
            this.ckbVehicle.Enabled = State;
            this.ckbDriver.Enabled = State;
            this.ckbDriverAccount.Enabled = State;

            this.cbbHomePage.Enabled = State;
        }
        #endregion
    }
}
