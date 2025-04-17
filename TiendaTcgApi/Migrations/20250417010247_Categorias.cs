using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaTcgApi.Migrations
{
    /// <inheritdoc />
    public partial class Categorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Categoriaid",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Categoriaid",
                table: "Productos",
                column: "Categoriaid");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Categorias_Categoriaid",
                table: "Productos",
                column: "Categoriaid",
                principalTable: "Categorias",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Categorias_Categoriaid",
                table: "Productos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Productos_Categoriaid",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Categoriaid",
                table: "Productos");
        }
    }
}
