namespace AuctionBackend.Migrations
{
    using System.Data.Entity.Migrations;

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal sealed class Configuration : DbMigrationsConfiguration<AuctionBackend.DataLayer.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AuctionBackend.DataLayer.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
