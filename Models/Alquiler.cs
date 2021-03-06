using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_SW_II.Models
{
    public class Alquiler
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Pago { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaComienzo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFin { get; set; }

        public Compañia compañia { get; set; }

        public Cuenta cuenta { get; set; }

        public Pelicula pelicula { get; set; }
    }
}
