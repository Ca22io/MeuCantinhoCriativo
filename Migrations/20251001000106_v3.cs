using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuCantinhoCriativo.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Hobbies",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Hobbies",
                newName: "Descricao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Hobbies",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Hobbies",
                newName: "Description");
        }
    }
}
