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
    public partial class frmTaskReleaseStock : Form
    {
        BillOrder objBillOrder = new BillOrder();
        public bool SavedState = false;
        public string TroughLineCode = "";

        public string DeliveryCode = "";
        public frmTaskReleaseStock()
        {
            InitializeComponent();
        }

        private void frmTaskReleaseStock_Load(object sender, EventArgs e)
        {
            try
            {
                this.cbbLineCode.DataSource = this.objBillOrder.getTrough();
                this.cbbLineCode.DisplayMember = "Name";
                this.cbbLineCode.ValueMember = "LineCode";

                if (this.TroughLineCode.Trim()  != "")
                {
                    this.cbbLineCode.SelectedValue = this.TroughLineCode;
                    //this.cbbLineCode.Enabled = false;
                    this.btnSave.Enabled = true;
                }
                else
                {
                    this.btnSave.Enabled = true;
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void frmTaskReleaseStock_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void frmTaskReleaseStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.cbbLineCode.SelectedValue.ToString() == "-/-")
            {
                MessageBox.Show("Bạn chưa chọn máng, kho, bãi xuất hàng!","Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.cbbLineCode.Focus();
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xếp vị trí này không?","Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (this.objBillOrder.UpdateTroughLineCode(this.lblDeliveryCode.Text,this.cbbLineCode.SelectedValue.ToString()) > 0){
                    this.SavedState = true;
                    this.Close();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
