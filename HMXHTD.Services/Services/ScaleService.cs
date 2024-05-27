using HMXHTD.Data.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD.Services.Services
{
    public interface IScaleService : IBaseService<tblScaleOperating>
    {
        bool UpdateWhenConfirmVehicle(string scaleCode, string deliveryCode, string vehicle, bool scaleIn, bool scaleOut);
        tblScaleOperating GetByScaleCode(string scaleCode);

    }
    public class ScaleService : BaseService<tblScaleOperating>, IScaleService
    {
        public ScaleService()
        {
        }
        public bool UpdateWhenConfirmVehicle(string scaleCode, string deliveryCode, string vehicle, bool scaleIn, bool scaleOut)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var scaleInfo = db.tblScaleOperatings.FirstOrDefault(x=>x.ScaleCode == scaleCode);
                scaleInfo.DeliveryCode = deliveryCode;
                scaleInfo.Vehicle = vehicle;
                scaleInfo.ScaleIn = scaleIn;
                scaleInfo.ScaleOut = scaleOut;
                scaleInfo.IsScaling = true;
                scaleInfo.TimeIn = DateTime.Now;
                scaleInfo.ModifieldOn = DateTime.Now;
                scaleInfo.TouchSensor = true;
                db.SaveChanges();
                return true;
            }
        }
        public tblScaleOperating GetByScaleCode(string scaleCode)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var scaleInfo = db.tblScaleOperatings.FirstOrDefault(x=>x.ScaleCode.Equals(scaleCode));
                return scaleInfo;
            }
        }
    }
}
