using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Threading;
using System.Data.SqlClient;
//using Oracle.ManagedDataAccess.Client;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using HMXHTD.Core;
using System.Deployment.Application;

namespace HMXHTD
{
    public partial class frmMain : Form
    {
        #region declare objects
        public static string UserName = "", FullName = "", Token = "";
        private Account objAccount = new Account();
        public static bool SysOperating = false;
        private int HomePage = 0;
        #endregion

        #region method frmMain
        public frmMain()
        {
            InitializeComponent();
        }
        #endregion

        #region method frmMain_Load
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                #region Phan quyen
                DataTable objTable = new DataTable();
                objTable = this.objAccount.getDataByUserName(frmMain.UserName);
                if (objTable.Rows.Count > 0)
                {
                    var test = new Driver().CryptographyMD5("hmc@123");
                    var test2 = new Driver().CryptographyMD5("123321");
                    //290532904001683407390578308278531520102312354
                    frmMain.SysOperating = bool.Parse(objTable.Rows[0]["SysOperating"].ToString());
                    this.mnuAccount.Enabled = bool.Parse(objTable.Rows[0]["SysAccount"].ToString());
                    this.mnuConfig.Enabled = bool.Parse(objTable.Rows[0]["SysConfig"].ToString());

                    this.mnuTaskOperating.Enabled = bool.Parse(objTable.Rows[0]["TaskOperating"].ToString());
                    this.mnuTaskConfirm.Enabled = bool.Parse(objTable.Rows[0]["TaskConfirm"].ToString());
                    this.mnuTaskInOut.Enabled = bool.Parse(objTable.Rows[0]["TaskInOut"].ToString());
                    this.mnuTaskScale.Enabled = bool.Parse(objTable.Rows[0]["TaskScale"].ToString());
                    this.mnuTaskRelease.Enabled = bool.Parse(objTable.Rows[0]["TaskRelease"].ToString());
                    this.mnuTaskRelease2.Enabled = bool.Parse(objTable.Rows[0]["TaskRelease2"].ToString());
                    this.mnuTaskDbet.Enabled = bool.Parse(objTable.Rows[0]["TaskDbet"].ToString());

                    this.mnuReportConfirm.Enabled = bool.Parse(objTable.Rows[0]["ReportConfirm"].ToString());
                    this.mnuReportInOut.Enabled = bool.Parse(objTable.Rows[0]["ReportInOut"].ToString());
                    this.mnuReportScale.Enabled = bool.Parse(objTable.Rows[0]["ReportScale"].ToString());
                    this.mnuReportRelease.Enabled = bool.Parse(objTable.Rows[0]["ReportRelease"].ToString());

                    this.mnuTrough.Enabled = bool.Parse(objTable.Rows[0]["DirTrough"].ToString());
                    this.mnuRFID.Enabled = bool.Parse(objTable.Rows[0]["DirRFID"].ToString());
                    this.mnuDevice.Enabled = bool.Parse(objTable.Rows[0]["DirDevice"].ToString());
                    this.mnuVehicle.Enabled = bool.Parse(objTable.Rows[0]["DirVehicle"].ToString());
                    this.mnuDriver.Enabled = bool.Parse(objTable.Rows[0]["DirDriver"].ToString());
                    this.mnuDriverAccount.Enabled = bool.Parse(objTable.Rows[0]["DirDriverAccount"].ToString());

                    this.HomePage = int.Parse(objTable.Rows[0]["HomePage"].ToString());
                }

                //if (this.HomePage == 1 && this.mnuTaskOperating.Enabled)
                //{
                //    this.mnuTaskOperating.PerformClick();
                //}
                //else if (this.HomePage == 2 && this.mnuTaskConfirm.Enabled)
                //{
                //    this.mnuTaskConfirm.PerformClick();
                //}
                //else if (this.HomePage == 3 && this.mnuTaskInOut.Enabled)
                //{
                //    this.mnuTaskInOut.PerformClick();
                //}
                //else if (this.HomePage == 4 && this.mnuTaskScale.Enabled)
                //{
                //    this.mnuTaskScale.PerformClick();
                //}
                //else if (this.HomePage == 5 && this.mnuTaskRelease.Enabled)
                //{
                //    this.mnuTaskRelease.PerformClick();
                //}
                //else if (this.HomePage == 6 && this.mnuTaskVehiceInfo.Enabled)
                //{
                //    this.mnuTaskVehiceInfo.PerformClick();
                //}
                #endregion

                this.clearControl(this.MainSplitContainer1, "objF1");
                frmMainControl objF1 = new frmMainControl();
                objF1.Name = "objF1";
                objF1.TopLevel = false;
                objF1.Dock = DockStyle.Fill;
                this.MainSplitContainer1.Panel1.Controls.Add(objF1);
                objF1.Show();

