using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace XHTD_WEB.Controllers
{
    [RoutePrefix("api/v1/time")]
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("now")]
        public async Task<Object> df()
        {
            return "1";

        }
    }
}