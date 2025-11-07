using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class Correction3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ShoppingLists_ShoppingListid",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ShoppingLists_ShoppingListid1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ShoppingListid",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ShoppingListid1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShoppingListid",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShoppingListid1",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoppingListid",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingListid1",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ShoppingListid",
                table: "AspNetUsers",
                column: "ShoppingListid");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ShoppingListid1",
                table: "AspNetUsers",
                column: "ShoppingListid1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ShoppingLists_ShoppingListid",
                table: "AspNetUsers",
                column: "ShoppingListid",
                principalTable: "ShoppingLists",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ShoppingLists_ShoppingListid1",
                table: "AspNetUsers",
                column: "ShoppingListid1",
                principalTable: "ShoppingLists",
                principalColumn: "id");
        }
    }
}
