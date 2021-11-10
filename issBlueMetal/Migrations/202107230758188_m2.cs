namespace issBlueMetal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.customerLedgers", "customerId", "dbo.Customers");
            DropIndex("dbo.customerLedgers", new[] { "customerId" });
            AlterColumn("dbo.customerLedgers", "customerId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.customerLedgers", "customerId", c => c.Int(nullable: false));
            CreateIndex("dbo.customerLedgers", "customerId");
            AddForeignKey("dbo.customerLedgers", "customerId", "dbo.Customers", "id", cascadeDelete: true);
        }
    }
}
