using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_SW_II.Models
{
    public class CategoriaPelicula
    {
        [Key]
        public int Id { get; set; }

        public Categoria categoria { get; set; }

        public Pelicula pelicula { get; set; }

    }
}
