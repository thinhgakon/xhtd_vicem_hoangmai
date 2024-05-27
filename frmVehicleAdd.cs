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
    public partial class frmVehicleAdd : Form
    {
        #region declare objects
        Vehicle objVehicle = new Vehicle();
        DataTable DataTMP = new DataTable();
        int Id = 0;
        #endregion

        #region method frmVehicleAdd
        public frmVehicleAdd()
        {
            InitializeComponent();
        } 
        #endregion

        #region method frmVehicleAdd_Load
        private void frmVehicleAdd_Load(object sender, EventArgs e)
        {
        } 
        #endregion

        #region method frmVehicleAdd_KeyDown
        private void frmVehicleAdd_KeyDown(object sender, KeyEventArgs e)
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
            if (this.txtPhuongTien.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập thông tin biển số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtPhuongTien.Focus();
                return;
            }

            if (this.txtTaiXe.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập thông tin tài xế", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtTaiXe.Focus();
                return;
            }

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
                this.btnSave.Enabled = false;
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
    }
}
