using HMXHTD.Data.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD.Services.Services
{
    public interface IDeviceOperatingService : IBaseService<tblDeviceOperating>
    {
        List<tblDeviceOperating> GetAllDeviceOperating();
        void UpdateStatus(int id);

    }
    public class DeviceOperatingService : BaseService<tblDeviceOperating>, IDeviceOperatingService
    {
        public DeviceOperatingService()
        {
        }
        public List<tblDeviceOperating> GetAllDeviceOperating()
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var devices = db.tblDeviceOperatings.ToList();
                return devices;
            }
        }
        public void UpdateStatus(int id)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var device = db.tblDeviceOperatings.FirstOrDefault(x => x.Id == id);
                device.State = false;
                db.SaveChanges();
            }
        }
    }
}
