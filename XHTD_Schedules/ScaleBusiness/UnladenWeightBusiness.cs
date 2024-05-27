using HMXHTD.Data.DataEntity;
using HMXHTD.Services.Services;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace XHTD_Schedules.ScaleBusiness
{
    public class UnladenWeightBusiness
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
     (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly IServiceFactory _serviceFactory;
        public UnladenWeightBusiness(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        public void InsertOrUpdate(string deliveryCode)
        {
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var orderScale = db.tblScaleLogOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode);
                    if (orderScale == null || orderScale.WeightScaleIn < 1) return;
                    var curentOrder = db.tblStoreOrderOperatings.FirstOrDefault(x=>x.DeliveryCode == deliveryCode);
                    var currentVehicle = db.tblVehicles.FirstOrDefault(x=>x.Vehicle == curentOrder.Vehicle && x.IsSetMediumUnladenWeight == false);
                    if(currentVehicle != null && curentOrder != null)
                    {
                        if(currentVehicle.UnladenWeight1 == null || currentVehicle.UnladenWeight1 < 1)
                        {
                            currentVehicle.UnladenWeight1 = (int)orderScale.WeightScaleIn;
                            db.SaveChanges();
                        }else if(currentVehicle.UnladenWeight2 == null ||  currentVehicle.UnladenWeight2 < 1)
                        {
                            currentVehicle.UnladenWeight2 = (int)orderScale.WeightScaleIn;
                            db.SaveChanges();
                        }
                        else if(currentVehicle.UnladenWeight3 == null ||  currentVehicle.UnladenWeight3 < 1)
                        {
                            currentVehicle.UnladenWeight3 = (int)orderScale.WeightScaleIn;
                            db.SaveChanges();
                        }
                        else
                        {
                            currentVehicle.TonnageDefault = (currentVehicle.UnladenWeight1 + currentVehicle.UnladenWeight2 + currentVehicle.UnladenWeight3) / 3;
                            currentVehicle.IsSetMediumUnladenWeight = true;
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
    }
}
