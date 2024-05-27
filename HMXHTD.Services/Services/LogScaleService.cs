using HMXHTD.Data.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD.Services.Services
{
    public interface ILogScaleService : IBaseService<tblScaleLogOperating>
    {
        bool InsertOrUpdateByDeliveryCode(string deliveryCode, string vehicle, bool IsInScale);
        List<tblScaleLogOperating> GetAllLogByStatus();

    }
    public class LogScaleService : BaseService<tblScaleLogOperating>, ILogScaleService
    {
        public LogScaleService()
        {
        }
        public bool InsertOrUpdateByDeliveryCode(string deliveryCode, string vehicle, bool IsInScale)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var scaleLogInfo = db.tblScaleLogOperatings.FirstOrDefault(x => x.DeliveryCode == deliveryCode && x.IsSynced != true);
                if(scaleLogInfo == null)
                {
                    var newScaleLog = new tblScaleLogOperating
                    {
                        Vehicle = vehicle,
                        DeliveryCode = deliveryCode,
                        CreatedOn = DateTime.Now,
                        ModifieldOn = DateTime.Now,
                        IsSentScaleIn = false,
                        IsSentScaleOut = false,
                        IsDistributeScaleIn = false,
                        IsDistributeScaleOut = false,
                        IsSynced = false
                    };
                    if (IsInScale)
                    {
                        newScaleLog.TimeInScale = DateTime.Now;
                    }
                    else
                    {
                        newScaleLog.TimeOutScale = DateTime.Now;
                    }
                    db.tblScaleLogOperatings.Add(newScaleLog);
                    db.SaveChanges();
                }
                else
                {
                    if (IsInScale)
                    {
                        scaleLogInfo.TimeInScale = DateTime.Now;
                        scaleLogInfo.ModifieldOn = DateTime.Now;
                    }
                    else
                    {
                        scaleLogInfo.TimeOutScale = DateTime.Now;
                        scaleLogInfo.ModifieldOn = DateTime.Now;
                    }
                    db.SaveChanges();
                }
                return true;
            }
        }
        public List<tblScaleLogOperating> GetAllLogByStatus()
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var timeCompare = DateTime.Now.AddDays(-1);
                var scaleLogs = db.tblScaleLogOperatings.Where(x=>x.IsSynced == false && x.CreatedOn > timeCompare).ToList();
                return scaleLogs;
            }
        }
    }
}
