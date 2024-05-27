﻿using HMXHTD.Data.DataEntity;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace XHTD_Schedules.Schedules
{
    public class IgnoreCallVehicleAndReIndexJob : IJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public IgnoreCallVehicleAndReIndexJob()
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
                IgnoreVehicleProcess();
            });
        }
        public void IgnoreVehicleProcess()
        {
            //log.Info("==============start process IgnoreVehicleProcess ====================");
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var vehicles = db.tblCallVehicleStatus.Where(x => x.IsDone == false && x.CountTry == 3).ToList();
                    if (vehicles.Count < 1) return;
                    var max = db.tblConfigOperatings.FirstOrDefault(x => x.Code == "MaxReIndex")?.Value ?? 3;

                    foreach (var vehicle in vehicles)
                    {
                        var isOverTime = ((DateTime)vehicle.ModifiledOn).AddMinutes(5) > DateTime.Now;
                        if (!isOverTime) continue;
                        var storeOrderOperating = db.tblStoreOrderOperatings.FirstOrDefault(x => x.Id == vehicle.StoreOrderOperatingId);
                        if (storeOrderOperating == null || storeOrderOperating.Step != 4)
                        {
                            vehicle.IsDone = true;
                            db.SaveChanges();
                        }
                        else
                        {
                            vehicle.IsDone = true;
                            vehicle.LogCall = $@"{vehicle.LogCall} # Quá 5 phút sau gần gọi cuối cùng mà xe không vào, cập nhật lúc {DateTime.Now}";
                            db.SaveChanges();
                            var orderIndexMax = db.tblStoreOrderOperatings.Where(x => (x.Step == 1 || x.Step == 4) && (x.IndexOrder2 ?? 0) == 0 && x.TypeProduct.Equals(storeOrderOperating.TypeProduct)).OrderBy(x => x.IndexOrder)?.Max(x => x.IndexOrder) ?? 0;
                            // ReIndexOrderWhenIgnoreIndex(storeOrderOperating);
                            var newIndex = orderIndexMax < 5 ? (orderIndexMax + 1) : (storeOrderOperating.IndexOrder + 5);
                            storeOrderOperating.CountReindex = (int)storeOrderOperating.CountReindex + 1;
                            storeOrderOperating.IndexOrder = newIndex;
                            storeOrderOperating.IndexOrder1 = newIndex;
                            storeOrderOperating.Step = 1;
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
        public void ReIndexOrderWhenIgnoreIndex(tblStoreOrderOperating storeOrderOperating)
        {
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var orderCurrent = db.tblStoreOrderOperatings.FirstOrDefault(x => x.Id == storeOrderOperating.Id);
                    var orders = db.tblStoreOrderOperatings.Where(x => x.IndexOrder > 0 && (x.IndexOrder2 ?? 0) == 0 && x.TypeProduct.Equals(storeOrderOperating.TypeProduct)).OrderBy(x => x.IndexOrder).ToList();
                    if (orders.Count > 5)
                    {

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
    }
}
