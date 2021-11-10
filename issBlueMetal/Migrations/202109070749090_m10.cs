namespace issBlueMetal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "customerId", c => c.Int(nullable: false));
            AlterColumn("dbo.customerLedgers", "customerId", c => c.Int(nullable: false));
            CreateIndex("dbo.customerLedgers", "customerId");
            CreateIndex("dbo.Sales", "customerId");
            //AddForeignKey("dbo.customerLedgers", "customerId", "dbo.Customers", "id", cascadeDelete: true);
            //AddForeignKey("dbo.Sales", "customerId", "dbo.Customers", "id", cascadeDelete: true);
            DropColumn("dbo.Sales", "customer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sales", "customer", c => c.String());
            DropForeignKey("dbo.Sales", "customerId", "dbo.Customers");
            DropForeignKey("dbo.customerLedgers", "customerId", "dbo.Customers");
            DropIndex("dbo.Sales", new[] { "customerId" });
            DropIndex("dbo.customerLedgers", new[] { "customerId" });
            AlterColumn("dbo.customerLedgers", "customerId", c => c.String());
            DropColumn("dbo.Sales", "customerId");
        }
    }
}
