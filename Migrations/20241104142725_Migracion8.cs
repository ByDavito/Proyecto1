using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto1.Migrations
{
    /// <inheritdoc />
    public partial class Migracion8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonaID",
                table: "TipoEjercicios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonaID",
                table: "Eventos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoEjercicios_PersonaID",
                table: "TipoEjercicios",
                column: "PersonaID");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_PersonaID",
                table: "Eventos",
                column: "PersonaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Personas_PersonaID",
                table: "Eventos",
                column: "PersonaID",
                principalTable: "Personas",
                principalColumn: "PersonaID");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoEjercicios_Personas_PersonaID",
                table: "TipoEjercicios",
                column: "PersonaID",
                principalTable: "Personas",
                principalColumn: "PersonaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Personas_PersonaID",
                table: "Eventos");

            migrationBuilder.DropForeignKey(
                name: "FK_TipoEjercicios_Personas_PersonaID",
                table: "TipoEjercicios");

            migrationBuilder.DropIndex(
                name: "IX_TipoEjercicios_PersonaID",
                table: "TipoEjercicios");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_PersonaID",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "PersonaID",
                table: "TipoEjercicios");

            migrationBuilder.DropColumn(
                name: "PersonaID",
                table: "Eventos");
        }
    }
}
