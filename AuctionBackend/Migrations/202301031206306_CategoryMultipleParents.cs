namespace AuctionBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryMultipleParents : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Parent_Id", "dbo.Categories");
            DropIndex("dbo.Categories", new[] { "Parent_Id" });
            CreateTable(
                "dbo.ParentChildCategory",
                c => new
                    {
                        ParentId = c.Int(nullable: false),
                        ChildId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ParentId, t.ChildId })
                .ForeignKey("dbo.Categories", t => t.ParentId)
                .ForeignKey("dbo.Categories", t => t.ChildId)
                .Index(t => t.ParentId)
                .Index(t => t.ChildId);
            
            DropColumn("dbo.Categories", "Parent_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Parent_Id", c => c.Int());
            DropForeignKey("dbo.ParentChildCategory", "ChildId", "dbo.Categories");
            DropForeignKey("dbo.ParentChildCategory", "ParentId", "dbo.Categories");
            DropIndex("dbo.ParentChildCategory", new[] { "ChildId" });
            DropIndex("dbo.ParentChildCategory", new[] { "ParentId" });
            DropTable("dbo.ParentChildCategory");
            CreateIndex("dbo.Categories", "Parent_Id");
            AddForeignKey("dbo.Categories", "Parent_Id", "dbo.Categories", "Id");
        }
    }
}
