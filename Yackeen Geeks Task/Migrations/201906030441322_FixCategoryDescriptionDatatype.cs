namespace Yackeen_Geeks_Task.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixCategoryDescriptionDatatype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Description", c => c.Int(nullable: false));
        }
    }
}
