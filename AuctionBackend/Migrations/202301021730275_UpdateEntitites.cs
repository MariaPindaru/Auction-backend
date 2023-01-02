namespace AuctionBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEntitites : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bids", "Auction_Id", c => c.Int());
            CreateIndex("dbo.Bids", "Auction_Id");
            AddForeignKey("dbo.Bids", "Auction_Id", "dbo.Auctions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bids", "Auction_Id", "dbo.Auctions");
            DropIndex("dbo.Bids", new[] { "Auction_Id" });
            DropColumn("dbo.Bids", "Auction_Id");
        }
    }
}
