namespace ScanEventWorker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ScanEvent");
        }
    }
}
