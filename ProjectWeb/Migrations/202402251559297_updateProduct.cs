namespace ProjectWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "CreatedBy", c => c.String());
            AddColumn("dbo.Product", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Product", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Product", "Modifiedby", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Modifiedby");
            DropColumn("dbo.Product", "ModifiedDate");
            DropColumn("dbo.Product", "CreatedDate");
            DropColumn("dbo.Product", "CreatedBy");
        }
    }
}
