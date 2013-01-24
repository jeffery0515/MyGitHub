using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xktec.hqfq.Entity;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace Xktec.hqfq.EfDal
{
   
        public class EfDbContext : DbContext
        {
            public EfDbContext()
                : base("working")
            {
                new DbInit().InitializeDatabase(this);
            }
            public DbSet<UserInfo> Users { get; set; }
            public DbSet<LineInfo> Lines { get; set; }
            public DbSet<SalerInfo> Salers { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Itinerary> Itinerary { get; set; }
            public DbSet<Image> Images { get; set; }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
               
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                base.OnModelCreating(modelBuilder);
            }

        }
        public class DbInit : DropCreateDatabaseIfModelChanges<DbContext>
        {
            public DbInit()
            {

            }
        }
    
    
}