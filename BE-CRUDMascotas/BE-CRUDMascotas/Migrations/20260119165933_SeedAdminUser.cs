using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_CRUDMascotas.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Usuarios",
                newName: "PasswordHash");

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "FechaCreacion", "PasswordHash", "PersonaId", "PersonasId", "RolId", "Username" },
                values: new object[] { 1, new DateTime(2026, 1, 19, 13, 59, 32, 797, DateTimeKind.Local).AddTicks(1251), "$2a$11$7g9hjhGe95vuvupJeCA4OemI8inSrfFFatgDwEoyxduWQJgyk3k6q", null, null, 1, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Usuarios",
                newName: "Password");
        }
    }
}
