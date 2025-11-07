using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class Correction2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "purchaseDate",
                table: "Parts",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<int>(
                name: "ShoppingListid",
                table: "Parts",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateTable(
                name: "ShoppingLists",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingLists", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_ShoppingListid",
                table: "Parts",
                column: "ShoppingListid");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_ShoppingLists_ShoppingListid",
                table: "Parts",
                column: "ShoppingListid",
                principalTable: "ShoppingLists",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ShoppingLists_ShoppingListid",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ShoppingLists_ShoppingListid1",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_ShoppingLists_ShoppingListid",
                table: "Parts");

            migrationBuilder.DropTable(
                name: "ShoppingLists");

            migrationBuilder.DropIndex(
                name: "IX_Parts_ShoppingListid",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ShoppingListid",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ShoppingListid1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShoppingListid",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "ShoppingListid",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShoppingListid1",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "purchaseDate",
                table: "Parts",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);
        }
    }
}
