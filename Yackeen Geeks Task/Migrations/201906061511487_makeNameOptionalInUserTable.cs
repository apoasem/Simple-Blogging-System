namespace Yackeen_Geeks_Task.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeNameOptionalInUserTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false));
        }
    }
}
