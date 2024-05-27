using Newtonsoft.Json.Linq;
using Patagames.Pdf.Net;
using Patagames.Pdf.Net.Controls.WinForms;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD.Core
{
    class Print
    {
        public bool PrintPDF(string deliveryCode)
        {
            bool isPrinted = true;
            try
            {
                var token = GetToken();
                if (token == "") return false;
                var linkPDF = GetLinkPDF(token, deliveryCode);
                WriteFilePdf(linkPDF);
                PrintFile("temp.pdf");
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
                var jo = JObject.Parse(response.Content);
                token = jo["access_token"].ToString();
            }
            catch (Exception ex)
            {
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
                data = data.Replace('"', ' ').Trim();
                linkpdf = $@"https://dathang.ximanghoangmai.vn/{data}";
            }
            catch (Exception ex)
            {
            }
            return linkpdf;
        }
        public void WriteFilePdf(string linkPdf)
        {
            try
            {
                var webClient = new WebClient();
                byte[] pdfBytes = webClient.DownloadData(linkPdf);
                webClient.DownloadFile(linkPdf, @"temp.pdf");
            }
            catch (Exception ex)
            {
            }
        }
        public void PrintFile(string path)
        {
            try
            {
                var doc = PdfDocument.Load(path);
                var printDoc = new PdfPrintDocument(doc);
                PrintController printController = new StandardPrintController();
                printDoc.PrinterSettings.PrinterName = "Brother HL-L2360D series Printer";


                IEnumerable<PaperSize> paperSizes = printDoc.PrinterSettings.PaperSizes.Cast<PaperSize>();
                PaperSize sizeA5 = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A5); // setting paper size to A5 size
                printDoc.DefaultPageSettings.PaperSize = sizeA5;

                printDoc.PrintController = printController;
                printDoc.Print();
            }
            catch (Exception ex)
            {
            }
        }
        public string GetNamePrint(string codeDevice)
        {
            string printName = "";
            string SQLQUERY = "SELECT TOP 1 * FROM dbo.tblPrintDeviceOperating WHERE Code = @Code";
            SqlConnection sqlCon = new SqlConnection(TVSOracle.SQL_Con);
            try
            {
                
                sqlCon.Open();
                SqlCommand Cmd = sqlCon.CreateCommand();
                Cmd.CommandText = SQLQUERY;
                Cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = codeDevice;
                SqlDataReader Rd = Cmd.ExecuteReader();
                while (Rd.Read())
                {
                    if (printName == "")
                    {
                        printName = Rd["PrintName"].ToString();
                    }
                }
                Rd.Close();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            catch
            {
                sqlCon.Close();
                sqlCon.Dispose();

            }
            return printName;
        }
    }
}
