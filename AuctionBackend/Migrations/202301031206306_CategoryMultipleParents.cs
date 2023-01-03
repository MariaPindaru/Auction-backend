// <copyright file="202301031206306_CategoryMultipleParents.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// CategoryMultipleParents.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class CategoryMultipleParents : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            this.DropForeignKey("dbo.Categories", "Parent_Id", "dbo.Categories");
            this.DropIndex("dbo.Categories", new[] { "Parent_Id" });
            this.CreateTable(
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

            this.DropColumn("dbo.Categories", "Parent_Id");
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            this.AddColumn("dbo.Categories", "Parent_Id", c => c.Int());
            this.DropForeignKey("dbo.ParentChildCategory", "ChildId", "dbo.Categories");
            this.DropForeignKey("dbo.ParentChildCategory", "ParentId", "dbo.Categories");
            this.DropIndex("dbo.ParentChildCategory", new[] { "ChildId" });
            this.DropIndex("dbo.ParentChildCategory", new[] { "ParentId" });
            this.DropTable("dbo.ParentChildCategory");
            this.CreateIndex("dbo.Categories", "Parent_Id");
            this.AddForeignKey("dbo.Categories", "Parent_Id", "dbo.Categories", "Id");
        }
    }
}
