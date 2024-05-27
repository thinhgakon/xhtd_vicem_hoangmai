using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;
using AForge.Video;
using System.Diagnostics;
using AForge.Video.DirectShow;
using System.Collections;
using System.IO;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Globalization;
using System.Net;
using iTextSharp.text.pdf;
using System.Drawing.Printing;
using Patagames.Pdf.Net.Controls.WinForms;
using iTextSharp.text;
using RestSharp;
using Newtonsoft.Json;

namespace HMXHTD
{
    public partial class frmConfirmRFIDCameraWithSign : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoDevice;
        private VideoCapabilities[] snapshotCapabilities;
        private ArrayList listCamera = new ArrayList();
        string pathSource = @"D:/xhtd_data";
        private Stopwatch stopWatch = null;
        private static bool needSnapshot = false;
        string vehicle = "";
        string rfid = "";
        public frmConfirmRFIDCameraWithSign(string vehicle, string rfid)
        {
            InitializeComponent();
            getListCameraUSB();
            this.vehicle = vehicle;
            this.rfid = rfid;
        }
        private static string _usbcamera;
        public string usbcamera
        {
            get { return _usbcamera; }
            set { _usbcamera = value; }
        }


        #region Open Scan Camera
        private void OpenCamera()
        {
            try
            {
                usbcamera = comboBox1.SelectedIndex.ToString();
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count != 0)
                {
                    // add all devices to combo
                    foreach (FilterInfo device in videoDevices)
                    {
                        listCamera.Add(device.Name);

                    }
                }
                else
                {
                    MessageBox.Show("Camera devices found");
                }

                videoDevice = new VideoCaptureDevice(videoDevices[Convert.ToInt32(usbcamera)].MonikerString);
                snapshotCapabilities = videoDevice.SnapshotCapabilities;
                if (snapshotCapabilities.Length == 0)
                {
                    //MessageBox.Show("Camera Capture Not supported");
                }

                OpenVideoSource(videoDevice);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }
        #endregion


        //Delegate Untuk Capture, insert database, update ke grid 
        public delegate void CaptureSnapshotManifast(Bitmap image);
        public void UpdateCaptureSnapshotManifast(Bitmap image)
        {
            try
            {
                needSnapshot = false;
                pictureBox2.Image = image;
                pictureBox2.Update();


                image.Save(string.Format($@"{pathSource}/images/{this.vehicle}.png"), ImageFormat.Png);
                if (ProcessImage())
                {
                    MessageBox.Show("Lưu ảnh thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Vui lòng thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            catch (Exception e)
            {
                var s = e.Message;
            }

        }
        private bool ProcessImage()
        {
            try
            {
                var base64 = ImageToBase64($@"{pathSource}/images/{this.vehicle}.png");
                var requestData = new
                {
                    vehicle = this.vehicle,
                    rfid = this.rfid,
                    base64 = base64
                };

                var client = new RestClient("http://tv.ximanghoangmai.vn:8189/api/v1/Images/create-image-rfid-confirm");
                var request = new RestRequest(Method.POST);
                request.AddJsonBody(requestData);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("content-type", "application/json");

                IRestResponse response = client.Execute(request);
                string data = response.Content;

                var jsonData = JsonConvert.DeserializeObject<ResultImageModel>(data);
                return jsonData.Status == 200;

            }
            catch (Exception ex)
            {

            }
            return false;
        }
        private string ImageToBase64(string path)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(path);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            return base64ImageRepresentation;
        }
        public void OpenVideoSource(IVideoSource source)
        {
            try
            {
                // set busy cursor
                this.Cursor = Cursors.WaitCursor;

                // stop current video source
                CloseCurrentVideoSource();

                // start new video source
                videoSourcePlayer1.VideoSource = source;
                videoSourcePlayer1.Start();

                // reset stop watch
                stopWatch = null;


                this.Cursor = Cursors.Default;
            }
            catch { }
        }
        private void getListCameraUSB()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count != 0)
            {
                // add all devices to combo
                foreach (FilterInfo device in videoDevices)
                {
                    comboBox1.Items.Add(device.Name);

                }
            }
            else
            {
                comboBox1.Items.Add("No DirectShow devices found");
            }

            comboBox1.SelectedIndex = 0;

        }
        public void CloseCurrentVideoSource()
        {
            try
            {

                if (videoSourcePlayer1.VideoSource != null)
                {
                    videoSourcePlayer1.SignalToStop();

                    // wait ~ 3 seconds
                    for (int i = 0; i < 30; i++)
                    {
                        if (!videoSourcePlayer1.IsRunning)
                            break;
                        System.Threading.Thread.Sleep(100);
                    }

                    if (videoSourcePlayer1.IsRunning)
                    {
                        videoSourcePlayer1.Stop();
                    }

                    videoSourcePlayer1.VideoSource = null;
                }
            }
            catch { }
        }
        private void videoSourcePlayer1_NewFrame_1(object sender, ref Bitmap image)
        {
            try
            {
                DateTime now = DateTime.Now;
                Graphics g = Graphics.FromImage(image);

                if (needSnapshot)
                {
                    this.Invoke(new CaptureSnapshotManifast(UpdateCaptureSnapshotManifast), image);
                }
                g.Dispose();
            }
            catch
            { }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            OpenCamera();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            needSnapshot = true;
        }

        private void frmConfirmRFIDCameraWithSign_Closing(object sender, FormClosingEventArgs e)
        {
            videoSourcePlayer1.Stop();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    public class ResultImageModel
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }
}
