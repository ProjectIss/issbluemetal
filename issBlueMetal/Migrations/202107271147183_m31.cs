namespace issBlueMetal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m31 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        tranNo = c.Int(nullable: false),
                        income = c.Int(nullable: false),
                        expence = c.Int(nullable: false),
                        flag = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                        roomNo = c.String(),
                        particular = c.String(),
                        status = c.String(),
                        user = c.String(),
                        mode = c.String(),
                        invNo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Hotels");
        }
    }
}
