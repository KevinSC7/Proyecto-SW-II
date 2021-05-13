using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_SW_II.Models
{
    public class IntermedioCuentaUsuarioRol
    {
        public Usuario usuario { get; set; }
        public Cuenta cuenta { get; set; }
        public Rol rol { get; set; }

        public IntermedioCuentaUsuarioRol(Usuario u, Cuenta c, Rol r)
        {
            usuario = u;
            cuenta = c;
            rol = r;
        }
    }

}
