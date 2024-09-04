using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto1.Migrations
{
    /// <inheritdoc />
    public partial class migracion4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EjerciciosFisicos_LugarID",
                table: "EjerciciosFisicos",
                column: "LugarID");

            migrationBuilder.AddForeignKey(
                name: "FK_EjerciciosFisicos_Lugares_LugarID",
                table: "EjerciciosFisicos",
                column: "LugarID",
                principalTable: "Lugares",
                principalColumn: "LugarID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EjerciciosFisicos_Lugares_LugarID",
                table: "EjerciciosFisicos");

            migrationBuilder.DropIndex(
                name: "IX_EjerciciosFisicos_LugarID",
                table: "EjerciciosFisicos");
        }
    }
}
