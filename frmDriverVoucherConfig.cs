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
    public partial class frmDriverVoucherConfig : Form
    {
        Voucher objVoucher = new Voucher();

        public frmDriverVoucherConfig()
        {
            InitializeComponent();
        }

        private void frmDriverVoucherConfig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmDriverVoucherConfig_Load(object sender, EventArgs e)
        {
            this.cbbTimeCancel.SelectedIndex = 0;
            DataTable objTable = this.objVoucher.getDataConfig();
            if (objTable.Rows.Count > 0)
            {
                this.cbbAutoRelease.Checked = bool.Parse(objTable.Rows[0]["AutoRelease"].ToString());
                if (int.Parse(objTable.Rows[0]["TimeCancel"].ToString()) > 2)
                {
                    this.cbbTimeCancel.SelectedIndex = int.Parse(objTable.Rows[0]["TimeCancel"].ToString()) - 2;
                }
                this.cbbShifts1.SelectedIndex = int.Parse(objTable.Rows[0]["Shifts1"].ToString());
                this.cbbShifts2.SelectedIndex = int.Parse(objTable.Rows[0]["Shifts2"].ToString());
                this.cbbShifts3.SelectedIndex = int.Parse(objTable.Rows[0]["Shifts3"].ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int tmpValue = this.objVoucher.setDataConfig(this.cbbAutoRelease.Checked, this.cbbTimeCancel.SelectedIndex+2, this.cbbShifts1.SelectedIndex, this.cbbShifts2.SelectedIndex, this.cbbShifts3.SelectedIndex);
            if (tmpValue > 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!","Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
