namespace issBlueMetal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m4 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Accountledgers", newName: "AcLedgers");
            DropForeignKey("dbo.PaymentEntries", "accountLedgerId", "dbo.Accountledgers");
            DropForeignKey("dbo.ReciptEntries", "accountLedgerId", "dbo.Accountledgers");
            DropIndex("dbo.PaymentEntries", new[] { "accountLedgerId" });
            DropIndex("dbo.ReciptEntries", new[] { "accountLedgerId" });
            AddColumn("dbo.PaymentEntries", "AcLedger_id", c => c.Int());
            AddColumn("dbo.ReciptEntries", "AcLedger_id", c => c.Int());
            CreateIndex("dbo.PaymentEntries", "AcLedger_id");
            CreateIndex("dbo.ReciptEntries", "AcLedger_id");
            AddForeignKey("dbo.PaymentEntries", "AcLedger_id", "dbo.AcLedgers", "id");
            AddForeignKey("dbo.ReciptEntries", "AcLedger_id", "dbo.AcLedgers", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReciptEntries", "AcLedger_id", "dbo.AcLedgers");
            DropForeignKey("dbo.PaymentEntries", "AcLedger_id", "dbo.AcLedgers");
            DropIndex("dbo.ReciptEntries", new[] { "AcLedger_id" });
            DropIndex("dbo.PaymentEntries", new[] { "AcLedger_id" });
            DropColumn("dbo.ReciptEntries", "AcLedger_id");
            DropColumn("dbo.PaymentEntries", "AcLedger_id");
            CreateIndex("dbo.ReciptEntries", "accountLedgerId");
            CreateIndex("dbo.PaymentEntries", "accountLedgerId");
            AddForeignKey("dbo.ReciptEntries", "accountLedgerId", "dbo.Accountledgers", "id", cascadeDelete: true);
            AddForeignKey("dbo.PaymentEntries", "accountLedgerId", "dbo.Accountledgers", "id", cascadeDelete: true);
            RenameTable(name: "dbo.AcLedgers", newName: "Accountledgers");
        }
    }
}
