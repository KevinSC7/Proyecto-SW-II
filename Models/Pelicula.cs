using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_SW_II.Models
{
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre de la película")]
        public string Titulo { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaLanzamiento { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Precio { get; set; }

        public string Portada { get; set; }

        public Compañia compañia { get; set; }
    }
}
