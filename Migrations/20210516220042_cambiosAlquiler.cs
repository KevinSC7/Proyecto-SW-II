using Microsoft.EntityFrameworkCore.Migrations;

namespace Proyecto_SW_II.Migrations
{
    public partial class cambiosAlquiler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "peliculaId",
                table: "Alquileres",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alquileres_peliculaId",
                table: "Alquileres",
                column: "peliculaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alquileres_Peliculas_peliculaId",
                table: "Alquileres",
                column: "peliculaId",
                principalTable: "Peliculas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alquileres_Peliculas_peliculaId",
                table: "Alquileres");

            migrationBuilder.DropIndex(
                name: "IX_Alquileres_peliculaId",
                table: "Alquileres");

            migrationBuilder.DropColumn(
                name: "peliculaId",
                table: "Alquileres");
        }
    }
}
