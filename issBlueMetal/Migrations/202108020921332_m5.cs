namespace issBlueMetal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PaymentEntries", "AcLedger_id", "dbo.AcLedgers");
            DropIndex("dbo.PaymentEntries", new[] { "AcLedger_id" });
            RenameColumn(table: "dbo.PaymentEntries", name: "AcLedger_id", newName: "acLedgerId");
            AlterColumn("dbo.PaymentEntries", "acLedgerId", c => c.Int(nullable: false));
            CreateIndex("dbo.PaymentEntries", "acLedgerId");
            AddForeignKey("dbo.PaymentEntries", "acLedgerId", "dbo.AcLedgers", "id", cascadeDelete: true);
            DropColumn("dbo.PaymentEntries", "accountLedgerId");
            DropColumn("dbo.PaymentEntries", "ledgerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaymentEntries", "ledgerId", c => c.Int(nullable: false));
            AddColumn("dbo.PaymentEntries", "accountLedgerId", c => c.Int(nullable: false));
            DropForeignKey("dbo.PaymentEntries", "acLedgerId", "dbo.AcLedgers");
            DropIndex("dbo.PaymentEntries", new[] { "acLedgerId" });
            AlterColumn("dbo.PaymentEntries", "acLedgerId", c => c.Int());
            RenameColumn(table: "dbo.PaymentEntries", name: "acLedgerId", newName: "AcLedger_id");
            CreateIndex("dbo.PaymentEntries", "AcLedger_id");
            AddForeignKey("dbo.PaymentEntries", "AcLedger_id", "dbo.AcLedgers", "id");
        }
    }
}
