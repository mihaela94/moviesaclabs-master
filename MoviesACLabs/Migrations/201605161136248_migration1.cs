namespace MoviesACLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Award",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActorId = c.Int(nullable: false),
                        AwardTitle = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actor", t => t.ActorId, cascadeDelete: true)
                .Index(t => t.ActorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Award", "ActorId", "dbo.Actor");
            DropIndex("dbo.Award", new[] { "ActorId" });
            DropTable("dbo.Award");
        }
    }
}
