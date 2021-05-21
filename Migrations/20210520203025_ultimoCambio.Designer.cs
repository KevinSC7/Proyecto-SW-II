﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Proyecto_SW_II.Data;

namespace Proyecto_SW_II.Migrations
{
    [DbContext(typeof(AplicationDBContext))]
    [Migration("20210520203025_ultimoCambio")]
    partial class ultimoCambio
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Proyecto_SW_II.Models.Alquiler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaComienzo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Pago")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("compañiaId")
                        .HasColumnType("int");

                    b.Property<int?>("cuentaId")
                        .HasColumnType("int");

                    b.Property<int?>("peliculaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("compañiaId");

                    b.HasIndex("cuentaId");

                    b.HasIndex("peliculaId");

                    b.ToTable("Alquileres");
                });

            modelBuilder.Entity("Proyecto_SW_II.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Proyecto_SW_II.Models.CategoriaPelicula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("categoriaId")
                        .HasColumnType("int");

                    b.Property<int?>("peliculaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("categoriaId");

                    b.HasIndex("peliculaId");

                    b.ToTable("RelacionesCategoriaPelicula");
                });

            modelBuilder.Entity("Proyecto_SW_II.Models.Compañia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Compañias");
                });

            modelBuilder.Entity("Proyecto_SW_II.Models.Cuenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MiusuarioId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("_Estado")
                        .HasColumnType("bit");

                    b.Property<string>("cuentaBancaria")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.HasKey("Id");

                    b.HasIndex("MiusuarioId");

                    b.ToTable("Cuentas");
                });

            modelBuilder.Entity("Proyecto_SW_II.Models.Pelicula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaLanzamiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Portada")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("compañiaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("compañiaId");

                    b.ToTable("Peliculas");
                });

            modelBuilder.Entity("Proyecto_SW_II.Models.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Proyecto_SW_II.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellido1")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Apellido2")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Direccion")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MirolId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MirolId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Proyecto_SW_II.Models.Alquiler", b =>
                {
                    b.HasOne("Proyecto_SW_II.Models.Compañia", "compañia")
                        .WithMany()
                        .HasForeignKey("compañiaId");

                    b.HasOne("Proyecto_SW_II.Models.Cuenta", "cuenta")
                        .WithMany()
                        .HasForeignKey("cuentaId");

                    b.HasOne("Proyecto_SW_II.Models.Pelicula", "pelicula")
                        .WithMany()
                        .HasForeignKey("peliculaId");

                    b.Navigation("compañia");

                    b.Navigation("cuenta");

                    b.Navigation("pelicula");
                });

            modelBuilder.Entity("Proyecto_SW_II.Models.CategoriaPelicula", b =>
                {
                    b.HasOne("Proyecto_SW_II.Models.Categoria", "categoria")
                        .WithMany()
                        .HasForeignKey("categoriaId");

                    b.HasOne("Proyecto_SW_II.Models.Pelicula", "pelicula")
                        .WithMany()
                        .HasForeignKey("peliculaId");

                    b.Navigation("categoria");

                    b.Navigation("pelicula");
                });

            modelBuilder.Entity("Proyecto_SW_II.Models.Cuenta", b =>
                {
                    b.HasOne("Proyecto_SW_II.Models.Usuario", "Miusuario")
                        .WithMany()
                        .HasForeignKey("MiusuarioId");

                    b.Navigation("Miusuario");
                });

            modelBuilder.Entity("Proyecto_SW_II.Models.Pelicula", b =>
                {
                    b.HasOne("Proyecto_SW_II.Models.Compañia", "compañia")
                        .WithMany()
                        .HasForeignKey("compañiaId");

                    b.Navigation("compañia");
                });

            modelBuilder.Entity("Proyecto_SW_II.Models.Usuario", b =>
                {
                    b.HasOne("Proyecto_SW_II.Models.Rol", "Mirol")
                        .WithMany()
                        .HasForeignKey("MirolId");

                    b.Navigation("Mirol");
                });
#pragma warning restore 612, 618
        }
    }
}
