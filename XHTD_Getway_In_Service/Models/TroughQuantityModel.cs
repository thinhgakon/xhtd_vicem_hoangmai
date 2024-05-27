using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XHTD_Getway_In_Service.Models
{
    public class TroughQuantityModel
    {
        public string DeliveryCode { get; set; }
        public float PlanQuantity { get; set; }
        public float CountQuantity { get; set; }
        public int LineStatus { get; set; }
    }
}
