using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class version06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListItem_ShoppingLists_shoppingListId",
                table: "ShoppingListItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingLists",
                table: "ShoppingLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingListItem",
                table: "ShoppingListItem");

            migrationBuilder.RenameTable(
                name: "ShoppingLists",
                newName: "shoppinglists");

            migrationBuilder.RenameTable(
                name: "ShoppingListItem",
                newName: "shoppinglistitem");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "shoppinglists",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "shoppinglists",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "shoppingListId",
                table: "shoppinglistitem",
                newName: "ShoppingListId");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "shoppinglistitem",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "purchased",
                table: "shoppinglistitem",
                newName: "Purchased");

            migrationBuilder.RenameColumn(
                name: "purchaseDate",
                table: "shoppinglistitem",
                newName: "PurchaseDate");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "shoppinglistitem",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingListItem_shoppingListId",
                table: "shoppinglistitem",
                newName: "IX_shoppinglistitem_ShoppingListId");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingListId",
                table: "shoppinglistitem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_shoppinglists",
                table: "shoppinglists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_shoppinglistitem",
                table: "shoppinglistitem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_shoppinglistitem_shoppinglists_ShoppingListId",
                table: "shoppinglistitem",
                column: "ShoppingListId",
                principalTable: "shoppinglists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shoppinglistitem_shoppinglists_ShoppingListId",
                table: "shoppinglistitem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_shoppinglists",
                table: "shoppinglists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_shoppinglistitem",
                table: "shoppinglistitem");

            migrationBuilder.RenameTable(
                name: "shoppinglists",
                newName: "ShoppingLists");

            migrationBuilder.RenameTable(
                name: "shoppinglistitem",
                newName: "ShoppingListItem");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ShoppingLists",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ShoppingLists",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ShoppingListId",
                table: "ShoppingListItem",
                newName: "shoppingListId");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ShoppingListItem",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Purchased",
                table: "ShoppingListItem",
                newName: "purchased");

            migrationBuilder.RenameColumn(
                name: "PurchaseDate",
                table: "ShoppingListItem",
                newName: "purchaseDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ShoppingListItem",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_shoppinglistitem_ShoppingListId",
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingLists",
                table: "ShoppingLists",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingListItem",
                table: "ShoppingListItem",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListItem_ShoppingLists_shoppingListId",
                table: "ShoppingListItem",
                column: "shoppingListId",
                principalTable: "ShoppingLists",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
