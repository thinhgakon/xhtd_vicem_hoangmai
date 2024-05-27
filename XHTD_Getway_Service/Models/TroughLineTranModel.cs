using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XHTD_Getway_Service.Models
{
    public class TroughLineTranModel
    {
        public string TransportName { get; set; }
        public DateTime? CheckInTime { get; set; }
        public int LineStatus { get; set; }
    }
}
