using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HMXHTD
{
    public partial class frmSetting : Form
    {
        #region declare Objects
        private clsConfigXML objConfig = new clsConfigXML();
        #endregion

        #region method frmSetting
        public frmSetting()
        {
            InitializeComponent();
        }
        #endregion

        #region method frmSetting_Load
        private void frmSetting_Load(object sender, EventArgs e)
        {
            this.txtServer.Text = objConfig.GetKey("Server");
            this.txtUid.Text = objConfig.GetKey("Uid");
            this.txtPwd.Text = objConfig.GetKey("Pwd");
            this.txtDatabase.Text = objConfig.GetKey("Database");

            this.txtServer_O.Text = objConfig.GetKey("Server_O");
            this.txtUid_O.Text = objConfig.GetKey("Uid_O");
            this.txtPwd_O.Text = objConfig.GetKey("Pwd_O");
            this.txtDatabase_O.Text = objConfig.GetKey("Database_O");

            try
            {
                this.nmrFrom.Value = decimal.Parse(objConfig.GetKey("Cycle"));
            }
            catch
            {
            }

            try
            {
                this.nmrNumDay.Value = decimal.Parse(objConfig.GetKey("NumDay"));
            }
            catch
            {
            }
           
        }
        #endregion

        #region method btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            objConfig.UpdateKey("Server", this.txtServer.Text);
            objConfig.UpdateKey("Uid", this.txtUid.Text);
            objConfig.UpdateKey("Pwd", this.txtPwd.Text);
            objConfig.UpdateKey("Database", this.txtDatabase.Text);

            objConfig.UpdateKey("Server_O", this.txtServer_O.Text);
            objConfig.UpdateKey("Uid_O", this.txtUid_O.Text);
            objConfig.UpdateKey("Pwd_O", this.txtPwd_O.Text);
            objConfig.UpdateKey("Database_O", this.txtDatabase_O.Text);

            objConfig.UpdateKey("Cycle", this.nmrFrom.Value.ToString());
            objConfig.UpdateKey("NumDay", this.nmrNumDay.Value.ToString());

            MessageBox.Show("Thông tin đã được cập nhật vào hệ thống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        #endregion

        #region method btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region method frmSetting_KeyDown
        private void frmSetting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.btnClose.PerformClick();
            }
        }
        #endregion
    }
}