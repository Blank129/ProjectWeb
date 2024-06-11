namespace ProjectWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class table_review_edit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Review", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.tb_Review", "ProductId");
            AddForeignKey("dbo.tb_Review", "ProductId", "dbo.Product", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_Review", "ProductId", "dbo.Product");
            DropIndex("dbo.tb_Review", new[] { "ProductId" });
            AlterColumn("dbo.tb_Review", "ProductId", c => c.String());
        }
    }
}
