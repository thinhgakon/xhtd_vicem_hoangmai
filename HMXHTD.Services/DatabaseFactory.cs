using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD.Services
{
    public interface IDatabaseFactory
    {
        Database GetDatabase();
    }
    public class DatabaseFactory : IDatabaseFactory
    {
        private readonly string _connectionString;
        private Database _database;

        public DatabaseFactory()
        {
            _connectionString = ConfigurationManager.AppSettings["connectionString"];
        }

        public Database GetDatabase()
        {
            if (_database == null)
            {
                _database = new Database(_connectionString);
            }
            return _database;
        }
    }
}
