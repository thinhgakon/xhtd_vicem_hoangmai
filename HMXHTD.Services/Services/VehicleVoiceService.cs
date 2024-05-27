using HMXHTD.Data.DataEntity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD.Services.Services
{
    public interface IVehicleVoiceService : IBaseService<tblVehicleVoice>
    {
        bool InsertVehicleVoice(string vehicle);
        tblVehicleVoice GetFirstByCreatedVoice();
        bool UpdateCreatedVoiceById(int id);

    }
    public class VehicleVoiceService : BaseService<tblVehicleVoice>, IVehicleVoiceService
    {
        public VehicleVoiceService()
        {
        }
        public bool InsertVehicleVoice(string vehicle)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                string sql = $@"IF NOT EXISTS ( SELECT  TOP 1 *
                                            FROM    tblVehicleVoice
                                            WHERE   Vehicle = @Vehicle )
                                BEGIN
                                    INSERT INTO dbo.tblVehicleVoice
                                            ( Vehicle ,
                                                CreatedOn ,
                                                IsCreatedVoice ,
                                                Flag
                                            )
                                    VALUES  ( @Vehicle ,
                                                GETDATE() ,
                                                0 ,
                                                0
                                            )
                                END;";
                var response = db.Database.ExecuteSqlCommand(sql, new SqlParameter("@Vehicle", vehicle));
                return response > 0;
            }
        }
        public tblVehicleVoice GetFirstByCreatedVoice()
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                var voice = db.tblVehicleVoices.FirstOrDefault(x=>x.IsCreatedVoice == false);
                return voice;
            }
        }
        public bool UpdateCreatedVoiceById(int id)
        {
            using (var db = new HMXuathangtudong_Entities())
            {
                string sqlUpdate = $@"UPDATE dbo.tblVehicleVoice SET IsCreatedVoice = 1 WHERE Id = @Id";
                var response = db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@Id", id));
                return response > 0;
            }
        }
    }
}
