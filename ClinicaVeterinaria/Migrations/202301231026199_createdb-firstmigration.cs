namespace ClinicaVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdbfirstmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animale",
                c => new
                    {
                        ID_Animale = c.Int(nullable: false, identity: true),
                        DataRegistrazione = c.DateTime(nullable: false, storeType: "date"),
                        Nome = c.String(nullable: false, maxLength: 30),
                        ID_TipologiaAnimale = c.Int(nullable: false),
                        ColoreMantello = c.String(nullable: false, maxLength: 30),
                        DataNascita = c.DateTime(storeType: "date"),
                        Microchip = c.Boolean(),
                        NumeroMicrochip = c.String(maxLength: 50),
                        NominativoProprietario = c.String(maxLength: 50),
                        Smarrito = c.Boolean(nullable: false),
                        Foto = c.String(),
                        DataInizioRicovero = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ID_Animale)
                .ForeignKey("dbo.TipologiaAnimale", t => t.ID_TipologiaAnimale)
                .Index(t => t.ID_TipologiaAnimale);
            
            CreateTable(
                "dbo.TipologiaAnimale",
                c => new
                    {
                        ID_TipologiaAnimale = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ID_TipologiaAnimale);
            
            CreateTable(
                "dbo.Visita",
                c => new
                    {
                        ID_Visita = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false, storeType: "date"),
                        Descrizione = c.String(),
                        ID_Animale = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID_Visita)
                .ForeignKey("dbo.Animale", t => t.ID_Animale)
                .Index(t => t.ID_Animale);
            
            CreateTable(
                "dbo.Utente",
                c => new
                    {
                        ID_Utente = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Psw = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID_Utente);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visita", "ID_Animale", "dbo.Animale");
            DropForeignKey("dbo.Animale", "ID_TipologiaAnimale", "dbo.TipologiaAnimale");
            DropIndex("dbo.Visita", new[] { "ID_Animale" });
            DropIndex("dbo.Animale", new[] { "ID_TipologiaAnimale" });
            DropTable("dbo.Utente");
            DropTable("dbo.Visita");
            DropTable("dbo.TipologiaAnimale");
            DropTable("dbo.Animale");
        }
    }
}
