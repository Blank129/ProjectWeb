namespace ProjectWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCategory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Category", "Title", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Category", "Title", c => c.String());
        }
    }
}
