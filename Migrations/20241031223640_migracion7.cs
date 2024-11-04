using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto1.Migrations
{
    /// <inheritdoc />
    public partial class migracion7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonaID",
                table: "Lugares",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lugares_PersonaID",
                table: "Lugares",
                column: "PersonaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lugares_Personas_PersonaID",
                table: "Lugares",
                column: "PersonaID",
                principalTable: "Personas",
                principalColumn: "PersonaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lugares_Personas_PersonaID",
                table: "Lugares");

            migrationBuilder.DropIndex(
                name: "IX_Lugares_PersonaID",
                table: "Lugares");

            migrationBuilder.DropColumn(
                name: "PersonaID",
                table: "Lugares");
        }
    }
}
