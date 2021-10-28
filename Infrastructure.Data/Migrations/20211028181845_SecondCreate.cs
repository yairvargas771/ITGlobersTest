using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class SecondCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutorLibro_Libro_BooksId",
                table: "AutorLibro");

            migrationBuilder.RenameColumn(
                name: "BooksId",
                table: "AutorLibro",
                newName: "LibrosId");

            migrationBuilder.RenameIndex(
                name: "IX_AutorLibro_BooksId",
                table: "AutorLibro",
                newName: "IX_AutorLibro_LibrosId");

            migrationBuilder.AddForeignKey(
                name: "FK_AutorLibro_Libro_LibrosId",
                table: "AutorLibro",
                column: "LibrosId",
                principalTable: "Libro",
                principalColumn: "ISBN",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutorLibro_Libro_LibrosId",
                table: "AutorLibro");

            migrationBuilder.RenameColumn(
                name: "LibrosId",
                table: "AutorLibro",
                newName: "BooksId");

            migrationBuilder.RenameIndex(
                name: "IX_AutorLibro_LibrosId",
                table: "AutorLibro",
                newName: "IX_AutorLibro_BooksId");

            migrationBuilder.AddForeignKey(
                name: "FK_AutorLibro_Libro_BooksId",
                table: "AutorLibro",
                column: "BooksId",
                principalTable: "Libro",
                principalColumn: "ISBN",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
