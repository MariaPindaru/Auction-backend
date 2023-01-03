// <copyright file="202301022331525_AddCurrencyForBid.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// AddCurrencyForBid.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class AddCurrencyForBid : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            this.AddColumn("dbo.Bids", "Currency", c => c.Int(nullable: false));
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            this.DropColumn("dbo.Bids", "Currency");
        }
    }
}
