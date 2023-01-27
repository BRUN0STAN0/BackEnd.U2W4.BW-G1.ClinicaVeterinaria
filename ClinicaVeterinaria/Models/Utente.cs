namespace ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Utente")]
    public partial class Utente
    {
        [Key]
        public int ID_Utente { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [Display(Name ="Password")]
        public string Psw { get; set; }

        public int ID_Ruolo { get; set; }

        public virtual Ruolo Ruolo { get; set; }
    }
}
