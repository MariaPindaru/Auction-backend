namespace AuctionBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCurrencyForBid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bids", "Currency", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bids", "Currency");
        }
    }
}
