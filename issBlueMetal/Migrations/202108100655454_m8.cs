namespace issBlueMetal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReciptEntries", "customerId", c => c.Int(nullable: false));
            CreateIndex("dbo.ReciptEntries", "customerId");
           // AddForeignKey("dbo.ReciptEntries", "customerId", "dbo.Customers", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReciptEntries", "customerId", "dbo.Customers");
            DropIndex("dbo.ReciptEntries", new[] { "customerId" });
            DropColumn("dbo.ReciptEntries", "customerId");
        }
    }
}
