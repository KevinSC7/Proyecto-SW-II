using Microsoft.EntityFrameworkCore.Migrations;

namespace Proyecto_SW_II.Migrations
{
    public partial class UpdatePeliculaAlquiler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "compañiaId",
                table: "Peliculas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "compañiaId",
                table: "Alquileres",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cuentaId",
                table: "Alquileres",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Peliculas_compañiaId",
                table: "Peliculas",
                column: "compañiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Alquileres_compañiaId",
                table: "Alquileres",
                column: "compañiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Alquileres_cuentaId",
                table: "Alquileres",
                column: "cuentaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alquileres_Compañias_compañiaId",
                table: "Alquileres",
                column: "compañiaId",
                principalTable: "Compañias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Alquileres_Cuentas_cuentaId",
                table: "Alquileres",
                column: "cuentaId",
                principalTable: "Cuentas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Peliculas_Compañias_compañiaId",
                table: "Peliculas",
                column: "compañiaId",
                principalTable: "Compañias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alquileres_Compañias_compañiaId",
                table: "Alquileres");

            migrationBuilder.DropForeignKey(
                name: "FK_Alquileres_Cuentas_cuentaId",
                table: "Alquileres");

            migrationBuilder.DropForeignKey(
                name: "FK_Peliculas_Compañias_compañiaId",
                table: "Peliculas");

            migrationBuilder.DropIndex(
                name: "IX_Peliculas_compañiaId",
                table: "Peliculas");

            migrationBuilder.DropIndex(
                name: "IX_Alquileres_compañiaId",
                table: "Alquileres");

            migrationBuilder.DropIndex(
                name: "IX_Alquileres_cuentaId",
                table: "Alquileres");

            migrationBuilder.DropColumn(
                name: "compañiaId",
                table: "Peliculas");

            migrationBuilder.DropColumn(
                name: "compañiaId",
                table: "Alquileres");

            migrationBuilder.DropColumn(
                name: "cuentaId",
                table: "Alquileres");
        }
    }
}
