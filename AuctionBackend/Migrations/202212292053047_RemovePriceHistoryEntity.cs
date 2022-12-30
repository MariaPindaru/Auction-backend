// <copyright file="202212292053047_RemovePriceHistoryEntity.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// RemovePriceHistoryEntity.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class RemovePriceHistoryEntity : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            this.DropForeignKey("dbo.Auctions", "Offerer_Id", "dbo.Users");
            this.DropForeignKey("dbo.Auctions", "Product_Id", "dbo.Products");
            this.DropIndex("dbo.Auctions", new[] { "Offerer_Id" });
            this.DropIndex("dbo.Auctions", new[] { "Product_Id" });
            this.AddColumn("dbo.Auctions", "CurrentPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            this.AlterColumn("dbo.Auctions", "Offerer_Id", c => c.Int(nullable: false));
            this.AlterColumn("dbo.Auctions", "Product_Id", c => c.Int(nullable: false));
            this.CreateIndex("dbo.Auctions", "Offerer_Id");
            this.CreateIndex("dbo.Auctions", "Product_Id");
            this.AddForeignKey("dbo.Auctions", "Offerer_Id", "dbo.Users", "Id", cascadeDelete: true);
            this.AddForeignKey("dbo.Auctions", "Product_Id", "dbo.Products", "Id", cascadeDelete: true);
            this.DropTable("dbo.PriceHistories");
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            this.CreateTable(
                "dbo.PriceHistories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                })
                .PrimaryKey(t => t.Id);

            this.DropForeignKey("dbo.Auctions", "Product_Id", "dbo.Products");
            this.DropForeignKey("dbo.Auctions", "Offerer_Id", "dbo.Users");
            this.DropIndex("dbo.Auctions", new[] { "Product_Id" });
            this.DropIndex("dbo.Auctions", new[] { "Offerer_Id" });
            this.AlterColumn("dbo.Auctions", "Product_Id", c => c.Int());
            this.AlterColumn("dbo.Auctions", "Offerer_Id", c => c.Int());
            this.DropColumn("dbo.Auctions", "CurrentPrice");
            this.CreateIndex("dbo.Auctions", "Product_Id");
            this.CreateIndex("dbo.Auctions", "Offerer_Id");
            this.AddForeignKey("dbo.Auctions", "Product_Id", "dbo.Products", "Id");
            this.AddForeignKey("dbo.Auctions", "Offerer_Id", "dbo.Users", "Id");
        }
    }
}
