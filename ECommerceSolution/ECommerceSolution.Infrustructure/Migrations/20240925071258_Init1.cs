using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblVendors_tblUsers_UserId",
                table: "tblVendors");

            migrationBuilder.DropIndex(
                name: "IX_tblVendors_UserId",
                table: "tblVendors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "tblVendors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "tblVendors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tblVendors_UserId",
                table: "tblVendors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblVendors_tblUsers_UserId",
                table: "tblVendors",
                column: "UserId",
                principalTable: "tblUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
