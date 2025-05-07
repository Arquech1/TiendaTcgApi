using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaTcgApi.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionDecolumnas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Categorias_Categoriaid",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_Categoriaid",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Categoriaid",
                table: "Productos");

            migrationBuilder.AddColumn<string>(
                name: "descripcion",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "foto",
                table: "Productos",
                type: "varchar(max)",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "precio",
                table: "Productos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "stock",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoriaProducto",
                columns: table => new
                {
                    categoriaid = table.Column<int>(type: "int", nullable: false),
                    productosid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaProducto", x => new { x.categoriaid, x.productosid });
                    table.ForeignKey(
                        name: "FK_CategoriaProducto_Categorias_categoriaid",
                        column: x => x.categoriaid,
                        principalTable: "Categorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoriaProducto_Productos_productosid",
                        column: x => x.productosid,
                        principalTable: "Productos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaProducto_productosid",
                table: "CategoriaProducto",
                column: "productosid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriaProducto");

            migrationBuilder.DropColumn(
                name: "descripcion",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "foto",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "precio",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "stock",
                table: "Productos");

            migrationBuilder.AddColumn<int>(
                name: "Categoriaid",
                table: "Productos",
                type: "int",
                nullable: true);

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
    }
}
