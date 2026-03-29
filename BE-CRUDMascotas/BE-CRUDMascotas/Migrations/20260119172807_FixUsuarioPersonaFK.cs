using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_CRUDMascotas.Migrations
{
    /// <inheritdoc />
    public partial class FixUsuarioPersonaFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Personas_PersonasId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_PersonasId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "PersonasId",
                table: "Usuarios");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2026, 1, 19, 14, 28, 6, 94, DateTimeKind.Local).AddTicks(3778));

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PersonaId",
                table: "Usuarios",
                column: "PersonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Personas_PersonaId",
                table: "Usuarios",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Personas_PersonaId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_PersonaId",
                table: "Usuarios");

            migrationBuilder.AddColumn<int>(
                name: "PersonasId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "PersonasId" },
                values: new object[] { new DateTime(2026, 1, 19, 13, 59, 32, 797, DateTimeKind.Local).AddTicks(1251), null });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PersonasId",
                table: "Usuarios",
                column: "PersonasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Personas_PersonasId",
                table: "Usuarios",
                column: "PersonasId",
                principalTable: "Personas",
                principalColumn: "Id");
        }
    }
}
