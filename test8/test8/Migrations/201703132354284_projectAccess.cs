namespace test8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projectAccess : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.projectAccesses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        user = c.String(),
                        projID = c.Int(nullable: false),
                        Project_id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Projects", t => t.Project_id)
                .Index(t => t.Project_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.projectAccesses", "Project_id", "dbo.Projects");
            DropIndex("dbo.projectAccesses", new[] { "Project_id" });
            DropTable("dbo.projectAccesses");
        }
    }
}
