using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ScanEventWorker.Model;

namespace ScanEventWorker.Database.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DatabaseContext")
        {
            
        }
        
        public DbSet<ParcelScanEventHistory> ParcelScanEvents { get; set; }
        public DbSet<ParcelScanEvent> ParcelScantEventTracker { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}  
