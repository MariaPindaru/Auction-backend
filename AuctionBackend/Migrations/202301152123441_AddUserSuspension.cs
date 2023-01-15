namespace AuctionBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserSuspension : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSuspensions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSuspensions", "User_Id", "dbo.Users");
            DropIndex("dbo.UserSuspensions", new[] { "User_Id" });
            DropTable("dbo.UserSuspensions");
        }
    }
}
