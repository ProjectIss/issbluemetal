namespace issBlueMetal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoomStatus",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        booking = c.Int(nullable: false),
                        vacancy = c.Int(nullable: false),
                        housKeeping = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RoomStatus");
        }
    }
}
