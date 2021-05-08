using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_SW_II.Models
{
    public class IntermedioCuentaUsuarioRol
    {
        public Usuario usuario { get; set; }
        public Cuenta cuenta { get; set; }
        public Rol rol { get; set; }
    }
}
