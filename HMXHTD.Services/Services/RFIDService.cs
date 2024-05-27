using HMXHTD.Data.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD.Services.Services
{
    public interface IRFIDService : IBaseService<tblRFID>
    {
        bool CheckRFIDByCardNo(string cardno);

    }
    public class RFIDService : BaseService<tblRFID>, IRFIDService
    {
        public RFIDService()
        {
        }
        public bool CheckRFIDByCardNo(string cardno)
        {
            bool isValid = false;
            try
            {
                if (cardno.StartsWith("10") || cardno.StartsWith("20") || cardno.StartsWith("21") || cardno.StartsWith("22") || cardno.StartsWith("23") || cardno.StartsWith("24") || cardno.StartsWith("25"))
                {
                    using (var db = new HMXuathangtudong_Entities())
                    {
                        var checkExists = db.tblRFIDs.Any(x => x.Code == cardno);
                        isValid =  checkExists;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return isValid;
        }
    }
}
