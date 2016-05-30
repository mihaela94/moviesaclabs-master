namespace MoviesACLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlanesAndAirlines : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Airline",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountryOfOrigin = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Plane",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AvailableSeats = c.Int(nullable: false),
                        Model = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Plane");
            DropTable("dbo.Airline");
        }
    }
}
