using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XHTD_Call_Service.Models
{
    public class OrderOracleModel
    {
        public int ORDER_ID { get; set; }
        public DateTime? ORDER_DATE { get; set; }
        public string STATUS { get; set; }
        public double ORDER_QUANTITY { get; set; }
        public string DRIVER_NAME { get; set; }
        public string VEHICLE_CODE { get; set; }
        public string DELIVERY_CODE { get; set; }
        public int CUSTOMER_ID { get; set; }
        public double BOOK_QUANTITY { get; set; }
        public string PRINT_STATUS { get; set; }
        public string LOCATION_CODE { get; set; }
        public int AREA_ID { get; set; }
        public string ITEM_NAME { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public int INVENTORY_ITEM_ID { get; set; }

    }
}
