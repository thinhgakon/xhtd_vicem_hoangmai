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
    public partial class frmExportPlan : Form
    {
        #region declare objects
        int Id = 0;
        ExportPlan objExportPlan = new ExportPlan();
        ExportPlanDetail objExportPlanDetail = new ExportPlanDetail();
        Customer objCustomer = new Customer();
        Trough objTrough = new Trough();
        bool add_flag = false;
        bool edit_flag = false;

        public enum Status
        {
            KHOI_TAO = 1,
            DA_DUYET = 2,
            DA_HOAN_THANH = 3,
            DA_BI_HUY = 0
        }
        #endregion

        #region method frmExportPlan
        public frmExportPlan()
        {
            InitializeComponent();
        }
        #endregion

        #region method frmExportPlan_Load
        private void frmExportPlan_Load(object sender, EventArgs e)
        {
            this.dgvPlan.AutoGenerateColumns = false;
            this.dgvPlan.EnableHeadersVisualStyles = false;
            this.dgvPlan.DataSource = this.objExportPlan.Get_Export_Plan_List();

            this.dgvDistributor.AutoGenerateColumns = false;
            this.dgvDistributor.EnableHeadersVisualStyles = false;
            this.dgvDistributor.DataSource = this.objCustomer.getDistributorData();

            this.dgvProduct.AutoGenerateColumns = false;
            this.dgvProduct.EnableHeadersVisualStyles = false;
            this.dgvProduct.DataSource = this.objTrough.getProducts();

            this.btnAdd.Enabled = true;
            this.btnEdit.Enabled = true;
            this.btnSave.Enabled = false;
            this.btnCancel.Enabled = false;
            this.btnDel.Enabled = true;

            this.txtPlanName.Text = this.txtShipName.Text = this.txtDateStart.Text = this.txtDateEnd.Text = "";

            this.grbPlanInfo.Enabled = false;
            this.dgvPlan.Enabled = true;
            this.ckbSelectAllDistributor.Enabled = this.ckbSelectAllProduct.Enabled = false;
            this.ckbSelectAllDistributor.Checked = this.ckbSelectAllProduct.Checked = false;

            this.dgvProduct.Columns["dgvProductOrderNumber"].DefaultCellStyle.Format = "N2";

            foreach (DataGridViewColumn column in this.dgvDistributor.Columns)
            {
                column.ReadOnly = true;
            }
            foreach (DataGridViewColumn column in this.dgvProduct.Columns)
            {
                column.ReadOnly = true;
            }
        }
        #endregion

        #region method dgvPlan_CellClick
        private void dgvPlan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btnDel.Enabled = true;

            if (e.RowIndex > -1)
            {
                this.Id = int.Parse(this.dgvPlan.Rows[e.RowIndex].Cells["dgvPlanId"].Value.ToString());
                this.txtPlanName.Text = this.dgvPlan.Rows[e.RowIndex].Cells["dgvPlanName"].Value.ToString();
                this.txtShipName.Text = this.dgvPlan.Rows[e.RowIndex].Cells["dgvPlanShipName"].Value.ToString();
                this.txtDateStart.Text = DateTime.Parse(this.dgvPlan.Rows[e.RowIndex].Cells["dgvPlanStartDate"].Value.ToString()).ToString("dd/MM/yyyy");
                this.txtDateEnd.Text = DateTime.Parse(this.dgvPlan.Rows[e.RowIndex].Cells["dgvPlanEndDate"].Value.ToString()).ToString("dd/MM/yyyy");

                List<string> DistributorIdsSyn = this.dgvPlan.Rows[e.RowIndex].Cells["dgvPlanDistributorIds"].Value.ToString()
                                                             .Split(',')
                                                             .Select(id => id.Trim())
                                                             .ToList();

                foreach (DataGridViewRow row in dgvDistributor.Rows)
                {
                    string distributorIdSyn = row.Cells["dgvDistributorIdSyn"].Value.ToString();

                    row.Cells["dgvDistributorSelect"].Value = DistributorIdsSyn.Contains(distributorIdSyn);
                }

                Dictionary<string, double> ProductList = this.objExportPlanDetail.Get_Export_Plan_Detail_List(this.Id).AsEnumerable()
                                                            .ToDictionary(
                                                               row => row["SynProductId"].ToString(),
                                                               row => double.Parse(row["OrderNumber"].ToString())
                                                            );

                foreach (DataGridViewRow row in dgvProduct.Rows)
                {
                    string productIdSyn = row.Cells["dgvProductIDProductSyn"].Value.ToString();

                    if (ProductList.ContainsKey(productIdSyn))
                    {
                        row.Cells["dgvProductSelect"].Value = true;
                        row.Cells["dgvProductOrderNumber"].Value = ProductList[productIdSyn];
                    }

                    else
                    {
                        row.Cells["dgvProductSelect"].Value = false;
                        row.Cells["dgvProductOrderNumber"].Value = null;
                    }
                }
            }
        }
        #endregion

        #region dgvPlan_CellEnter
        private void dgvPlan_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.btnDel.Enabled = true;

            if (e.RowIndex > -1)
            {
                this.Id = int.Parse(this.dgvPlan.Rows[e.RowIndex].Cells["dgvPlanId"].Value.ToString());
                this.txtPlanName.Text = this.dgvPlan.Rows[e.RowIndex].Cells["dgvPlanName"].Value.ToString();
                this.txtShipName.Text = this.dgvPlan.Rows[e.RowIndex].Cells["dgvPlanShipName"].Value.ToString();
                this.txtDateStart.Text = DateTime.Parse(this.dgvPlan.Rows[e.RowIndex].Cells["dgvPlanStartDate"].Value.ToString()).ToString("dd/MM/yyyy");
                this.txtDateEnd.Text = DateTime.Parse(this.dgvPlan.Rows[e.RowIndex].Cells["dgvPlanEndDate"].Value.ToString()).ToString("dd/MM/yyyy");

                List<string> DistributorIdsSyn = this.dgvPlan.Rows[e.RowIndex].Cells["dgvPlanDistributorIds"].Value.ToString()
                                                             .Split(',')
                                                             .Select(id => id.Trim())
                                                             .ToList();

                foreach (DataGridViewRow row in dgvDistributor.Rows)
                {
                    string distributorIdSyn = row.Cells["dgvDistributorIdSyn"].Value.ToString();

                    row.Cells["dgvDistributorSelect"].Value = DistributorIdsSyn.Contains(distributorIdSyn);
                }

                Dictionary<string, double> ProductList = this.objExportPlanDetail.Get_Export_Plan_Detail_List(this.Id).AsEnumerable()
                                                             .ToDictionary(
                                                                row => row["SynProductId"].ToString(),
                                                                row => double.Parse(row["OrderNumber"].ToString())
                                                             );

                foreach (DataGridViewRow row in dgvProduct.Rows)
                {
                    string productIdSyn = row.Cells["dgvProductIDProductSyn"].Value.ToString();

                    if (ProductList.ContainsKey(productIdSyn))
                    {
                        row.Cells["dgvProductSelect"].Value = true;
                        row.Cells["dgvProductOrderNumber"].Value = ProductList[productIdSyn];
                    }

                    else
                    {
                        row.Cells["dgvProductSelect"].Value = false;
                        row.Cells["dgvProductOrderNumber"].Value = null;
                    }
                }
            }
        }
        #endregion

        #region Buttons
        private void btnAdd_Click(object sender, EventArgs e)
        {
            add_flag = true;
            edit_flag = false;
            this.txtPlanName.Text = string.Empty;
            this.txtShipName.Text = string.Empty;
            this.txtDateStart.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txtDateEnd.Text = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");
            this.btnAdd.Enabled = this.btnEdit.Enabled = this.btnDel.Enabled = false;
            this.btnSave.Enabled = this.btnCancel.Enabled = true;
            this.grbPlanInfo.Enabled = true;
            this.dgvPlan.Enabled = false;
            this.ckbSelectAllDistributor.Enabled = this.ckbSelectAllProduct.Enabled = true;

            foreach (DataGridViewRow row in dgvDistributor.Rows)
            {
                row.Cells["dgvDistributorSelect"].ReadOnly = false;
                row.Cells["dgvDistributorSelect"].Value = false;
            }

            foreach (DataGridViewRow row in dgvProduct.Rows)
            {
                row.Cells["dgvProductSelect"].ReadOnly = false;
                row.Cells["dgvProductSelect"].Value = false;

                row.Cells["dgvProductOrderNumber"].ReadOnly = false;
                row.Cells["dgvProductOrderNumber"].Value = null;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            add_flag = false;
            edit_flag = true;
            this.btnAdd.Enabled = this.btnEdit.Enabled = this.btnDel.Enabled = false;
            this.btnSave.Enabled = this.btnCancel.Enabled = true;
            this.grbPlanInfo.Enabled = true;
            this.dgvPlan.Enabled = false;
            this.ckbSelectAllDistributor.Enabled = this.ckbSelectAllProduct.Enabled = true;

            foreach (DataGridViewRow row in dgvDistributor.Rows)
            {
                row.Cells["dgvDistributorSelect"].ReadOnly = false;
            }

            foreach (DataGridViewRow row in dgvProduct.Rows)
            {
                row.Cells["dgvProductSelect"].ReadOnly = false;
                row.Cells["dgvProductOrderNumber"].ReadOnly = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Nếu bấm nút Thêm
            if (add_flag && !edit_flag)
            {
                if (string.IsNullOrEmpty(this.txtPlanName.Text))
                {
                    MessageBox.Show("Trường tên kế hoạch không được để trống!");
                    return;
                }

                if (string.IsNullOrEmpty(this.txtShipName.Text))
                {
                    MessageBox.Show("Trường tên tàu không được để trống!");
                    return;
                }

                if (string.IsNullOrEmpty(this.txtDateStart.Text) || string.IsNullOrEmpty(this.txtDateEnd.Text))
                {
                    MessageBox.Show("Trường ngày bắt đầu và kết thúc không được để trống!");
                    return;
                }

                string distributorIdSynList = string.Empty;

                // Lấy danh sách nhà phân phối được chọn
                foreach (DataGridViewRow row in dgvDistributor.Rows)
                {
                    if (bool.Parse(row.Cells["dgvDistributorSelect"].Value.ToString()) == true)
                    {
                        if (distributorIdSynList.Length > 0)
                        {
                            distributorIdSynList += ", ";
                        }
                        distributorIdSynList += row.Cells["dgvDistributorIdSyn"].Value.ToString();
                    }
                }

                int plan_id = this.objExportPlan.Insert(this.txtPlanName.Text, this.txtShipName.Text, distributorIdSynList, DateTime.Parse(this.txtDateStart.Text), DateTime.Parse(this.txtDateEnd.Text));

                if (plan_id == 0)
                {
                    MessageBox.Show("Tạo mới kế hoạch thất bại!");
                    return;
                }

                Dictionary<string, double> productList = new Dictionary<string, double>();

                // Lấy danh sách sản phẩm + khối lượng được chọn
                foreach (DataGridViewRow row in dgvProduct.Rows)
                {
                    if (bool.Parse(row.Cells["dgvProductSelect"].Value.ToString()) == true)
                    {
                        productList[row.Cells["dgvProductIDProductSyn"].Value.ToString()] = row.Cells["dgvProductOrderNumber"].Value != null ?
                                                                                            double.Parse(row.Cells["dgvProductOrderNumber"].Value.ToString()) : 0;
                    }
                }

                foreach (KeyValuePair<string, double> product in productList)
                {
                    this.objExportPlanDetail.Insert(plan_id, int.Parse(product.Key), product.Value, 0);
                }

                MessageBox.Show("Tạo mới kế hoạch thành công!");
            }

            // Nếu bấm nút Cập nhật
            else if (!add_flag && edit_flag)
            {
                if (string.IsNullOrEmpty(this.txtPlanName.Text))
                {
                    MessageBox.Show("Trường tên kế hoạch không được để trống!");
                    return;
                }

                if (string.IsNullOrEmpty(this.txtShipName.Text))
                {
                    MessageBox.Show("Trường tên tàu không được để trống!");
                    return;
                }

                if (string.IsNullOrEmpty(this.txtDateStart.Text) || string.IsNullOrEmpty(this.txtDateEnd.Text))
                {
                    MessageBox.Show("Trường ngày bắt đầu và kết thúc không được để trống!");
                    return;
                }

                string distributorIdSynList = string.Empty;

                // Lấy danh sách nhà phân phối được chọn
                foreach (DataGridViewRow row in dgvDistributor.Rows)
                {
                    if (bool.Parse(row.Cells["dgvDistributorSelect"].Value.ToString()) == true)
                    {
                        if (distributorIdSynList.Length > 0)
                        {
                            distributorIdSynList += ", ";
                        }
                        distributorIdSynList += row.Cells["dgvDistributorIdSyn"].Value.ToString();
                    }
                }

                bool isSuccess = this.objExportPlan.Update(this.Id, this.txtPlanName.Text, this.txtShipName.Text, distributorIdSynList, DateTime.Parse(this.txtDateStart.Text), DateTime.Parse(this.txtDateEnd.Text), 1);

                if (!isSuccess)
                {
                    MessageBox.Show("Cập nhật kế hoạch thất bại!");
                    return;
                }

                // Lấy danh sách sản phẩm + khối lượng trước đó
                Dictionary<string, double> ProductListBefore = this.objExportPlanDetail.Get_Export_Plan_Detail_List(this.Id).AsEnumerable()
                                                         .ToDictionary(
                                                            row => row["SynProductId"].ToString(),
                                                            row => double.Parse(row["OrderNumber"].ToString())
                                                         );

                Dictionary<string, double> ProductListAfter = new Dictionary<string, double>();

                // Lấy danh sách sản phẩm + khối lượng sau khi chỉnh sửa
                foreach (DataGridViewRow row in dgvProduct.Rows)
                {
                    if (bool.Parse(row.Cells["dgvProductSelect"].Value.ToString()) == true)
                    {
                        ProductListAfter[row.Cells["dgvProductIDProductSyn"].Value.ToString()] = row.Cells["dgvProductOrderNumber"].Value != null ?
                                                                                                 double.Parse(row.Cells["dgvProductOrderNumber"].Value.ToString()) : 0;
                    }
                }

                foreach (KeyValuePair<string, double> product in ProductListAfter)
                {
                    // Nếu ProductIdSyn có trong cả 2 ds sp mà khối lượng khác nhau thì Update
                    if (ProductListBefore.ContainsKey(product.Key) && ProductListBefore[product.Key] != product.Value)
                    {
                        this.objExportPlanDetail.Update(this.Id, int.Parse(product.Key), product.Value, 0);
                    }

                    // Nếu ProductIdSyn không có ở ds sp trước mà có ở ds sp sau thì Insert
                    else if (!ProductListBefore.ContainsKey(product.Key))
                    {
                        this.objExportPlanDetail.Insert(this.Id, int.Parse(product.Key), product.Value, 0);
                    }
                }

                foreach (KeyValuePair<string, double> product in ProductListBefore)
                {
                    // Nếu ProductIdSyn có ở ds sp trước mà không có ở ds sp sau thì Delete
                    if (!ProductListAfter.ContainsKey(product.Key))
                    {
                        this.objExportPlanDetail.Delete(this.Id, int.Parse(product.Key));
                    }
                }

                MessageBox.Show("Cập nhật kế hoạch thành công!");
            }

            add_flag = edit_flag = false;

            this.frmExportPlan_Load(sender, e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            add_flag = edit_flag = false;
            this.frmExportPlan_Load(sender, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Bạn có chắc muốn xóa kế hoạch này?", "Xác nhận!", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                MessageBox.Show("Đã xóa!");

                frmExportPlan_Load(sender, e);
            }
        }
        #endregion

        #region method ckbSelectAllProduct_CheckedChanged
        private void ckbSelectAllProduct_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dgvProduct.RowCount; i++)
            {
                this.dgvProduct.Rows[i].Cells["dgvProductSelect"].Value = this.ckbSelectAllProduct.Checked;
            }
        }
        #endregion

        #region method ckbSelectAllDistributor_CheckedChanged
        private void ckbSelectAllDistributor_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dgvDistributor.RowCount; i++)
            {
                this.dgvDistributor.Rows[i].Cells["dgvDistributorSelect"].Value = this.ckbSelectAllDistributor.Checked;
            }
        }
        #endregion

        #region method frmExportPlan_KeyDown
        private void frmExportPlan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region method dgvProductOrderNumber_KeyPress
        private void dgvProduct_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvProduct.CurrentCell.ColumnIndex == dgvProduct.Columns["dgvProductOrderNumber"].Index)
            {
                TextBox tb = e.Control as TextBox;

                if (tb != null)
                {
                    tb.KeyPress -= new KeyPressEventHandler(dgvProductOrderNumber_KeyPress);

                    tb.KeyPress += new KeyPressEventHandler(dgvProductOrderNumber_KeyPress);
                }
            }
        }
        #endregion

        #region method dgvProductOrderNumber_KeyPress
        private void dgvProductOrderNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            TextBox tb = sender as TextBox;
            if ((e.KeyChar == '.' || e.KeyChar == ',') && (tb.Text.Contains(".") || tb.Text.Contains(",")))
            {
                e.Handled = true;
            }
        }
        #endregion

        #region method dgvProduct_CellEndEdit
        private void dgvProduct_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvProduct.Columns["dgvProductOrderNumber"].Index)
            {
                var cell = dgvProduct.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value != null)
                {
                    if (double.TryParse(cell.Value.ToString(), out double value))
                    {
                        cell.Value = value.ToString("N2");
                    }
                }
            }
        }
        #endregion
    }
}
