namespace issBlueMetal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TblGRCs", "status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TblGRCs", "status");
        }
    }
}
