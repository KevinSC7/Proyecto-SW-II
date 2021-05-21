using Microsoft.EntityFrameworkCore.Migrations;

namespace Proyecto_SW_II.Migrations
{
    public partial class ultimoCambio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Cuentas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "cuentaBancaria",
                table: "Cuentas",
                type: "nvarchar(24)",
                maxLength: 24,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Cuentas");

            migrationBuilder.DropColumn(
                name: "cuentaBancaria",
                table: "Cuentas");
        }
    }
}
