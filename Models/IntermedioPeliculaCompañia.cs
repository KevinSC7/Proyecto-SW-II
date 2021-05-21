using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_SW_II.Models
{
    public class IntermedioPeliculaCompañia
    {
        public Pelicula pelicula { get; set; }

        public Compañia compañia { get; set; }

        public IntermedioPeliculaCompañia(Pelicula p, Compañia c)
        {
            pelicula = p;
            compañia = c;
        }
    }
}
