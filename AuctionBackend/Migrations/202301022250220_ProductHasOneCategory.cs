// <copyright file="202301022250220_ProductHasOneCategory.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// ProductHasOneCategory.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class ProductHasOneCategory : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            this.DropForeignKey("dbo.CategoryProducts", "Category_Id", "dbo.Categories");
            this.DropForeignKey("dbo.CategoryProducts", "Product_Id", "dbo.Products");
            this.DropIndex("dbo.CategoryProducts", new[] { "Category_Id" });
            this.DropIndex("dbo.CategoryProducts", new[] { "Product_Id" });
            this.AddColumn("dbo.Products", "Category_Id", c => c.Int(nullable: false));
            this.CreateIndex("dbo.Products", "Category_Id");
            this.AddForeignKey("dbo.Products", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
            this.DropTable("dbo.CategoryProducts");
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            this.CreateTable(
                "dbo.CategoryProducts",
                c => new
                {
                    Category_Id = c.Int(nullable: false),
                    Product_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Category_Id, t.Product_Id });

            this.DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            this.DropIndex("dbo.Products", new[] { "Category_Id" });
            this.DropColumn("dbo.Products", "Category_Id");
            this.CreateIndex("dbo.CategoryProducts", "Product_Id");
            this.CreateIndex("dbo.CategoryProducts", "Category_Id");
            this.AddForeignKey("dbo.CategoryProducts", "Product_Id", "dbo.Products", "Id", cascadeDelete: true);
            this.AddForeignKey("dbo.CategoryProducts", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
