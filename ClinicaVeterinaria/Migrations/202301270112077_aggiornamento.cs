namespace ClinicaVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aggiornamento : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Utente", "ID_Ruolo", "dbo.Ruolo");
            AddForeignKey("dbo.Utente", "ID_Ruolo", "dbo.Ruolo", "ID_Ruolo");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Utente", "ID_Ruolo", "dbo.Ruolo");
            AddForeignKey("dbo.Utente", "ID_Ruolo", "dbo.Ruolo", "ID_Ruolo", cascadeDelete: true);
        }
    }
}
