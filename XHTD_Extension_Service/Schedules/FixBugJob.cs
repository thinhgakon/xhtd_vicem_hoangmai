using HMXHTD.Data.DataEntity;
using HMXHTD.Services.Services;
using Microsoft.AspNet.SignalR.Client;
using Oracle.DataAccess.Client;
using Quartz;
using RoundRobin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XHTD_Extension_Service.Schedules
{
    public class FixBugJob : IJob
    {
        private HubConnection Connection { get; set; }
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        RoundRobinList<string> roundRobinList = new RoundRobinList<string>(
                    new List<string>{
                        "PCB40", "PCB30","ROI", "CLINKER","XK"
                    }
                );
        public FixBugJob(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            await Task.Run(async () =>
            {
                FixBugProcess();
            });
        }
        public void TestLog()
        {
            try
            {
             //   _serviceFactory.StoreOrderOperating.TestLogService();
            }
            catch (Exception ex)
            {
                var s = ex.Message;
            }
        }
        public void TestRoundRoBin()
        {
            TestRoundRoBin1();
            TestRoundRoBin2();

        }
        public void TestRoundRoBin1()
        {
            try
            {
                
                //the weight of the element 1 will be increase by 2 units
                //  roundRobinList.IncreaseWeight(element: 1, amount: 2);


                Console.WriteLine($"{roundRobinList.Next()},");
                Console.WriteLine($"{roundRobinList.Next()},");
            }
            catch (Exception ex)
            {

            }
        }
        public void TestRoundRoBin2()
        {
            try
            {
               
                Console.WriteLine($"{roundRobinList.Next()},");
                Console.WriteLine($"{roundRobinList.Next()},");
            }
            catch (Exception ex)
            {

            }
        }
        public void ReIndexByTypeProduct(string typeProduct)
        {
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var orders = db.tblStoreOrderOperatings.Where(x => (x.Step == 1 || x.Step == 4) && (x.IndexOrder2 ?? 0) == 0 && x.IndexOrder > 0 && x.TypeProduct.Equals(typeProduct)).OrderBy(x => x.IndexOrder).ToList();

                    int index = 0;
                    foreach (var order in orders)
                    {
                        index = index + 1;
                        UpdateIndexOrder((int)order.OrderId, index);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        public void UpdateIndexOrder(int orderId, int index)
        {
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.OrderId == orderId && (x.IndexOrder2 ?? 0) == 0);
                    if (orderExist == null) return;
                    orderExist.IndexOrder1 = orderExist.IndexOrder;
                    orderExist.IndexOrder = index;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        public void FixBugProcess()
        {
            log.Info("service is running...");
            FixBugClinkerReceived();
        }
        public void FixBugClinkerReceived()
        {
            try
            {
                var orders = new List<tblStoreOrderOperating>();
                var startDate = DateTime.Now.AddDays(-3);
                using (var db = new HMXuathangtudong_Entities())
                {
                    orders = db.tblStoreOrderOperatings.Where(x => x.Step >= 0 && x.Step < 8 && x.OrderDate > startDate).ToList();
                }
                foreach (var order in orders)
                {
                    var isReceived = CheckReceivedByDeliveryCode(order.DeliveryCode);
                    if (isReceived)
                    {
                        _serviceFactory.StoreOrderOperating.UpdateOrderDoneByDeliverycode(order.DeliveryCode);
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

        }
        public void FixBugXKReceived()
        {
            try
            {
                var orders = new List<tblStoreOrderOperating>();
                var startDate = DateTime.Now.AddDays(-3);
                using (var db = new HMXuathangtudong_Entities())
                {
                    orders = db.tblStoreOrderOperatings.Where(x => x.Step >= 0 && x.Step < 8 && x.OrderDate > startDate && x.TypeProduct.Equals("XK")).ToList();
                }
                foreach (var order in orders)
                {
                    var isReceived = CheckReceivedByDeliveryCode(order.DeliveryCode);
                    if (isReceived)
                    {
                        _serviceFactory.StoreOrderOperating.UpdateOrderDoneByDeliverycode(order.DeliveryCode);
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

        }
        public void ProcessOrderReceiving()
        {
            try
            {
                var orders = new List<tblStoreOrderOperating>();
                using (var db = new HMXuathangtudong_Entities())
                {
                    orders = db.tblStoreOrderOperatings.Where(x => x.Step > 0 && x.Step < 8 && (x.DriverUserName ?? "") != "").ToList();
                }
                foreach (var order in orders)
                {
                    var isReceived = CheckReceivedByDeliveryCode(order.DeliveryCode);
                    if (isReceived)
                    {
                        _serviceFactory.StoreOrderOperating.UpdateOrdercompletedByDeliverycode(order.DeliveryCode);
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

        }
        public bool CheckReceivedByDeliveryCode(string deliveryCode)
        {
            double weightNull = 0;
            double weightFull = 0;
            try
            {
                #region Oracle
                string sqlQuery = "";
                string strConString = System.Configuration.ConfigurationManager.ConnectionStrings["MbfConnOracle"].ConnectionString.ToString();

                sqlQuery = $@"select cvw.* from sales_orders so
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
                            break;
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {

            }
            if(weightFull > 0 && weightNull > 0)
            {
                return true;
            }
            return false;
        }
        public void UpdateData()
        {
            try
            {

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
    }
}
