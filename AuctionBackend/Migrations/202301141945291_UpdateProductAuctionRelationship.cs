// <copyright file="202301141945291_UpdateProductAuctionRelationship.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductAuctionRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Auctions", "Offerer_Id", "dbo.Users");
            DropForeignKey("dbo.Bids", "Auction_Id", "dbo.Auctions");
            DropIndex("dbo.Auctions", new[] { "Offerer_Id" });
            DropIndex("dbo.Bids", new[] { "Auction_Id" });
            AddColumn("dbo.Products", "Offerer_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Bids", "Auction_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Bids", "Auction_Id");
            CreateIndex("dbo.Products", "Offerer_Id");
            AddForeignKey("dbo.Products", "Offerer_Id", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Bids", "Auction_Id", "dbo.Auctions", "Id", cascadeDelete: false);
            DropColumn("dbo.Auctions", "Offerer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Auctions", "Offerer_Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.Bids", "Auction_Id", "dbo.Auctions");
            DropForeignKey("dbo.Products", "Offerer_Id", "dbo.Users");
            DropIndex("dbo.Products", new[] { "Offerer_Id" });
            DropIndex("dbo.Bids", new[] { "Auction_Id" });
            AlterColumn("dbo.Bids", "Auction_Id", c => c.Int());
            DropColumn("dbo.Products", "Offerer_Id");
            CreateIndex("dbo.Bids", "Auction_Id");
            CreateIndex("dbo.Auctions", "Offerer_Id");
            AddForeignKey("dbo.Bids", "Auction_Id", "dbo.Auctions", "Id");
            AddForeignKey("dbo.Auctions", "Offerer_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
