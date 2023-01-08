namespace AuctionBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Auctions", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Bids", "Auction_Id", "dbo.Auctions");
            DropIndex("dbo.Auctions", new[] { "Product_Id" });
            DropPrimaryKey("dbo.Auctions");
            AlterColumn("dbo.Auctions", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Auctions", "Id");
            CreateIndex("dbo.Auctions", "Id");
            AddForeignKey("dbo.Auctions", "Id", "dbo.Products", "Id");
            AddForeignKey("dbo.Bids", "Auction_Id", "dbo.Auctions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bids", "Auction_Id", "dbo.Auctions");
            DropForeignKey("dbo.Auctions", "Id", "dbo.Products");
            DropIndex("dbo.Auctions", new[] { "Id" });
            DropPrimaryKey("dbo.Auctions");
            AlterColumn("dbo.Auctions", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Auctions", "Id");
            RenameColumn(table: "dbo.Auctions", name: "Id", newName: "Product_Id");
            AddColumn("dbo.Auctions", "Id", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Auctions", "Product_Id");
            AddForeignKey("dbo.Bids", "Auction_Id", "dbo.Auctions", "Id");
            AddForeignKey("dbo.Auctions", "Product_Id", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
