using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XHTD_ConfirmationPointModule_Service.Models
{
    public class OrderSearchRequestModel
    {
        public int pageSize { get; set; }
        public int pageIndex { get; set; }
        public string deliveryCode { get; set; }
        public string customerId { get; set; }
        public string status { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public int orderType { get; set; }
        public int inventoryItemId { get; set; }
        public int shippointId { get; set; }
        public string vehicleCode { get; set; }
        public string docNum { get; set; }
    }
    public class OrderItemResponse
    {
        public double? RN { get; set; }
        public int? ORDER_ID { get; set; }
        public string DELIVERY_CODE { get; set; }
        public string DOC_NUM { get; set; }
        public string ORDER_DATE { get; set; }
        public string VEHICLE_CODE { get; set; }
        public string DRIVER_NAME { get; set; }
        public string DRIVER_INFO { get; set; }
        public object MOOC_CODE { get; set; }
        public string LOCATION_CODE { get; set; }
        public decimal? ORDER_QUANTITY { get; set; }
        public string UOM_CODE { get; set; }
        public string UNIT_PRICE { get; set; }
        public string DISCOUNT { get; set; }
        public string ORDER_AMOUNT { get; set; }
        public string CURRENCY_CODE { get; set; }
        public string STATUS { get; set; }
        public string PRINT_STATUS { get; set; }
        public string SO_STATUS { get; set; }
        public string STATUS_DESCRIPTION { get; set; }
        public string SHIP_FROM_NAME { get; set; }
        public decimal? SHIPPOINT_ID { get; set; }
        public string SHIPPOINT_NAME { get; set; }
        public string AREA_NAME { get; set; }
        public string INVOICE_TO_NAME { get; set; }
        public string PARTY_NAME { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public decimal? CUSTOMER_ID { get; set; }
        public decimal? ORDER_TYPE { get; set; }
        public string LOAIDH { get; set; }
        public string ORDER_TYPE_FULLNAME { get; set; }
        public int? INVENTORY_ITEM_ID { get; set; }
        public string MA_VT { get; set; }
        public string ITEM_NAME { get; set; }
        public string ITEM_CATEGORY { get; set; }
        public object PACK_TYPE { get; set; }
        public object BAG_TYPE { get; set; }
        public string CHECKPOINT_NAME { get; set; }
        public string TRANSPORT_METHOD_NAME { get; set; }
        public string BLANKET_NUMBER { get; set; }
        public string BLANKET_NUM { get; set; }
        public string BLANKET_DATE { get; set; }
        public object XA_NTM { get; set; }
        public string DVT { get; set; }
        public object PAYMENT_METHOD { get; set; }
        public object CUSTOMER_ACCOUNT { get; set; }
        public object GHI_CHU { get; set; }
        public object PROMO_FLAG { get; set; }
        public object RECEIVER_ID { get; set; }
        public decimal? BLANKET_ID { get; set; }
        public decimal? SHIP_FROM_ORG_ID { get; set; }
        public decimal? CHECKPOINT_ID { get; set; }
        public decimal? TRANSPORT_METHOD_ID { get; set; }
        public decimal? SHIP_TO_ORG_ID { get; set; }
        public decimal? PRICE_LIST_ID { get; set; }
        public object LOT_NUMBER { get; set; }
        public string DOC_SERIES { get; set; }
        public string DOC_TEMPLATE { get; set; }
        public decimal? BOOK_QUANTITY { get; set; }
        public string TIMEIN { get; set; }
        public string TIMEOUT { get; set; }
        public decimal? ORDER_SHIFT { get; set; }
        public string ORDER_LOG { get; set; }
    }

    public class OrderSearchResponseModel
    {
        public int total { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public List<OrderItemResponse> datas { get; set; }
    }
}
