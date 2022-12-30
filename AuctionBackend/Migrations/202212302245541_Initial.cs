namespace AuctionBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auctions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Currency = c.Int(nullable: false),
                        StartPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        IsFinished = c.Boolean(nullable: false),
                        Offerer_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Offerer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Offerer_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Role = c.Int(nullable: false),
                        Score = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Bids",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Bidder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Bidder_Id)
                .Index(t => t.Bidder_Id);
            
            CreateTable(
                "dbo.CategoryProducts",
                c => new
                    {
                        Category_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.Product_Id })
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bids", "Bidder_Id", "dbo.Users");
            DropForeignKey("dbo.Auctions", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.CategoryProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.CategoryProducts", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Categories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Auctions", "Offerer_Id", "dbo.Users");
            DropIndex("dbo.CategoryProducts", new[] { "Product_Id" });
            DropIndex("dbo.CategoryProducts", new[] { "Category_Id" });
            DropIndex("dbo.Bids", new[] { "Bidder_Id" });
            DropIndex("dbo.Categories", new[] { "Category_Id" });
            DropIndex("dbo.Auctions", new[] { "Product_Id" });
            DropIndex("dbo.Auctions", new[] { "Offerer_Id" });
            DropTable("dbo.CategoryProducts");
            DropTable("dbo.Bids");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Users");
            DropTable("dbo.Auctions");
        }
    }
}
