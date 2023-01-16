// <copyright file="202301151030154_Initial.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Initial migration that creates the database.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class Initial : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            this.CreateTable(
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

            this.CreateTable(
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

            this.CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            this.CreateTable(
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

            this.CreateTable(
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

            this.CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);

            this.CreateTable(
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

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            this.DropForeignKey("dbo.Auctions", "Product_Id", "dbo.Products");
            this.DropForeignKey("dbo.Bids", "Bidder_Id", "dbo.Users");
            this.DropForeignKey("dbo.UserScores", "ScoredUser_Id", "dbo.Users");
            this.DropForeignKey("dbo.Products", "Offerer_Id", "dbo.Users");
            this.DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            this.DropForeignKey("dbo.ParentChildCategory", "ParentId", "dbo.Categories");
            this.DropForeignKey("dbo.ParentChildCategory", "ChildId", "dbo.Categories");
            this.DropForeignKey("dbo.UserScores", "ScoringUser_Id", "dbo.Users");
            this.DropForeignKey("dbo.Bids", "Auction_Id", "dbo.Auctions");
            this.DropIndex("dbo.ParentChildCategory", new[] { "ParentId" });
            this.DropIndex("dbo.ParentChildCategory", new[] { "ChildId" });
            this.DropIndex("dbo.Products", new[] { "Offerer_Id" });
            this.DropIndex("dbo.Products", new[] { "Category_Id" });
            this.DropIndex("dbo.UserScores", new[] { "ScoredUser_Id" });
            this.DropIndex("dbo.UserScores", new[] { "ScoringUser_Id" });
            this.DropIndex("dbo.Bids", new[] { "Bidder_Id" });
            this.DropIndex("dbo.Bids", new[] { "Auction_Id" });
            this.DropIndex("dbo.Auctions", new[] { "Product_Id" });
            this.DropTable("dbo.ParentChildCategory");
            this.DropTable("dbo.Categories");
            this.DropTable("dbo.Products");
            this.DropTable("dbo.UserScores");
            this.DropTable("dbo.Users");
            this.DropTable("dbo.Bids");
            this.DropTable("dbo.Auctions");
        }
    }
}
