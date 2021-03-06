using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_SW_II.Models
{
    public class Cuenta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre de Usuario")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Contraseña { get; set; }

        [Required]
        [StringLength(24, ErrorMessage = "Cuenta errónea", MinimumLength = 24)]
        public string cuentaBancaria { get; set; }

        public const string EmailValidationRegEx = @"^\s*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*$";

        [Required]
        [RegularExpression(EmailValidationRegEx, ErrorMessage = "Email incorrecto")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        public bool Estado = true;
        public Boolean _Estado
        {
            get
            {
                return Estado;
            }
            set
            {
                Estado = value;
            }
        }
        
        public Usuario Miusuario { get; set; }
    }
}
