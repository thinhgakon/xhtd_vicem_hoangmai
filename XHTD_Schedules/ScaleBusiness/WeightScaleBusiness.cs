using HMXHTD.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMXHTD.Data.DataEntity;
using XHTD_Schedules.ApiScales;
using Autofac;
using System.Data.SqlClient;

namespace XHTD_Schedules.ScaleBusiness
{
    public class WeightScaleBusiness
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public WeightScaleBusiness(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        public void UpdateWeight(string deliveryCode, int weight, bool scaleIn)
        {
            try
            {
                var cardno = _serviceFactory.StoreOrderOperating.GetCardNoByDeliveryCode(deliveryCode);
                var orders = _serviceFactory.StoreOrderOperating.GetAllOrderReceivingByCardNo(cardno);
                using (var db = new HMXuathangtudong_Entities())
                {
                    if (scaleIn)
                    {
                        foreach (var order in orders)
                        {
                            var sqlUpdate = "UPDATE dbo.tblStoreOrderOperating SET WeightIn = @WeightIn, WeightInTime = GETDATE() WHERE DeliveryCode = @DeliveryCode";
                            var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@WeightIn", weight), new SqlParameter("@DeliveryCode", order.DeliveryCode));
                        }
                      
                    }
                    else
                    {
                        if(orders.Count == 1)
                        {
                            var sqlUpdate = "UPDATE dbo.tblStoreOrderOperating SET WeightOut = @WeightOut, WeightOutTime = GETDATE() WHERE DeliveryCode = @DeliveryCode";
                            var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@WeightOut", weight), new SqlParameter("@DeliveryCode", deliveryCode));
                        }
                        else
                        {
                            var sumQuantity = orders.Sum(x => x.SumNumber);
                            foreach (var order in orders)
                            {
                                var weightInDivide = (int)(((sumQuantity - order.SumNumber) / sumQuantity) * order.WeightIn);
                                var weightByOrder = (int)((order.SumNumber/sumQuantity) * weight) + weightInDivide;
                                var sqlUpdate = "UPDATE dbo.tblStoreOrderOperating SET WeightOut = @WeightOut, WeightOutTime = GETDATE() WHERE DeliveryCode = @DeliveryCode";
                                var updateResponse = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@WeightOut", weightByOrder), new SqlParameter("@DeliveryCode", order.DeliveryCode));
                            }
                        }
                        
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
