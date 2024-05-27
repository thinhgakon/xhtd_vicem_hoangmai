
using HMXHTD.Data.DataEntity;
using Newtonsoft.Json.Linq;
using Patagames.Pdf.Net;
using Patagames.Pdf.Net.Controls.WinForms;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using PdfiumViewer;
using System.Diagnostics;
using Spire.Pdf; 
using Spire.Pdf.Annotations;
using Spire.Pdf.Widget;
using System.Drawing.Printing;
using PdfDocument = Spire.Pdf.PdfDocument;

namespace XHTD_WEB.Business
{
    public class PrintBusiness
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public bool PrintCouponScale(string deliveryCode)
        {
            bool isPrinted = true;
            try
            {
                var filepdf = HttpContext.Current.Server.MapPath($"~/pdf/coupon_scale/{deliveryCode}.pdf");
                //var doc = PdfDocument.Load(filepdf);
                //var printDoc = new PdfPrintDocument(doc);
                //PrintController printController = new StandardPrintController();
                //printDoc.PrinterSettings.PrinterName = GetPrintName();

                //IEnumerable<PaperSize> paperSizes = printDoc.PrinterSettings.PaperSizes.Cast<PaperSize>(); 
                //printDoc.PrintController = printController;
                //printDoc.Print();
                PrintByPdfiumViewer(GetPrintName(), filepdf);
            }
            catch (Exception ex)
            {
                isPrinted = false;
                log.Error($"{ex.Message}, {ex.StackTrace}");
            }
            return isPrinted;
        }
        public bool PrintPDF(string deliveryCode)
        {
            bool isPrinted = true;
            try
            {
                var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
                var token = GetToken();
                if (token == "") return false;
                var linkPDF = GetLinkPDF(token, deliveryCode);
                log.Info("======PrintPDF=====" + linkPDF);
                if (linkPDF == "") return false;
                WriteFilePdf(linkPDF, timestamp);
                PrintFile(timestamp);
            }
            catch (Exception ex)
            {
                isPrinted = false;
            }
            return isPrinted;
        }
        public string GetToken()
        {
            var token = "";
            try
            {
                var client = new RestClient(System.Configuration.ConfigurationSettings.AppSettings["LinkAPI_DatHang"].ToString() + "/connect/token");
                var request = new RestRequest();
                request.Method = Method.POST;
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "multipart/form-data");
                request.Parameters.Clear();
                request.AddParameter("grant_type", "password");
                request.AddParameter("client_secret", System.Configuration.ConfigurationSettings.AppSettings["client_secret"].ToString());
                request.AddParameter("username", System.Configuration.ConfigurationSettings.AppSettings["username"].ToString());
                request.AddParameter("password", System.Configuration.ConfigurationSettings.AppSettings["password"].ToString());
                request.AddParameter("client_id", System.Configuration.ConfigurationSettings.AppSettings["client_id"].ToString());
                var response = client.Execute(request);
                string data = response.Content;
                if (string.IsNullOrEmpty(data))
                {
                    return System.Configuration.ConfigurationSettings.AppSettings["TOKEN"].ToString();
                }
                log.Info("=====data=======" + data);
                var jo = JObject.Parse(response.Content);
                token = jo["access_token"].ToString();
            }
            catch (Exception ex)
            {
                log.Error($"=======GetToken========={ex.StackTrace}");
            }
            return token;
        }
        public string GetLinkPDF(string token, string deliveryCode)
        {
            string linkpdf = "";
            try
            {
                var client = new RestClient(System.Configuration.ConfigurationSettings.AppSettings["LinkAPI_DatHang"].ToString() + $@"/api-einvoice/03pxknb/download/{deliveryCode}");
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("Content-Type", "text/plain");
                IRestResponse response = client.Execute(request);
                string data = response.Content;
                if (response.StatusCode == HttpStatusCode.BadRequest) return "";
                data = data.Replace('"', ' ').Trim();
                linkpdf = $@"https://dathang.ximanghoangmai.vn/{data}";
            }
            catch (Exception ex)
            {
                log.Error($"=======GetLinkPDF========={ex.StackTrace}");
            }
            return linkpdf;
        }
        public void WriteFilePdf(string linkPdf, string timestamp)
        {
            try
            {
                var webClient = new WebClient();
                byte[] pdfBytes = webClient.DownloadData(linkPdf);
                var filesave = HttpContext.Current.Server.MapPath("~/PrintFile/" + $"temp_{timestamp}.pdf");
                webClient.DownloadFile(linkPdf, filesave);
            }
            catch (Exception ex)
            {
                log.Error($"=======WriteFilePdf========={ex.StackTrace}");
            }
        }
        public void PrintFile(string timestamp)
        {
            try
            {
                // SplitA4ToA5(HttpContext.Current.Server.MapPath($"~/PrintFile/temp_{timestamp}.pdf"), HttpContext.Current.Server.MapPath($"~/PrintFile/temp_{timestamp}_a5.pdf"));
                // RotatePages(HttpContext.Current.Server.MapPath($"~/PrintFile/temp_{timestamp}_a5.pdf"), HttpContext.Current.Server.MapPath($"~/PrintFile/temp_{timestamp}_a5_normal.pdf"), 90);
                RotatePages(HttpContext.Current.Server.MapPath($"~/PrintFile/temp_{timestamp}.pdf"), HttpContext.Current.Server.MapPath($"~/PrintFile/temp_{timestamp}_a5_normal.pdf"), 90);
                var filepdf = HttpContext.Current.Server.MapPath("~/PrintFile/" + $"temp_{timestamp}_a5_normal.pdf");
                PrintByPdfiumViewer(GetPrintName(), filepdf);
                //var doc = PdfDocument.Load(filepdf);
                //var printDoc = new PdfPrintDocument(doc);
                //PrintController printController = new StandardPrintController();
                //printDoc.PrinterSettings.PrinterName = GetPrintName();

                 

                //printDoc.PrintController = printController;
                //printDoc.Print();
            }
            catch (Exception ex)
            {
                log.Error("======PrintFile========" + ex.Message);
            }
        }
        public void SplitA4ToA5(string inputPath, string outputPath)
        {
            FileStream newPdfStream = new FileStream(outputPath, FileMode.Create, FileAccess.ReadWrite);
            iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(inputPath);
            try
            {
               
                var width = reader.GetPageSize(1).Width; //595
                var height = reader.GetPageSize(1).Height;  //842
                iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(
                    reader.GetPageSize(1).Width,
                    (reader.GetPageSize(1).Height - reader.GetPageSize(1).Height * 1 / 2));
                iTextSharp.text.Document document = new iTextSharp.text.Document(pageSize);
                iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, newPdfStream);
                document.Open();
                iTextSharp.text.pdf.PdfContentByte content = writer.DirectContent;
                iTextSharp.text.pdf.PdfImportedPage page = writer.GetImportedPage(reader, 1);
                content.AddTemplate(page, 0, -reader.GetPageSize(1).Height * 1 / 2);
                content.Fill();
                document.SetPageSize(pageSize);
                document.Close();
                reader.Close();
                
            }
            catch (Exception ex)
            {
                reader.Close();
                log.Error("======SplitA4ToA5========" + ex.Message);
            }
        }
        private void RotatePages(string pdfFilePath, string outputPath, int rotateDegree)
        {
            iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(pdfFilePath);
            int pagesCount = reader.NumberOfPages;

            try
            {
               
                for (int n = 1; n <= pagesCount; n++)
                {
                    iTextSharp.text.pdf.PdfDictionary page = reader.GetPageN(n);
                    iTextSharp.text.pdf.PdfNumber rotate = page.GetAsNumber(iTextSharp.text.pdf.PdfName.ROTATE);
                    int rotation =
                            rotate == null ? rotateDegree : (rotate.IntValue + rotateDegree) % 360;

                    page.Put(iTextSharp.text.pdf.PdfName.ROTATE, new iTextSharp.text.pdf.PdfNumber(rotation));
                }

                iTextSharp.text.pdf.PdfStamper stamper = new iTextSharp.text.pdf.PdfStamper(reader, new FileStream(outputPath, FileMode.Create));
                stamper.Close();
                reader.Close();
            }
            catch (Exception ex)
            {
                reader.Close();
                log.Error("======RotatePages========" + ex.Message);
            }
        }
        public string GetPrintName()
        {
            var printName = "TSC MH240";
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var print = db.tblConfigOperatings.FirstOrDefault(x => x.Code == "PrintCouple");
                    if(print.Value == 0)
                    {
                        printName = System.Configuration.ConfigurationSettings.AppSettings["Printer_Name_1"].ToString();
                    }
                    else
                    {
                        printName = System.Configuration.ConfigurationSettings.AppSettings["Printer_Name_2"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return printName;
        }

        public bool PrintByPdfiumViewer(string printerName, string  filePath)
        {
            try
            {
                PdfDocument doc = new PdfDocument();
                doc.LoadFromFile(filePath);
                doc.PrinterName =  printerName;
                PrintDocument printDoc = doc.PrintDocument; 
                printDoc.Print();


                return true;
            }
            catch (Exception ex)
            {
                log.Error("======PrintByPdfiumViewer========" + ex.Message);
                return false;
            }
        }

    }
}