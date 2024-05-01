using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class sdvcasdv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_offer_AspNetUsers_OwnerId1",
                table: "offer");

            migrationBuilder.DropIndex(
                name: "IX_offer_OwnerId1",
                table: "offer");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "offer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId1",
                table: "offer",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_offer_OwnerId1",
                table: "offer",
                column: "OwnerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_offer_AspNetUsers_OwnerId1",
                table: "offer",
                column: "OwnerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
