namespace ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Animale")]
    public partial class Animale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Animale()
        {
            Visita = new HashSet<Visita>();
        }

        [Key]
        public int ID_Animale { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataRegistrazione { get; set; }

        [Required]
        [StringLength(30)]
        public string Nome { get; set; }

        public int ID_TipologiaAnimale { get; set; }

        [Required]
        [StringLength(30)]
        public string ColoreMantello { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataNascita { get; set; }

        public bool? Microchip { get; set; }

        [StringLength(50)]
        public string NumeroMicrochip { get; set; }

        [StringLength(50)]
        public string NominativoProprietario { get; set; }

        public bool Smarrito { get; set; }

        public string Foto { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataInizioRicovero { get; set; }

        public virtual TipologiaAnimale TipologiaAnimale { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visita> Visita { get; set; }
    }
}
