using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_CRUDMascotas.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2026, 3, 24, 23, 9, 2, 814, DateTimeKind.Local).AddTicks(2382));

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "FechaCreacion", "PasswordHash", "PersonaId", "RolId", "Username" },
                values: new object[] { 2, new DateTime(2026, 3, 24, 23, 9, 2, 814, DateTimeKind.Local).AddTicks(2383), "$2a$11$3fDFszUwJ.lHFptWqc85Eu2JD8sxTEeYhawHeh9GiOcydUAReMkAe", null, 2, "user1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2026, 1, 19, 14, 28, 6, 94, DateTimeKind.Local).AddTicks(3778));
        }
    }
}
