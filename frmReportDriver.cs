using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HMXHTD.Core;

namespace HMXHTD
{
    public partial class frmReportDriver : Form
    {
        Report objReport = new Report();

        public frmReportDriver()
        {
            InitializeComponent();
        }

        private void frmReportDriver_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void frmReportDriver_Load(object sender, EventArgs e)
        {
            this.dtpFromDay.Value = new DateTime(DateTime.Now.Year,DateTime.Now.Month,1,0,0,0);
        }

        #region method getData
        private void getData2()
        {
            this.lblMSG.Text = "Đang tải dữ liệu, vui lòng đợi!";
            this.lblMSG.Refresh();

            this.dgvAccount.AutoGenerateColumns = false;
            this.dgvAccount.EnableHeadersVisualStyles = false;

            DataTable objTable = this.objReport.getDriver(this.dtpFromDay.Value, this.dtpToDay.Value, this.txtSearch.Text);

            for (int i = 0; i < objTable.Rows.Count; i++)
            {
                objTable.Rows[i]["TotalNumber"] = this.objReport.getTotalNumber(this.dtpFromDay.Value, this.dtpToDay.Value, objTable.Rows[i]["User_Name"].ToString());
            }
            objTable.DefaultView.Sort = "TotalNumber DESC";
            this.dgvAccount.DataSource = objTable;

            this.lblMSG.Text = "";
            this.lblMSG.Refresh();
        }
        #endregion
        #region method getDataTest
        private void getData()
        {
            this.lblMSG.Text = "Đang tải dữ liệu, vui lòng đợi!";
            this.lblMSG.Refresh();

            this.dgvAccount.AutoGenerateColumns = false;
            this.dgvAccount.EnableHeadersVisualStyles = false;

            DataTable objTable = this.objReport.getDriver(this.dtpFromDay.Value, this.dtpToDay.Value, this.txtSearch.Text);

            DataTable objTableSum = this.objReport.getAllTotalNumber(this.dtpFromDay.Value, this.dtpToDay.Value);
            for (int i = 0; i < objTable.Rows.Count; i++)
            {
                var userName = objTable.Rows[i]["User_Name"]?.ToString();
                DataRow item = objTableSum.AsEnumerable().SingleOrDefault(r => r.Field<string>("DriverUserName")?.ToUpper() == userName?.ToUpper());
                objTable.Rows[i]["TotalNumber"] = item == null ? 0 : item["SumNumber"];
            }
            objTable.DefaultView.Sort = "TotalNumber DESC";
            this.dgvAccount.DataSource = objTable;

            this.lblMSG.Text = "";
            this.lblMSG.Refresh();
        }
        #endregion

        #region method btnSearch_Click
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.getData();
        }
        #endregion

        #region method txtSearch_KeyDown
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.getData();
            }
        }
        #endregion

        private void frmReportDriver_Shown(object sender, EventArgs e)
        {
            this.getData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {

        }

        private void dgvAccount_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.dgvListBillOrder.AutoGenerateColumns = false;
                this.dgvListBillOrder.EnableHeadersVisualStyles = false;
                this.dgvListBillOrder.DataSource = this.objReport.getTotalNumberListOrder(this.dtpFromDay.Value, this.dtpToDay.Value, this.dgvAccount.Rows[e.RowIndex].Cells["dgvAccountUserName"].Value.ToString());

                this.lblDriver.Text = "Lái xe: "+ this.dgvAccount.Rows[e.RowIndex].Cells["dgvAccountUserName"].Value.ToString()+"-"+ this.dgvAccount.Rows[e.RowIndex].Cells["dgvAccountFullName"].Value.ToString();

                float tmpValue = 0;
                for (int i = 0; i < this.dgvListBillOrder.RowCount; i++)
                {
                    tmpValue += float.Parse(this.dgvListBillOrder.Rows[i].Cells["dgvListBillOrderSumNumber"].Value.ToString());
                }
                this.lblDriverTotalNumber.Text = tmpValue.ToString();
                this.tabControl1.SelectedIndex = 1;
            }
            catch
            {

            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 0)
            {
                this.lblDriver.Text = "";
                this.lblDriverTotalNumber.Text = "";
                this.dgvListBillOrder.DataSource = null;
            }
        }
    }
}
