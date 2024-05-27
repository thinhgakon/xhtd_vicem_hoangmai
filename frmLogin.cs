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
using HMXHTD.Data.DataEntity;
using Microsoft.AspNet.SignalR.Client;
using System.Net.Http;
using Oracle.ManagedDataAccess.Client;

namespace HMXHTD
{
    public partial class frmLogin : Form
    {
        #region declare objects
        private Account objAccount = new Account();
        private TVSOracle objTVSOracle = new TVSOracle();
        #endregion
        private String UserName { get; set; }
        private IHubProxy HubProxy { get; set; }
        const string ServerURI = "http://192.168.0.10:8091/signalr"; // service ở con 0.10
       // const string ServerURI = "http://192.168.158.55:8091/signalr"; // service ở con 55

        //"http://localhost:8091/signalr"; // dành cho trường hợp đẩy lên server
        private HubConnection Connection { get; set; }

        #region method frmLogin
        public frmLogin()
        {
            InitializeComponent();
            this.lblMsg.Text = "version 0.0.198 -  11/11/2021";
            //ConnectAsync();
        }
        private async void ConnectAsync()
        {
            Connection = new HubConnection(ServerURI);
            Connection.Closed += Connection_Closed;
            HubProxy = Connection.CreateHubProxy("MyHub");

            //HubProxy.On<string, string>("SendMessage", (name, message) =>
            //    this.Invoke((Action)(() =>
            //        this.lblMsg.Text = (String.Format("{0}: {1}" + Environment.NewLine, name, message))
            //    ))
            //);
            try
            {
                await Connection.Start();
            }
            catch (HttpRequestException ex)
            {
                this.lblMsg.Text = "Unable to connect to server: Start server before connecting clients.";
                return;
            }

            //Activate UI
            this.lblMsg.Text=("Connected to server at " + ServerURI + Environment.NewLine);
        }
        private void Connection_Closed()
        {
            //Deactivate chat UI; show login UI. 
        }
        #endregion

        #region method btnLogin_Click
        private void btnLogin_Click(object sender, EventArgs e)
        {
           // var tes5t = this.objAccount.CryptographyMD5("123123"); //1023375801172532913083541455433787343290327
            //  HubProxy.Invoke("Send", "Step_8","xe ra").Wait();
            //try
            //{
            //    HubProxy.Invoke("SendScaleInfo1", DateTime.Now, "gửi lên").Wait();
            //    //   HubProxy.Invoke("SendScaleInfo2", "gửi lên", DateTime.Now);
            //}
            //catch (Exception ex)
            //{

            //    this.lblMsg.Text = ex.Message;
            //}
            // return;
            //TestOracle();
            this.lblMsg.Text = "";

            if (this.txtUsername.Text.Trim() == "")
            {
                this.lblMsg.Text = "Tài khoản không hợp lệ!";
                this.txtUsername.Focus();
                return;
            }

            if (this.txtPassword.Text.Trim() == "")
            {
                this.lblMsg.Text = "Mật khẩu không hợp lệ!";
                this.txtPassword.Focus();
                return;
            }

            string FullName = "";
            if (this.objAccount.login(this.txtUsername.Text.Trim(), this.txtPassword.Text.Trim(), ref FullName))
            {
                frmMain.UserName = this.txtUsername.Text.Trim();
                frmMain.FullName = FullName;
                this.Close();
                this.lblMsg.Text = "";
            }
            else
            {
                this.lblMsg.Text = "Đăng nhập không thành công!";
                return;
            }
        }
        #endregion

        public void TestOracle()
        {
            try
            {
                #region Oracle
                string sqlQuery = "";
                string strConString = System.Configuration.ConfigurationManager.ConnectionStrings["MbfConnOracle"].ConnectionString.ToString();
                VehicleStoreInfo objVehicleStoreInfo;
                List<VehicleStoreInfo> objList = new List<VehicleStoreInfo>();
                sqlQuery = "SELECT * FROM dev_om_item_list_v";
                using (OracleConnection connection = new OracleConnection(strConString))
                {
                    OracleCommand Cmd = new OracleCommand(sqlQuery, connection);
                    connection.Open();
                    //Cmd.Parameters.Add("CodeStore", SqlDbType.NVarChar).Value = objVehicleStoreParam.CodeStore;
                    using (OracleDataReader Rd = Cmd.ExecuteReader())
                    {
                        while (Rd.Read())
                        {
                            var test = Rd["DESCRIPTION"].ToString();
                            //objVehicleStoreInfo = new VehicleStoreInfo();
                            //objVehicleStoreInfo.Vehicle = Rd["DRIVER_NAME"].ToString();
                            //objList.Add(objVehicleStoreInfo);
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                var res = ex.Message;
            }
        }

        #region method frmLogin_FormClosing
        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.txtUsername.Text.Trim() != "" && this.txtPassword.Text.Trim() != "")
            {
                this.Hide();
                frmMain objMain = new frmMain();
                objMain.ShowDialog();

            }
        }
        #endregion

        #region method btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.txtUsername.Text = "";
            this.txtPassword.Text = "";
            this.Close();
        }
        #endregion

        #region method txtUsername_KeyDown
        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnLogin.PerformClick();
            }
        }
        #endregion

        #region method txtPassword_KeyDown
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnLogin.PerformClick();
            }
        }
        #endregion
    }
    public class VehicleStoreInfo
    {
        public string Vehicle { get; set; }
        public string NameDriver { get; set; }
        public string IdCardNumber { get; set; }
    }
}
