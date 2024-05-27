using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMXHTD
{
    public partial class frmTaskVehiceInfo : Form
    {
        #region declare objects
        private Thread threadData;
        private int TimeSleep = 5000;
        private BillOrder objBillOrder = new BillOrder();
        #endregion

        #region method frmTaskVehiceInfo
        public frmTaskVehiceInfo()
        {
            InitializeComponent();
        }
        #endregion

        #region method frmTaskVehiceInfo_Shown
        private void frmTaskVehiceInfo_Shown(object sender, EventArgs e)
        {
            try
            {
                this.threadData = new Thread(t =>
                {
                    while (1 > 0)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.getData();
                        });

                        Thread.Sleep(this.TimeSleep);
                    }
                }) { IsBackground = true };
                this.threadData.Start();
            }
            catch
            {

            }
        }
        #endregion

        #region method getData
        private void getData()
        {

            this.dgvVehicleInfo.AutoGenerateColumns = false;
            this.dgvVehicleInfo.EnableHeadersVisualStyles = false;

            DataTable objTable = this.objBillOrder.getVehicleInfo();
            //var listState = new List<string>(new string[] { "Mời xe vào", "Chờ loa gọi", "Chưa xác thực" });
            //objTable.Rows[0]["State1"] = listState[new Random().Next(0,2)].ToString();
            objTable.DefaultView.Sort = "IndexOrder ASC";

            this.dgvVehicleInfo.DataSource = null;
            this.dgvVehicleInfo.Rows.Clear();
            this.dgvVehicleInfo.Refresh();

            this.dgvVehicleInfo.DataSource = objTable;

            this.dgvVehicleInfo.Columns[0].HeaderCell.Style.Font = new Font("Times New Roman",20,FontStyle.Bold);
            this.dgvVehicleInfo.Columns[1].HeaderCell.Style.Font = new Font("Times New Roman", 20, FontStyle.Bold);
            this.dgvVehicleInfo.Columns[2].HeaderCell.Style.Font = new Font("Times New Roman", 20, FontStyle.Bold);
            this.dgvVehicleInfo.Columns[3].HeaderCell.Style.Font = new Font("Times New Roman", 20, FontStyle.Bold);

            //if (this.dgvVehicleInfo.Rows.Count > 0)
            //{
            //    this.dgvVehicleInfo.Rows[this.dgvVehicleInfo.Rows.Count - 1].Selected = true;
            //}
        }
        #endregion

        #region method frmTaskVehiceInfo_KeyDown
        private void frmTaskVehiceInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region method frmTaskVehiceInfo_FormClosing
        private void frmTaskVehiceInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.threadData.IsAlive)
                {
                    this.threadData.Abort();
                }
            }
            catch
            {

            }
        }
        #endregion

        #region method dgvVehicleInfo_RowPrePaint
        private void dgvVehicleInfo_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            //if (this.dgvVehicleInfo.Rows[e.RowIndex].Cells["dgvVehicleInfoState1"].Value.ToString() == "Mời xe vào")
            //{
            //    this.dgvVehicleInfo.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
            //    this.dgvVehicleInfo.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
            //}
        }
        #endregion
    }
}
