using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class version05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListItem_ShoppingLists_ShoppingListid",
                table: "ShoppingListItem");

            migrationBuilder.RenameColumn(
                name: "ShoppingListid",
                table: "ShoppingListItem",
                newName: "shoppingListId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingListItem_ShoppingListid",
                table: "ShoppingListItem",
                newName: "IX_ShoppingListItem_shoppingListId");

            migrationBuilder.AlterColumn<int>(
                name: "shoppingListId",
                table: "ShoppingListItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListItem_ShoppingLists_shoppingListId",
                table: "ShoppingListItem",
                column: "shoppingListId",
                principalTable: "ShoppingLists",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListItem_ShoppingLists_shoppingListId",
                table: "ShoppingListItem");

            migrationBuilder.RenameColumn(
                name: "shoppingListId",
                table: "ShoppingListItem",
                newName: "ShoppingListid");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingListItem_shoppingListId",
                table: "ShoppingListItem",
                newName: "IX_ShoppingListItem_ShoppingListid");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingListid",
                table: "ShoppingListItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListItem_ShoppingLists_ShoppingListid",
                table: "ShoppingListItem",
                column: "ShoppingListid",
                principalTable: "ShoppingLists",
                principalColumn: "id");
        }
    }
}
