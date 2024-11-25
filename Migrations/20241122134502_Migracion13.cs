using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto1.Migrations
{
    /// <inheritdoc />
    public partial class Migracion13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoEjercicios_Personas_PersonaID",
                table: "TipoEjercicios");

            migrationBuilder.DropIndex(
                name: "IX_TipoEjercicios_PersonaID",
                table: "TipoEjercicios");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "TipoEjercicios");

            migrationBuilder.DropColumn(
                name: "PersonaID",
                table: "TipoEjercicios");

            migrationBuilder.CreateTable(
                name: "Persona_tipoEjercicio",
                columns: table => new
                {
                    relacionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaID = table.Column<int>(type: "int", nullable: false),
                    TipoEjercicioID = table.Column<int>(type: "int", nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona_tipoEjercicio", x => x.relacionID);
                    table.ForeignKey(
                        name: "FK_Persona_tipoEjercicio_Personas_PersonaID",
                        column: x => x.PersonaID,
                        principalTable: "Personas",
                        principalColumn: "PersonaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Persona_tipoEjercicio_TipoEjercicios_TipoEjercicioID",
                        column: x => x.TipoEjercicioID,
                        principalTable: "TipoEjercicios",
                        principalColumn: "TipoEjercicioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persona_tipoEjercicio_PersonaID",
                table: "Persona_tipoEjercicio",
                column: "PersonaID");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_tipoEjercicio_TipoEjercicioID",
                table: "Persona_tipoEjercicio",
                column: "TipoEjercicioID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persona_tipoEjercicio");

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "TipoEjercicios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PersonaID",
                table: "TipoEjercicios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoEjercicios_PersonaID",
                table: "TipoEjercicios",
                column: "PersonaID");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoEjercicios_Personas_PersonaID",
                table: "TipoEjercicios",
                column: "PersonaID",
                principalTable: "Personas",
                principalColumn: "PersonaID");
        }
    }
}
