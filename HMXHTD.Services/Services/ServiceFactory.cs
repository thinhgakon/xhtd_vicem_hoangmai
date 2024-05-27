using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMXHTD.Data.DataEntity;

namespace HMXHTD.Services.Services
{
    public interface IServiceFactory
    {
        IDistributorService Distributor { get; }
        ILogStoreOrderOperatingService LogStoreOrderOperating { get; }
        IStoreOrderOperatingService StoreOrderOperating { get; }
        IRFIDService RFID { get; }
        IVehicleVoiceService VehicleVoice { get; }
        IScaleService Scale { get; }
        ILogScaleService LogScale { get; }
        ICommonService Common { get; }
        INotificationService Notification { get; }
        IConfigOperatingService ConfigOperating { get; }
        IDeviceOperatingService DeviceOperating { get; }
        void Save();
        Task<int> SaveChangesAsync();

    }
    public class ServiceFactory : IServiceFactory
    {
        private HMXuathangtudong_Entities _db;
        private IDistributorService distributorService;
        private ILogStoreOrderOperatingService logStoreOrderOperatingService;
        private IStoreOrderOperatingService storeOrderOperatingService;
        private IRFIDService rfIDService;
        private IVehicleVoiceService vehicleVoiceService;
        private IScaleService scaleService;
        private ILogScaleService logScaleService;
        private ICommonService commonService;
        private INotificationService notificationService;
        private IConfigOperatingService configOperatingService;
        private IDeviceOperatingService deviceOperatingService;
        public ServiceFactory()
        {
            _db = new HMXuathangtudong_Entities();
        }


        public IDistributorService Distributor => distributorService ?? new DistributorService();
        public ILogStoreOrderOperatingService LogStoreOrderOperating => logStoreOrderOperatingService ?? new LogStoreOrderOperatingService();
        public IStoreOrderOperatingService StoreOrderOperating => storeOrderOperatingService ?? new StoreOrderOperatingService();
        public IRFIDService RFID => rfIDService ?? new RFIDService();
        public IVehicleVoiceService VehicleVoice => vehicleVoiceService ?? new VehicleVoiceService();
        public IScaleService Scale => scaleService ?? new ScaleService();
        public ILogScaleService LogScale => logScaleService ?? new LogScaleService();
        public ICommonService Common => commonService ?? new CommonService();
        public INotificationService Notification => notificationService ?? new NotificationService();
        public IConfigOperatingService ConfigOperating => configOperatingService ?? new ConfigOperatingService();
        public IDeviceOperatingService DeviceOperating => deviceOperatingService ?? new DeviceOperatingService();
        public void Save()
        {
            _db.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
