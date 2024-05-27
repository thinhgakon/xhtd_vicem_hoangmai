using HMXHTD.Data.DataEntity;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XHTD_WEB.Models;

namespace XHTD_WEB.Controllers
{
    public class StepsController : Controller
    {
        // GET: Steps
        public ActionResult Index()
        {
            var orders = new List<tblStoreOrderOperatingExtension>();
            using (var db = new HMXuathangtudong_Entities())
            {
                var startDate = DateTime.Now.Date;
                // var sqlSelect = $@"SELECT TOP 1000 Step, CardNo, DriverUserName, DriverAccept,* FROM dbo.tblStoreOrderOperating WHERE TimeConfirm1 > @startDate AND ISNULL(DriverUserName,'') != '' AND Step > 0 ORDER BY TimeConfirm1 DESC";
                var sqlSelect = $@"SELECT TOP 1000 s.Step, s.CardNo, s.DriverUserName, s.DriverAccept,s.*, t.IsSentTrough, t.TimeSendTrough ,  c.LogCall
                                    FROM dbo.tblStoreOrderOperating AS s
                                    LEFT JOIN dbo.tblDeliveryCodeTroughOperating AS t ON t.DeliveryCode = s.DeliveryCode
                                    LEFT JOIN dbo.tblCallVehicleStatus AS c ON	c.StoreOrderOperatingId = s.Id
                                    WHERE TimeConfirm1 > @startDate AND ISNULL(DriverUserName,'') != '' AND Step > 0 and ISNULL(IsVoiced,0) = 0
                                    ORDER BY TimeConfirm1 DESC";
                orders = db.Database.SqlQuery<tblStoreOrderOperatingExtension>(sqlSelect, new SqlParameter("@startDate", startDate )).ToList();

            }
            return View(orders);
        }
        public ActionResult OrderDetails(string deliveryCode)
        {
            OrderOracleModel orderModel = new OrderOracleModel();
            double weightNull = 0;
            double weightFull = 0;
            DateTime timeIn;
            DateTime timeOut;
            try
            {
                
                string sqlQuery = "";
                string strConString = System.Configuration.ConfigurationManager.ConnectionStrings["MbfConnOracle"].ConnectionString.ToString();

                sqlQuery = $@"select so.*, cvw.LOADWEIGHTNULL, cvw.LOADWEIGHTFULL,cvw.ITEMNAME as ITEM_NAME, cvw.TIMEIN, cvw.TIMEOUT  from sales_orders so
                         ,cx_vehicle_weight cvw 
                         where so.delivery_code = cvw.delivery_code 
                         and so.VEHICLE_CODE IS NOT NULL
                         and so.DELIVERY_CODE = :DELIVERY_CODE";
                using (OracleConnection connection = new OracleConnection(strConString))
                {
                    OracleCommand Cmd = new OracleCommand(sqlQuery, connection);

                    Cmd.Parameters.Add(new OracleParameter("DELIVERY_CODE", deliveryCode));
                    connection.Open();
                    using (OracleDataReader Rd = Cmd.ExecuteReader())
                    {
                        while (Rd.Read())
                        {
                            Double.TryParse(Rd["LOADWEIGHTNULL"]?.ToString(), out weightNull);
                            Double.TryParse(Rd["LOADWEIGHTFULL"]?.ToString(), out weightFull);
                            DateTime.TryParse(Rd["TIMEIN"]?.ToString(), out timeIn);
                            DateTime.TryParse(Rd["TIMEOUT"]?.ToString(), out timeOut);
                            orderModel.ORDER_ID = Int32.Parse(Rd["ORDER_ID"].ToString());
                            orderModel.STATUS = Rd["STATUS"].ToString();
                            orderModel.ORDER_QUANTITY = Double.Parse(Rd["ORDER_QUANTITY"].ToString());
                            orderModel.DRIVER_NAME = Rd["DRIVER_NAME"].ToString();
                            orderModel.VEHICLE_CODE = Rd["VEHICLE_CODE"].ToString();
                            orderModel.MOOC_CODE = Rd["MOOC_CODE"].ToString();
                            orderModel.DELIVERY_CODE = Rd["DELIVERY_CODE"].ToString();
                            orderModel.CUSTOMER_ID = Int32.Parse(Rd["CUSTOMER_ID"].ToString());
                            orderModel.BOOK_QUANTITY = Double.Parse(Rd["BOOK_QUANTITY"].ToString());
                            orderModel.PRINT_STATUS = Rd["PRINT_STATUS"].ToString();
                            orderModel.LOCATION_CODE = Rd["LOCATION_CODE"].ToString();
                            orderModel.AREA_ID = Int32.Parse(Rd["AREA_ID"].ToString());
                            orderModel.ITEM_NAME = Rd["ITEM_NAME"]?.ToString();
                            orderModel.TIMEIN = timeIn;
                            orderModel.TIMEOUT = timeOut;
                            //orderModel.CUSTOMER_NAME = Rd["CUSTOMER_NAME"]?.ToString();
                            //orderModel.NAME_STORE = Rd["NAME_STORE"]?.ToString();
                            //orderModel.CODE_STORE = Rd["CODE_STORE"]?.ToString();
                            //orderModel.INVENTORY_ITEM_ID = Int32.Parse(Rd["INVENTORY_ITEM_ID"]?.ToString());
                            orderModel.WEIGHTNULL = weightNull;
                            orderModel.WEIGHTFULL = weightFull;
                            break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return View(orderModel);
        }

        public ActionResult Test()
        {
            return View();
        }
    }
    
}