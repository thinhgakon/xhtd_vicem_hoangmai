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
    public partial class frmRFIDRegister : Form
    {
        #region declare Objects
        private RFID objRFID = new RFID();
        private Vehicle objVehicle = new Vehicle();
        private int Id = 0;
        private bool sFcheck = false;
        #endregion

        #region method frmRFIDRegister
        public frmRFIDRegister()
        {
            InitializeComponent();
        }
        #endregion

        #region method btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtCode.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số hiệu thẻ","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtCode.Focus();
                return;
            }

            if (this.txtVehicle.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập thông tin phương tiện", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtVehicle.Focus();
                return;
            }

            DateTime? DayReleased = null;
            if (this.txtDayReleased.Text.Replace(" ", "").Length != 10)
            {
                MessageBox.Show("Bạn chưa nhập ngày phát hành thẻ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtDayReleased.Focus();
                return;
            }

            try
            {
                DayReleased = new DateTime(int.Parse(txtDayReleased.Text.Trim().Replace(" ", "").Substring(6, 4)), int.Parse(txtDayReleased.Text.Trim().Replace(" ", "").Substring(3, 2)), int.Parse(txtDayReleased.Text.Trim().Replace(" ", "").Substring(0, 2)), 0,0,0);
            }
            catch
            {
                DayReleased = null;
            }

            if (DayReleased == null)
            {
                MessageBox.Show("Bạn chưa nhập ngày phát hành thẻ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtDayReleased.Focus();
                return;
            }

            DateTime? DayExpired = null;
            if (this.txtDayExpired.Text.Replace(" ", "").Length != 10)
            {
                MessageBox.Show("Bạn chưa nhập ngày hết hạn thẻ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtDayExpired.Focus();
                return;
            }

            try
            {
                DayExpired = new DateTime(int.Parse(txtDayExpired.Text.Trim().Replace(" ", "").Substring(6, 4)), int.Parse(txtDayExpired.Text.Trim().Replace(" ", "").Substring(3, 2)), int.Parse(txtDayExpired.Text.Trim().Replace(" ", "").Substring(0, 2)), 0, 0, 0);
            }
            catch
            {
                DayExpired = null;
            }

            if (DayExpired == null)
            {
                MessageBox.Show("Bạn chưa nhập ngày phát hành thẻ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtDayReleased.Focus();
                return;
            }

            if (objRFID.CheckVehicleExit(this.txtCode.Text.Trim(), this.txtVehicle.Text.Trim()) && sFcheck)
            {
                MessageBox.Show("Số xe hoặc số thẻ đã tồn tại trên hệ thống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string strMsg = "";
            if (this.objRFID.setData(this.Id, this.txtCode.Text, this.txtVehicle.Text, DayReleased.Value, DayExpired.Value, "", true, ref strMsg) == 1)
            {
                this.txtCode.ReadOnly = true;
                this.txtDayReleased.ReadOnly = true;
                this.txtDayExpired.ReadOnly = true;
                this.txtVehicle.ReadOnly = true;
                this.txtCode.Focus();

                this.Id = 0;
                this.txtCode.Text = "";
                this.txtDayReleased.Text = "";
                this.txtDayExpired.Text = "";
                this.txtVehicle.Text = "";
               
                this.btnSearchVehicle.Enabled = false;
            }
            else
            {
                MessageBox.Show(strMsg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
        }
        #endregion

        #region method btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region method btnSearchVehicle_Click
        private void btnSearchVehicle_Click(object sender, EventArgs e)
        {
            frmVehicle objfrmVehicle = new frmVehicle();
            objfrmVehicle.ShowDialog();
            txtVehicle.Text = objfrmVehicle.VehicleCode;
        }
        #endregion

        #region method frmRFIDRegister_KeyDown
        private void frmRFIDRegister_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion
    }
}
