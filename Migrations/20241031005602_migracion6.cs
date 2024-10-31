using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto1.Migrations
{
    /// <inheritdoc />
    public partial class migracion6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonaID",
                table: "EjerciciosFisicos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    PersonaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Altura = table.Column<float>(type: "real", nullable: false),
                    Peso = table.Column<float>(type: "real", nullable: false),
                    Sexo = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CuentaID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.PersonaID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EjerciciosFisicos_PersonaID",
                table: "EjerciciosFisicos",
                column: "PersonaID");

            migrationBuilder.AddForeignKey(
                name: "FK_EjerciciosFisicos_Personas_PersonaID",
                table: "EjerciciosFisicos",
                column: "PersonaID",
                principalTable: "Personas",
                principalColumn: "PersonaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EjerciciosFisicos_Personas_PersonaID",
                table: "EjerciciosFisicos");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_EjerciciosFisicos_PersonaID",
                table: "EjerciciosFisicos");

            migrationBuilder.DropColumn(
                name: "PersonaID",
                table: "EjerciciosFisicos");
        }
    }
}
