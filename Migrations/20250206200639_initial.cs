using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlibiPerfeito_CRUD.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Categoria",
                table: "Categoria");

            migrationBuilder.EnsureSchema(
                name: "AlibiPerfeito");

            migrationBuilder.RenameTable(
                name: "Desculpas",
                newName: "Desculpas",
                newSchema: "AlibiPerfeito");

            migrationBuilder.RenameTable(
                name: "Categoria",
                newName: "Categorias",
                newSchema: "AlibiPerfeito");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorias",
                schema: "AlibiPerfeito",
                table: "Categorias",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorias",
                schema: "AlibiPerfeito",
                table: "Categorias");

            migrationBuilder.RenameTable(
                name: "Desculpas",
                schema: "AlibiPerfeito",
                newName: "Desculpas");

            migrationBuilder.RenameTable(
                name: "Categorias",
                schema: "AlibiPerfeito",
                newName: "Categoria");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categoria",
                table: "Categoria",
                column: "Id");
        }
    }
}
