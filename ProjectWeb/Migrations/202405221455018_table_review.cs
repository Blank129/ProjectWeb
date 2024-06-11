namespace ProjectWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class table_review : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_Review",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.String(),
                        UserName = c.String(),
                        FullName = c.String(),
                        Email = c.String(),
                        Content = c.String(),
                        Rate = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Avatar = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tb_Review");
        }
    }
}
