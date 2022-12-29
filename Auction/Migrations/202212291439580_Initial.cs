// <copyright file="202212291439580_Initial.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace Auction.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Initial migration.
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
                    IsFinished = c.Boolean(nullable: false),
                    User_Id = c.Int(),
                    User_Id1 = c.Int(),
                    Offerer_Id = c.Int(),
                    Product_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.User_Id1)
                .ForeignKey("dbo.Users", t => t.Offerer_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1)
                .Index(t => t.Offerer_Id)
                .Index(t => t.Product_Id);

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
                "dbo.Products",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    Description = c.String(nullable: false, maxLength: 100),
                })
                .PrimaryKey(t => t.Id);

            this.CreateTable(
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

            this.CreateTable(
                "dbo.PriceHistories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                })
                .PrimaryKey(t => t.Id);

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
            this.DropForeignKey("dbo.Categories", "Category_Id", "dbo.Categories");
            this.DropForeignKey("dbo.Auctions", "Offerer_Id", "dbo.Users");
            this.DropForeignKey("dbo.Auctions", "User_Id1", "dbo.Users");
            this.DropForeignKey("dbo.Auctions", "User_Id", "dbo.Users");
            this.DropIndex("dbo.CategoryProducts", new[] { "Product_Id" });
            this.DropIndex("dbo.CategoryProducts", new[] { "Category_Id" });
            this.DropIndex("dbo.Categories", new[] { "Category_Id" });
            this.DropIndex("dbo.Auctions", new[] { "Product_Id" });
            this.DropIndex("dbo.Auctions", new[] { "Offerer_Id" });
            this.DropIndex("dbo.Auctions", new[] { "User_Id1" });
            this.DropIndex("dbo.Auctions", new[] { "User_Id" });
            this.DropTable("dbo.CategoryProducts");
            this.DropTable("dbo.PriceHistories");
            this.DropTable("dbo.Categories");
            this.DropTable("dbo.Products");
            this.DropTable("dbo.Users");
            this.DropTable("dbo.Auctions");
        }
    }
}
