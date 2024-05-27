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
    public partial class frmRFID : Form
    {
        #region Declare Objects
        RFID objRFID = new RFID();
        Vehicle objVehicle = new Vehicle();
        public static string sVehicleCode = "", W = "", H = "", L="";
        int Id = 0;
        private bool sFcheck = false;
        #endregion

        #region method InitializeComponent
        public frmRFID()
        {
            InitializeComponent();
        }
        #endregion

        #region method btnAdd_Click
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Id = 0;
            this.txtCode.Text = "";
            this.txtDayReleased.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txtDayExpired.Text = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");
            this.txtVehicle.Text = "";
            this.ckbState.Enabled = true;
            this.sFcheck = true;

            this.txtCode.ReadOnly = false;
            this.txtDayReleased.ReadOnly = false;
            this.txtDayExpired.ReadOnly = false;
            //this.txtVehicle.ReadOnly = false;
            this.txtCode.Focus();

            this.btnAdd.Enabled = false;
            this.brnEdit.Enabled = false;
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;

            this.dgvRFID.Enabled = false;
            this.btnSearchVehicle.Enabled = true;

            this.btnSearchVehicle.Enabled = true;
            this.btnSearchVehicle.Visible = true;
            this.ckbState.Checked = true;

            this.btnSearchVehicle.PerformClick();
        }
        #endregion

        #region method brnEdit_Click
        private void brnEdit_Click(object sender, EventArgs e)
        {
            this.btnSearchVehicle.Enabled = true;
            this.txtDayReleased.ReadOnly = false;
            this.txtDayExpired.ReadOnly = false;
            this.txtVehicle.Focus();

            this.btnAdd.Enabled = false;
            this.brnEdit.Enabled = false;
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.ckbState.Enabled = true;
            this.sFcheck = false;
            this.dgvRFID.Enabled = false;

            this.txtCode.ReadOnly = false;
            this.txtVehicle.ReadOnly = false;
        }
        #endregion

        #region method btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtCode.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số hiệu thẻ, vui lòng kiểm tra lại!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtCode.Focus();
                return;
            }

            if (this.txtVehicle.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập thông tin phương tiện, vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtVehicle.Focus();
                return;
            }

            DateTime? DayReleased = null;
            if (this.txtDayReleased.Text.Replace(" ", "").Length != 10)
            {
                MessageBox.Show("Bạn chưa nhập ngày phát hành thẻ, vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Bạn chưa nhập ngày phát hành thẻ, vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtDayReleased.Focus();
                return;
            }

            DateTime? DayExpired = null;
            if (this.txtDayExpired.Text.Replace(" ", "").Length != 10)
            {
                MessageBox.Show("Bạn chưa nhập ngày hết hạn thẻ, vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Bạn chưa nhập ngày phát hành thẻ, vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtDayReleased.Focus();
                return;
            }

            if (objRFID.CheckVehicleExit(this.txtCode.Text.Trim(), this.txtVehicle.Text.Trim()) && sFcheck)
            {
                MessageBox.Show("Số xe hoặc số thẻ đã tồn tại trên hệ thống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string strMsg = "";
            if (this.objRFID.setData(this.Id, this.txtCode.Text, this.txtVehicle.Text, DayReleased.Value, DayExpired.Value, "", this.ckbState.Checked, ref strMsg) == 1)
            {
                // cập nhật thẻ cho các đơn hàng đang có hiệu lực
                this.objRFID.saveRfidToBillOrder(this.txtCode.Text, this.txtVehicle.Text.Trim());

                objVehicle.UpdateData(this.txtVehicle.Text.Trim(), txtH.Text.Trim(), txtW.Text.Trim(), txtL.Text.Trim());
                this.txtCode.ReadOnly = true;
                this.txtDayReleased.ReadOnly = true;
                this.txtDayExpired.ReadOnly = true;
                this.txtVehicle.ReadOnly = true;
                this.txtCode.Focus();

                this.btnAdd.Enabled = true;
                this.brnEdit.Enabled = true;
                this.btnSave.Enabled = false;
                this.btnCancel.Enabled = false;

                this.Id = 0;
                this.txtCode.Text = "";
                this.txtDayReleased.Text = "";
                this.txtDayExpired.Text = "";
                this.txtVehicle.Text = "";
               
                this.btnSearchVehicle.Enabled = false;
                dgvRFID.Enabled = true;
                this.getData();
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

        #region method getData
        private void getData()
        {
            this.dgvRFID.AutoGenerateColumns = false;
            this.dgvRFID.EnableHeadersVisualStyles = false;
            this.bindingSource1.DataSource = this.objRFID.getRFID(this.txtSearchKey.Text.Trim());
            bindingNavigator1.BindingSource = this.bindingSource1;
            this.dgvRFID.DataSource = this.bindingSource1;
        }
        #endregion

        #region method frmRFID_Load
        private void frmRFID_Load(object sender, EventArgs e)
        {
            this.getData();
            btnSearchVehicle.Enabled = false;
        }
        #endregion

        #region method dgvRFID_CellClick
        private void dgvRFID_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Id = 0;
                this.Id = int.Parse(this.dgvRFID.Rows[e.RowIndex].Cells["dgvRFIDId"].Value.ToString());
                this.txtCode.Text = this.dgvRFID.Rows[e.RowIndex].Cells["dgvRFIDCode"].Value.ToString();
                this.txtVehicle.Text = this.dgvRFID.Rows[e.RowIndex].Cells["dgvRFIDVehicle"].Value.ToString();
                this.txtDayReleased.Text = DateTime.Parse(this.dgvRFID.Rows[e.RowIndex].Cells["dgvRFIDDayReleased"].Value.ToString()).ToString("dd/MM/yyyy");
                this.txtDayExpired.Text = DateTime.Parse(this.dgvRFID.Rows[e.RowIndex].Cells["dgvRFIDDayExpired"].Value.ToString()).ToString("dd/MM/yyyy");
                this.ckbState.Checked = bool.Parse(this.dgvRFID.Rows[e.RowIndex].Cells["dgvRFIDState"].Value.ToString());

                //this.pictureBox_RFID_Confirm.Image = Image.FromFile($@"D:/xhtd_data/images/{this.txtVehicle.Text}.png");
                this.pictureBox_RFID_Confirm.Load($"http://tv.ximanghoangmai.vn:8189/images/{this.txtVehicle.Text}.PNG");
                if (this.txtCode.Text.Trim() != "")
                {
                    this.btnSearchVehicle.Enabled = false;
                    this.btnSearchVehicle.Visible = false;
                }
            }
            catch
            {
                this.pictureBox_RFID_Confirm.Image = Image.FromFile($@"D:/xhtd_data/images/noimage.png");
            }
        }
        #endregion

        #region method btnSearch_Click
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.getData();
            
        }
        #endregion

        #region method btnSearchVehicle_Click
        private void btnSearchVehicle_Click(object sender, EventArgs e)
        {
            frmVehicle objfrmVehicle = new frmVehicle();
            objfrmVehicle.ShowDialog();
            txtVehicle.Text = sVehicleCode;
            txtH.Text = H;
            txtW.Text = W;
            txtL.Text = L;
        }
        #endregion

        #region method txtSearchKey_KeyPress
        private void txtSearchKey_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.getData();
            }
        }

        private void txtSearchKey_TextChanged(object sender, EventArgs e)
        {
            this.getData();
        }

        
        #endregion

        #region txtVehicle_TextChanged
        private void txtVehicle_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable objTMP = new DataTable();
                objTMP = objVehicle.getDataVihicleID(txtVehicle.Text.Trim());
                if (objTMP.Rows.Count > 0)
                {
                    txtH.Text = objTMP.Rows[0]["HeightVehicle"].ToString();
                    txtW.Text = objTMP.Rows[0]["WidthVehicle"].ToString();
                    txtL.Text = objTMP.Rows[0]["LongVehicle"].ToString();
                }
                else
                {
                    txtH.Text = "";
                    txtW.Text = "";
                    txtL.Text = "";
                }
            }
            catch { }
        }

        private void btnCreateRecordConfirm_Click(object sender, EventArgs e)
        {
            if (this.txtCode.Text.Trim() != "")
            {
                //frmConfirmRFID objConfig = new frmConfirmRFID(this.txtVehicle.Text, this.txtCode.Text);
                //objConfig.ShowDialog();

                frmConfirmRFIDCamera objConfig = new frmConfirmRFIDCamera(this.txtVehicle.Text, this.txtCode.Text);
                objConfig.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn xe trước khi tạo biên bản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void btnSaveRecordConfirm_Click(object sender, EventArgs e)
        {
            if (this.txtCode.Text.Trim() != "")
            {
                //frmConfirmRFIDWithImage objConfig = new frmConfirmRFIDWithImage(this.txtVehicle.Text, this.txtCode.Text);
                //objConfig.ShowDialog();

                frmConfirmRFIDCameraWithSign objConfig = new frmConfirmRFIDCameraWithSign(this.txtVehicle.Text, this.txtCode.Text);
                objConfig.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn xe trước khi tạo biên bản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region method frmRFID_KeyDown
        private void frmRFID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region method btnCancel_Click
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.txtCode.ReadOnly = true;
            this.txtDayReleased.ReadOnly = true;
            this.txtDayExpired.ReadOnly = true;
            this.txtVehicle.ReadOnly = true;
            this.txtCode.Focus();

            this.btnAdd.Enabled = true;
            this.brnEdit.Enabled = true;
            this.btnSave.Enabled = false;

            dgvRFID.Enabled = true;
        }
        #endregion

        #region method btnSaveToBill_Click
        private void btnSaveToBill_Click(object sender, EventArgs e)
        {
            if (this.txtCode.Text.Trim() != "")
            {
                int tmpValue = 0;
                tmpValue = this.objRFID.saveRfidToBillOrder(this.txtCode.Text, this.txtVehicle.Text.Trim());
                if (tmpValue > 0)
                {
                    MessageBox.Show("Cập nhật thành công " + tmpValue.ToString() + " đơn hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion
    }
}
