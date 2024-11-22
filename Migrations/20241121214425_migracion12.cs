using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto1.Migrations
{
    /// <inheritdoc />
    public partial class migracion12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_AspNetUsers_UsersId",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Personas_UsersId",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<float>(
                name: "MET",
                table: "TipoEjercicios",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MET",
                table: "TipoEjercicios");

            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "Personas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_UsersId",
                table: "Personas",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_AspNetUsers_UsersId",
                table: "Personas",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
