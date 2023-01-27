namespace ClinicaVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RuoliDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ruolo",
                c => new
                    {
                        ID_Ruolo = c.Int(nullable: false, identity: true),
                        Descrizione = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID_Ruolo);
            
            AddColumn("dbo.Utente", "ID_Ruolo", c => c.Int(nullable: false));
            CreateIndex("dbo.Utente", "ID_Ruolo");
            AddForeignKey("dbo.Utente", "ID_Ruolo", "dbo.Ruolo", "ID_Ruolo", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Utente", "ID_Ruolo", "dbo.Ruolo");
            DropIndex("dbo.Utente", new[] { "ID_Ruolo" });
            DropColumn("dbo.Utente", "ID_Ruolo");
            DropTable("dbo.Ruolo");
        }
    }
}
