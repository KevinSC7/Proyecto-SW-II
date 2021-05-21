using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_SW_II.Models
{
    public class IntermedioCategoriasRelaciones
    {
        public int idpeli { get; set; }

        public List<Categoria> asignadas { get; set; }

        public List<Categoria> noasignadas { get; set; }

        public List<Categoria> todas { get; set; }

        public IntermedioCategoriasRelaciones(List<Categoria> lc, List<CategoriaPelicula> r, int id)
        {
            bool exist;
            asignadas = new List<Categoria>();
            noasignadas = new List<Categoria>();
            todas = new List<Categoria>();
            foreach (var cat in lc)
            {
                exist = false;
                foreach (var rel in r)
                {
                    if(cat.Nombre == rel.categoria.Nombre)
                    {
                        exist = true;
                        asignadas.Add(cat);
                        break;
                    }
                }
                if (exist == false) noasignadas.Add(cat);
                todas.Add(cat);
            }
            idpeli = id;
        }
    }
}
