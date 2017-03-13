namespace test8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeInput : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.timeLogs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        user = c.String(),
                        time = c.Double(nullable: false),
                        date = c.DateTime(nullable: false),
                        Project_id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Projects", t => t.Project_id)
                .Index(t => t.Project_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.timeLogs", "Project_id", "dbo.Projects");
            DropIndex("dbo.timeLogs", new[] { "Project_id" });
            DropTable("dbo.timeLogs");
        }
    }
}
