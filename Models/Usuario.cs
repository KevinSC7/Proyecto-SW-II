using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_SW_II.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression("^([0-9]{8}[A-Z])|[XYZ][0-9]{7}[A-Z]$", ErrorMessage = "DNI no válido")]
        public string Dni { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(200)]
        public string Apellido1 { get; set; }

        [Required]
        [StringLength(200)]
        public string Apellido2 { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "El campo {0} es obligario")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Range(18, 100)]
        public int Edad { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Número de teléfono")]
        public string Telefono { get; set; }

        [StringLength(500)]
        public string Direccion { get; set; }
    }
}
