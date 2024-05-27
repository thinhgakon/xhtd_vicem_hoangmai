using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD.Data.Models
{
    public class RequestVoiceTTS
    {
        public string app_id { get; set; }
        public string key { get; set; }
        public string voice { get; set; }
        public string rate { get; set; }
        public string time { get; set; }
        public string user_id { get; set; }
        public string input_text { get; set; }
    }
}
