namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFollowModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Follows",
                c => new
                    {
                        FollowingId = c.String(nullable: false, maxLength: 128),
                        FollowedId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FollowingId, t.FollowedId })
                .ForeignKey("dbo.AspNetUsers", t => t.FollowedId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.FollowingId)
                .Index(t => t.FollowingId)
                .Index(t => t.FollowedId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Follows", "FollowingId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Follows", "FollowedId", "dbo.AspNetUsers");
            DropIndex("dbo.Follows", new[] { "FollowedId" });
            DropIndex("dbo.Follows", new[] { "FollowingId" });
            DropTable("dbo.Follows");
        }
    }
}
