namespace AuctionBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCategoryEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Categories", new[] { "Category_Id" });
            CreateTable(
                "dbo.category_related",
                c => new
                    {
                        CategoryParentId = c.Int(nullable: false),
                        CategoryChildId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CategoryParentId, t.CategoryChildId })
                .ForeignKey("dbo.Categories", t => t.CategoryParentId)
                .ForeignKey("dbo.Categories", t => t.CategoryChildId)
                .Index(t => t.CategoryParentId)
                .Index(t => t.CategoryChildId);
            
            DropColumn("dbo.Categories", "Category_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Category_Id", c => c.Int());
            DropForeignKey("dbo.category_related", "CategoryChildId", "dbo.Categories");
            DropForeignKey("dbo.category_related", "CategoryParentId", "dbo.Categories");
            DropIndex("dbo.category_related", new[] { "CategoryChildId" });
            DropIndex("dbo.category_related", new[] { "CategoryParentId" });
            DropTable("dbo.category_related");
            CreateIndex("dbo.Categories", "Category_Id");
            AddForeignKey("dbo.Categories", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
