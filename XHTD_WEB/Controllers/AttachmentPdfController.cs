using HMXHTD.Data.DataEntity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace XHTD_WEB.Controllers
{
    [RoutePrefix("api/v1/AttachmentPdf")]
    public class AttachmentPdfController : ApiController
    {
        [HttpPost]
        [Route("attachment-xiroi")]
        public async Task<Object> AttacmentPdfXiroi(int id, string deliveryCode)
        {
            try
            {
                
                HttpResponseMessage result = null;
                 var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var docfiles = new List<string>();
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        var filePath = HttpContext.Current.Server.MapPath("~/pdf/xiroi/" + $@"{file}.pdf");
                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath);
                    }
                    return new
                    {
                        Status = 200,
                        Message = "Upload file thành công"
                    };
                }
                else
                {
                    return new
                    {
                        Status = 500,
                        Message = "Lỗi không upload được file"
                    };
                }
                return result;
            }
            catch (Exception ex)
            {

            }
            return new
            {
                Status = 500,
                Message = "Lỗi không upload được file"
            };
        }
        [HttpPost]
        [Route("attachment-xiroi-kcs")]
        public async Task<Object> AttacmentPdfXiroiKCS(int id, string codeKCS)
        {
            try
            {

                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var docfiles = new List<string>();
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        var filePath = HttpContext.Current.Server.MapPath("~/pdf/xiroi/" + $@"{file}.pdf");
                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath);
                    }
                    return new
                    {
                        Status = 200,
                        Message = "Upload file thành công"
                    };
                }
                else
                {
                    return new
                    {
                        Status = 500,
                        Message = "Lỗi không upload được file"
                    };
                }
                return result;
            }
            catch (Exception ex)
            {

            }
            return new
            {
                Status = 500,
                Message = "Lỗi không upload được file"
            };
        }
    }
}