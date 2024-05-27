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
    public partial class frmVehicleBillOrder : Form
    {
        #region declare objects
        private BillOrder objBillOrder = new BillOrder();
        public string Vehicle = "";
        public int TotalItem = 0;
        #endregion

        #region method frmVehicleBillOrder
        public frmVehicleBillOrder()
        {
            InitializeComponent();
        }
        #endregion

        #region method frmVehicleBillOrder_Load
        private void frmVehicleBillOrder_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region method frmVehicleBillOrder_KeyDown
        private void frmVehicleBillOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region method frmVehicleBillOrder_Shown
        private void frmVehicleBillOrder_Shown(object sender, EventArgs e)
        {
            this.dgvBillOrder.AutoGenerateColumns = false;
            this.dgvBillOrder.DataSource = this.objBillOrder.getBillOrderByVehicleV1(this.Vehicle);
            this.lblVehicle.Text = "Phương tiện: " + this.Vehicle;
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
