namespace issBlueMetal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomStatus", "Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoomStatus", "Date");
        }
    }
}
