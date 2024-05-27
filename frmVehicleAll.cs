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
    public partial class frmVehicleAll : Form
    {
        #region Decslare objects
        DataTable objTable = new DataTable();
        int customerId = 0, vehicleId = 0, moocId = 0, driverId = 0;
        #endregion

        #region method frmVehicleAll
        public frmVehicleAll()
        {
            InitializeComponent();
        }
        #endregion

        #region method button1_Click
        private void button1_Click(object sender, EventArgs e)
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
        #endregion

        #region method getData
        private void getData()
        {
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

                this.lblCountVehicle.Text = "";
                this.lblState.Text = "Đang tải dữ liệu, vui lòng đợi ...";
                this.lblState.Refresh();

                this.objTable.Clear();

                var client = new RestClient(TVSOracle.API + "/api/vehicle/all");
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Bearer " + frmMain.Token);
                request.AddHeader("Content-Type", "application/json");
                IRestResponse response = client.Execute(request);
                string data = response.Content;

                dynamic jsonData = JsonConvert.DeserializeObject(data);

                if (jsonData != null)
                {
                    int id = 0;
                    string vehicleCode = "";
                    string vehicleCertificate = "";
                    int vehicleType = 0;
                    int transportMethodId = 0;
                    string registrationDeadline = "";
                    int weightLimit = 0;

                    foreach (var item in jsonData)
                    {
                        id = 0;
                        vehicleCode = "";
                        vehicleCertificate = "";
                        vehicleType = 0;
                        transportMethodId = 0;
                        registrationDeadline = "";
                        weightLimit = 0;

                        if (item.id != null)
                        {
                            id = item.id;
                        }

                        if (item.vehicleCode != null)
                        {
                            vehicleCode = item.vehicleCode;
                        }

                        if (item.vehicleCertificate != null)
                        {
                            vehicleCertificate = item.vehicleCertificate;
                        }

                        if (item.vehicleType != null)
                        {
                            vehicleType = item.vehicleType;
                        }

                        if (item.transportMethodId != null)
                        {
                            transportMethodId = item.transportMethodId;
                        }

                        if (item.registrationDeadline != null)
                        {
                            registrationDeadline = item.registrationDeadline;
                        }

                        if (item.weightLimit != null)
                        {
                            weightLimit = item.weightLimit;
                        }

                        this.objTable.Rows.Add(id, vehicleCode, vehicleCertificate, vehicleType, vehicleType.ToString().Replace("1","Xe tải").Replace("2", "Xe đầu kéo").Replace("3", "Tàu").Replace("4", "Xà lan"), transportMethodId, transportMethodId.ToString().Replace("1","Đường bộ"), registrationDeadline, weightLimit);
                    }

                    this.dgvVehicle.AutoGenerateColumns = false;
                    this.dgvVehicle.DataSource = this.objTable;

                    this.lblCountVehicle.Text = this.objTable.Rows.Count.ToString();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            finally
            {
                this.lblState.Text = "";
                this.lblState.Refresh();
            }
        }
        #endregion

        #region method getCustomerData
        private void getCustomerData()
        {
            this.lblCountCustomer.Text = "";

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
                DataColumn objC1 = new DataColumn("CustomerId",typeof(int));
                DataColumn objC2 = new DataColumn("CustomerName", typeof(string));
                objTableCustomer.Columns.Add(objC1);
                objTableCustomer.Columns.Add(objC2);

                objTableCustomer.Rows.Add(0,"Chọn nhà phân phối");

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

                    this.lblCountCustomer.Text = objTableCustomer.Rows.Count.ToString();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            finally
            {
                this.lblState.Text = "";
                this.lblState.Refresh();
            }
        }
        #endregion

        #region method getCustomerVehicleData
        private void getCustomerVehicleData(int CustomerId)
        {
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

                DataTable objTableVehicleCustomer = new DataTable();
                
                DataColumn objC1 = new DataColumn("id", typeof(string));
                DataColumn objC2 = new DataColumn("vehicleId", typeof(string));

                DataColumn objC3 = new DataColumn("vehicleCode", typeof(string));
                DataColumn objC4 = new DataColumn("vehicleType", typeof(string));

                DataColumn objC5 = new DataColumn("moocId", typeof(string));
                DataColumn objC6 = new DataColumn("moocCode", typeof(string));

                DataColumn objC7 = new DataColumn("driverId", typeof(string));
                DataColumn objC8 = new DataColumn("driverName", typeof(string));

                DataColumn objC9 = new DataColumn("fromDate", typeof(string));
                DataColumn objC10 = new DataColumn("toDate", typeof(string));

                DataColumn objC11 = new DataColumn("customerId", typeof(string));
                DataColumn objC12 = new DataColumn("vehicleLimit", typeof(string));

                DataColumn objC13 = new DataColumn("moocLimit", typeof(string));
                DataColumn objC14 = new DataColumn("driverLicense", typeof(string));

                objTableVehicleCustomer.Columns.Add(objC1);
                objTableVehicleCustomer.Columns.Add(objC2);
                objTableVehicleCustomer.Columns.Add(objC3);
                objTableVehicleCustomer.Columns.Add(objC4);
                objTableVehicleCustomer.Columns.Add(objC5);
                objTableVehicleCustomer.Columns.Add(objC6);
                objTableVehicleCustomer.Columns.Add(objC7);
                objTableVehicleCustomer.Columns.Add(objC8);
                objTableVehicleCustomer.Columns.Add(objC9);
                objTableVehicleCustomer.Columns.Add(objC10);
                objTableVehicleCustomer.Columns.Add(objC11);
                objTableVehicleCustomer.Columns.Add(objC12);
                objTableVehicleCustomer.Columns.Add(objC13);
                objTableVehicleCustomer.Columns.Add(objC14);

                var client = new RestClient(TVSOracle.API + "/api/vehicle-customer/"+ CustomerId + "/1");
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Bearer " + frmMain.Token);
                request.AddHeader("Content-Type", "application/json");
                IRestResponse response = client.Execute(request);
                string data = response.Content;

                dynamic jsonData = JsonConvert.DeserializeObject(data);

                if (jsonData != null)
                {
                    string id = "";
                    string vehicleId = "";
                    string vehicleCode = "";
                    string vehicleType = "";
                    string moocId = "";
                    string moocCode = "";
                    string driverId = "";
                    string driverName = "";
                    string fromDate = "";
                    string toDate = "";
                    string customerId = "";
                    string vehicleLimit = "";
                    string moocLimit = "";
                    string driverLicense = "";

                    foreach (var item in jsonData)
                    {
                        id = "";
                        vehicleId = "";
                        vehicleCode = "";
                        vehicleType = "";
                        moocId = "";
                        moocCode = "";
                        driverId = "";
                        driverName = "";
                        fromDate = "";
                        toDate = "";
                        customerId = "";
                        vehicleLimit = "";
                        moocLimit = "";
                        driverLicense = "";

                        if (item.id != null)
                        {
                            id = item.id;
                        }

                        if (item.vehicleId != null)
                        {
                            vehicleId = item.vehicleId;
                        }

                        if (item.vehicleCode != null)
                        {
                            vehicleCode = item.vehicleCode;
                        }

                        if (item.vehicleType != null)
                        {
                            vehicleType = item.vehicleType;
                        }

                        if (item.moocId != null)
                        {
                            moocId = item.moocId;
                        }

                        if (item.moocCode != null)
                        {
                            moocCode = item.moocCode;
                        }

                        if (item.driverId != null)
                        {
                            driverId = item.driverId;
                        }

                        if (item.driverName != null)
                        {
                            driverName = item.driverName;
                        }

                        if (item.fromDate != null)
                        {
                            fromDate = item.fromDate;
                        }

                        if (item.toDate != null)
                        {
                            toDate = item.toDate;
                        }

                        if (item.customerId != null)
                        {
                            customerId = item.customerId;
                        }

                        if (item.vehicleLimit != null)
                        {
                            vehicleLimit = item.vehicleLimit;
                        }

                        if (item.moocLimit != null)
                        {
                            moocLimit = item.moocLimit;
                        }

                        if (item.driverLicense != null)
                        {
                            driverLicense = item.driverLicense;
                        }

                        objTableVehicleCustomer.Rows.Add(id,
                        vehicleId,
                        vehicleCode,
                        vehicleType,
                        moocId,
                        moocCode,
                        driverId,
                        driverName,
                        fromDate,
                        toDate,
                        customerId,
                        vehicleLimit,
                        moocLimit,
                        driverLicense);
                    }

                    this.dgvVehicleCustomer.AutoGenerateColumns = false;
                    this.dgvVehicleCustomer.DataSource = objTableVehicleCustomer;

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            finally
            {
                this.lblState.Text = "";
                this.lblState.Refresh();
            }
        }
        #endregion

        #region method getStoreData
        private void getStoreData(int CustomerId)
        {
            this.lblCountStore.Text = "";
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

                this.lblCountStore.Text = objTableStore.Rows.Count.ToString();
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

        #region method getStoreVehicleData
        private void getStoreVehicleData(int StoreId)
        {
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
                DataTable objTableVehicleStore = new DataTable();

                DataColumn objC1 = new DataColumn("id", typeof(string));
                DataColumn objC2 = new DataColumn("fromDate", typeof(string));
                DataColumn objC3 = new DataColumn("toDate", typeof(string));
                DataColumn objC4 = new DataColumn("vehicleId", typeof(string));
                DataColumn objC5 = new DataColumn("vehicleCode", typeof(string));
                DataColumn objC6 = new DataColumn("vehicleWeightLimit", typeof(string));
                DataColumn objC7 = new DataColumn("vehicleType", typeof(string));
                DataColumn objC8 = new DataColumn("moocId", typeof(string));
                DataColumn objC9 = new DataColumn("moocCode", typeof(string));
                DataColumn objC10 = new DataColumn("moocWeightLimit", typeof(string));
                DataColumn objC11 = new DataColumn("driverId", typeof(string));
                DataColumn objC12 = new DataColumn("driverName", typeof(string));
                DataColumn objC13 = new DataColumn("driverIndentityCard", typeof(string));
                DataColumn objC14 = new DataColumn("driverLicense", typeof(string));
                DataColumn objC15 = new DataColumn("phoneNumber", typeof(string));

                objTableVehicleStore.Columns.Add(objC1);
                objTableVehicleStore.Columns.Add(objC2);
                objTableVehicleStore.Columns.Add(objC3);
                objTableVehicleStore.Columns.Add(objC4);
                objTableVehicleStore.Columns.Add(objC5);
                objTableVehicleStore.Columns.Add(objC6);
                objTableVehicleStore.Columns.Add(objC7);
                objTableVehicleStore.Columns.Add(objC8);
                objTableVehicleStore.Columns.Add(objC9);
                objTableVehicleStore.Columns.Add(objC10);
                objTableVehicleStore.Columns.Add(objC11);
                objTableVehicleStore.Columns.Add(objC12);
                objTableVehicleStore.Columns.Add(objC13);
                objTableVehicleStore.Columns.Add(objC14);
                objTableVehicleStore.Columns.Add(objC15);

                var client = new RestClient(TVSOracle.API + "/api/vehiclemanagement/byVehicleStore?StoreId="+StoreId);
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Bearer " + frmMain.Token);
                request.AddHeader("Content-Type", "application/json");
                IRestResponse response = client.Execute(request);
                string data = response.Content;

                dynamic jsonData = JsonConvert.DeserializeObject(data);

                if (jsonData != null)
                {
                    string id = "";
                    string fromDate = "";
                    string toDate = "";
                    string vehicleId = "";
                    string vehicleCode = "";
                    string vehicleWeightLimit = "";
                    string vehicleType = "";
                    string moocId = "";
                    string moocCode = "";
                    string moocWeightLimit = "";
                    string driverId = "";
                    string driverName = "";
                    string driverIndentityCard = "";
                    string driverLicense = "";
                    string phoneNumber = "";

                    foreach (var item in jsonData)
                    {
                        id = "";
                        fromDate = "";
                        toDate = "";
                        vehicleId = "";
                        vehicleCode = "";
                        vehicleWeightLimit = "";
                        vehicleType = "";
                        moocId = "";
                        moocCode = "";
                        moocWeightLimit = "";
                        driverId = "";
                        driverName = "";
                        driverIndentityCard = "";
                        driverLicense = "";
                        phoneNumber = "";

                        if (item.id != null)
                        {
                            id = item.id;
                        }

                        if (item.fromDate != null)
                        {
                            fromDate = item.fromDate;
                        }

                        if (item.toDate != null)
                        {
                            toDate = item.toDate;
                        }

                        if (item.vehicleId != null)
                        {
                            vehicleId = item.vehicleId;
                        }

                        if (item.vehicleCode != null)
                        {
                            vehicleCode = item.vehicleCode;
                        }

                        if (item.vehicleWeightLimit != null)
                        {
                            vehicleWeightLimit = item.vehicleWeightLimit;
                        }

                        if (item.vehicleType != null)
                        {
                            vehicleType = item.vehicleType;
                        }

                        if (item.moocId != null)
                        {
                            moocId = item.moocId;
                        }

                        if (item.moocCode != null)
                        {
                            moocCode = item.moocCode;
                        }

                        if (item.moocWeightLimit != null)
                        {
                            moocWeightLimit = item.moocWeightLimit;
                        }

                        if (item.driverId != null)
                        {
                            driverId = item.driverId;
                        }

                        if (item.driverName != null)
                        {
                            driverName = item.driverName;
                        }

                        if (item.driverIndentityCard != null)
                        {
                            driverIndentityCard = item.driverIndentityCard;
                        }

                        if (item.driverLicense != null)
                        {
                            driverLicense = item.driverLicense;
                        }

                        if (item.phoneNumber != null)
                        {
                            phoneNumber = item.phoneNumber;
                        }

                        objTableVehicleStore.Rows.Add(
                           id,
                        fromDate,
                        toDate,
                        vehicleId,
                        vehicleCode,
                        vehicleWeightLimit,
                        vehicleType,
                        moocId,
                        moocCode,
                        moocWeightLimit,
                        driverId,
                        driverName,
                        driverIndentityCard,
                        driverLicense,
                        phoneNumber
                        );
                    }

                    this.dgvVehicleStore.AutoGenerateColumns = false;
                    this.dgvVehicleStore.DataSource = objTableVehicleStore;

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            finally
            {
                this.lblState.Text = "";
                this.lblState.Refresh();
            }
        }
        #endregion

        #region method frmVehicleAll_KeyDown
        private void frmVehicleAll_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region method frmVehicleAll_Load
        private void frmVehicleAll_Load(object sender, EventArgs e)
        {
            DataColumn objC1 = new DataColumn("id",typeof(int));
            DataColumn objC2 = new DataColumn("vehicleCode", typeof(string));
            DataColumn objC3 = new DataColumn("vehicleCertificate", typeof(string));
            DataColumn objC4 = new DataColumn("vehicleType", typeof(int));
            DataColumn objC5 = new DataColumn("vehicleTypeName", typeof(string));
            DataColumn objC6 = new DataColumn("transportMethodId", typeof(int));
            DataColumn objC7 = new DataColumn("transportMethodName", typeof(string));
            DataColumn objC8 = new DataColumn("registrationDeadline", typeof(string));
            DataColumn objC9 = new DataColumn("weightLimit", typeof(int));

            this.objTable.Columns.Add(objC1);
            this.objTable.Columns.Add(objC2);
            this.objTable.Columns.Add(objC3);
            this.objTable.Columns.Add(objC4);
            this.objTable.Columns.Add(objC5);
            this.objTable.Columns.Add(objC6);
            this.objTable.Columns.Add(objC7);
            this.objTable.Columns.Add(objC8);
            this.objTable.Columns.Add(objC9);
        }
        #endregion

        #region method frmVehicleAll_Shown
        private void frmVehicleAll_Shown(object sender, EventArgs e)
        {
            this.getData();
            this.getCustomerData();
        }
        #endregion

        #region method dgvVehicle_CellMouseDoubleClick
        private void dgvVehicle_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            frmVehicleAllEdit objfrmVehicleAllEdit = new frmVehicleAllEdit();
            objfrmVehicleAllEdit.txtId.Text = this.dgvVehicle.Rows[e.RowIndex].Cells["dgvVehicleid"].Value.ToString();
            objfrmVehicleAllEdit.txtVehicleCode.Text = this.dgvVehicle.Rows[e.RowIndex].Cells["dgvVehiclevehicleCode"].Value.ToString();
            objfrmVehicleAllEdit.txtvehicleCertificate.Text = this.dgvVehicle.Rows[e.RowIndex].Cells["dgvVehiclevehicleCertificate"].Value.ToString();
            objfrmVehicleAllEdit.cbbvehicleType.SelectedIndex = int.Parse(this.dgvVehicle.Rows[e.RowIndex].Cells["dgvVehiclevehicleType"].Value.ToString());
            objfrmVehicleAllEdit.cbbTransportMethodId.SelectedIndex = int.Parse(this.dgvVehicle.Rows[e.RowIndex].Cells["dgvVehicletransportMethodId"].Value.ToString());
            try
            {
                objfrmVehicleAllEdit.dtpRegistrationDeadline.Value = DateTime.Parse(this.dgvVehicle.Rows[e.RowIndex].Cells["dgvVehicleregistrationDeadline"].Value.ToString());
            }
            catch
            {

            }
            objfrmVehicleAllEdit.txtWeightLimit.Text = this.dgvVehicle.Rows[e.RowIndex].Cells["dgvVehicleweightLimit"].Value.ToString();
            objfrmVehicleAllEdit.ShowDialog();

            if (objfrmVehicleAllEdit.updateCode.Trim() != "")
            {
                this.getData();
            }
        }
        #endregion

        #region method cbbCustomer_SelectedIndexChanged
        private void cbbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.getStoreData(int.Parse(this.cbbCustomer.SelectedValue.ToString()));
                this.getCustomerVehicleData(int.Parse(this.cbbCustomer.SelectedValue.ToString()));
            }
            catch
            {

            }
        }
        #endregion

        #region method cbbStore_SelectedIndexChanged
        private void cbbStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cbbStore.SelectedValue.ToString() != "0")
                {
                    this.btnAdd.Enabled = true;
                    this.getStoreVehicleData(int.Parse(this.cbbStore.SelectedValue.ToString()));
                }
                else
                {
                    this.btnAdd.Enabled = false;
                }
            }
            catch
            {

            }
        }

        private void dgvVehicle_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.customerId = 0;
            this.vehicleId = 0;
            this.moocId = 0;
            this.driverId = 0;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.customerId = 0;
            this.vehicleId = 0;
            this.moocId = 0;
            this.driverId = 0;

            if (this.tabControl1.SelectedIndex == 1)
            {
                this.getStoreVehicleData(int.Parse(this.cbbStore.SelectedValue.ToString()));
            }
        }
        #endregion

        #region method btnAdd_Click
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.customerId == 0 || this.vehicleId == 0)
            {
                MessageBox.Show("Bạn chưa xác định đầy đủ thông tin phương tiện!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string strBody = "{";
            strBody += "\"customerId\": "+this.customerId.ToString()+",";
            strBody += "\"vehicleId\": "+this.vehicleId.ToString()+",";
            strBody += "\"moocId\": "+this.moocId.ToString()+ ",";
            strBody += "\"driverId\": "+this.driverId.ToString()+",";
            strBody += "\"fromDate\": \"2021-05-10T16:05:14.563Z\",";
            strBody += "\"toDate\": \"2021-05-10T16:05:14.563Z\",";
            strBody += "\"storeId\": "+this.cbbStore.SelectedValue.ToString();
            strBody += "}";

            var client = new RestClient("http://upwebsale.ximanghoangmai.vn:5555/api/vehiclemanagement");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + frmMain.Token);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", strBody, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "Created")
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.tabControl1.SelectedIndex = 1;
            }
            else
            {
                MessageBox.Show("Lỗi: " + response.StatusCode.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region method dgvVehicleCustomer_CellEnter
        private void dgvVehicleCustomer_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.customerId = int.Parse(this.dgvVehicleCustomer.Rows[e.RowIndex].Cells["dgvVehicleCustomercustomerId"].Value.ToString());
                this.vehicleId = int.Parse(this.dgvVehicleCustomer.Rows[e.RowIndex].Cells["dgvVehicleCustomervehicleId"].Value.ToString());
                this.moocId = int.Parse(this.dgvVehicleCustomer.Rows[e.RowIndex].Cells["dgvVehicleCustomermoocId"].Value.ToString());
                this.driverId = int.Parse(this.dgvVehicleCustomer.Rows[e.RowIndex].Cells["dgvVehicleCustomerdriverId"].Value.ToString());
            }
            catch
            {

            }
        }
        #endregion
    }
}