                this.lblFooter.Text = "Phiên làm việc: "+frmMain.FullName+" - Đăng nhập lúc: "+DateTime.Now.ToString("HH:mm dd/MM/yyyy");
            }
            catch
            {
            }
        }
        #endregion

        #region method mnuAccount_Click
        private void mnuAccount_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmAccount objAccount = new frmAccount();
            objAccount.TopLevel = false;
            objAccount.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objAccount);
            this.Text = "VICEM HOANG MAI - TÀI KHOẢN";
            objAccount.Show();

        }
        #endregion

        #region method mnuConfig_Click
        private void mnuConfig_Click(object sender, EventArgs e)
        {
            frmConfig objConfig = new frmConfig();
            objConfig.ShowDialog();
        }
        #endregion

        #region method mnuExit_Click
        private void mnuExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch
            {

            }
        }
        #endregion

        #region method frmMain_Shown
        private void frmMain_Shown(object sender, EventArgs e)
        {
        }
        #endregion

        #region method frmMain_Resize
        private void frmMain_Resize(object sender, EventArgs e)
        {
            //if (this.WindowState == FormWindowState.Minimized)
            //{
            //    this.Hide();
            //    this.notifyIcon1.Visible = true;
            //}
        }
        #endregion

        #region method mnuTaskOperating_Click
        private void mnuTaskOperating_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmTaskOperating objTaskOperating = new frmTaskOperating();
            objTaskOperating.TopLevel = false;
            objTaskOperating.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objTaskOperating);
            this.Text = "VICEM HOANG MAI - QUẢN LÝ ĐIỀU HÀNH";
            objTaskOperating.Show();
        }
        #endregion

        #region method mnuTaskConfirm_Click
        private void mnuTaskConfirm_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmTaskConfirm objConfirm = new frmTaskConfirm();
            objConfirm.TopLevel = false;
            objConfirm.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objConfirm);
            this.Text = "VICEM HOANG MAI - QUẢN LÝ XÁC THỰC";
            objConfirm.Show();
        }
        #endregion

        #region method mnuTaskInOut_Click
        private void mnuTaskInOut_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmTaskInOutNew objTaskInOut = new frmTaskInOutNew();
            objTaskInOut.TopLevel = false;
            objTaskInOut.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objTaskInOut);
            this.Text = "VICEM HOANG MAI - QUẢN LÝ VÀO, RA";
            objTaskInOut.Show();
        }
        #endregion

        #region method mnuTaskScale_Click
        private void mnuTaskScale_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmTaskScale objTaskScale = new frmTaskScale();
            objTaskScale.TopLevel = false;
            objTaskScale.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objTaskScale);
            this.Text = "VICEM HOANG MAI - QUẢN LÝ CÂN";
            objTaskScale.Show();
        }
        #endregion

        #region method mnuTaskRelease_Click
        private void mnuTaskRelease_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmTaskRelease objTaskRelease = new frmTaskRelease();
            objTaskRelease.TopLevel = false;
            objTaskRelease.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objTaskRelease);
            this.Text = "VICEM HOANG MAI - QUẢN LÝ XUẤT HÀNG";
            objTaskRelease.Show();
        }
        #endregion

        #region method mnuTaskRelease2_Click
        private void mnuTaskRelease2_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmTaskRelease2 objTaskRelease2 = new frmTaskRelease2();
            objTaskRelease2.TopLevel = false;
            objTaskRelease2.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objTaskRelease2);
            this.Text = "VICEM HOANG MAI - QUẢN LÝ XUẤT HÀNG";
            objTaskRelease2.Show();
        }
        #endregion

        #region method mnuTaskVehiceInfo_Click
        private void mnuTaskVehiceInfo_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmTaskVehiceInfo objTaskVehiceInfo = new frmTaskVehiceInfo();
            objTaskVehiceInfo.TopLevel = false;
            objTaskVehiceInfo.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objTaskVehiceInfo);
            this.Text = "VICEM HOANG MAI - BẢNG THÔNG TIN XE LẤY HÀNG";
            objTaskVehiceInfo.Show();
        }
        #endregion

        #region method mnuTrough_Click
        private void mnuTrough_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmTrough objTrough = new frmTrough();
            objTrough.TopLevel = false;
            objTrough.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objTrough);
            this.Text = "VICEM HOANG MAI - DANH MỤC MÁNG";
            objTrough.Show();
        }
        #endregion

        #region method mnuRFID_Click
        private void mnuRFID_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmRFID objRFID = new frmRFID();
            objRFID.TopLevel = false;
            objRFID.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objRFID);
            this.Text = "VICEM HOANG MAI - DANH MỤC THẺ RFID";
            objRFID.Show();
        }
        #endregion

        #region method mnuDevice_Click
        private void mnuDevice_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmDevice objDevice = new frmDevice();
            objDevice.TopLevel = false;
            objDevice.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objDevice);
            this.Text = "VICEM HOANG MAI - DANH MỤC THIẾT BỊ";
            objDevice.Show();

            //this.clearControl(this.MainSplitContainer1, "objF1");
            //frmControlRFID1 objDevice = new frmControlRFID1();
            //objDevice.TopLevel = false;
            //objDevice.Dock = DockStyle.Fill;
            //this.MainSplitContainer1.Panel1.Controls.Add(objDevice);
            //objDevice.Show();
        }
        #endregion

        #region method mnuVehicle_Click
        private void mnuVehicle_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmVehicleCategory objVehicle = new frmVehicleCategory();
            objVehicle.TopLevel = false;
            objVehicle.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objVehicle);
            this.Text = "VICEM HOANG MAI - DANH MỤC PHƯƠNG TIỆN";
            objVehicle.Show();
        }
        #endregion

        #region methohd mnuVehicleAll_Click
        private void mnuVehicleAll_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmVehicleAll objfrmVehicleAll = new frmVehicleAll();
            objfrmVehicleAll.TopLevel = false;
            objfrmVehicleAll.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objfrmVehicleAll);
            this.Text = "VICEM HOANG MAI - DANH MỤC PHƯƠNG TIỆN - ALL";
            objfrmVehicleAll.Show();
        }
        #endregion

        #region method frmMain_FormClosing
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bạn có muốn đóng ứng dụng không?","Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                {
                    e.Cancel = true;
                    Application.Exit();
                }
                
            }
            catch
            {

            }
        }
        #endregion

        #region method mnuUserGuide_Click
        private void mnuUserGuide_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region method mnuIntro_Click
        private void mnuIntro_Click(object sender, EventArgs e)
        {
            frmIntro objIntro = new frmIntro();
            objIntro.ShowDialog();
        }
        #endregion

        #region method mnuDriver_Click
        private void mnuDriver_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            FrmDriver objDriver = new FrmDriver();
            objDriver.TopLevel = false;
            objDriver.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objDriver);
            this.Text = "VICEM HOANG MAI - DANH MỤC LÁI XE";
            objDriver.Show();
        }
        #endregion

        #region method checkControl
        private void clearControl(System.Windows.Forms.SplitContainer ctl, string objName)
        {
            for (int i = 0; i < ctl.Panel1.Controls.Count; i++)
            {
                if (ctl.Panel1.Controls[i].Name.ToString().ToUpper() != objName.ToUpper())
                {
                    ctl.Panel1.Controls.RemoveAt(i);
                    return;
                }
            }
        }
        #endregion

        #region method MainSplitContainer1_Panel1_ControlRemoved
        private void MainSplitContainer1_Panel1_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (this.MainSplitContainer1.Panel1.Controls.Count == 1)
            {
                this.Text = "VICEM HOANG MAI - XUẤT HÀNG TỰ ĐỘNG";
            }
        }
        #endregion

        #region method mnuReportConfirm_Click
        private void mnuReportConfirm_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmReportConfirm objfrmReportConfirm = new frmReportConfirm();
            objfrmReportConfirm.TopLevel = false;
            objfrmReportConfirm.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objfrmReportConfirm);
            this.Text = "VICEM HOANG MAI - BÁO CÁO XÁC THỰC";
            objfrmReportConfirm.Show();
        }
        #endregion

        #region method mnuReportInOut_Click
        private void mnuReportInOut_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmReportInOut objfrmReportInOut = new frmReportInOut();
            objfrmReportInOut.TopLevel = false;
            objfrmReportInOut.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objfrmReportInOut);
            this.Text = "VICEM HOANG MAI - BÁO CÁO VÀO, RA";
            objfrmReportInOut.Show();
        }
        #endregion

        #region method mnuReportScale_Click
        private void mnuReportScale_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmReportScale objfrmReportScale = new frmReportScale();
            objfrmReportScale.TopLevel = false;
            objfrmReportScale.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objfrmReportScale);
            this.Text = "VICEM HOANG MAI - BÁO CÁO CÂN VÀO, RA";
            objfrmReportScale.Show();
        }
        #endregion

        #region method mnuReportRelease_Click
        private void mnuReportRelease_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmReportRelease objfrmReportRelease = new frmReportRelease();
            objfrmReportRelease.TopLevel = false;
            objfrmReportRelease.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objfrmReportRelease);
            this.Text = "VICEM HOANG MAI - BÁO CÁO XUẤT HÀNG";
            objfrmReportRelease.Show();
        }
        #endregion

        #region method mnuReportBarie_Click
        private void mnuReportBarie_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmReportBarie objfrmReportBarie = new frmReportBarie();
            objfrmReportBarie.TopLevel = false;
            objfrmReportBarie.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objfrmReportBarie);
            this.Text = "VICEM HOANG MAI - BÁO CÁO ĐÓNG, MỞ BARIE";
            objfrmReportBarie.Show();
        }
        #endregion

        #region method mnuReportDriver_Click
        private void mnuReportDriver_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmReportDriver objfrmReportDriver = new frmReportDriver();
            objfrmReportDriver.TopLevel = false;
            objfrmReportDriver.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objfrmReportDriver);
            this.Text = "VICEM HOANG MAI - BÁO CÁO ĐÓNG SẢN LƯỢNG LÁI XE";
            objfrmReportDriver.Show();
        }
        #endregion

        #region method mnuDriverAccount_Click
        private void mnuDriverAccount_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmDriverAccount objfrmDriverAccount = new frmDriverAccount();
            objfrmDriverAccount.TopLevel = false;
            objfrmDriverAccount.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objfrmDriverAccount);
            this.Text = "VICEM HOANG MAI - DANH MỤC TÀI KHOẢN LÁI XE";
            objfrmDriverAccount.Show();
        }

        private void mnuDriverVoucher_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmDriverVoucher objfrmDriverVoucher = new frmDriverVoucher();
            objfrmDriverVoucher.TopLevel = false;
            objfrmDriverVoucher.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objfrmDriverVoucher);
            this.Text = "VICEM HOANG MAI - QUẢN LÝ PHIẾU ĂN";
            objfrmDriverVoucher.Show();
        }

        private void mnuExportPlan_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmExportPlan objExportPlan = new frmExportPlan();
            objExportPlan.TopLevel = false;
            objExportPlan.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objExportPlan);
            this.Text = "VICEM HOANG MAI - KẾ HOẠCH XUẤT HÀNG";
            objExportPlan.Show();
        }

        private void mnuCheckUpdate_Click(object sender, EventArgs e)
        {
            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                MessageBox.Show("Kiểm tra");
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = ad.CheckForDetailedUpdate();

                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBox.Show("Phiên bản mới không thể tải. \n\nKiểm tra mạng và thử lại sau. Thông báo: " + dde.Message);
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBox.Show("Không thể kiểm tra phiên bản mới. Kiểm tra lại phiên bản. Thông báo: " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show("Không có bản cập nhật nào mới. Thông báo: " + ioe.Message);
                    return;
                }

                if (info.UpdateAvailable)
                {
                    Boolean doUpdate = true;

                    if (!info.IsUpdateRequired)
                    {
                        DialogResult dr = MessageBox.Show("Có bản cập nhật mới.Bạn có muốn tải không?", "Cập nhật", MessageBoxButtons.OKCancel);
                        if (!(DialogResult.OK == dr))
                        {
                            doUpdate = false;
                        }
                    }
                    else
                    {
                        // Display a message that the app MUST reboot. Display the minimum required version.
                        MessageBox.Show("This application has detected a mandatory update from your current " +
                            "version to version " + info.MinimumRequiredVersion.ToString() +
                            ". The application will now install the update and restart.",
                            "Update Available", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                    if (doUpdate)
                    {
                        try
                        {
                            ad.Update();
                            MessageBox.Show("Ứng dụng đã được cập nhật, và sẽ được khởi động lại.");
                            Application.Restart();
                        }
                        catch (DeploymentDownloadException dde)
                        {
                            MessageBox.Show("Không thể cài đặt bản cập nhật của phần mềm. \n\nThử kiểm tra lại mạng và thử lại sau. Thông báo: " + dde);
                            return;
                        }
                    }
                }
            }
        }
        #endregion

        #region method mnuTaskDbet_Click
        private void mnuTaskDbet_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmTaskDbet objfrmTaskDbet = new frmTaskDbet();
            objfrmTaskDbet.TopLevel = false;
            objfrmTaskDbet.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objfrmTaskDbet);
            this.Text = "VICEM HOANG MAI - QUẢN LÝ CÔNG NỢ";
            objfrmTaskDbet.Show();
        }
        #endregion

        #region method mnuVehicleAll1_Click
        private void mnuVehicleAll1_Click(object sender, EventArgs e)
        {
            this.clearControl(this.MainSplitContainer1, "objF1");
            frmVehicleAllV1 objfrmVehicleAllV1 = new frmVehicleAllV1();
            objfrmVehicleAllV1.TopLevel = false;
            objfrmVehicleAllV1.Dock = DockStyle.Fill;
            this.MainSplitContainer1.Panel1.Controls.Add(objfrmVehicleAllV1);
            this.Text = "VICEM HOANG MAI - DANH MỤC PHƯƠNG TIỆN - ALL";
            objfrmVehicleAllV1.Show();
        }
        #endregion
    }
}