namespace ClinicaVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class microchipboolnotnull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Animale", "Microchip", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Animale", "Microchip", c => c.Boolean());
        }
    }
}
