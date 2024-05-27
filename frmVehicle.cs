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
    public partial class frmVehicle : Form
    {
        #region declare objects
        private Vehicle objVehicle = new Vehicle();
        private int Id = 0;
        public string VehicleCode = "";
        #endregion

        #region method frmVehicle
        public frmVehicle()
        {
            InitializeComponent();
        }
        #endregion

        #region method getData
        private void getData()
        {
            this.dgvVehicle.AutoGenerateColumns = false;
            this.dgvVehicle.EnableHeadersVisualStyles = false;
            this.dgvVehicle.DataSource = this.objVehicle.getDataVihicle(this.txtSearchKey.Text.Trim());
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

        #region method dgvVehicle_CellDoubleClick
        private void dgvVehicle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmRFID.sVehicleCode = this.dgvVehicle.Rows[e.RowIndex].Cells["dgvVehicleCode"].Value.ToString().Trim();
            frmRFID.H = this.dgvVehicle.Rows[e.RowIndex].Cells["H"].Value.ToString().Trim();
            frmRFID.W = this.dgvVehicle.Rows[e.RowIndex].Cells["W"].Value.ToString().Trim();
            frmRFID.L = this.dgvVehicle.Rows[e.RowIndex].Cells["L"].Value.ToString().Trim();
            VehicleCode = this.dgvVehicle.Rows[e.RowIndex].Cells["dgvVehicleCode"].Value.ToString().Trim();
            if (VehicleCode.Trim() != "")
            {
                this.Close();
            }
        }
        #endregion

        #region method frmVehicle_KeyDown
        private void frmVehicle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion
    }
}
