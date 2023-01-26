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

        [Display(Name = "Data Registrazione")]
        [DisplayFormat(DataFormatString ="{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DataRegistrazione { get; set; }

        [Required]
        [StringLength(30)]
        public string Nome { get; set; }

        [Display(Name = "Tipo")]
        public int ID_TipologiaAnimale { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name ="Colore")]
        public string ColoreMantello { get; set; }

        [Column(TypeName = "date")]

        [Display(Name = "Data Nascita")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? DataNascita { get; set; }

        [Display(Name = "Possiede il chip")]
        public bool Microchip { get; set; }

        [StringLength(50)]

        [Display(Name = "Nr. Chip")]
        public string NumeroMicrochip { get; set; }

        [StringLength(50)]

        [Display(Name = "Proprietario")]
        public string NominativoProprietario { get; set; }

        public bool Smarrito { get; set; }

        public string Foto { get; set; }

        [Column(TypeName = "date")]

        [Display(Name = "Data Ricovero")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? DataInizioRicovero { get; set; }

        public virtual TipologiaAnimale TipologiaAnimale { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visita> Visita { get; set; }
    }
}
