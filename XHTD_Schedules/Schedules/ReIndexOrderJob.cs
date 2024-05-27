using HMXHTD.Data.DataEntity;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XHTD_Schedules.SignalRNotification;

namespace XHTD_Schedules.Schedules
{
    public class ReIndexOrderJob : IJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ReIndexOrderJob()
        {
        }
        public async Task Execute(IJobExecutionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            await Task.Run(() =>
            {
                ProcessReIndex();
            });
        }
        public void UpdateCallVehicle()
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var orders = db.tblStoreOrderOperatings.Where(x => x.Step == 4 && x.IndexOrder2 == 0).ToList();
                foreach (var order in orders)
                {
                    var voice = db.tblCallVehicleStatus.FirstOrDefault(x => x.StoreOrderOperatingId == order.Id);
                    if(voice == null)
                    {
                        var newCallVoice = new tblCallVehicleStatu
                        {
                            StoreOrderOperatingId = order.Id,
                            CountTry = 0,
                            CreatedOn = DateTime.Now,
                            ModifiledOn = DateTime.Now,
                            IsDone = false
                        };
                        db.tblCallVehicleStatus.Add(newCallVoice);
                        db.SaveChanges();
                    }
                }
            }
        }
        public void ProcessReIndex()
        {
            ProcessReIndexOrder("PCB40");
            ProcessReIndexOrder("PCB30");
            ProcessReIndexOrder("ROI");
            ProcessReIndexOrder("CLINKER");
        }
        public void ProcessReIndexOrder(string typeProduct)
        {
            try
            {
                var orders = new List<tblStoreOrderOperating>();
                using (var db = new HMXuathangtudong_Entities())
                {
                    orders = db.tblStoreOrderOperatings.Where(x => (x.IndexOrder ?? 0 ) > 0 && (x.IndexOrder2 ?? 0) == 0 && x.TypeProduct.Equals(typeProduct) && x.Step == 1 && (x.DriverUserName ?? "") != "").OrderBy(x=>x.IndexOrder).ToList();
                }
                int index = 0;
                foreach (var order in orders)
                {
                    index = index + 1;
                    UpdateIndexOrder((int)order.OrderId, index);
                }
            }
            catch(Exception ex)
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
                    var orderExist = db.tblStoreOrderOperatings.FirstOrDefault(x => x.OrderId == orderId);
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

    }
}
