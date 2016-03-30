namespace DDDPoC.Infrastructure.Persistence.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Comment = c.String(nullable: false, maxLength: 500),
                        Score = c.Short(nullable: false),
                        ShopId = c.Guid(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shops", t => t.ShopId, cascadeDelete: true)
                .Index(t => t.ShopId);
            
            CreateTable(
                "dbo.Merchants",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shops",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        MerchantId = c.Guid(nullable: false),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Merchants", t => t.MerchantId, cascadeDelete: true)
                .Index(t => t.MerchantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shops", "MerchantId", "dbo.Merchants");
            DropForeignKey("dbo.Feedbacks", "ShopId", "dbo.Shops");
            DropIndex("dbo.Shops", new[] { "MerchantId" });
            DropIndex("dbo.Feedbacks", new[] { "ShopId" });
            DropTable("dbo.Shops");
            DropTable("dbo.Merchants");
            DropTable("dbo.Feedbacks");
        }
    }
}
