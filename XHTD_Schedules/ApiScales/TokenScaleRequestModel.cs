using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XHTD_Schedules.ApiScales
{
    public class TokenScaleRequestModel
    {
        public string grant_type { get; set; }
        public string client_secret { get; set; }
        public string client_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
    public class TokenScaleResponseModel
    {
        public string access_token { get; set; }
        public double expires_in { get; set; }
    }
    public class RequestUpdateWeightModel
    {
        public string deliveryCode { get; set; }
        public int weight { get; set; }
    }
    public class ScaleResponseModel
    {
        public string code { get; set; }
        public string message { get; set; }
       
    }
    public class LogicErrors
    {
        public List<string> LogicError { get; set; }
    }
}
