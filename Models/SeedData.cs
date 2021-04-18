using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Proyecto_SW_II.Data;
using System;
using System.Linq;

namespace Proyecto_SW_II.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AplicationDBContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AplicationDBContext>>()))
            {
                // Look for any movies.
                if (context.Usuarios.Any())
                {
                    return;   // DB has been seeded
                }
                context.Roles.AddRange(
                    new Rol
                    {
                        Id = 1,
                        TipoUsuario='A',
                        Descripcion="Administrador: administra categorias, peliculas, cuentas cambio de rol y compañias"
                    },
                    new Rol
                    {
                        Id = 2,
                        TipoUsuario = 'C',
                        Descripcion = "Cliente: administra sus datos, explora y alquila peliculas y administra sus peliculas alquiladas"
                    }
                    );
                /*context.Movie.AddRange(
                    new Movie
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "Romantic Comedy",
                        Price = 7.99M
                    },

                    new Movie
                    {
                        Title = "Ghostbusters ",
                        ReleaseDate = DateTime.Parse("1984-3-13"),
                        Genre = "Comedy",
                        Price = 8.99M
                    },

                    new Movie
                    {
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = "Comedy",
                        Price = 9.99M
                    },

                    new Movie
                    {
                        Title = "Rio Bravo",
                        ReleaseDate = DateTime.Parse("1959-4-15"),
                        Genre = "Western",
                        Price = 3.99M
                    }
                );*/
                context.SaveChanges();
            }
        }
    }
}
