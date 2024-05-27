using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using RestSharp;

namespace HMXHTD
{
    public partial class frmConfirmRFIDWithImage : Form
    {
        string pathSource = @"D:/xhtd_data";
        VideoCapture capture;
        Mat frame;
        Bitmap image;
        private Thread camera;
        bool isCameraRunning = false;
        string vehicle = "";
        string rfid = "";
        public frmConfirmRFIDWithImage(string vehicle, string rfid)
        {
            InitializeComponent();
            this.vehicle = vehicle;
            this.rfid = rfid;
        }
        private void SetLoading(bool displayLoader)
        {
            if (displayLoader)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    pictureLoading.Visible = true;
                    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                });
            }
            else
            {
                this.Invoke((MethodInvoker)delegate
                {
                    pictureLoading.Visible = false;
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                });
            }
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text.Equals("Bắt đầu"))
            {
                OpenCamera(true);
            }
            else
            {
                OpenCamera(false);
            }
        }
        protected void OpenCamera(bool open)
        {
            if (open)
            {
                CaptureCamera();
                btnStart.Text = "Kết thúc";
                isCameraRunning = true;
            }
            else
            {
                capture.Release();
                btnStart.Text = "Bắt đầu";
                isCameraRunning = false;
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (isCameraRunning)
            {
                Bitmap snapshot = new Bitmap(pictureBox1.Image);

                snapshot.Save(string.Format($@"{pathSource}/images/{this.vehicle}.png", Guid.NewGuid()), ImageFormat.Png);
                ProcessImage();
                MessageBox.Show("Lưu ảnh thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OpenCamera(false);
            }
            else
            {
                MessageBox.Show("Vui lòng bật camera", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ProcessImage()
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
                
                var client = new RestClient("http://tv.ximanghoangmai.vn:8189/api/Images/create-image-rfid-confirm");
                var request = new RestRequest(Method.POST);
                request.AddJsonBody(requestData);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("content-type", "application/json");

                IRestResponse response = client.Execute(request);
                string data = response.Content;

                dynamic jsonData = JsonConvert.DeserializeObject(data);

            }
            catch (Exception ex)
            {

            }
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        private string ImageToBase64(string path)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(path);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            return base64ImageRepresentation;
        }
        private void CaptureCamera()
        {
            camera = new Thread(new ThreadStart(CaptureCameraCallback));
            camera.Start();
        }
        private void CaptureCameraCallback()
        {
            SetLoading(true);
            frame = new Mat();
            capture = new VideoCapture(0);
            capture.Open(0);
            int interval = (int)(1000 / capture.Fps);
            SetLoading(false);
            if (capture.IsOpened())
            {
                while (isCameraRunning)
                {

                    try
                    {
                        capture.Read(frame);
                        image = BitmapConverter.ToBitmap(frame);
                        if (pictureBox1.Image != null)
                        {
                            pictureBox1.Image.Dispose();
                        }
                        pictureBox1.Image = image;
                    }
                    catch (Exception ex)
                    {

                    }
                    Thread.Sleep(interval);
                }
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.camera.Abort();
            this.Close();
        }

        private void frmConfirmRFIDWithImage_Closing(object sender, FormClosingEventArgs e)
        {
            capture.Release();
            this.camera.Abort();
            this.Close();
        }
    }
}
