namespace AuctionBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BidRemoveNullBidder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bids", "Bidder_Id", "dbo.Users");
            DropIndex("dbo.Bids", new[] { "Bidder_Id" });
            AlterColumn("dbo.Bids", "Bidder_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Bids", "Bidder_Id");
            AddForeignKey("dbo.Bids", "Bidder_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bids", "Bidder_Id", "dbo.Users");
            DropIndex("dbo.Bids", new[] { "Bidder_Id" });
            AlterColumn("dbo.Bids", "Bidder_Id", c => c.Int());
            CreateIndex("dbo.Bids", "Bidder_Id");
            AddForeignKey("dbo.Bids", "Bidder_Id", "dbo.Users", "Id");
        }
    }
}
