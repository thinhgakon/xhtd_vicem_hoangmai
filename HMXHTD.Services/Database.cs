using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD.Services
{
    public partial class Database : DbContext, IDatabase
    {


        public Database()
        : base("Name=HMXuathangtudong_Entities")
        {
            InitializePartial();
        }

        public Database(string connectionString)
            : base(connectionString)
        {
            InitializePartial();
        }

        public Database(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model)
            : base(connectionString, model)
        {
            InitializePartial();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            return modelBuilder;
        }
        partial void InitializePartial();
        partial void OnModelCreatingPartial(DbModelBuilder modelBuilder);
    }
    public interface IDatabase : IDisposable
    {
    }
}
