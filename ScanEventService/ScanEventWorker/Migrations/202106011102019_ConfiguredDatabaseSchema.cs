namespace ScanEventWorker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfiguredDatabaseSchema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParcelScanEventHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventId = c.Int(nullable: false),
                        ParcelId = c.Int(nullable: false),
                        ParcelType = c.Int(nullable: false),
                        CreatedDateTimeUtc = c.DateTime(nullable: false),
                        StatusCode = c.String(),
                        RunId = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.ParcelScanEvent",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastEventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ScanEvent",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        ParcelId = c.Int(nullable: false),
                        ParcelType = c.Int(nullable: false),
                        CreatedDateTimeUtc = c.DateTime(nullable: false),
                        StatusCode = c.String(),
                        RunId = c.String(),
                    })
                .PrimaryKey(t => t.EventId);
            
            DropTable("dbo.ParcelScanEvent");
            DropTable("dbo.ParcelScanEventHistory");
        }
    }
}
