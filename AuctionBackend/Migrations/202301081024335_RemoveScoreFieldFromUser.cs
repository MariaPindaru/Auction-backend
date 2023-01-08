namespace AuctionBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveScoreFieldFromUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Score");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Score", c => c.Single(nullable: false));
        }
    }
}
