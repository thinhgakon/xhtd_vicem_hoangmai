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
    public partial class frmVehicleDeliveryCode : Form
    {
        #region declare objects
        private BillOrder objBillOrder = new BillOrder();
        public string Vehicle = "";
        public int TotalItem = 0;
        #endregion

        #region method frmVehicleDeliveryCode
        public frmVehicleDeliveryCode()
        {
            InitializeComponent();
        }
        #endregion

        #region method frmVehicleDeliveryCode_KeyDown
        private void frmVehicleDeliveryCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region method frmVehicleDeliveryCode_Shown
        private void frmVehicleDeliveryCode_Shown(object sender, EventArgs e)
        {
            this.dgvBillOrder.AutoGenerateColumns = false;
            this.dgvBillOrder.DataSource = this.objBillOrder.getBillOrderByVehicle(this.Vehicle);
            this.lblVehicle.Text = "Phương tiện: "+this.Vehicle;
        }
        #endregion

        #region method btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn hủy số thứ tự của xe này không?","Xác nhận",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }    
            for (int i = 0; i < this.dgvBillOrder.RowCount; i++)
            {
                try
                {
                    this.TotalItem += this.objBillOrder.resetBillOrderToDefault(this.dgvBillOrder.Rows[i].Cells["dgvBillOrderDeliveryCode"].Value.ToString());
                }
                catch
                {

                }
            }

            if (this.TotalItem > 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
