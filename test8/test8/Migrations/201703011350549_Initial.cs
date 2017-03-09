namespace test8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "users", c => c.Int(nullable: false));
            AlterColumn("dbo.Projects", "title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "title", c => c.String());
            DropColumn("dbo.Projects", "users");
        }
    }
}
