using HMXHTD.Services.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMXHTD.Data.DataEntity;
using System.Data.SqlClient;

namespace XHTD_Schedules.Schedules
{
    public class DistributeScaleWeightOrderJob : IJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public DistributeScaleWeightOrderJob(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            await Task.Run(() =>
            {
                DistributeScaleWeightOrderProcess();
            });
        }
        public void DistributeScaleWeightOrderProcess()
        {
          //  log.Info("==============start process DistributeScaleWeightOrderProcess ====================");
            DistributeScaleInOrderProcess();
            DistributeScaleOutOrderProcess();
        }
        public void DistributeScaleInOrderProcess()
        {
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var orderScaleLogs = db.tblScaleLogOperatings.Where(x => x.IsDistributeScaleIn == false && x.WeightScaleIn > 0).ToList();
                    foreach (var orderScaleLog in orderScaleLogs)
                    {
                        //var orders = db.tblStoreOrderOperatings.Where(x=>x.DeliveryCode == orderScaleLog.DeliveryCode || x.DeliveryCodeParent == orderScaleLog.DeliveryCode).ToList();
                        //foreach (var order in orders)
                        //{
                        //    order.
                        //}
                        var sqlUpdate = "UPDATE dbo.tblStoreOrderOperating SET WeightIn = @WeightIn WHERE DeliveryCode = @DeliveryCode OR DeliveryCodeParent = @DeliveryCode";
                        var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@WeightIn", orderScaleLog.WeightScaleIn), new SqlParameter("@DeliveryCode", orderScaleLog.DeliveryCode));
                        if(updateResponse > 0)
                        {
                            orderScaleLog.IsSentScaleIn = true;
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        public void DistributeScaleOutOrderProcess()
        {
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var orderScaleLogs = db.tblScaleLogOperatings.Where(x => x.IsDistributeScaleOut == false && x.WeightScaleOut > 0).ToList();
                    foreach (var orderScaleLog in orderScaleLogs)
                    {
                        var orders = db.tblStoreOrderOperatings.Where(x => x.DeliveryCode == orderScaleLog.DeliveryCode || x.DeliveryCodeParent == orderScaleLog.DeliveryCode).ToList();
                        var totalQuanlity = orders.Sum(x => x.SumNumber);
                        var ratio = orderScaleLog.WeightScaleOut / (double)(totalQuanlity * 1000);
                        foreach (var order in orders)
                        {
                            var sqlUpdateOrder = "UPDATE dbo.tblStoreOrderOperating SET WeightOut = @WeightOut WHERE Id = @Id";
                            var weight = (double)order.SumNumber * ratio;
                            var updateOrderResponse = db.Database.ExecuteSqlCommand(sqlUpdateOrder, new SqlParameter("@WeightOut", weight), new SqlParameter("@Id", order.Id));
                        }
                        
                        orderScaleLog.IsSentScaleIn = true;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

    }
}
