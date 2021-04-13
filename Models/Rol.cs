using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_SW_II.Models
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }

        public char TipoUsuario { get; set; }

        public string Descripcion { get; set; }
    }
}
