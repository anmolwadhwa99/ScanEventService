namespace ScanEventWorker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewColumnScannedDateTimeUtc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParcelScanEventHistory", "ScannedDateTimeUtc", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParcelScanEventHistory", "ScannedDateTimeUtc");
        }
    }
}
