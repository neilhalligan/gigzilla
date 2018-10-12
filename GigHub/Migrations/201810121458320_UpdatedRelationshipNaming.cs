namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedRelationshipNaming : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Follows", "FollowedId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Follows", "FollowingId", "dbo.AspNetUsers");
            DropIndex("dbo.Follows", new[] { "FollowingId" });
            DropIndex("dbo.Follows", new[] { "FollowedId" });
            CreateTable(
                "dbo.Followings",
                c => new
                    {
                        FollowerId = c.String(nullable: false, maxLength: 128),
                        FolloweeId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FollowerId, t.FolloweeId })
                .ForeignKey("dbo.AspNetUsers", t => t.FolloweeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.FollowerId)
                .Index(t => t.FollowerId)
                .Index(t => t.FolloweeId);
            
            DropTable("dbo.Follows");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Follows",
                c => new
                    {
                        FollowingId = c.String(nullable: false, maxLength: 128),
                        FollowedId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FollowingId, t.FollowedId });
            
            DropForeignKey("dbo.Followings", "FollowerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followings", "FolloweeId", "dbo.AspNetUsers");
            DropIndex("dbo.Followings", new[] { "FolloweeId" });
            DropIndex("dbo.Followings", new[] { "FollowerId" });
            DropTable("dbo.Followings");
            CreateIndex("dbo.Follows", "FollowedId");
            CreateIndex("dbo.Follows", "FollowingId");
            AddForeignKey("dbo.Follows", "FollowingId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Follows", "FollowedId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
