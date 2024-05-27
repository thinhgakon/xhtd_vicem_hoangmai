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
    public partial class frmTroughForward : Form
    {
        #region declare objeccts
        private Trough objTrough = new Trough();
        private BillOrder objBillOrder = new BillOrder();
        public int TroughId = 0, OrderId = 0;
        public bool saved = false;
        #endregion

        #region method frmTroughForward
        public frmTroughForward()
        {
            InitializeComponent();
        }
        #endregion

        #region method frmTroughForward_KeyDown
        private void frmTroughForward_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region method frmTroughForward_Shown
        private void frmTroughForward_Shown(object sender, EventArgs e)
        {
            this.cbbTrough.DataSource = this.objTrough.getTroughForward();
            this.cbbTrough.DisplayMember = "Name";
            this.cbbTrough.ValueMember = "Id";
        }
        #endregion

        #region method btnForward_Click
        private void btnForward_Click(object sender, EventArgs e)
        {
            if (this.cbbTrough.Items.Count == 0)
            {
                MessageBox.Show("Không có máng nào để chuyển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắc muốn chuyển máng không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                if (this.objBillOrder.setTroughForward(this.OrderId,this.TroughId,int.Parse(this.cbbTrough.SelectedValue.ToString())) == 1)
                {
                     this.saved = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Lỗi xảy ra khi xử lý thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
