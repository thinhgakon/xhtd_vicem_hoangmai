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
    [RoutePrefix("api/v1/Images")]
    public class ImagesController : ApiController
    {
        [HttpPost]
        [Route("create-image-rfid-confirm")]
        public async Task<Object> CreateImageRFIDConfirm(ImageRFIDConfirmModel model)
        {
            try
            {
                if (String.IsNullOrEmpty(model.base64) || String.IsNullOrEmpty(model.vehicle)) return new
                {
                    Status = 500,
                    Message = "Thông tin chưa đầy đủ"
                };
                LoadImage(model.vehicle, model.base64);
                using (var db = new HMXuathangtudong_Entities())
                {
                    string SQLQUERY = @"IF NOT EXISTS (SELECT * FROM dbo.tblRFIDSign WHERE Vehicle = @Vehicle)
                                    BEGIN
                                        INSERT INTO tblRFIDSign
                                        (
                                            Vehicle,
                                            RfidCode,
                                            CreatedOn,
                                            ModifiedOn,
                                            Image
                                        )
                                        VALUES
                                        (@Vehicle, @RfidCode, GETDATE(), GETDATE(), @Image);
                                    END;
                                    ELSE
                                    BEGIN
                                        UPDATE tblRFIDSign
                                        SET ModifiedOn = GETDATE(),
                                            Image = @Image
                                        WHERE Vehicle = @Vehicle;
                                    END;";
                    var updateResponse = db.Database.ExecuteSqlCommand(SQLQUERY,
                        new SqlParameter("@Vehicle", model.vehicle),
                        new SqlParameter("@RfidCode", model.rfid),
                        new SqlParameter("@Image", $"{model.vehicle}.PNG"));
                    if (updateResponse > 0)
                        return new
                        {
                            Status = 200,
                            Message = "Thành Công"
                        };
                }
            }
            catch (Exception ex)
            {

            }
            return new
            {
                Status = 500,
                Message = "Thông tin chưa đầy đủ"
            };
        }
        public void LoadImage(string vehicle, string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            image.Save(HttpContext.Current.Server.MapPath("~/images/" + $"{vehicle}.PNG"), System.Drawing.Imaging.ImageFormat.Png);
        }
    }
    public class ImageRFIDConfirmModel
    {
        public string vehicle { get; set; }
        public string rfid { get; set; }
        public string base64 { get; set; }
    }
}