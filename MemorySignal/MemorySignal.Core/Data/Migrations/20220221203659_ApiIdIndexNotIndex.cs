using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataMigrations
{
    public partial class ApiIdIndexNotIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cards_ApiId",
                table: "Cards");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_ApiId",
                table: "Cards",
                column: "ApiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cards_ApiId",
                table: "Cards");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_ApiId",
                table: "Cards",
                column: "ApiId",
                unique: true);
        }
    }
}
