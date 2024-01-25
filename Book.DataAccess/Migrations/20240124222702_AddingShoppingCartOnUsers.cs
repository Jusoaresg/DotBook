using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddingShoppingCartOnUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ShoppingCartItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ApplicationUserId",
                table: "ShoppingCartItems",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_AspNetUsers_ApplicationUserId",
                table: "ShoppingCartItems",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_AspNetUsers_ApplicationUserId",
                table: "ShoppingCartItems");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartItems_ApplicationUserId",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ShoppingCartItems");
        }
    }
}
