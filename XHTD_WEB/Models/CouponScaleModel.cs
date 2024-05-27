using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XHTD_WEB.Models
{
    public class CouponScaleModel
    {
        public string Title { get; set; }
        public string CouponNumber { get; set; }
        public string DateCreated { get; set; }
        public string NameOfCustomer { get; set; }
        public string LocationCode { get; set; }
        public string VehicleCode { get; set; }
        public string ProductName { get; set; }
        public string QuantityCoupon { get; set; }
        public string LoadWeightNull { get; set; }
        public string LoadWeightFull { get; set; }
        public string WeightReal { get; set; }
        public string Seri { get; set; }
        public string Mooc { get; set; }
        public string Package { get; set; }
        public string DeliveryCode { get; set; }
        public string TimeScaleIn { get; set; }
        public string TimeScaleOut { get; set; }
        public string CountSeal { get; set; }
        public string DocNum { get; set; }
    }
}