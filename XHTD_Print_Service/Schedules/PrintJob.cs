using HMXHTD.Data.DataEntity;
using HMXHTD.Services.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Patagames.Pdf.Net;
using Patagames.Pdf.Net.Controls.WinForms;
using Quartz;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace XHTD_Print_Service.Schedules
{
    public class PrintJob : IJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public PrintJob(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            await Task.Run(() =>
            {
                PrintProcess();
            });
        }
        public void PrintProcess()
        {
            try
            {
                var token = GetToken();
                if(token == "")
                {
                    log.Info("Token invalid"); return;
                }
                var linkPDF = GetLinkPDF(token, "431318-21");
                WriteFilePdf(linkPDF);
                PrintFile();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
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
                log.Error(ex.Message);
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
                log.Error(ex.Message);
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
                log.Error(ex.Message);
            }
        }
        public void PrintFile()
        {
            try
            {
                var doc = PdfDocument.Load("temp.pdf");
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
                log.Error(ex.Message);
            }
        }
    }
}
