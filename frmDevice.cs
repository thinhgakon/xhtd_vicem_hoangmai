using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace HMXHTD
{
    public partial class frmDevice : Form
    {
        private HMXHTD.Core.Device objDevice = new Core.Device();
        private IntPtr h = IntPtr.Zero;
        private Thread threadArea;
        delegate void SetTextCallback(string text);
        public bool Area = true;
        int TimeSleep = 2000;
        private Fun objFun = new Fun();

        [DllImport("C:\\WINDOWS\\system32\\plcommpro.dll", EntryPoint = "Connect")]
        public static extern IntPtr Connect(string Parameters);
        [DllImport("plcommpro.dll", EntryPoint = "PullLastError")]
        public static extern int PullLastError();

        [DllImport("plcommpro.dll", EntryPoint = "GetRTLog")]
        public static extern int GetRTLog(IntPtr h, ref byte buffer, int buffersize);

        [DllImport("plcommpro.dll", EntryPoint = "Disconnect")]
        public static extern void Disconnect(IntPtr h);

        public frmDevice()
        {
            InitializeComponent();
        }

        private void frmDevice_Load(object sender, EventArgs e)
        {
            var deviceinfos = this.objDevice.getAllDeviceInfo();
            
            var RFID11 = deviceinfos.FirstOrDefault(x => x.Code.Equals("RFID11"));
            if(RFID11 != null)
            {
                if(RFID11.State.ToString() == "False")
                {
                    this.txtName_RFID_C1.BackColor = Color.Red;
                }
                this.txtName_RFID_C1.Text = RFID11.Name;
                this.txtIpAddress_RFID_C1.Text = RFID11.IpAddress;
                this.txtPortNumber_RFID_C1.Text = RFID11.PortNumber?.ToString();
                this.txtHistory_RFID_C1.Text = RFID11.LogHistory;
            }

           

            var LIGHT11 = deviceinfos.FirstOrDefault(x => x.Code.Equals("LIGHT11"));
            if (LIGHT11 != null)
            {
                if (LIGHT11.State.ToString() == "False")
                {
                    this.txtName_LIGHT_C1.BackColor = Color.Red;
                }
                this.txtName_LIGHT_C1.Text = LIGHT11.Name;
                this.txtIpAddress_LIGHT_C1.Text = LIGHT11.IpAddress;
                this.txtPortNumber_LIGHT_C1.Text = LIGHT11.PortNumber?.ToString();
                this.txtHistory_LIGHT_C1.Text = LIGHT11.LogHistory;
            }


            var RFID31 = deviceinfos.FirstOrDefault(x => x.Code.Equals("RFID31"));
            if (RFID31 != null)
            {
                if (RFID31.State.ToString() == "False")
                {
                    this.txtName_RFID_C3.BackColor = Color.Red;
                }
                this.txtName_RFID_C3.Text = RFID31.Name;
                this.txtIpAddress_RFID_C3.Text = RFID31.IpAddress;
                this.txtPortNumber_RFID_C3.Text = RFID31.PortNumber?.ToString();
                this.txtHistory_RFID_C3.Text = RFID31.LogHistory;
            }

            var BARRIER31 = deviceinfos.FirstOrDefault(x => x.Code.Equals("BARRIER31"));
            if (BARRIER31 != null)
            {
                if (BARRIER31.State.ToString() == "False")
                {
                    this.txtName_BARRIER_C3.BackColor = Color.Red;
                }
                this.txtName_BARRIER_C3.Text = BARRIER31.Name;
                this.txtIpAddress_BARRIER_C3.Text = BARRIER31.IpAddress;
                this.txtPortNumber_BARRIER_C3.Text = BARRIER31.PortNumber?.ToString();
                this.txtHistory_BARRIER_C3.Text = BARRIER31.LogHistory;
            }

            var LEDTO31 = deviceinfos.FirstOrDefault(x => x.Code.Equals("LEDTO31"));
            if (LEDTO31 != null)
            {
                if (LEDTO31.State.ToString() == "False")
                {
                    this.txtName_LED31_C3.BackColor = Color.Red;
                }
                this.txtName_LED31_C3.Text = LEDTO31.Name;
                this.txtIpAddress_LED31_C3.Text = LEDTO31.IpAddress;
                this.txtPortNumber_LED31_C3.Text = LEDTO31.PortNumber?.ToString();
                this.txtHistory_LED31_C3.Text = LEDTO31.LogHistory;
            }

         

            var RFID41 = deviceinfos.FirstOrDefault(x => x.Code.Equals("RFID41"));
            if (RFID41 != null)
            {
                if (RFID41.State.ToString() == "False")
                {
                    this.txtName_RFID_SCALE.BackColor = Color.Red;
                }
                this.txtName_RFID_SCALE.Text = RFID41.Name;
                this.txtIpAddress_RFID_SCALE.Text = RFID41.IpAddress;
                this.txtPortNumber_RFID_SCALE.Text = RFID41.PortNumber?.ToString();
                this.txtHistory_RFID_SCALE.Text = RFID41.LogHistory;
            }

            var BARRIER41 = deviceinfos.FirstOrDefault(x => x.Code.Equals("BARRIER41"));
            if (BARRIER41 != null)
            {
                if (BARRIER41.State.ToString() == "False")
                {
                    this.txtName_BARRIER_SCALE.BackColor = Color.Red;
                }
                this.txtName_BARRIER_SCALE.Text = BARRIER41.Name;
                this.txtIpAddress_BARRIER_SCALE.Text = BARRIER41.IpAddress;
                this.txtPortNumber_BARRIER_SCALE.Text = BARRIER41.PortNumber?.ToString();
                this.txtHistory_BARRIER_SCALE.Text = BARRIER41.LogHistory;
            }

            var SENSOR = deviceinfos.FirstOrDefault(x => x.Code.Equals("SENSOR"));
            if (SENSOR != null)
            {
                if (SENSOR.State.ToString() == "False")
                {
                    this.txtName_SENSOR_SCALE.BackColor = Color.Red;
                }
                this.txtName_SENSOR_SCALE.Text = SENSOR.Name;
                this.txtIpAddress_SENSOR_SCALE.Text = SENSOR.IpAddress;
                this.txtPortNumber_SENSOR_SCALE.Text = SENSOR.PortNumber?.ToString();
                this.txtHistory_SENSOR_SCALE.Text = SENSOR.LogHistory;
            }

            var LEDM1 = deviceinfos.FirstOrDefault(x => x.Code.Equals("LEDM1"));
            if (LEDM1 != null)
            {
                if (LEDM1.State.ToString() == "False")
                {
                    this.txtName_LEDM1.BackColor = Color.Red;
                }
                this.txtName_LEDM1.Text = LEDM1.Name;
                this.txtIpAddress_LEDM1.Text = LEDM1.IpAddress;
                this.txtPortNumber_LEDM1.Text = LEDM1.PortNumber?.ToString();
                this.txtHistory_LEDM1.Text = LEDM1.LogHistory;
            }

            var LEDM2 = deviceinfos.FirstOrDefault(x => x.Code.Equals("LEDM2"));
            if (LEDM2 != null)
            {
                if (LEDM2.State.ToString() == "False")
                {
                    this.txtName_LEDM2.BackColor = Color.Red;
                }
                this.txtName_LEDM2.Text = LEDM2.Name;
                this.txtIpAddress_LEDM2.Text = LEDM2.IpAddress;
                this.txtPortNumber_LEDM2.Text = LEDM2.PortNumber?.ToString();
                this.txtHistory_LEDM2.Text = LEDM2.LogHistory;
            }

            var LEDM3 = deviceinfos.FirstOrDefault(x => x.Code.Equals("LEDM3"));
            if (LEDM3 != null)
            {
                if (LEDM3.State.ToString() == "False")
                {
                    this.txtName_LEDM3.BackColor = Color.Red;
                }
                this.txtName_LEDM3.Text = LEDM3.Name;
                this.txtIpAddress_LEDM3.Text = LEDM3.IpAddress;
                this.txtPortNumber_LEDM3.Text = LEDM3.PortNumber?.ToString();
                this.txtHistory_LEDM3.Text = LEDM3.LogHistory;
            }

            var LEDM4 = deviceinfos.FirstOrDefault(x => x.Code.Equals("LEDM4"));
            if (LEDM4 != null)
            {
                if (LEDM4.State.ToString() == "False")
                {
                    this.txtName_LEDM4.BackColor = Color.Red;
                }
                this.txtName_LEDM4.Text = LEDM4.Name;
                this.txtIpAddress_LEDM4.Text = LEDM4.IpAddress;
                this.txtPortNumber_LEDM4.Text = LEDM4.PortNumber?.ToString();
                this.txtHistory_LEDM4.Text = LEDM4.LogHistory;
            }

            var LEDM5 = deviceinfos.FirstOrDefault(x => x.Code.Equals("LEDM5"));
            if (LEDM5 != null)
            {
                if (LEDM5.State.ToString() == "False")
                {
                    this.txtName_LEDM5.BackColor = Color.Red;
                }
                this.txtName_LEDM5.Text = LEDM5.Name;
                this.txtIpAddress_LEDM5.Text = LEDM5.IpAddress;
                this.txtPortNumber_LEDM5.Text = LEDM5.PortNumber?.ToString();
                this.txtHistory_LEDM5.Text = LEDM5.LogHistory;
            }

            var LEDM6 = deviceinfos.FirstOrDefault(x => x.Code.Equals("LEDM6"));
            if (LEDM6 != null)
            {
                if (LEDM6.State.ToString() == "False")
                {
                    this.txtName_LEDM6.BackColor = Color.Red;
                }
                this.txtName_LEDM6.Text = LEDM6.Name;
                this.txtIpAddress_LEDM6.Text = LEDM6.IpAddress;
                this.txtPortNumber_LEDM6.Text = LEDM6.PortNumber?.ToString();
                this.txtHistory_LEDM6.Text = LEDM6.LogHistory;
            }


        }
      

        private void frmDevice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }



        private void btnEdit_RFID_C1_Click(object sender, EventArgs e)
        {
            EditAble(true, "_RFID_C1");
        }

        private void btnSave_RFID_C1_Click(object sender, EventArgs e)
        {
            if (this.objDevice.UpdateDeviceInfo(this.txtCode_RFID_C1.Text, this.txtName_RFID_C1.Text, this.txtIpAddress_RFID_C1.Text, Int32.Parse(this.txtPortNumber_RFID_C1?.Text ?? "0"), this.txtHistory_RFID_C1.Text) == 1)
            {
                MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi xảy ra khi cập nhật thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            EditAble(false, "_RFID_C1");
        }
        
        private void EditAble(bool edit, string key)
        {
            if (edit)
            {
                (this.Controls.Find("txtName" + key, true).FirstOrDefault() as TextBox).ReadOnly = false;
                (this.Controls.Find("txtPortNumber" + key, true).FirstOrDefault() as TextBox).ReadOnly = false;
                (this.Controls.Find("txtIpAddress" + key, true).FirstOrDefault() as TextBox).ReadOnly = false;
                (this.Controls.Find("txtHistory" + key, true).FirstOrDefault() as TextBox).ReadOnly = false;
                (this.Controls.Find("btnEdit" + key, true).FirstOrDefault() as Button).Enabled = false;
                (this.Controls.Find("btnSave" + key, true).FirstOrDefault() as Button).Enabled = true;
                (this.Controls.Find("txtName" + key, true).FirstOrDefault() as TextBox).Focus();
             
            }
            else
            {
                (this.Controls.Find("txtName" + key, true).FirstOrDefault() as TextBox).ReadOnly = true;
                (this.Controls.Find("txtPortNumber" + key, true).FirstOrDefault() as TextBox).ReadOnly = true;
                (this.Controls.Find("txtIpAddress" + key, true).FirstOrDefault() as TextBox).ReadOnly = true;
                (this.Controls.Find("txtHistory" + key, true).FirstOrDefault() as TextBox).ReadOnly = true;
                (this.Controls.Find("btnEdit" + key, true).FirstOrDefault() as Button).Enabled = true;
                (this.Controls.Find("btnSave" + key, true).FirstOrDefault() as Button).Enabled = false;
            }
        }

        private void btnEdit_LIGHT_C1_Click(object sender, EventArgs e)
        {
            EditAble(true, "_LIGHT_C1");
        }

        private void btnSave_LIGHT_C1_Click(object sender, EventArgs e)
        {
            if (this.objDevice.UpdateDeviceInfo(this.txtCode_LIGHT_C1.Text, this.txtName_LIGHT_C1.Text, this.txtIpAddress_LIGHT_C1.Text, Int32.Parse(this.txtPortNumber_LIGHT_C1?.Text ?? "0"), this.txtHistory_LIGHT_C1.Text) == 1)
            {
                MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi xảy ra khi cập nhật thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            EditAble(false, "_LIGHT_C1");
        }

        private void btnEdit_LED_C1_Click(object sender, EventArgs e)
        {
            EditAble(true, "_LED_C1");
        }


        private void btnEdit_RFID_C3_Click(object sender, EventArgs e)
        {
            EditAble(true, "_RFID_C3");
        }

        private void btnSave_RFID_C3_Click(object sender, EventArgs e)
        {
            if (this.objDevice.UpdateDeviceInfo(this.txtCode_RFID_C3.Text, this.txtName_RFID_C3.Text, this.txtIpAddress_RFID_C3.Text, Int32.Parse(this.txtPortNumber_RFID_C3?.Text ?? "0"), this.txtHistory_RFID_C3.Text) == 1)
            {
                MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi xảy ra khi cập nhật thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            EditAble(false, "_RFID_C3");
        }

        private void btnEdit_BARRIER_C3_Click(object sender, EventArgs e)
        {
            EditAble(true, "_BARRIER_C3");
        }

        private void btnSave_BARRIER_C3_Click(object sender, EventArgs e)
        {
            if (this.objDevice.UpdateDeviceInfo(this.txtCode_BARRIER_C3.Text, this.txtName_BARRIER_C3.Text, this.txtIpAddress_BARRIER_C3.Text, Int32.Parse(this.txtPortNumber_BARRIER_C3?.Text ?? "0"), this.txtHistory_BARRIER_C3.Text) == 1)
            {
                MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi xảy ra khi cập nhật thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            EditAble(false, "_BARRIER_C3");
        }

        private void btnEdit_LED31_C3_Click(object sender, EventArgs e)
        {
            EditAble(true, "_LED31_C3");
        }

        private void btnSave_LED31_C3_Click(object sender, EventArgs e)
        {
            if (this.objDevice.UpdateDeviceInfo(this.txtCode_LED31_C3.Text, this.txtName_LED31_C3.Text, this.txtIpAddress_LED31_C3.Text, Int32.Parse(this.txtPortNumber_LED31_C3?.Text ?? "0"), this.txtHistory_LED31_C3.Text) == 1)
            {
                MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi xảy ra khi cập nhật thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            EditAble(false, "_LED31_C3");
        }

        private void btnEdit_LED32_C3_Click(object sender, EventArgs e)
        {
            EditAble(true, "_LED32_C3");
        }


        private void btnEdit_LED33_C3_Click(object sender, EventArgs e)
        {
            EditAble(true, "_LED33_C3");
        }


        private void btnEdit_RFID_SCALE_Click(object sender, EventArgs e)
        {
            EditAble(true, "_RFID_SCALE");
        }

        private void btnSave_RFID_SCALE_Click(object sender, EventArgs e)
        {
            if (this.objDevice.UpdateDeviceInfo(this.txtCode_RFID_SCALE.Text, this.txtName_RFID_SCALE.Text, this.txtIpAddress_RFID_SCALE.Text, Int32.Parse(this.txtPortNumber_RFID_SCALE?.Text ?? "0"), this.txtHistory_RFID_SCALE.Text) == 1)
            {
                MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi xảy ra khi cập nhật thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            EditAble(false, "_RFID_SCALE");
        }

        private void btnEdit_BARRIER_SCALE_Click(object sender, EventArgs e)
        {
            EditAble(true, "_BARRIER_SCALE");
        }

        private void btnSave_BARRIER_SCALE_Click(object sender, EventArgs e)
        {
            if (this.objDevice.UpdateDeviceInfo(this.txtCode_BARRIER_SCALE.Text, this.txtName_BARRIER_SCALE.Text, this.txtIpAddress_BARRIER_SCALE.Text, Int32.Parse(this.txtPortNumber_BARRIER_SCALE?.Text ?? "0"), this.txtHistory_BARRIER_SCALE.Text) == 1)
            {
                MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi xảy ra khi cập nhật thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            EditAble(false, "_BARRIER_SCALE");
        }

        private void btnEdit_SENSOR_SCALE_Click(object sender, EventArgs e)
        {
            EditAble(true, "_SENSOR_SCALE");
        }

        private void btnSave_SENSOR_SCALE_Click(object sender, EventArgs e)
        {
            if (this.objDevice.UpdateDeviceInfo(this.txtCode_SENSOR_SCALE.Text, this.txtName_SENSOR_SCALE.Text, this.txtIpAddress_SENSOR_SCALE.Text, Int32.Parse(this.txtPortNumber_SENSOR_SCALE?.Text ?? "0"), this.txtHistory_SENSOR_SCALE.Text) == 1)
            {
                MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi xảy ra khi cập nhật thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            EditAble(false, "_SENSOR_SCALE");
        }

        private void btnEdit_LEDM1_Click(object sender, EventArgs e)
        {
            EditAble(true, "_LEDM1");
        }
        private void btnSave_LEDM1_Click(object sender, EventArgs e)
        {
            if (this.objDevice.UpdateDeviceInfo(this.txtCode_LEDM1.Text, this.txtName_LEDM1.Text, this.txtIpAddress_LEDM1.Text, Int32.Parse(this.txtPortNumber_LEDM1?.Text ?? "0"), this.txtHistory_LEDM1.Text) == 1)
            {
                MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi xảy ra khi cập nhật thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            EditAble(false, "_LEDM1");
        }
        private void btnEdit_LEDM2_Click(object sender, EventArgs e)
        {
            EditAble(true, "_LEDM2");
        }
        private void btnSave_LEDM2_Click(object sender, EventArgs e)
        {
            if (this.objDevice.UpdateDeviceInfo(this.txtCode_LEDM2.Text, this.txtName_LEDM2.Text, this.txtIpAddress_LEDM2.Text, Int32.Parse(this.txtPortNumber_LEDM2?.Text ?? "0"), this.txtHistory_LEDM2.Text) == 1)
            {
                MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi xảy ra khi cập nhật thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            EditAble(false, "_LEDM2");
        }
        private void btnEdit_LEDM3_Click(object sender, EventArgs e)
        {
            EditAble(true, "_LEDM3");
        }
        private void btnSave_LEDM3_Click(object sender, EventArgs e)
        {
            if (this.objDevice.UpdateDeviceInfo(this.txtCode_LEDM3.Text, this.txtName_LEDM3.Text, this.txtIpAddress_LEDM3.Text, Int32.Parse(this.txtPortNumber_LEDM3?.Text ?? "0"), this.txtHistory_LEDM3.Text) == 1)
            {
                MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi xảy ra khi cập nhật thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            EditAble(false, "_LEDM3");
        }

        private void btnEdit_LEDM4_Click(object sender, EventArgs e)
        {
            EditAble(true, "_LEDM4");
        }
        private void btnSave_LEDM4_Click(object sender, EventArgs e)
        {
            if (this.objDevice.UpdateDeviceInfo(this.txtCode_LEDM4.Text, this.txtName_LEDM4.Text, this.txtIpAddress_LEDM4.Text, Int32.Parse(this.txtPortNumber_LEDM4?.Text ?? "0"), this.txtHistory_LEDM4.Text) == 1)
            {
                MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi xảy ra khi cập nhật thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            EditAble(false, "_LEDM4");
        }
        private void btnEdit_LEDM5_Click(object sender, EventArgs e)
        {
            EditAble(true, "_LEDM5");
        }
        private void btnSave_LEDM5_Click(object sender, EventArgs e)
        {
            if (this.objDevice.UpdateDeviceInfo(this.txtCode_LEDM5.Text, this.txtName_LEDM5.Text, this.txtIpAddress_LEDM5.Text, Int32.Parse(this.txtPortNumber_LEDM5?.Text ?? "0"), this.txtHistory_LEDM5.Text) == 1)
            {
                MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi xảy ra khi cập nhật thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            EditAble(false, "_LEDM5");
        }
        private void btnEdit_LEDM6_Click(object sender, EventArgs e)
        {
            EditAble(true, "_LEDM6");
        }
        private void btnSave_LEDM6_Click(object sender, EventArgs e)
        {
            if (this.objDevice.UpdateDeviceInfo(this.txtCode_LEDM6.Text, this.txtName_LEDM6.Text, this.txtIpAddress_LEDM6.Text, Int32.Parse(this.txtPortNumber_LEDM6?.Text ?? "0"), this.txtHistory_LEDM6.Text) == 1)
            {
                MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi xảy ra khi cập nhật thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            EditAble(false, "_LEDM6");
        }
    }
}
