// <copyright file="202301142316082_UpdateProductTable.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bids", "Auction_Id", "dbo.Auctions");
            DropPrimaryKey("dbo.Auctions");
            AlterColumn("dbo.Auctions", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Auctions", "Id");
            AddForeignKey("dbo.Bids", "Auction_Id", "dbo.Auctions", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bids", "Auction_Id", "dbo.Auctions");
            DropPrimaryKey("dbo.Auctions");
            AlterColumn("dbo.Auctions", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Auctions", "Id");
            AddForeignKey("dbo.Bids", "Auction_Id", "dbo.Auctions", "Id", cascadeDelete: true);
        }
    }
}
