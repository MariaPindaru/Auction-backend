namespace AuctionBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCategoryRelatedTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.category_related", newName: "CategoryChildCategory");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.CategoryChildCategory", newName: "category_related");
        }
    }
}
