using Microsoft.EntityFrameworkCore.Migrations;

namespace Proyecto_SW_II.Migrations
{
    public partial class cambiosCategoriaPelicula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "categoriaId",
                table: "RelacionesCategoriaPelicula",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "peliculaId",
                table: "RelacionesCategoriaPelicula",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RelacionesCategoriaPelicula_categoriaId",
                table: "RelacionesCategoriaPelicula",
                column: "categoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_RelacionesCategoriaPelicula_peliculaId",
                table: "RelacionesCategoriaPelicula",
                column: "peliculaId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelacionesCategoriaPelicula_Categorias_categoriaId",
                table: "RelacionesCategoriaPelicula",
                column: "categoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RelacionesCategoriaPelicula_Peliculas_peliculaId",
                table: "RelacionesCategoriaPelicula",
                column: "peliculaId",
                principalTable: "Peliculas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelacionesCategoriaPelicula_Categorias_categoriaId",
                table: "RelacionesCategoriaPelicula");

            migrationBuilder.DropForeignKey(
                name: "FK_RelacionesCategoriaPelicula_Peliculas_peliculaId",
                table: "RelacionesCategoriaPelicula");

            migrationBuilder.DropIndex(
                name: "IX_RelacionesCategoriaPelicula_categoriaId",
                table: "RelacionesCategoriaPelicula");

            migrationBuilder.DropIndex(
                name: "IX_RelacionesCategoriaPelicula_peliculaId",
                table: "RelacionesCategoriaPelicula");

            migrationBuilder.DropColumn(
                name: "categoriaId",
                table: "RelacionesCategoriaPelicula");

            migrationBuilder.DropColumn(
                name: "peliculaId",
                table: "RelacionesCategoriaPelicula");
        }
    }
}
