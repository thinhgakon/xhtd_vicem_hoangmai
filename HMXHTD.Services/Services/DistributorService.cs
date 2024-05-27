using HMXHTD.Data.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD.Services.Services
{
    public interface IDistributorService : IBaseService<tblDistributor>
    {
        List<tblDistributor> GetListDistributors();
        int InsertLog(string cardNo, int step);
        void InsertLogOnly(string cardNo, int step);

    }
    public class DistributorService : BaseService<tblDistributor>, IDistributorService
    {
        public DistributorService()
        {
        }
        public List<tblDistributor> GetListDistributors()
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var distributors = db.tblDistributors.ToList();
                return distributors;
            }
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
        public void InsertLogOnly(string cardNo, int step)
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
                    VALUES  ( N'' ,
                              N'{cardNo}' ,
                              GETDATE() ,
                              GETDATE() ,
                              {step}
                            )");
            }
        }
    }
}
