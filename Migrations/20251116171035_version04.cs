using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class version04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Categories_categoryId",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListItem_Parts_partid",
                table: "ShoppingListItem");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingListItem_partid",
                table: "ShoppingListItem");

            migrationBuilder.DropIndex(
                name: "IX_Parts_categoryId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "partid",
                table: "ShoppingListItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "partid",
                table: "ShoppingListItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListItem_partid",
                table: "ShoppingListItem",
                column: "partid");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_categoryId",
                table: "Parts",
                column: "categoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Categories_categoryId",
                table: "Parts",
                column: "categoryId",
                principalTable: "Categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListItem_Parts_partid",
                table: "ShoppingListItem",
                column: "partid",
                principalTable: "Parts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
