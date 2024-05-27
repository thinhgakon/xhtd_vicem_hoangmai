using HMXHTD.Data.DataEntity;
using Oracle.ManagedDataAccess.Client;
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
using XHTD_WEB.Business;
using XHTD_WEB.Models;

namespace XHTD_WEB.Controllers
{
    [RoutePrefix("api/v1/CouponScale")]
    public class CouponScaleController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
       (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpPost]
        [Route("create-coupon-scale")]
        public async Task<Object> CreateCouponScale(string deliveryCode)
        {
            var linkPdfScale = "";
            double weightNull = 0;
            double weightFull = 0;
            DateTime timeIn;
            DateTime timeOut;
            var couponModel = new CouponScaleModel();
            try
            {

                string sqlQuery = "";
                string strConString = System.Configuration.ConfigurationManager.ConnectionStrings["MbfConnOracle"].ConnectionString.ToString();

                sqlQuery = $@"select so.*, cvw.lot_number as PACKAGE_NUMBER, cvw.LOADWEIGHTNULL, cvw.LOADWEIGHTFULL, cvw.REQ_QTY, item.description ITEM_NAME, cvw.TIMEIN, cvw.TIMEOUT, cvw.TOP_SEAL_COUNT, cvw.TOP_SEAL_DES, cvw.VEHICLE_WEIGHT_ID ,c2.name_store, c2.code_store
                            from sales_orders so
                            ,cx_vehicle_weight cvw 
                            , ORDER_C2 c2
                            ,dev_om_item_list_v item
                            where so.delivery_code = cvw.delivery_code 
                            and so.delivery_code = c2.delivery_code(+)
                            and so.inventory_item_id = item.inventory_item_id 
                            and so.VEHICLE_CODE IS NOT NULL
                            and so.DELIVERY_CODE = :DELIVERY_CODE
                            and rownum = 1";
                using (OracleConnection connection = new OracleConnection(strConString))
                {
                    OracleCommand Cmd = new OracleCommand(sqlQuery, connection);

                    Cmd.Parameters.Add(new OracleParameter("DELIVERY_CODE", deliveryCode));
                    connection.Open();
                    using (OracleDataReader Rd = Cmd.ExecuteReader())
                    {
                        while (Rd.Read())
                        {
                            var itemName = Rd["ITEM_NAME"].ToString().ToUpper();
                            Double.TryParse(Rd["LOADWEIGHTNULL"]?.ToString(), out weightNull);
                            Double.TryParse(Rd["LOADWEIGHTFULL"]?.ToString(), out weightFull);
                            DateTime.TryParse(Rd["TIMEIN"]?.ToString(), out timeIn);
                            DateTime.TryParse(Rd["TIMEOUT"]?.ToString(), out timeOut);
                            couponModel.Title = Rd["ITEM_NAME"].ToString().ToUpper().Contains("RỜI") ? "Phiếu cân kiêm biên bản kẹp chì".ToUpper() : "phiếu cân".ToUpper();
                            couponModel.CouponNumber = Rd["VEHICLE_WEIGHT_ID"].ToString();
                            couponModel.DateCreated = timeOut.ToString("dd/MM/yyyy");
                            couponModel.NameOfCustomer = Rd["NAME_STORE"].ToString();
                            couponModel.LocationCode = Rd["LOCATION_CODE"].ToString();
                            couponModel.VehicleCode = Rd["VEHICLE_CODE"].ToString();
                            couponModel.ProductName = Rd["ITEM_NAME"].ToString();
                            couponModel.QuantityCoupon = Rd["BOOK_QUANTITY"].ToString();
                            couponModel.LoadWeightNull = Rd["LOADWEIGHTNULL"].ToString();
                            couponModel.LoadWeightFull = Rd["LOADWEIGHTFULL"].ToString();
                            couponModel.WeightReal = Rd["REQ_QTY"].ToString();
                            couponModel.Seri = Rd["TOP_SEAL_DES"]?.ToString();
                            couponModel.Mooc = Rd["MOOC_CODE"]?.ToString();
                            couponModel.Package = Rd["PACKAGE_NUMBER"]?.ToString();
                            couponModel.DeliveryCode = deliveryCode;
                            couponModel.TimeScaleIn = timeIn.ToString("dd/MM/yyyy HH:mm:ss");
                            couponModel.TimeScaleOut = timeOut.ToString("dd/MM/yyyy HH:mm:ss");
                            couponModel.CountSeal = Rd["TOP_SEAL_COUNT"]?.ToString();
                            couponModel.DocNum = Rd["DOC_NUM"]?.ToString();

                            break;
                        }
                    }
                    new CouponScaleBusiness().CreateCouponScalePdf(couponModel);
                    return new
                    {
                        StatusCode = 200,
                        Message = "Tạo phiếu thành công",
                        Url = $@"http://tv.ximanghoangmai.vn:8189/pdf/coupon_scale/{couponModel.DeliveryCode}.pdf"
                    };
                }

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return new
                {
                    StatusCode = 500,
                    Message = "Không tạo được phiếu cân",
                    StackTrace = $"{ex.Message} , {ex.StackTrace}"
                };
            }
            
        }
    }
}