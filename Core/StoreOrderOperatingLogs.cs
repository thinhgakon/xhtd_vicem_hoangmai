using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD.Core
{
    public class StoreOrderOperatingLogs
    {
        public  bool InsertLog(string vehicle, string cardNo, int step)
        {
            try
            {
                string sql = $@"INSERT INTO LogStoreOrderOperating(Vehicle,CardNo,CreatedOn, ModifiedOn, Step ) Values(N'{vehicle}',N'{cardNo}','{DateTime.Now}','{DateTime.Now}', {step})";
                return DataUnit.ExeSQLQuery(sql);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertLogByCardNo(string cardNo)
        {
            try
            {
                string sql = $@"INSERT INTO LogStoreOrderOperating(Vehicle,CardNo,CreatedOn, ModifiedOn, Step ) SELECT TOP 1 Vehicle, CardNo, GETDATE(), GETDATE(), Step FROM tblStoreOrderOperating WHERE CardNo = '{cardNo}' ORDER  BY Id DESC";
                return DataUnit.ExeSQLQuery(sql);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
