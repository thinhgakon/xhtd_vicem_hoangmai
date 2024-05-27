using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMXHTD
{
    public partial class frmVehicleAllV1 : Form
    {
        private Vehicle objVehicle = new Vehicle();
        private string _Vehicle = "";

        public frmVehicleAllV1()
        {
            InitializeComponent();
        }

        private void frmVehicleAllV1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmVehicleAllV1_Shown(object sender, EventArgs e)
        {
            this.getData();
        }

        #region method getData
        private void getData()
        {
            this.dgvVehicle.AutoGenerateColumns = false;
            this.dgvVehicle.DataSource = this.objVehicle.getDataVihicleORC(this.cbbTypeSearch.SelectedIndex, this.cbbConfirmSearch.SelectedIndex, this.cbbStatusSearch.SelectedIndex, this.txtCodeSearch.Text.Trim());

            this.tabPage1.Text = "["+ this.dgvVehicle .RowCount.ToString()+ "] - Phương tiện";
        }
        #endregion

        #region method getDataCustomer
        private void getDataCustomer(string Vehicle)
        {
            if (this.cbbCustomer.Items.Count > 0)
            {
                try
                {
                    this.dgvVehicleCustomer.AutoGenerateColumns = false;
                    this.dgvVehicleCustomer.DataSource = this.objVehicle.getDataVihicleCustomer(Vehicle, int.Parse(this.cbbCustomer.SelectedValue.ToString()));
                }
                catch
                {

                }
            }
            else
            {
                this.dgvVehicleCustomer.AutoGenerateColumns = false;
                this.dgvVehicleCustomer.DataSource = this.objVehicle.getDataVihicleCustomer(Vehicle,0);
            }

            this.tabPage2.Text = "[" + this.dgvVehicleCustomer.RowCount.ToString() + "] - Phương tiện -> Nhà phân phối";
        }
        #endregion

        #region method getDataStore
        private void getDataStore(string Vehicle)
        {
            if (this.cbbStore.Items.Count > 0)
            {
                try
                {
                    if (this.cbbStore.SelectedValue != null)
                    {
                        this.dgvVehicleStore.AutoGenerateColumns = false;
                        this.dgvVehicleStore.DataSource = this.objVehicle.getDataVihicleStore(Vehicle, int.Parse(this.cbbStore.SelectedValue.ToString()));
                    }
                }
                catch
                {

                }
            }
            else
            {
                this.dgvVehicleStore.AutoGenerateColumns = false;
                this.dgvVehicleStore.DataSource = this.objVehicle.getDataVihicleStore(Vehicle, 0);
            }
            this.tabPage3.Text = "[" + this.dgvVehicleStore.RowCount.ToString() + "] - Phương tiện -> Cửa hàng";
        }
        #endregion

        #region method getDataDriver
        private void getDataDriver(string Vehicle)
        {
            this.dgvVehicleDriver.AutoGenerateColumns = false;
            this.dgvVehicleDriver.DataSource = this.objVehicle.getDataVihicleDriver(Vehicle);
            this.tabPage4.Text = "[" + this.dgvVehicleDriver.RowCount.ToString() + "] - Phương tiện -> Cửa hàng";
        }
        #endregion
        private void dgvVehicle_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this._Vehicle = this.dgvVehicle.Rows[e.RowIndex].Cells["dgvVehicleCode"].Value.ToString();
                this.txtID.Text = this.dgvVehicle.Rows[e.RowIndex].Cells["dgvVehicleid"].Value.ToString();
                this.txtCode.Text = this.dgvVehicle.Rows[e.RowIndex].Cells["dgvVehicleCode"].Value.ToString();
                this.txtTONNAGE.Text = this.dgvVehicle.Rows[e.RowIndex].Cells["dgvVehicleTONNAGE"].Value.ToString();

                this.cbbType.SelectedIndex = int.Parse(this.dgvVehicle.Rows[e.RowIndex].Cells["dgvVehicleTYPE"].Value.ToString());

                if (this.dgvVehicle.Rows[e.RowIndex].Cells["dgvVehicleIS_CONFIRM"].Value.ToString() == "1")
                {
                    this.ckbConfirm.Checked = true;
                }
                else
                {
                    this.ckbConfirm.Checked = false;
                }

                if (this.dgvVehicle.Rows[e.RowIndex].Cells["dgvVehicleSTATUS"].Value.ToString() == "1")
                {
                    this.ckbStatus.Checked = true;
                }
                else
                {
                    this.ckbStatus.Checked = false;
                }

                this.btnAdd.Enabled = true;
                this.btnEdit.Enabled = true;
                this.btnDel.Enabled = true;
                this.btnCancel.Enabled = false;
                this.btnSave.Enabled = false;
            }
            catch
            {
                this._Vehicle = "";
            }
            finally
            {

            }
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabMain.SelectedIndex == 1 && this._Vehicle != "")
            {
                if (this.cbbCustomer.Items.Count > 0)
                {
                    this.cbbCustomer.SelectedIndex = 0;
                    this.getDataCustomer(this._Vehicle);
                    this.getCustomerData();
                }
                else
                {
                    this.getDataCustomer(this._Vehicle);
                    this.getCustomerData();
                }
            }
            else if (this.tabMain.SelectedIndex == 2 && this._Vehicle != "")
            {
                if (this.cbbCustomerStore.Items.Count == 0)
                {
                    this.getCustomerData();
                }

                this.getDataStore(this._Vehicle);
            }
            else if (this.tabMain.SelectedIndex == 3)
            {
                this.getDataDriver(this._Vehicle);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnDel.Enabled = false;
            this.btnCancel.Enabled = true;
            this.btnSave.Enabled = true;

            this.txtID.Text = "";
            this.txtCode.Text = "";
            this.txtTONNAGE.Text = "";
            this.cbbType.SelectedIndex = 0;

            this.txtCode.ReadOnly = false;
            this.txtTONNAGE.ReadOnly = false;
            this.cbbType.Enabled = true;
            this.ckbConfirm.Checked = false;
            this.ckbConfirm.Enabled = true;
            this.ckbStatus.Checked = false;
            this.ckbStatus.Enabled = true;
            this.ckbConfirm.Enabled = true;
            this.ckbStatus.Enabled = true;

            this.txtCode.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnDel.Enabled = false;
            this.btnCancel.Enabled = true;
            this.btnSave.Enabled = true;
            this.ckbConfirm.Enabled = true;
            this.ckbStatus.Enabled = true;

            this.txtCode.ReadOnly = true;
            this.cbbType.Enabled = true;
            this.txtTONNAGE.ReadOnly = false;
            this.txtCode.Focus();

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (this.txtID.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa xác định phương tiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtID.Focus();
                return;
            }

            if (this.txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập số hiệu phương tiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtCode.Focus();
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa mục được chọn không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (this.objVehicle.delData(int.Parse(this.txtID.Text), this.txtCode.Text.Trim().ToUpper()) > 0)
                {
                    this.txtID.Text = "";
                    this.txtCode.Text = "";
                    this.txtTONNAGE.Text = "";
                    this.cbbType.SelectedIndex = 0;
                    this.cbbType.Enabled = false;

                    this.txtTONNAGE.ReadOnly = true;
                    this.txtCode.ReadOnly = true;

                    this.ckbConfirm.Checked = false;
                    this.ckbConfirm.Enabled = false;

                    this.ckbStatus.Checked = false;
                    this.ckbStatus.Enabled = false;

                    this.btnAdd.Enabled = true;
                    this.btnEdit.Enabled = true;
                    this.btnDel.Enabled = false;
                    this.btnCancel.Enabled = false;
                    this.btnSave.Enabled = false;

                    this.getData();
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.txtID.Text = "";
            this.txtCode.Text = "";
            this.txtTONNAGE.Text = "";
            this.cbbType.SelectedIndex = 0;
            this.cbbType.Enabled = false;

            this.txtTONNAGE.ReadOnly = true;
            this.txtCode.ReadOnly = true;

            this.ckbConfirm.Checked = false;
            this.ckbConfirm.Enabled = false;

            this.ckbStatus.Checked = false;
            this.ckbStatus.Enabled = false;

            this.btnAdd.Enabled = true;
            this.btnEdit.Enabled = true;
            this.btnDel.Enabled = false;
            this.btnCancel.Enabled = false;
            this.btnSave.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập số hiệu phương tiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtCode.Focus();
                return;
            }

            if (this.cbbType.SelectedIndex == 0)
            {
                MessageBox.Show("Bạn chưa chọn loại phương tiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.cbbType.Focus();
                return;
            }

            int tmpTONNAGE = 0;

            try
            {
                tmpTONNAGE = int.Parse(this.txtTONNAGE.Text.Trim());
            }
            catch
            {
                tmpTONNAGE = -1;
            }

            if (tmpTONNAGE < 0)
            {
                MessageBox.Show("Bạn chưa nhập trọng tải của phương tiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtTONNAGE.Focus();
                return;
            }

            if (this.txtID.Text != "0" && this.txtID.Text != "")
            {
                if (this.objVehicle.editData(int.Parse(this.txtID.Text), cbbType.SelectedIndex, int.Parse(this.txtTONNAGE.Text), int.Parse(this.ckbConfirm.Checked.ToString().ToUpper().Replace("TRUE","1").Replace("FALSE","0"))) > 0)
                {
                    this.txtID.Text = "";
                    this.txtCode.Text = "";
                    this.txtTONNAGE.Text = "";
                    this.cbbType.SelectedIndex = 0;
                    this.cbbType.Enabled = false;

                    this.txtTONNAGE.ReadOnly = true;
                    this.txtCode.ReadOnly = true;

                    this.ckbConfirm.Checked = false;
                    this.ckbConfirm.Enabled = false;

                    this.ckbStatus.Checked = false;
                    this.ckbStatus.Enabled = false;

                    this.btnAdd.Enabled = true;
                    this.btnEdit.Enabled = true;
                    this.btnDel.Enabled = false;
                    this.btnCancel.Enabled = false;
                    this.btnSave.Enabled = false;

                    this.getData();
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin thất bại!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                string tmpValue = this.objVehicle.addData(this.txtCode.Text.Trim().ToUpper(), int.Parse(this.txtTONNAGE.Text.Trim()), this.cbbType.SelectedIndex, int.Parse(this.ckbStatus.Checked.ToString().ToUpper().Replace("TRUE", "1").Replace("FALSE", "0")), int.Parse(this.ckbConfirm.Checked.ToString().ToUpper().Replace("TRUE", "1").Replace("FALSE", "0")));
                if (tmpValue.Trim() == "OK")
                {
                    this.txtID.Text = "";
                    this.txtCode.Text = "";
                    this.txtTONNAGE.Text = "";
                    this.cbbType.SelectedIndex = 0;
                    this.cbbType.Enabled = false;

                    this.txtTONNAGE.ReadOnly = true;
                    this.txtCode.ReadOnly = true;

                    this.ckbConfirm.Checked = false;
                    this.ckbConfirm.Enabled = false;

                    this.ckbStatus.Checked = false;
                    this.ckbStatus.Enabled = false;

                    this.btnAdd.Enabled = true;
                    this.btnEdit.Enabled = true;
                    this.btnDel.Enabled = false;
                    this.btnCancel.Enabled = false;
                    this.btnSave.Enabled = false;

                    this.getData();
                }
                else
                {
                    MessageBox.Show(tmpValue, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void frmVehicleAllV1_Load(object sender, EventArgs e)
        {
            this.cbbTypeSearch.SelectedIndex = 0;
            this.cbbConfirmSearch.SelectedIndex = 0;
            this.cbbStatusSearch.SelectedIndex = 0;
            this.cbbType.SelectedIndex = 0;
        }

        private void btnConfirmByQRCode_Click(object sender, EventArgs e)
        {
            this.getData();
        }

        private void cbbTypeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.getData();
            }
            catch
            {

            }
        }

        private void cbbConfirmSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.getData();
            }
            catch
            {

            }
        }

        private void cbbStatusSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.getData();
            }
            catch
            {

            }
        }

        private void txtCodeSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.getData();
            }
        }

        #region method getCustomerData
        private void getCustomerData()
        {
            if (this.cbbCustomer.Items.Count > 0)
            {
                return;
            }

            if (frmMain.Token == "")
            {
                ServicePointManager.DefaultConnectionLimit = 100;
                ServicePointManager.MaxServicePointIdleTime = 5000;
                var client = new RestClient(TVSOracle.API + "/connect/token");
                var request = new RestRequest();
                request.Method = Method.POST;
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "multipart/form-data");
                request.Parameters.Clear();
                request.AddParameter("grant_type", "password");
                request.AddParameter("client_secret", "eb300de4-add9-42f4-a3ac-abd3c60f1919"); // DEV
                request.AddParameter("username", "mobifone");
                request.AddParameter("password", "mobi@A123456");
                request.AddParameter("client_id", "t8agr5xKt4$3");  // DEV
                var response = client.Execute(request);
                string data = response.Content;
                var jo = JObject.Parse(response.Content);
                var id = jo["access_token"].ToString();
                var expires = jo["expires_in"].ToString();
                frmMain.Token = id;
            }

            if (frmMain.Token == "")
            {
                return;
            }

            try
            {

                DataTable objTableCustomer = new DataTable();
                DataColumn objC1 = new DataColumn("CustomerId", typeof(int));
                DataColumn objC2 = new DataColumn("CustomerName", typeof(string));
                objTableCustomer.Columns.Add(objC1);
                objTableCustomer.Columns.Add(objC2);

                objTableCustomer.Rows.Add(0, "Chọn nhà phân phối");

                var client = new RestClient(TVSOracle.API + "/api/customer/contract");
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Bearer " + frmMain.Token);
                request.AddHeader("Content-Type", "application/json");
                IRestResponse response = client.Execute(request);
                string data = response.Content;

                dynamic jsonData = JsonConvert.DeserializeObject(data);

                if (jsonData != null)
                {
                    int customerId = 0;
                    string customerName = "";

                    foreach (var item in jsonData)
                    {
                        customerId = 0;
                        customerName = "";

                        if (item.customerId != null)
                        {
                            customerId = item.customerId;
                        }

                        if (item.customerName != null)
                        {
                            customerName = item.customerName;
                        }

                        objTableCustomer.Rows.Add(customerId, customerName);
                    }

                    this.cbbCustomer.DataSource = objTableCustomer;
                    this.cbbCustomer.ValueMember = "CustomerId";
                    this.cbbCustomer.DisplayMember = "CustomerName";

                    this.cbbCustomer.SelectedIndex = 0;

                    this.cbbCustomerStore.DataSource = objTableCustomer;
                    this.cbbCustomerStore.ValueMember = "CustomerId";
                    this.cbbCustomerStore.DisplayMember = "CustomerName";

                    this.cbbCustomerStore.SelectedIndex = 0;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            finally
            {
            }
        }
        #endregion

        private void cbbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cbbCustomer.SelectedIndex == 0)
                {
                    this.getDataCustomer(this._Vehicle);
                }
                else
                {
                    this.getDataCustomer("");
                }
            }
            catch
            {

            }
        }

        private void cbbCustomerStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.getStoreData(int.Parse(this.cbbCustomerStore.SelectedValue.ToString()));
            }
            catch
            {

            }
        }

        #region method getStoreData
        private void getStoreData(int CustomerId)
        {
            DataTable objTableStore = new DataTable();

            OracleConnection ORC_CONN = new OracleConnection();
            ORC_CONN.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MBFConnOracle"].ConnectionString;

            try
            {
                ORC_CONN.Open();
                OracleGlobalization glob = ORC_CONN.GetSessionInfo();
                glob.Language = "AMERICAN";
                ORC_CONN.SetSessionInfo(glob);
                OracleCommand Cmd = ORC_CONN.CreateCommand();
                Cmd.CommandText = "SELECT DISTINCT A.Id, A.Name_Store FROM STORE A, store_area B WHERE A.ID = B.store_id AND B.distributor_id_syn = " + CustomerId + " ORDER BY A.Name_Store";
                OracleDataAdapter da = new OracleDataAdapter();
                da.SelectCommand = Cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                objTableStore = ds.Tables[0];
                DataRow objRow = objTableStore.NewRow();
                objRow[0] = 0;
                objRow[1] = "Chọn cửa hàng";

                this.cbbStore.DataSource = objTableStore;
                this.cbbStore.ValueMember = "Id";
                this.cbbStore.DisplayMember = "Name_Store";

            }
            catch (Exception ex)
            {
                var str = ex.ToString();
            }

            finally
            {
                ORC_CONN.Close();
                ORC_CONN.Dispose();
            }
        }
        #endregion

        private void cbbStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbbStore .SelectedIndex == 0)
            {
                this.getDataStore(this._Vehicle);
            }
            else
            {
                this.getDataStore("");
            }
        }
    }
}
