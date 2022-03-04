namespace DivishRam_PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cartable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        Carname = c.String(),
                        Year = c.Int(nullable: false),
                        Color = c.String(),
                    })
                .PrimaryKey(t => t.CarId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cars");
        }
    }
}
