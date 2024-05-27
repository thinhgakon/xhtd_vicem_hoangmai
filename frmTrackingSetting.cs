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
    public partial class frmTrackingSetting : Form
    {
        #region declare objects
        HMXHTD.Core.Driver objDriver = new Core.Driver();
        private string CurrUserName = "";
        public int OrderId = 0, StepId = 0;
        #endregion

        #region method frmTrackingSetting
        public frmTrackingSetting()
        {
            InitializeComponent();
        }
        #endregion

        #region method frmTrackingSetting_KeyDown
        private void frmTrackingSetting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region method btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.CurrUserName == "")
            {
                MessageBox.Show("Bạn chưa chọn thông tin lái xe!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.txtDriverName.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn thông tin lái xe!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string tmpValue = this.objDriver.setOrderTracking(this.OrderId, this.CurrUserName, this.txtDriverName.Text.Trim(), this.txtVehicle.Text.Trim(), null);
            if (tmpValue == "OK")
            {
                MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (tmpValue != "")
            {
                MessageBox.Show(tmpValue, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Cập nhật dữ liệu thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region method btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region method frmTrackingSetting_Shown
        private void frmTrackingSetting_Shown(object sender, EventArgs e)
        {
            this.dgvDrive.AutoGenerateColumns = false;
            this.dgvDrive.EnableHeadersVisualStyles = false;
            this.dgvDrive.DataSource = this.objDriver.getDataAsDriver(this.txtSearch.Text.Trim());
            this.txtSearch.Focus();
            this.getDataBillOrder();
        }
        #endregion

        #region method getDataBillOrder
        private void getDataBillOrder()
        {
            DataTable objTable = new DataTable();
            objTable = this.objDriver.getStoreOrderInfo(this.OrderId);
            if (objTable.Rows.Count > 0)
            {
                if (objTable.Rows[0]["DriverUserName"].ToString() != "")
                {
                    this.CurrUserName = objTable.Rows[0]["DriverUserName"].ToString();
                }
                else
                {
                    this.CurrUserName = "";
                }
                if (objTable.Rows[0]["TrackingBegin"].ToString() != "")
                {
                    this.btnSave.Visible = false;
                    this.btnSave.Enabled = false;

                    this.dgvDrive.Enabled = false;

                    this.btnCancel.Enabled = true;
                    this.btnCancel.Visible = true;
                }
                else if (objTable.Rows[0]["DriverUserName"].ToString() != "")
                {
                    this.btnSave.Visible = false;
                    this.btnSave.Enabled = false;

                    this.dgvDrive.Enabled = false;
                    
                    this.btnCancel.Enabled = true;
                    this.btnCancel.Visible = true;
                }
                else
                {
                    this.btnSave.Visible = true;
                    this.btnSave.Enabled = true;

                    this.dgvDrive.Enabled = true;

                    this.btnCancel.Visible = false;
                    this.btnCancel.Enabled = false;
                }

                if (objTable.Rows[0]["DayFinish"].ToString() != "")
                {
                    this.btnSave.Visible = false;
                    this.btnSave.Enabled = false;

                    this.btnCancel.Enabled = false;
                    this.dgvDrive.Enabled = false;
                }
            }
        }
        #endregion

        #region method btnSearch_Click
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.dgvDrive.AutoGenerateColumns = false;
            this.dgvDrive.EnableHeadersVisualStyles = false;
            this.dgvDrive.DataSource = this.objDriver.getDataAsDriver(this.txtSearch.Text.Trim());
            this.txtSearch.Focus();
        }
        #endregion

        #region method txtSearch_TextChanged
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.dgvDrive.AutoGenerateColumns = false;
            this.dgvDrive.EnableHeadersVisualStyles = false;
            this.dgvDrive.DataSource = this.objDriver.getDataAsDriver(this.txtSearch.Text.Trim());
        }
        #endregion

        #region method dgvDrive_CellEnter
        private void dgvDrive_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.btnSave.Visible)
            {
                try
                {
                    this.CurrUserName = this.dgvDrive.Rows[e.RowIndex].Cells["dgvDriveUserName"].Value.ToString();
                    this.txtDriverName.Text = this.dgvDrive.Rows[e.RowIndex].Cells["dgvDriveFullName"].Value.ToString();
                }
                catch
                {
                    this.CurrUserName = "";
                }
            }
            else
            {
                return;
            }
        }
        #endregion

        #region method btnCancel_Click
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.objDriver.delOrderTracking(this.OrderId) > 0)
            {
                MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.getDataBillOrder();
            }
            else
            {
                MessageBox.Show("Cập nhật dữ liệu thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}
