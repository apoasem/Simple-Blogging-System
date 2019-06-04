namespace Yackeen_Geeks_Task.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeUserIdAndAddVisitorNameToCommentModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "UserId" });
            AddColumn("dbo.Comments", "VisitorName", c => c.String(nullable: false));
            DropColumn("dbo.Comments", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.Comments", "VisitorName");
            CreateIndex("dbo.Comments", "UserId");
            AddForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
