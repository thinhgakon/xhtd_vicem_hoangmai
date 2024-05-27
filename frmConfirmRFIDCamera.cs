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

namespace HMXHTD
{
    public partial class frmConfirmRFIDCamera : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoDevice;
        private VideoCapabilities[] snapshotCapabilities;
        private ArrayList listCamera = new ArrayList();
        string pathSource = @"D:/xhtd_data";
        private Stopwatch stopWatch = null;
        private static bool needSnapshot = false;
        public frmConfirmRFIDCamera(string vehicle, string rfid)
        {
            InitializeComponent();
            this.txtBSX.Text = vehicle;
            this.txtRFID.Text = rfid;
            getListCameraUSB();
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


                image.Save(string.Format($@"D:/xhtd_data/images/gplx.png", Guid.NewGuid()), ImageFormat.Png);
                MessageBox.Show("Chụp ảnh thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //if (Directory.Exists(pathFolder))
                //{
                //    pictureBox2.Image.Save(pathFolder, ImageFormat.Bmp);
                //}
                //else
                //{
                //    Directory.CreateDirectory(pathFolder);
                //    pictureBox2.Image.Save(pathFolder, ImageFormat.Bmp);
                //}

            }

            catch(Exception e) {
                var s = e.Message;
            }

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

                // paint current time
                //SolidBrush brush = new SolidBrush(Color.Red);
                //g.DrawString(now.ToString(), this.Font, brush, new PointF(5, 5));
                //brush.Dispose();
                if (needSnapshot)
                {
                    this.Invoke(new CaptureSnapshotManifast(UpdateCaptureSnapshotManifast), image);
                }
                g.Dispose();
            }
            catch
            { }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenCamera();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            needSnapshot = true;
        }

        private void frmConfirmRFIDCamera_Load(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            BaseFont f_cb = BaseFont.CreateFont($@"{pathSource}/fonts/vuTimesBold.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            BaseFont f_cn = BaseFont.CreateFont($@"{pathSource}/fonts/vuTimes.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            try
            {

                using (System.IO.FileStream fs = new FileStream($@"{pathSource}/pdfs/phieuxacnhanrfid.pdf", FileMode.Create))
                {
                    Document document = new Document(PageSize.A5, 25, 25, 30, 1);
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);
                    document.SetPageSize(iTextSharp.text.PageSize.A5.Rotate());


                    document.AddAuthor("XHTD_HM");
                    document.AddCreator("TrungNC");
                    document.AddKeywords("PDF tutorial education");
                    document.AddSubject("Describing the steps creating a PDF document");
                    document.AddTitle("Phiếu RFID");

                    document.Open();

                    PdfContentByte cb = writer.DirectContent;

                    iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance($@"{pathSource}/logo/logoHM.png");
                    png.ScaleAbsolute(200, 120);
                    png.SetAbsolutePosition(20, 320);
                    cb.AddImage(png);

                    cb.BeginText();



                    writeText(cb, "Cộng hòa xã hội chủ nghĩa Việt Nam", 350, CaculatorHeight(1) + 10, f_cb, 14);
                    writeText(cb, "Độc lập - Tự do - Hạnh phúc", 370, CaculatorHeight(2) + 10, f_cb, 14);
                    writeText(cb, "Biên bản xác nhận xe đã gán thẻ RFID", 200, CaculatorHeight(3), f_cb, 14);
                    writeText(cb, $@"(Biển số xe: {this.txtBSX.Text}, Mã RFID: {this.txtRFID.Text})", 200, CaculatorHeight(4), f_cn, 12);
                    // HEader details; nội dung phiếu
                    //writeText(cb, "Biển số xe:", 420, CaculatorHeight(5), f_cn, 12); writeText(cb, "37H30208", 490, CaculatorHeight(5), f_cb, 12);
                    //writeText(cb, "Mã thẻ RFID:", 420, CaculatorHeight(6), f_cn, 12); writeText(cb, "2130001", 490, CaculatorHeight(6), f_cb, 12);

                    writeText(cb, "Chữ ký lái xe", 430, 60, f_cn, 14);
                    writeText(cb, $@"Hoàng Mai, ngày {DateTime.Now.Day} tháng {DateTime.Now.Month} năm {DateTime.Now.Year} ", 380, 80, f_cn, 12);
                    cb.EndText();

                    iTextSharp.text.Image gplx = iTextSharp.text.Image.GetInstance($@"{pathSource}/images/gplx.png");
                    gplx.ScaleAbsolute((float)(85 * 4.2), (float)(54 * 4));
                    gplx.SetAbsolutePosition(120, 100);
                    cb.AddImage(gplx);


                    cb.SetLineWidth(0f);
                    cb.MoveTo(40, 570);
                    cb.LineTo(560, 570);
                    cb.Stroke();

                    document.Close();
                    writer.Close();
                    fs.Close();

                    RotatePages($@"{pathSource}/pdfs/phieuxacnhanrfid.pdf", $@"{pathSource}/pdfs/phieuxacnhanrfid_end.pdf", 90);
                    PrintFile();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"{error.Message} , {error.StackTrace}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void PrintFile()
        {
            try
            {
                Process.Start("chrome.exe", $@"{pathSource}/pdfs/phieuxacnhanrfid_end.pdf");
                return;
                var filepdf = $@"{pathSource}/pdfs/phieuxacnhanrfid_end.pdf";
                var doc = Patagames.Pdf.Net.PdfDocument.Load(filepdf);
                var printDoc = new PdfPrintDocument(doc);
                PrintController printController = new StandardPrintController();
                printDoc.PrinterSettings.PrinterName = new Core.Print().GetNamePrint("PrintForRFID");


                IEnumerable<PaperSize> paperSizes = printDoc.PrinterSettings.PaperSizes.Cast<PaperSize>();
                PaperSize sizeA5 = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A5);
                printDoc.DefaultPageSettings.PaperSize = sizeA5;

                printDoc.PrintController = printController;
                printDoc.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} , {ex.StackTrace}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static int CaculatorHeight(int line)
        {
            return 400 - line * 20;
        }
        public static void writeText(PdfContentByte cb, string Text, int X, int Y, BaseFont font, int Size)
        {
            cb.SetFontAndSize(font, Size);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Text, X, Y, 0);
        }
        private static void RotatePages(string pdfFilePath, string outputPath, int rotateDegree)
        {
            try
            {
                PdfReader reader = new PdfReader(pdfFilePath);
                int pagesCount = reader.NumberOfPages;

                for (int n = 1; n <= pagesCount; n++)
                {
                    PdfDictionary page = reader.GetPageN(n);
                    PdfNumber rotate = page.GetAsNumber(PdfName.ROTATE);
                    int rotation =
                            rotate == null ? rotateDegree : (rotate.IntValue + rotateDegree) % 360;

                    page.Put(PdfName.ROTATE, new PdfNumber(rotation));
                }

                PdfStamper stamper = new PdfStamper(reader, new FileStream(outputPath, FileMode.Create));
                stamper.Close();
                reader.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void frmConfirmRFIDCamera_Closing(object sender, FormClosingEventArgs e)
        {
            videoSourcePlayer1.Stop();
        }
    }
}
