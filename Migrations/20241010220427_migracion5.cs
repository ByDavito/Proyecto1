using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto1.Migrations
{
    /// <inheritdoc />
    public partial class migracion5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventoID",
                table: "EjerciciosFisicos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    EventoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.EventoID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EjerciciosFisicos_EventoID",
                table: "EjerciciosFisicos",
                column: "EventoID");

            migrationBuilder.AddForeignKey(
                name: "FK_EjerciciosFisicos_Eventos_EventoID",
                table: "EjerciciosFisicos",
                column: "EventoID",
                principalTable: "Eventos",
                principalColumn: "EventoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EjerciciosFisicos_Eventos_EventoID",
                table: "EjerciciosFisicos");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_EjerciciosFisicos_EventoID",
                table: "EjerciciosFisicos");

            migrationBuilder.DropColumn(
                name: "EventoID",
                table: "EjerciciosFisicos");
        }
    }
}
