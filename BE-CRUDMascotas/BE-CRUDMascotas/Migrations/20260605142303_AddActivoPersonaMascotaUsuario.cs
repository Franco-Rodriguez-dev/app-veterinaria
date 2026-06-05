using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_CRUDMascotas.Migrations
{
    /// <inheritdoc />
    public partial class AddActivoPersonaMascotaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Personas",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Mascota",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Mascota");
        }
    }
}
