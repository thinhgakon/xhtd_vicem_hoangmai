using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XHTD_Getway_Service.Models
{
    public class TokenRequestModel
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string OUId { get; set; }
    }
    public class TokenResponseModel
    {
        public string token { get; set; }
        public DateTime expires { get; set; }
    }
    class TokenModels
    {
    }
}
