using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XHTD_Call_Service.Models
{
    public class ResponseOrder
    {
        public List<ResponseOrderItem> orders { get; set; }
    }
    public class ResponseOrderItem
    {
        public int ORDER_ID { get; set; }
        public string STATUS { get; set; }
        public string PRINT_STATUS { get; set; }
        public string SO_STATUS { get; set; }
        public double? LOADWEIGHTNULL { get; set; }
        public double? LOADWEIGHTFULL { get; set; }
    }
}
