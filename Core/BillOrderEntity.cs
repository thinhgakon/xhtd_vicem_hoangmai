using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD
{
    public class BillOrderEntity
    {
        private string rn;  

        public string Rn
        {
            get { return rn; }
            set { rn = value; }
        }
        private string order_Id;

        public string Order_Id
        {
            get { return order_Id; }
            set { order_Id = value; }
        }
        private string delivery_code;

        public string Delivery_code
        {
            get { return delivery_code; }
            set { delivery_code = value; }
        }
        private string doc_num;

        public string Doc_num
        {
            get { return doc_num; }
            set { doc_num = value; }
        }
        private string order_date;

        public string Order_date
        {
            get { return order_date; }
            set { order_date = value; }
        }
        private string vehice_code;

        public string Vehice_code
        {
            get { return vehice_code; }
            set { vehice_code = value; }
        }
        private string drive_name;

        public string Drive_name
        {
            get { return drive_name; }
            set { drive_name = value; }
        }
        private string drive_info;

        public string Drive_info
        {
            get { return drive_info; }
            set { drive_info = value; }
        }
        private string mooc_code;

        public string Mooc_code
        {
            get { return mooc_code; }
            set { mooc_code = value; }
        }
        private string location_code;

        public string Location_code
        {
            get { return location_code; }
            set { location_code = value; }
        }
        private string order_quantity;

        public string Order_quantity
        {
            get { return order_quantity; }
            set { order_quantity = value; }
        }
        private string uom_code;

        public string Uom_code
        {
            get { return uom_code; }
            set { uom_code = value; }
        }
        private string unit_price;

        public string Unit_price
        {
            get { return unit_price; }
            set { unit_price = value; }
        }
        private string discount;

        public string Discount
        {
            get { return discount; }
            set { discount = value; }
        }
        private string order_amount;

        public string Order_amount
        {
            get { return order_amount; }
            set { order_amount = value; }
        }
        private string currency_code;

        public string Currency_code
        {
            get { return currency_code; }
            set { currency_code = value; }
        }
        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        private string print_status;

        public string Print_status
        {
            get { return print_status; }
            set { print_status = value; }
        }
        private string so_status;

        public string So_status
        {
            get { return so_status; }
            set { so_status = value; }
        }
        private string status_description;

        public string Status_description
        {
            get { return status_description; }
            set { status_description = value; }
        }
        private string ship_from_name;

        public string Ship_from_name
        {
            get { return ship_from_name; }
            set { ship_from_name = value; }
        }
        private string shippoint_id;

        public string Shippoint_id
        {
            get { return shippoint_id; }
            set { shippoint_id = value; }
        }
        private string shippoint_name;

        public string Shippoint_name
        {
            get { return shippoint_name; }
            set { shippoint_name = value; }
        }
        private string area_name;

        public string Area_name
        {
            get { return area_name; }
            set { area_name = value; }
        }
        private string invoice_to_name;

        public string Invoice_to_name
        {
            get { return invoice_to_name; }
            set { invoice_to_name = value; }
        }
        private string party_name;

        public string Party_name
        {
            get { return party_name; }
            set { party_name = value; }
        }
        private string customer_name;

        public string Customer_name
        {
            get { return customer_name; }
            set { customer_name = value; }
        }
        private string customer_id;

        public string Customer_id
        {
            get { return customer_id; }
            set { customer_id = value; }
        }
        private string order_type;

        public string Order_type
        {
            get { return order_type; }
            set { order_type = value; }
        }
        private string loaidh;

        public string Loaidh
        {
            get { return loaidh; }
            set { loaidh = value; }
        }
        private string order_type_fullname;

        public string Order_type_fullname
        {
            get { return order_type_fullname; }
            set { order_type_fullname = value; }
        }
        private string inventory_item_id;

        public string Inventory_item_id
        {
            get { return inventory_item_id; }
            set { inventory_item_id = value; }
        }
        private string ma_vt;

        public string Ma_vt
        {
            get { return ma_vt; }
            set { ma_vt = value; }
        }
        private string item_name;

        public string Item_name
        {
            get { return item_name; }
            set { item_name = value; }
        }
        private string item_category;

        public string Item_category
        {
            get { return item_category; }
            set { item_category = value; }
        }
        private string pack_type;

        public string Pack_type
        {
            get { return pack_type; }
            set { pack_type = value; }
        }
        private string bag_type;

        public string Bag_type
        {
            get { return bag_type; }
            set { bag_type = value; }
        }
        private string checkpoint_name;

        public string Checkpoint_name
        {
            get { return checkpoint_name; }
            set { checkpoint_name = value; }
        }
        private string transport_method_name;

        public string Transport_method_name
        {
            get { return transport_method_name; }
            set { transport_method_name = value; }
        }
        private string blanket_number;

        public string Blanket_number
        {
            get { return blanket_number; }
            set { blanket_number = value; }
        }
        private string blanket_num;

        public string Blanket_num
        {
            get { return blanket_num; }
            set { blanket_num = value; }
        }
        private string blanket_date;

        public string Blanket_date
        {
            get { return blanket_date; }
            set { blanket_date = value; }
        }
        private string xa_ntm;

        public string Xa_ntm
        {
            get { return xa_ntm; }
            set { xa_ntm = value; }
        }
        private string dvt;

        public string Dvt
        {
            get { return dvt; }
            set { dvt = value; }
        }
        private string payment_method;

        public string Payment_method
        {
            get { return payment_method; }
            set { payment_method = value; }
        }
        private string customer_account;

        public string Customer_account
        {
            get { return customer_account; }
            set { customer_account = value; }
        }
        private string ghichu;

        public string Ghichu
        {
            get { return ghichu; }
            set { ghichu = value; }
        }
        private string promo_flag;

        public string Promo_flag
        {
            get { return promo_flag; }
            set { promo_flag = value; }
        }
        private string receiver_id;

        public string Receiver_id
        {
            get { return receiver_id; }
            set { receiver_id = value; }
        }
        private string blanket_id;

        public string Blanket_id
        {
            get { return blanket_id; }
            set { blanket_id = value; }
        }
        private string ship_from_org_id;

        public string Ship_from_org_id
        {
            get { return ship_from_org_id; }
            set { ship_from_org_id = value; }
        }
        private string checkpoint_id;

        public string Checkpoint_id
        {
            get { return checkpoint_id; }
            set { checkpoint_id = value; }
        }
        private string transport_method_id;

        public string Transport_method_id
        {
            get { return transport_method_id; }
            set { transport_method_id = value; }
        }
        private string ship_to_org_id;

        public string Ship_to_org_id
        {
            get { return ship_to_org_id; }
            set { ship_to_org_id = value; }
        }
        private string price_list_id;

        public string Price_list_id
        {
            get { return price_list_id; }
            set { price_list_id = value; }
        }
        private string lot_number;

        public string Lot_number
        {
            get { return lot_number; }
            set { lot_number = value; }
        }
        private string doc_series;

        public string Doc_series
        {
            get { return doc_series; }
            set { doc_series = value; }
        }
        private string doc_template;

        public string Doc_template
        {
            get { return doc_template; }
            set { doc_template = value; }
        }
        private string book_quantity;

        public string Book_quantity
        {
            get { return book_quantity; }
            set { book_quantity = value; }
        }
        private string timeIn;

        public string TimeIn
        {
            get { return timeIn; }
            set { timeIn = value; }
        }
        private string timeOut;

        public string TimeOut
        {
            get { return timeOut; }
            set { timeOut = value; }
        }
        private string order_shift;

        public string Order_shift
        {
            get { return order_shift; }
            set { order_shift = value; }
        }
        private string order_log;

        public string Order_log
        {
            get { return order_log; }
            set { order_log = value; }
        }
    }
}
