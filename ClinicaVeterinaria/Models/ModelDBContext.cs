using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ClinicaVeterinaria.Models
{
    public partial class ModelDBContext : DbContext
    {
        public ModelDBContext()
            : base("name=connessioneDB")
        {
        }

        public virtual DbSet<Animale> Animale { get; set; }
        public virtual DbSet<TipologiaAnimale> TipologiaAnimale { get; set; }
        public virtual DbSet<Utente> Utente { get; set; }
        public virtual DbSet<Visita> Visita { get; set; }
        public virtual DbSet<Ruolo> Ruolo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animale>()
                .HasMany(e => e.Visita)
                .WithRequired(e => e.Animale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipologiaAnimale>()
                .HasMany(e => e.Animale)
                .WithRequired(e => e.TipologiaAnimale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ruolo>()
                .HasMany(e => e.Utente)
                .WithRequired(e => e.Ruolo)
                .WillCascadeOnDelete(false);
        }
    }
}
