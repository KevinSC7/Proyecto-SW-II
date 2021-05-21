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

                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                     new Rol
                     {
                         TipoUsuario = 'A',
                         Descripcion = "Administrador: administra categorias, peliculas, cuentas cambio de rol y compañias"
                     },
                     new Rol
                     {
                         TipoUsuario = 'C',
                         Descripcion = "Cliente: administra sus datos, explora y alquila peliculas y administra sus peliculas alquiladas"
                     }
                 );
                    context.SaveChanges();
                }


                /*if (!context.Usuarios.Any())
                {
                    context.Usuarios.Add(new Usuario
                    {
                        Nombre = "ROOT",
                        Apellido1 = "",
                        Apellido2 = "",
                        Direccion = "",
                        Dni = "",
                        Edad = 20,
                        FechaNacimiento = DateTime.Now,
                        Telefono = "987213696",
                        Mirol = context.Roles.Find(1)
                    });
                    context.SaveChanges();
                }
                if (!context.Cuentas.Any())
                {
                    context.Cuentas.Add(
                    new Cuenta
                    {
                        Contraseña = "SOY_ADMIN",
                        Estado = true,
                        Nombre = "ROOT",
                        Miusuario = context.Usuarios.Find(1)
                    });
                    context.SaveChanges();
                }*/
                if (!context.Cuentas.Any())
                {
                    context.Cuentas.Add(
                        new Cuenta
                        {
                            Contraseña = "SOY_ADMIN",
                            Estado = true,
                            Nombre = "ROOT",
                            Email = "root@gmail.com",
                            cuentaBancaria = "",
                            Miusuario = new Usuario
                            {
                                Nombre = "ROOT",
                                Apellido1 = "",
                                Apellido2 = "",
                                Direccion = "",
                                Dni = "",
                                FechaNacimiento = DateTime.Now,
                                Telefono = "987213696",
                                Mirol = context.Roles.Find(1)
                            }
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
