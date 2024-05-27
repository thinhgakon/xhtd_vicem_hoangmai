using HMXHTD.Data.DataEntity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD.Services.Services
{
    public interface IConfigOperatingService : IBaseService<tblConfigOperating>
    {
        int GetValueByCode(string code);

    }
    public class ConfigOperatingService : BaseService<tblConfigOperating>, IConfigOperatingService
    {
        public ConfigOperatingService()
        {
        }
        public int GetValueByCode(string code)
        {
            var res = 0;
            try
            {
                using (var db = new HMXuathangtudong_Entities())
                {
                    var sqlSelect = $@"SELECT Value FROM dbo.tblConfigOperating WHERE Code = @code";
                    res = db.Database.SqlQuery<int>(sqlSelect, new SqlParameter("@code", code)).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {

            }
            return res;
        }
    }
}
