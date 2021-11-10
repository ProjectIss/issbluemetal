namespace issBlueMetal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        grcNo = c.Int(nullable: false),
                        grcDate = c.DateTime(nullable: false),
                        name = c.String(),
                        phone = c.String(),
                        age = c.String(),
                        adult = c.String(),
                        child = c.String(),
                        arrtime = c.DateTime(nullable: false),
                        roomType = c.String(),
                        roomOption = c.String(),
                        roomNo = c.String(),
                        tariff = c.String(),
                        billNo = c.Int(nullable: false),
                        noofDay = c.Int(nullable: false),
                        total = c.Int(nullable: false),
                        chkdate = c.DateTime(nullable: false),
                        chkTime = c.DateTime(nullable: false),
                        taxAmt = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbos");
        }
    }
}
