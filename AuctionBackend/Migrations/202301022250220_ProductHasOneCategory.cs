namespace AuctionBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductHasOneCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CategoryProducts", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.CategoryProducts", "Product_Id", "dbo.Products");
            DropIndex("dbo.CategoryProducts", new[] { "Category_Id" });
            DropIndex("dbo.CategoryProducts", new[] { "Product_Id" });
            AddColumn("dbo.Products", "Category_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "Category_Id");
            AddForeignKey("dbo.Products", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
            DropTable("dbo.CategoryProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CategoryProducts",
                c => new
                    {
                        Category_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.Product_Id });
            
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropColumn("dbo.Products", "Category_Id");
            CreateIndex("dbo.CategoryProducts", "Product_Id");
            CreateIndex("dbo.CategoryProducts", "Category_Id");
            AddForeignKey("dbo.CategoryProducts", "Product_Id", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CategoryProducts", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
