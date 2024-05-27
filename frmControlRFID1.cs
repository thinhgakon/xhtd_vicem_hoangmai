using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMXHTD
{
    public partial class frmControlRFID1 : Form
    {
        #region Declare objects
        private IntPtr h = IntPtr.Zero;
        private Thread threadArea;
        delegate void SetTextCallback(string text);
        public bool Area = true;
        int TimeSleep = 1000;
        #endregion

        #region method frmControlRFID1
        public frmControlRFID1()
        {
            InitializeComponent();
        }
        #endregion

        #region Declare Connect
        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "Connect")]
        public static extern IntPtr Connect(string Parameters);
        [DllImport("plcommpro.dll", EntryPoint = "PullLastError")]
        public static extern int PullLastError();
        #endregion

        #region Declare GetRTLog
        [DllImport("plcommpro.dll", EntryPoint = "GetRTLog")]
        public static extern int GetRTLog(IntPtr h, ref byte buffer, int buffersize);
        #endregion

        #region Declare Disconnect
        [DllImport("plcommpro.dll", EntryPoint = "Disconnect")]
        public static extern void Disconnect(IntPtr h);
        #endregion

        #region method SetTextArea
        private void SetTextArea(string text)
        {
            if (this.lblRTEInfo.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTextArea);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.lblRTEInfo.Text = text;
            }
        }
        #endregion

        #region method btnStart_Click
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                this.Area = true;
                this.btnStart.Enabled = false;
                this.btnStop.Enabled = true;
                threadArea = new Thread(t =>
                {
                    while (this.Area)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            int ret = 0, i = 0, buffersize = 256;
                            string str = "";
                            string[] tmp = null;
                            byte[] buffer = new byte[256];
                            i = this.lsvrtlog.Items.Count;          //The current list of numbers assigned to i

                            if (IntPtr.Zero != h)
                            {

                                ret = GetRTLog(h, ref buffer[0], buffersize);
                                if (ret >= 0)
                                {
                                    str = Encoding.Default.GetString(buffer);
                                    tmp = str.Split(',');
                                    if (tmp[2] == "0" || tmp[2] == "")
                                    {
                                        return;
                                    }
                                    this.lsvrtlog.Items.Add(tmp[0]);
                                    this.lsvrtlog.Items[i].SubItems.Add(tmp[1]);
                                    this.lsvrtlog.Items[i].SubItems.Add(tmp[2]);
                                    this.lsvrtlog.Items[i].SubItems.Add(tmp[3]);
                                    this.lsvrtlog.Items[i].SubItems.Add(tmp[4]);
                                    this.lsvrtlog.Items[i].SubItems.Add(tmp[5]);
                                    this.lsvrtlog.Items[i].SubItems.Add(tmp[6]);
                                }
                                i++;
                            }
                            else
                            {
                                MessageBox.Show("Connect device failed!");
                                return;
                            }

                        });

                        this.SetTextArea(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        Thread.Sleep(this.TimeSleep);
                    }
                }) { IsBackground = true };
                threadArea.Start();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        #endregion

        #region method btnStop_Click
        private void btnStop_Click(object sender, EventArgs e)
        {
            this.threadArea.Abort();
            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;
        }
        #endregion

        #region method btnSetting_Click
        private void btnSetting_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region method btnConnect_Click
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "protocol=TCP,ipaddress=" + this.txtIpAddress.Text.Trim() + ",port=" + this.txtPortNumber.Text.Trim() + ",timeout=2000,passwd=";
                int ret = 0; // Error ID number
                Cursor = Cursors.WaitCursor;

                if (this.btnConnect.Text == "Kết nối")
                {
                    if (IntPtr.Zero == h)
                    {
                        h = Connect(str);
                        Cursor = Cursors.Default;
                        if (h != IntPtr.Zero)
                        {
                            this.lblDeviceState.Text = "Đã kết nối";
                            this.btnConnect.Text = "Đóng kết nối";
                            this.btnStart.Enabled = true;
                            this.txtName.ReadOnly = true;
                            this.txtIpAddress.ReadOnly = true;
                            this.txtPortNumber.ReadOnly = true;
                        }
                        else
                        {
                            ret = PullLastError();
                            Cursor = Cursors.Default;
                            this.txtName.ReadOnly = false;
                            this.txtIpAddress.ReadOnly = false;
                            this.txtPortNumber.ReadOnly = false;
                            MessageBox.Show("Connect device Failed! The error id is: " + ret);

                        }
                    }
                }
                else
                {
                    if (IntPtr.Zero != h)
                    {
                        this.btnStop.PerformClick();
                        Disconnect(h);
                        h = IntPtr.Zero;
                        Cursor = Cursors.Default;
                        this.lblDeviceState.Text = "Chưa kết nối";
                        this.btnConnect.Text = "Kết nối";
                        this.btnStart.Enabled = false;
                        this.btnStop.Enabled = false;
                        this.btncls.Enabled = false;

                        this.txtName.ReadOnly = false;
                        this.txtIpAddress.ReadOnly = false;
                        this.txtPortNumber.ReadOnly = false;
                    }
                    return;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        #endregion

        #region method btncls_Click
        private void btncls_Click(object sender, EventArgs e)
        {
            this.lsvrtlog.Items.Clear();
        }
        #endregion

        #region method btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.threadArea.IsAlive)
                {
                    this.threadArea.Abort();
                }
            }
            catch
            {

            }
            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;
            this.Close();
        }
        #endregion
    }
}
