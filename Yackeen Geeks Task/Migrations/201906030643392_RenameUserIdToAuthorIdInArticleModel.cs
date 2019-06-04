namespace Yackeen_Geeks_Task.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameUserIdToAuthorIdInArticleModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Articles", name: "UserId", newName: "AuthorId");
            RenameIndex(table: "dbo.Articles", name: "IX_UserId", newName: "IX_AuthorId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Articles", name: "IX_AuthorId", newName: "IX_UserId");
            RenameColumn(table: "dbo.Articles", name: "AuthorId", newName: "UserId");
        }
    }
}
