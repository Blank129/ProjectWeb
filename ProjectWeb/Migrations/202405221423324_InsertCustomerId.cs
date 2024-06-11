namespace ProjectWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertCustomerId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "CustomerId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "CustomerId");
        }
    }
}
