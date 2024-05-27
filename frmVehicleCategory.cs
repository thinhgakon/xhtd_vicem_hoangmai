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
    public partial class frmVehicleCategory : Form
    {
        #region declare objects
        Vehicle objVehicle = new Vehicle();
        DataTable DataTMP = new DataTable();
        int Id = 0;
        #endregion

        #region method frmVehicleCategory
        public frmVehicleCategory()
        {
            InitializeComponent();
        } 
        #endregion

        #region method getData
        private void getData()
        {
            this.dgvVehicle.AutoGenerateColumns = false;
            this.dgvVehicle.EnableHeadersVisualStyles = false;
            DataTMP = this.objVehicle.getDataVihicle(this.txtSearchKey.Text.Trim());
            this.dgvVehicle.DataSource = DataTMP;
        }
        #endregion

        #region method frmVehicle_Load
        private void frmVehicle_Load(object sender, EventArgs e)
        {
            this.getData();
            this.dgvVehicle.ReadOnly = true;
        } 
        #endregion

        #region method txtSearchKey_TextChanged
        private void txtSearchKey_TextChanged(object sender, EventArgs e)
        {
            getData();
        } 
        #endregion

        #region method dgvVehicle_CellEnter
        private void dgvVehicle_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Id = 0;
                this.Id = int.Parse(this.dgvVehicle.Rows[e.RowIndex].Cells["dgvVehicleId"].Value.ToString());
                this.txtPhuongTien.Text = this.dgvVehicle.Rows[e.RowIndex].Cells["dgvVehicleCode"].Value.ToString();
                this.txtGPLX.Text = this.dgvVehicle.Rows[e.RowIndex].Cells["IdCardNumber"].Value.ToString();
                this.txtTaiXe.Text = this.dgvVehicle.Rows[e.RowIndex].Cells["dgvname"].Value.ToString();
                this.txtTrongTai.Text = this.dgvVehicle.Rows[e.RowIndex].Cells["dgvVehicleTonnage"].Value.ToString();
                this.txtH.Text = this.dgvVehicle.Rows[e.RowIndex].Cells["H"].Value.ToString();
                this.txtW.Text = this.dgvVehicle.Rows[e.RowIndex].Cells["W"].Value.ToString();
                this.txtL.Text = this.dgvVehicle.Rows[e.RowIndex].Cells["L"].Value.ToString();
                this.txtTonnageDefault.Text= this.dgvVehicle.Rows[e.RowIndex].Cells["dgvVehicleTonnageDefault"].Value.ToString();
                this.txtCardNo.Text = this.dgvVehicle.Rows[e.RowIndex].Cells["dgvVehicleCardNo"].Value.ToString();
                this.btnDel.Enabled = true;
            }
            catch { }
        } 
        #endregion

        #region method btnAdd_Click
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Id = 0;
            this.txtPhuongTien.Text = "";
            this.txtTaiXe.Text = "";
            this.txtTrongTai.Text = "";
            this.txtH.Text = "";
            this.txtW.Text = "";
            this.txtL.Text = "";
            this.btnSave.Enabled = true;
            this.txtPhuongTien.Enabled = true;
            this.btnEdit.Enabled = false;
            this.btnAdd.Enabled = false;
            this.btnCancel.Enabled = true;
        } 
        #endregion

        #region method btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.txtPhuongTien.Focus();

            if (this.txtPhuongTien.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập thông tin biển số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtPhuongTien.Focus();
                return;
            }

            //if (this.txtTaiXe.Text == "")
            //{
            //    MessageBox.Show("Bạn chưa nhập thông tin tài xế", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.txtTaiXe.Focus();
            //    return;
            //}

            if (this.txtTrongTai.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập thông tin trọng tải", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtTrongTai.Focus();
                return;
            }

            if (objVehicle.setData(this.Id, txtPhuongTien.Text.Trim(), txtTaiXe.Text, txtTrongTai.Text, txtGPLX.Text, txtH.Text, txtW.Text, txtL.Text) > 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.btnSave.Enabled = false;
                this.getData();

                this.btnEdit.Enabled = true;
                this.btnAdd.Enabled = true;
                this.btnSave.Enabled = false;
                this.btnCancel.Enabled = false;
            }
            else
            {
                MessageBox.Show("Lỗi khi cập nhật thông tin!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            btnSave.Enabled = true;
            txtPhuongTien.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnAdd.Enabled = false;
            this.btnCancel.Enabled = true;
        } 
        #endregion

        #region method frmVehicleCategory_KeyDown
        private void frmVehicleCategory_KeyDown(object sender, KeyEventArgs e)
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
            this.btnAdd.Enabled = true;
            this.btnEdit.Enabled = true;
            this.btnSave.Enabled = false;
            this.btnCancel.Enabled = false;
            this.btnDel.Enabled = false;
        }
        #endregion

        #region method btnDel_Click
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa phương tiện \""+txtPhuongTien.Text.ToString().ToUpper()+"\" không?","Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    if (this.Id > 0)
                    {
                        if (this.objVehicle.delData(this.Id) > 0)
                        {
                            MessageBox.Show("Xóa thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.getData();
                        }
                    }
                }
                catch
                {

                }
            }
        }
        #endregion

        #region method btnCardEdit_Click
        private void btnCardEdit_Click(object sender, EventArgs e)
        {
            this.txtCardNo.ReadOnly = false;
            this.btnCardSave.Visible = true;
            this.btnCardEdit.Visible = false;
            this.txtCardNo.Focus();
        }
        #endregion

        #region method btnCardSave_Click
        private void btnCardSave_Click(object sender, EventArgs e)
        {
            if (this.txtCardNo.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập thông tin thẻ RFID!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string msg = this.objVehicle.updateRFID(this.txtPhuongTien.Text, this.txtCardNo.Text);
            if (msg == "Cập nhật thông tin thành công")
            {
                this.txtCardNo.ReadOnly = true;
                this.btnCardSave.Visible = false;
                this.btnCardEdit.Visible = true;
                this.getData();
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.txtCardNo.ReadOnly = true;
                this.btnCardSave.Visible = false;
                this.btnCardEdit.Visible = true;
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}
