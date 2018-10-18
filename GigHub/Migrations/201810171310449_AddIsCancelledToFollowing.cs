namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsCancelledToFollowing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Followings", "IsCancelled", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Followings", "IsCancelled");
        }
    }
}
