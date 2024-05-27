using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XHTD_WEB.Business;

namespace XHTD_WEB.Controllers
{
    [RoutePrefix("api/v1/PrintApi")]
    public class PrintApiController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
       (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        [HttpPost]
        [Route("print-order")]
        public object PrintOrder(string deliveryCode)
        {
            log.Info("======PrintOrder=====" + deliveryCode);
            var printBusiness = new PrintBusiness();
            var print = printBusiness.PrintPDF(deliveryCode);
            if (print)
            {
                return new
                {
                    Status = 200,
                    Message = "Thành Công"
                };
            }
            else
            {
                return new
                {
                    Status = 500,
                    Message = "Lỗi trong quá trình in"
                };
            }
        }

        [HttpPost]
        [Route("print-coupon-scale")]
        
        public object PrintCouponScale(string deliveryCode)
        {
            log.Info("in");
            var printBusiness = new PrintBusiness();
            var print = printBusiness.PrintCouponScale(deliveryCode);
            if (print)
            {
                return new
                {
                    Status = 200,
                    Message = "Thành Công"
                };
            }
            else
            {
                return new
                {
                    Status = 500,
                    Message = "Lỗi trong quá trình in"
                };
            }
        }

    }
}