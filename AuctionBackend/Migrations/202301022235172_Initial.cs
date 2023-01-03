// <copyright file="202301022235172_Initial.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Initial.
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
                    Offerer_Id = c.Int(nullable: false),
                    Product_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Offerer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Offerer_Id)
                .Index(t => t.Product_Id);

            this.CreateTable(
                "dbo.Bids",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Bidder_Id = c.Int(nullable: false),
                    Auction_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Bidder_Id, cascadeDelete: true)
                .ForeignKey("dbo.Auctions", t => t.Auction_Id)
                .Index(t => t.Bidder_Id)
                .Index(t => t.Auction_Id);

            this.CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Role = c.Int(nullable: false),
                    Score = c.Single(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            this.CreateTable(
                "dbo.Products",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    Description = c.String(nullable: false, maxLength: 500),
                })
                .PrimaryKey(t => t.Id);

            this.CreateTable(
                "dbo.Categories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 30),
                    Parent_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Parent_Id)
                .Index(t => t.Parent_Id);

            this.CreateTable(
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

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            this.DropForeignKey("dbo.Auctions", "Product_Id", "dbo.Products");
            this.DropForeignKey("dbo.CategoryProducts", "Product_Id", "dbo.Products");
            this.DropForeignKey("dbo.CategoryProducts", "Category_Id", "dbo.Categories");
            this.DropForeignKey("dbo.Categories", "Parent_Id", "dbo.Categories");
            this.DropForeignKey("dbo.Auctions", "Offerer_Id", "dbo.Users");
            this.DropForeignKey("dbo.Bids", "Auction_Id", "dbo.Auctions");
            this.DropForeignKey("dbo.Bids", "Bidder_Id", "dbo.Users");
            this.DropIndex("dbo.CategoryProducts", new[] { "Product_Id" });
            this.DropIndex("dbo.CategoryProducts", new[] { "Category_Id" });
            this.DropIndex("dbo.Categories", new[] { "Parent_Id" });
            this.DropIndex("dbo.Bids", new[] { "Auction_Id" });
            this.DropIndex("dbo.Bids", new[] { "Bidder_Id" });
            this.DropIndex("dbo.Auctions", new[] { "Product_Id" });
            this.DropIndex("dbo.Auctions", new[] { "Offerer_Id" });
            this.DropTable("dbo.CategoryProducts");
            this.DropTable("dbo.Categories");
            this.DropTable("dbo.Products");
            this.DropTable("dbo.Users");
            this.DropTable("dbo.Bids");
            this.DropTable("dbo.Auctions");
        }
    }
}
