using HMXHTD.Data.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD.Services.Services
{
    public interface ILogStoreOrderOperatingService : IBaseService<LogStoreOrderOperating>
    {
        int InsertLog(string cardNo, int step);
        void InsertLogOnly(string vehicle, string cardNo, int step);

    }
    public class LogStoreOrderOperatingService : BaseService<LogStoreOrderOperating>, ILogStoreOrderOperatingService
    {
        public LogStoreOrderOperatingService()
        {
        }

        public int InsertLog(string cardNo, int step)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var vehicle = db.tblStoreOrderOperatings.FirstOrDefault(x => x.CardNo == cardNo);
                var newLog = new LogStoreOrderOperating
                {
                    CardNo = cardNo,
                    Step = step,
                    Vehicle = vehicle?.Vehicle,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                };
                db.LogStoreOrderOperatings.Add(newLog);
                db.SaveChanges();
                return newLog.Id;
            }
        }
        public void InsertLogOnly(string vehicle, string cardNo, int step)
        {
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    db.Database.ExecuteSqlCommand($@"INSERT INTO dbo.LogStoreOrderOperating
                            ( Vehicle ,
                              CardNo ,
                              CreatedOn ,
                              ModifiedOn ,
                              Step
                            )
                    VALUES  ( N'{vehicle}' ,
                              N'{cardNo}' ,
                              GETDATE() ,
                              GETDATE() ,
                              {step}
                            )");
                }
            }
            catch (Exception exx)
            {

            }
        }
    }
}
