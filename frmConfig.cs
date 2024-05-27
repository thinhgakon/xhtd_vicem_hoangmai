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
    public partial class frmConfig : Form
    {

        #region Declare objects
        private Fun objFun = new Fun();
        #endregion

        #region method frmConfig
        public frmConfig()
        {
            InitializeComponent();
        }
        #endregion

        #region method frmConfig_Load
        private void frmConfig_Load(object sender, EventArgs e)
        {
            this.numericMaxVehicleClinker.Value = this.objFun.getConfigOperating("MaxVehicleClinker");
            this.numericMaxVehicleXK.Value = this.objFun.getConfigOperating("MaxVehicleExport");
            this.numericMaxReIndex.Value = this.objFun.getConfigOperating("MaxReIndex");

            this.numericMaxVehicleInTrough_PCB30.Value = this.objFun.getConfigOperating("MaxVehicleInTrough_PCB30");
            this.numericMaxVehicleInTrough_PCB40.Value = this.objFun.getConfigOperating("MaxVehicleInTrough_PCB40");
            this.numericMaxVehicleInTrough_XIROI.Value = this.objFun.getConfigOperating("MaxVehicleInTrough_ROI");



            this.numericUpDownCallVoice.Value = this.objFun.getConfigOperating("IsCall");
            this.numericUpDownCallVoiceClinker.Value = this.objFun.getConfigOperating("PushToDbCallClinkerJob");
            this.numericUpDownCallVoiceXK.Value = this.objFun.getConfigOperating("PushToDbCallOrderExportJob");
            this.numericUpDownCallVoicePCB30.Value = this.objFun.getConfigOperating("PushToDbCallOrderPCB30");
            this.numericUpDownCallVoicePCB40.Value = this.objFun.getConfigOperating("PushToDbCallOrderPCB40");
            this.numericUpDownCallVoiceXiRoi.Value = this.objFun.getConfigOperating("PushToDbCallOrderROI");

            this.numericUpDownIsAutoScale.Value = this.objFun.getConfigOperating("IsAutoScale");
            this.numericUpDownMaxItemCheckBalance.Value = this.objFun.getConfigOperating("MaxItemCheckBalance");

            this.numericUpDown1.Value = this.objFun.getConfigOperating("CallVehicleJob");
            this.numericUpDown2.Value = this.objFun.getConfigOperating("ScaleModuleJob");
            this.numericUpDown3.Value = this.objFun.getConfigOperating("ConfirmationPointModule_Job");
            this.numericUpDown4.Value = this.objFun.getConfigOperating("GatewayModuleJob");
            this.numericUpDown5.Value = this.objFun.getConfigOperating("PushDeliveryCodeToTroughJob");

            this.numericUpDownBarrierCN.Value = this.objFun.getConfigOperating("ActiveBarrierCN");
            this.numericUpDownBarrierCC.Value = this.objFun.getConfigOperating("ActiveBarrierCC");
            this.numericUpDownBarrierC3.Value = this.objFun.getConfigOperating("ActiveBarrierGateway");
            this.numericUpDownIsCheckMaxVehicleBetweenGetwayAndScale.Value = this.objFun.getConfigOperating("IsCheckMaxVehicleBetweenGetwayAndScale");
            this.numericUpDownMaxVehicleBetweenGetwayAndScale.Value = this.objFun.getConfigOperating("MaxVehicleBetweenGetwayAndScale");
            this.cbPrint.SelectedIndex  = this.objFun.getConfigOperating("PrintCouple");
            this.numericUpDownSleepWakeUpScaleLight.Value = this.objFun.getConfigOperating("SleepWakeUpScaleLight");
            this.txtLocationXiGiong.Text = this.objFun.getStringConfigOperating("LocationXiGiong");
            this.numericUpDownIsPriorityPCB30.Value = this.objFun.getConfigOperating("IsPriorityPCB30");

            this.txtTimeFinishAllow.Text = this.objFun.getAppConfig().ToString();
        }
        #endregion

        #region method frmConfig_KeyDown
        private void frmConfig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion


        #region method btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region method btnSaveMaxReIndex_Click
        private void btnSaveMaxReIndex_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("MaxReIndex", (int)this.numericMaxReIndex.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        private void btnSaveCallVoice_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("IsCall", (int)this.numericUpDownCallVoice.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveCallVoiceClinker_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("PushToDbCallClinkerJob", (int)this.numericUpDownCallVoiceClinker.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveAutoScale_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("IsAutoScale", (int)this.numericUpDownIsAutoScale.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveMaxItemCheckBalance_Click(object sender, EventArgs e)
        {
            if((int)this.numericUpDownMaxItemCheckBalance.Value < 10 || (int)this.numericUpDownMaxItemCheckBalance.Value > 100){
                MessageBox.Show("Giá trị ngoài khoảng cho phép", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.objFun.setConfigOperating("MaxItemCheckBalance", (int)this.numericUpDownMaxItemCheckBalance.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("CallVehicleJob", (int)this.numericUpDown1.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("ScaleModuleJob", (int)this.numericUpDown2.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("ConfirmationPointModule_Job", (int)this.numericUpDown3.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("GatewayModuleJob", (int)this.numericUpDown4.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("PushDeliveryCodeToTroughJob", (int)this.numericUpDown5.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveMaxVehicleClinker_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("MaxVehicleClinker", (int)this.numericMaxVehicleClinker.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveMaxVehicleXK_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("MaxVehicleExport", (int)this.numericMaxVehicleXK.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveBarrierCN_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("ActiveBarrierCN", (int)this.numericUpDownBarrierCN.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveBarrierCC_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("ActiveBarrierCC", (int)this.numericUpDownBarrierCC.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveBarrierC3_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("ActiveBarrierGateway", (int)this.numericUpDownBarrierC3.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveIsCheckMaxVehicleBetweenGetwayAndScale_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("IsCheckMaxVehicleBetweenGetwayAndScale", (int)this.numericUpDownIsCheckMaxVehicleBetweenGetwayAndScale.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveMaxVehicleBetweenGetwayAndScale_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("MaxVehicleBetweenGetwayAndScale", (int)this.numericUpDownMaxVehicleBetweenGetwayAndScale.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveCallVoiceXK_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("PushToDbCallOrderExportJob", (int)this.numericUpDownCallVoiceXK.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveCallVoicePCB30_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("PushToDbCallOrderPCB30", (int)this.numericUpDownCallVoicePCB30.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveCallVoicePCB40_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("PushToDbCallOrderPCB40", (int)this.numericUpDownCallVoicePCB40.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveCallVoiceXiRoi_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("PushToDbCallOrderROI", (int)this.numericUpDownCallVoiceXiRoi.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSavePrintDevice_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("PrintCouple", (int)this.cbPrint.SelectedIndex) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveSleepWakeUpScaleLight_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("SleepWakeUpScaleLight", (int)this.numericUpDownSleepWakeUpScaleLight.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTimeFinishAllow_Click(object sender, EventArgs e)
        {
            if (this.txtTimeFinishAllow.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập thời gian tối thiểu cho phép kết thúc đơn!","",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtTimeFinishAllow.Focus();
                return;
            }
            int TimeFinishAllow = 30;
            try
            {
                TimeFinishAllow = int.Parse(this.txtTimeFinishAllow.Text.Trim());
            }
            catch
            {
                TimeFinishAllow = 0;
            }
            finally
            {

            }
            if (TimeFinishAllow < 30)
            {
                MessageBox.Show("Thời gian kết thúc đơn tối thiểu phải 30 phút!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtTimeFinishAllow.Focus();
                return;
            }
            if (this.objFun.setAppConfig(TimeFinishAllow) > 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveLocation_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperatingString("LocationXiGiong",  this.txtLocationXiGiong.Text.ToUpper()) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveIsPriorityPCB30_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("IsPriorityPCB30", (int)this.numericUpDownIsPriorityPCB30.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveMaxVehicleInTrough_PCB30_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("MaxVehicleInTrough_PCB30", (int)this.numericMaxVehicleInTrough_PCB30.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveMaxVehicleInTrough_PCB40_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("MaxVehicleInTrough_PCB40", (int)this.numericMaxVehicleInTrough_PCB40.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveMaxVehicleInTrough_XIROI_Click(object sender, EventArgs e)
        {
            if (this.objFun.setConfigOperating("MaxVehicleInTrough_ROI", (int)this.numericMaxVehicleInTrough_XIROI.Value) >= 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi: Cập nhật thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
