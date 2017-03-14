namespace test8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projectAccessLevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.projectAccesses", "level", c => c.Int(nullable: false));
            DropColumn("dbo.projectAccesses", "projID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.projectAccesses", "projID", c => c.Int(nullable: false));
            DropColumn("dbo.projectAccesses", "level");
        }
    }
}
