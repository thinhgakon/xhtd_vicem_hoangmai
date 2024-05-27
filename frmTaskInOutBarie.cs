using HMXHTD.Core;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMXHTD
{
    public partial class frmTaskInOutBarie : Form
    {
        #region declare objects
        private IntPtr h = IntPtr.Zero;
        private Device objDevice = new Device();
        #endregion

        #region DeclareTool
        [DllImport("plcommpro.dll", EntryPoint = "ControlDevice")]
        public static extern int ControlDevice(IntPtr h, int operationid, int param1, int param2, int param3, int param4, string options);

        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "Connect")]
        public static extern IntPtr Connect(string Parameters);
        #endregion

        #region method frmTaskInOutBarie
        public frmTaskInOutBarie()
        {
            InitializeComponent();
        }
        #endregion

        #region method frmTaskInOutBarie_KeyDown
        private void frmTaskInOutBarie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region method frmTaskInOutBarie_Shown
        private void frmTaskInOutBarie_Shown(object sender, EventArgs e)
        {
            this.cbbSubject.SelectedIndex = 0;
        }
        #endregion

        #region method cbbSubject_SelectedIndexChanged
        private void cbbSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbbSubject.Text != "Chọn lý do mở Barie")
            {
                this.txtNote.Text = this.cbbSubject.Text;
            }
            else
            {
                this.txtNote.Text = "";
            }
        }
        #endregion

        #region method btnBarieTop_Click
        private void btnBarieTop_Click(object sender, EventArgs e)
        {
            if (this.txtNote.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa xác định lý do mở barie!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            #region Mở barier cân nổi
            if (MessageBox.Show("Bạn có chắc chắn muốn mở barie không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                h = Connect("protocol=TCP,ipaddress=192.168.22.16,port=4370,timeout=2000,passwd=");
                int ret = 0;
                int operID = 1;
                int doorOrAuxoutID = 4;
                int outputAddrType = 1;
                int doorAction = 1;

                if (operID == 1)
                {
                    outputAddrType = 1;
                    if (outputAddrType == 1)
                    {
                        doorOrAuxoutID = 1;
                        doorAction = 1;
                    }
                }
                if (IntPtr.Zero != h)
                {
                    ret = ControlDevice(h, operID, doorOrAuxoutID, outputAddrType, doorAction, 0, "");     //call ControlDevice funtion from PullSDK
                    //ControlDevice(h,1,1,1,1,0,"");
                }
                else
                {
                    MessageBox.Show("Lỗi: Mở barie thất bại, vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (ret >= 0)
                {
                    MessageBox.Show("Mở barie thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.objDevice.setBarieLog("Barie cổng bảo vệ số 3", this.txtNote.Text, frmMain.UserName);
                    return;
                }

                this.Close();
            }
            #endregion
        }
        #endregion

        #region method btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
