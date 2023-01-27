using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClinicaVeterinaria.Models
{
    [Table("Ruolo")]
    public partial class Ruolo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_Ruolo { get; set; }

        [Required]
        public string Descrizione { get; set; }

        public virtual ICollection<Utente> Utente { get; set; }
    }
}