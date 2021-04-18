using Microsoft.EntityFrameworkCore.Migrations;

namespace Proyecto_SW_II.Migrations
{
    public partial class foreignKeyCuentaUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Cuentas",
                newName: "_Estado");

            migrationBuilder.AddColumn<int>(
                name: "MirolId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MiusuarioId",
                table: "Cuentas",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Pago",
                table: "Alquileres",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_MirolId",
                table: "Usuarios",
                column: "MirolId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_MiusuarioId",
                table: "Cuentas",
                column: "MiusuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuentas_Usuarios_MiusuarioId",
                table: "Cuentas",
                column: "MiusuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_MirolId",
                table: "Usuarios",
                column: "MirolId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuentas_Usuarios_MiusuarioId",
                table: "Cuentas");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_MirolId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_MirolId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Cuentas_MiusuarioId",
                table: "Cuentas");

            migrationBuilder.DropColumn(
                name: "MirolId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "MiusuarioId",
                table: "Cuentas");

            migrationBuilder.RenameColumn(
                name: "_Estado",
                table: "Cuentas",
                newName: "Estado");

            migrationBuilder.AlterColumn<int>(
                name: "Pago",
                table: "Alquileres",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
