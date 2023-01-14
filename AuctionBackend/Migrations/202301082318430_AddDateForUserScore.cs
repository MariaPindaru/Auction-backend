namespace AuctionBackend.Migrations
{
    using System.Data.Entity.Migrations;

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class AddDateForUserScore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserScores", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserScores", "Date");
        }
    }
}
