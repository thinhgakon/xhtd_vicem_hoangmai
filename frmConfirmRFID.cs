using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

using OpenCvSharp;
using OpenCvSharp.Extensions;
using Patagames.Pdf.Net.Controls.WinForms;

namespace HMXHTD
{
    public partial class frmConfirmRFID : Form
    {
        string pathSource = @"D:/xhtd_data";
        VideoCapture capture;
        Mat frame;
        Bitmap image;
        private Thread camera;
        bool isCameraRunning = false;
        public frmConfirmRFID(string vehicle, string rfid)
        {
            InitializeComponent();
            this.txtBSX.Text = vehicle;
            this.txtRFID.Text = rfid;
        }

        private void frmConfirmRFID_Load(object sender, EventArgs e)
        {
          //  OpenCamera(true);
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

                snapshot.Save(string.Format($@"{pathSource}/images/gplx.png", Guid.NewGuid()), ImageFormat.Png);
                MessageBox.Show("Chụp ảnh thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OpenCamera(false);
            }
            else
            {
                MessageBox.Show("Vui lòng bật camera", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            catch (Exception rror)
            {
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

        private void CaptureCamera()
        {
            //camera = new Thread(t =>
            //{
            //    this.Invoke((MethodInvoker)delegate
            //    {


            //        this.CaptureCameraCallback();
            //    });

            //})
            //{ IsBackground = true };
            camera = new Thread(new ThreadStart(CaptureCameraCallback));
            camera.Priority = ThreadPriority.Highest;
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
                    //Thread.Sleep(interval);
                }
            }
        }

        private void frmConfirmRFID_Closing(object sender, FormClosingEventArgs e)
        {
            capture.Release();
        }
    }
}
