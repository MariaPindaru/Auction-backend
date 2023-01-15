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
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Bids",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.Int(nullable: false),
                        Auction_Id = c.Int(nullable: false),
                        Bidder_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Auctions", t => t.Auction_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Bidder_Id, cascadeDelete: true)
                .Index(t => t.Auction_Id)
                .Index(t => t.Bidder_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserScores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        ScoringUser_Id = c.Int(nullable: false),
                        ScoredUser_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ScoringUser_Id)
                .ForeignKey("dbo.Users", t => t.ScoredUser_Id)
                .Index(t => t.ScoringUser_Id)
                .Index(t => t.ScoredUser_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 500),
                        Category_Id = c.Int(nullable: false),
                        Offerer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Offerer_Id, cascadeDelete: false)
                .Index(t => t.Category_Id)
                .Index(t => t.Offerer_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParentChildCategory",
                c => new
                    {
                        ChildId = c.Int(nullable: false),
                        ParentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ChildId, t.ParentId })
                .ForeignKey("dbo.Categories", t => t.ChildId)
                .ForeignKey("dbo.Categories", t => t.ParentId)
                .Index(t => t.ChildId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Auctions", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Bids", "Bidder_Id", "dbo.Users");
            DropForeignKey("dbo.UserScores", "ScoredUser_Id", "dbo.Users");
            DropForeignKey("dbo.Products", "Offerer_Id", "dbo.Users");
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.ParentChildCategory", "ParentId", "dbo.Categories");
            DropForeignKey("dbo.ParentChildCategory", "ChildId", "dbo.Categories");
            DropForeignKey("dbo.UserScores", "ScoringUser_Id", "dbo.Users");
            DropForeignKey("dbo.Bids", "Auction_Id", "dbo.Auctions");
            DropIndex("dbo.ParentChildCategory", new[] { "ParentId" });
            DropIndex("dbo.ParentChildCategory", new[] { "ChildId" });
            DropIndex("dbo.Products", new[] { "Offerer_Id" });
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropIndex("dbo.UserScores", new[] { "ScoredUser_Id" });
            DropIndex("dbo.UserScores", new[] { "ScoringUser_Id" });
            DropIndex("dbo.Bids", new[] { "Bidder_Id" });
            DropIndex("dbo.Bids", new[] { "Auction_Id" });
            DropIndex("dbo.Auctions", new[] { "Product_Id" });
            DropTable("dbo.ParentChildCategory");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.UserScores");
            DropTable("dbo.Users");
            DropTable("dbo.Bids");
            DropTable("dbo.Auctions");
        }
    }
}
