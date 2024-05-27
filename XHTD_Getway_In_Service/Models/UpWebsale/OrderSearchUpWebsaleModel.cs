using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XHTD_Getway_In_Service.Models.UpWebsale
{
    public class UpWebsale_OrderSearchRequestModel
    {
        public int limit { get; set; } = 200;
        public int offset { get; set; } = 1;
        public string from { get; set; }
        public string to { get; set; }
        public string deliveryDate { get; set; } = "";
        public int orderType { get; set; } = 0;
        public string deliveryCode { get; set; } = "";
        public string vehicleCode { get; set; } = "";
        public int customerId { get; set;}
        public int areaId { get; set; } = 0;
        public int productId { get; set; } = 0;
        public string status { get; set; } = "";
        public string printStatus { get; set; } = "";

    }
    public class UpWebsale_OrderItemResponse
    {
        public int id { get; set; }
        public string deliveryCode { get; set; }
        public DateTime? orderDate { get; set; }
        public DateTime? deliveryDate { get; set; }
        public int? customerId { get; set; }
        public int? branchId { get; set; }
        public string branchName { get; set; }
        //public string customerNumber { get; set; }
        public string customerName { get; set; }
        public string customerAddress { get; set; }
        public string customerTax { get; set; }
        public int? contractId { get; set; }
        public string contractNumber { get; set; }
        public DateTime? contractDate { get; set; }
        public int? productId { get; set; }
        public string productName { get; set; }
        public int? shippointId { get; set; }
        public string shippointName { get; set; }
        public int? checkpointId { get; set; }
        public string checkpointName { get; set; }
        public int? areaId { get; set; }
        public string areaName { get; set; }
        public int? bookQuantity { get; set; }
        public double? orderQuantity { get; set; }
        public double? realQuantity { get; set; }
        public double? orderAmount { get; set; }
        public int? priceId { get; set; }
        public double? unitPrice { get; set; }
        public string currency { get; set; }
        public string uomCode { get; set; }
        public string locationCode { get; set; }
        public int? transportMethodId { get; set; }
        public string transportMethodName { get; set; }
        public int? transportMethodTypeId { get; set; }
        public string transportMethodTypeName { get; set; }
        public int? orderType { get; set; }
        public string orderTypeName { get; set; }
        public DateTime? timeIn { get; set; }
        public DateTime? timeOut { get; set; }
        public string vehicleCode { get; set; }
        public string moocCode { get; set; }
        public string driverName { get; set; }
        public string status { get; set; }
        public string orderPrintStatus { get; set; }
        public string productionLine { get; set; }
        public string shipFromWareHouse { get; set; }
        public string documentNumber { get; set; }
        public string documentSeries { get; set; }
        public int? lotNumber { get; set; }
        public string docTemplate { get; set; }
        public string description { get; set; }
        public int? sourceDocumentId { get; set; }
        //public object sourceDocumentType { get; set; }
        //public object driverIndex { get; set; }
        //public object packType { get; set; }
        //public object bagType { get; set; }
        public string orderLog { get; set; }
    }

    public class UpWebsale_OrderSearchResponseModel
    {
        public int total { get; set; }
        public double totalBookQuantity { get; set; }
        public double totalOrderQuantity { get; set; }
        public List<UpWebsale_OrderItemResponse> collection { get; set; }
    }
}
