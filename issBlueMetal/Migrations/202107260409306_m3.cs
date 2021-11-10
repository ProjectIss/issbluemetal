namespace issBlueMetal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.errorLogs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ErrorDate = c.DateTime(nullable: false),
                        controllerName = c.String(),
                        MethodName = c.String(),
                        ErrorMessage = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.errorLogs");
        }
    }
}
