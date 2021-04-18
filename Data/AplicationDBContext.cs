using Microsoft.EntityFrameworkCore;
using Proyecto_SW_II.Models;

namespace Proyecto_SW_II.Data
{
    public class AplicationDBContext : DbContext
    {
        public AplicationDBContext(DbContextOptions<AplicationDBContext> options)
            : base(options)
        {
        }

        public DbSet<Alquiler> Alquileres { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<CategoriaPelicula> RelacionesCategoriaPelicula { get; set; }
        public DbSet<Compañia> Compañias { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        
    }
}
