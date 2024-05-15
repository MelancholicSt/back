using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class @as : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chars_CharKeys_KeyId",
                table: "Chars");

            migrationBuilder.DropForeignKey(
                name: "FK_Chars_CharValues_ValueId",
                table: "Chars");

            migrationBuilder.DropForeignKey(
                name: "FK_CharValues_CharKeys_KeyId",
                table: "CharValues");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOrders_AspNetUsers_ClientId",
                table: "UserOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOrders_Statuses_StatusId",
                table: "UserOrders");

            migrationBuilder.DropIndex(
                name: "IX_UserOrders_StatusId",
                table: "UserOrders");

            migrationBuilder.DropIndex(
                name: "IX_CharValues_KeyId",
                table: "CharValues");

            migrationBuilder.DropColumn(
                name: "KeyId",
                table: "CharValues");

            migrationBuilder.RenameColumn(
                name: "ValueId",
                table: "Chars",
                newName: "AttributeValueId");

            migrationBuilder.RenameColumn(
                name: "KeyId",
                table: "Chars",
                newName: "AttributeNameId");

            migrationBuilder.RenameIndex(
                name: "IX_Chars_ValueId",
                table: "Chars",
                newName: "IX_Chars_AttributeValueId");

            migrationBuilder.RenameIndex(
                name: "IX_Chars_KeyId",
                table: "Chars",
                newName: "IX_Chars_AttributeNameId");

            migrationBuilder.UpdateData(
                table: "UserOrders",
                keyColumn: "ClientId",
                keyValue: null,
                column: "ClientId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "UserOrders",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<ulong>(
                name: "CategoryId",
                table: "ProductInfo",
                type: "bigint unsigned",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "AttributeNameId",
                table: "CharKeys",
                type: "bigint unsigned",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategoryId = table.Column<ulong>(type: "bigint unsigned", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    OrdersOrderId = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    StatusesId = table.Column<uint>(type: "int unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => new { x.OrdersOrderId, x.StatusesId });
                    table.ForeignKey(
                        name: "FK_OrderStatus_Statuses_StatusesId",
                        column: x => x.StatusesId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderStatus_UserOrders_OrdersOrderId",
                        column: x => x.OrdersOrderId,
                        principalTable: "UserOrders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInfo_CategoryId",
                table: "ProductInfo",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CharKeys_AttributeNameId",
                table: "CharKeys",
                column: "AttributeNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CategoryId",
                table: "Category",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatus_StatusesId",
                table: "OrderStatus",
                column: "StatusesId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharKeys_CharValues_AttributeNameId",
                table: "CharKeys",
                column: "AttributeNameId",
                principalTable: "CharValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chars_CharKeys_AttributeValueId",
                table: "Chars",
                column: "AttributeValueId",
                principalTable: "CharKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chars_CharValues_AttributeNameId",
                table: "Chars",
                column: "AttributeNameId",
                principalTable: "CharValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInfo_Category_CategoryId",
                table: "ProductInfo",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOrders_AspNetUsers_ClientId",
                table: "UserOrders",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharKeys_CharValues_AttributeNameId",
                table: "CharKeys");

            migrationBuilder.DropForeignKey(
                name: "FK_Chars_CharKeys_AttributeValueId",
                table: "Chars");

            migrationBuilder.DropForeignKey(
                name: "FK_Chars_CharValues_AttributeNameId",
                table: "Chars");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInfo_Category_CategoryId",
                table: "ProductInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOrders_AspNetUsers_ClientId",
                table: "UserOrders");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "OrderStatus");

            migrationBuilder.DropIndex(
                name: "IX_ProductInfo_CategoryId",
                table: "ProductInfo");

            migrationBuilder.DropIndex(
                name: "IX_CharKeys_AttributeNameId",
                table: "CharKeys");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ProductInfo");

            migrationBuilder.DropColumn(
                name: "AttributeNameId",
                table: "CharKeys");

            migrationBuilder.RenameColumn(
                name: "AttributeValueId",
                table: "Chars",
                newName: "ValueId");

            migrationBuilder.RenameColumn(
                name: "AttributeNameId",
                table: "Chars",
                newName: "KeyId");

            migrationBuilder.RenameIndex(
                name: "IX_Chars_AttributeValueId",
                table: "Chars",
                newName: "IX_Chars_ValueId");

            migrationBuilder.RenameIndex(
                name: "IX_Chars_AttributeNameId",
                table: "Chars",
                newName: "IX_Chars_KeyId");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "UserOrders",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<ulong>(
                name: "KeyId",
                table: "CharValues",
                type: "bigint unsigned",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.CreateIndex(
                name: "IX_UserOrders_StatusId",
                table: "UserOrders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CharValues_KeyId",
                table: "CharValues",
                column: "KeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chars_CharKeys_KeyId",
                table: "Chars",
                column: "KeyId",
                principalTable: "CharKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chars_CharValues_ValueId",
                table: "Chars",
                column: "ValueId",
                principalTable: "CharValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharValues_CharKeys_KeyId",
                table: "CharValues",
                column: "KeyId",
                principalTable: "CharKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOrders_AspNetUsers_ClientId",
                table: "UserOrders",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserOrders_Statuses_StatusId",
                table: "UserOrders",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
