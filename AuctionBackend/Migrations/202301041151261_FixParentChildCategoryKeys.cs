// <copyright file="202301041151261_FixParentChildCategoryKeys.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// FixParentChildCategoryKeys.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class FixParentChildCategoryKeys : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            this.RenameColumn(table: "dbo.ParentChildCategory", name: "ParentId", newName: "__mig_tmp__0");
            this.RenameColumn(table: "dbo.ParentChildCategory", name: "ChildId", newName: "ParentId");
            this.RenameColumn(table: "dbo.ParentChildCategory", name: "__mig_tmp__0", newName: "ChildId");
            this.RenameIndex(table: "dbo.ParentChildCategory", name: "IX_ParentId", newName: "__mig_tmp__0");
            this.RenameIndex(table: "dbo.ParentChildCategory", name: "IX_ChildId", newName: "IX_ParentId");
            this.RenameIndex(table: "dbo.ParentChildCategory", name: "__mig_tmp__0", newName: "IX_ChildId");
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            this.RenameIndex(table: "dbo.ParentChildCategory", name: "IX_ChildId", newName: "__mig_tmp__0");
            this.RenameIndex(table: "dbo.ParentChildCategory", name: "IX_ParentId", newName: "IX_ChildId");
            this.RenameIndex(table: "dbo.ParentChildCategory", name: "__mig_tmp__0", newName: "IX_ParentId");
            this.RenameColumn(table: "dbo.ParentChildCategory", name: "ChildId", newName: "__mig_tmp__0");
            this.RenameColumn(table: "dbo.ParentChildCategory", name: "ParentId", newName: "ChildId");
            this.RenameColumn(table: "dbo.ParentChildCategory", name: "__mig_tmp__0", newName: "ParentId");
        }
    }
}
