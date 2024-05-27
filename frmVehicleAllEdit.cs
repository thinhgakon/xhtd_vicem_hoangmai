using RestSharp;
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
    public partial class frmVehicleAllEdit : Form
    {
        public string updateCode = "";

        public frmVehicleAllEdit()
        {
            InitializeComponent();
        }

        private void frmVehicleAllEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmVehicleAllEdit_Load(object sender, EventArgs e)
        {

        }

        private void frmVehicleAllEdit_Shown(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtVehicleCode.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập thông tin số hiệu phương tiện","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtVehicleCode.Focus();
                return;
            }

            if (this.cbbvehicleType.SelectedIndex == 0)
            {
                MessageBox.Show("Bạn chưa xác định loại phương tiện", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.cbbvehicleType.Focus();
                return;
            }

            if (this.cbbTransportMethodId.SelectedIndex == 0)
            {
                MessageBox.Show("Bạn chưa xác định phương thức vận chuyển của phương tiện", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.cbbTransportMethodId.Focus();
                return;
            }

            int WeightLimit = 0;
            if (this.txtWeightLimit.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập thông tin tải trọng của  phương tiện", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtWeightLimit.Focus();
                return;
            }

            try
            {
                WeightLimit = int.Parse(this.txtWeightLimit.Text);
            }
            catch
            {
                MessageBox.Show("Bạn chưa nhập thông tin tải trọng của  phương tiện", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtWeightLimit.Focus();
                return;
            }

            if (WeightLimit <= 0)
            {
                MessageBox.Show("Bạn chưa nhập thông tin tải trọng của  phương tiện", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtWeightLimit.Focus();
                return;
            }

            var client = new RestClient("http://upwebsale.ximanghoangmai.vn:5555/api/vehicle");
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Authorization", "Bearer "+frmMain.Token);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n   \"id\": "+this.txtId.Text+",\r\n        \"vehicleCode\": \""+this.txtVehicleCode.Text+"\",\r\n        \"vehicleCertificate\": \""+this.txtvehicleCertificate.Text+"\",\r\n        \"vehicleType\": "+ this.cbbvehicleType .SelectedIndex+ ",\r\n        \"transportMethodId\": "+this.cbbTransportMethodId.SelectedIndex+",\r\n        \"registrationDeadline\": \""+this.dtpRegistrationDeadline.Value.ToString("MM/dd/yyyy")+"\",\r\n        \"weightLimit\": "+this.txtWeightLimit.Text+"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            this.updateCode = response.StatusCode.ToString();

            if (this.updateCode == "NoContent"){
                MessageBox.Show("Cập nhật thông tin thành công!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Lỗi: "+ this.updateCode,"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.updateCode = "";
            this.Close();
        }
    }
}
