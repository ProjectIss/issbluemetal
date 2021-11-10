namespace issBlueMetal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReciptEntries", "AcLedger_id", "dbo.AcLedgers");
            DropIndex("dbo.ReciptEntries", new[] { "AcLedger_id" });
            RenameColumn(table: "dbo.ReciptEntries", name: "AcLedger_id", newName: "acLedgerId");
            AlterColumn("dbo.ReciptEntries", "acLedgerId", c => c.Int(nullable: false));
            CreateIndex("dbo.ReciptEntries", "acLedgerId");
            AddForeignKey("dbo.ReciptEntries", "acLedgerId", "dbo.AcLedgers", "id", cascadeDelete: true);
            DropColumn("dbo.ReciptEntries", "accountLedgerId");
            DropColumn("dbo.ReciptEntries", "ledgerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReciptEntries", "ledgerId", c => c.Int(nullable: false));
            AddColumn("dbo.ReciptEntries", "accountLedgerId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ReciptEntries", "acLedgerId", "dbo.AcLedgers");
            DropIndex("dbo.ReciptEntries", new[] { "acLedgerId" });
            AlterColumn("dbo.ReciptEntries", "acLedgerId", c => c.Int());
            RenameColumn(table: "dbo.ReciptEntries", name: "acLedgerId", newName: "AcLedger_id");
            CreateIndex("dbo.ReciptEntries", "AcLedger_id");
            AddForeignKey("dbo.ReciptEntries", "AcLedger_id", "dbo.AcLedgers", "id");
        }
    }
}
