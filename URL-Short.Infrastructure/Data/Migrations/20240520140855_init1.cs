using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URL_Short.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_URLs_Users_UserId",
                table: "URLs");

            migrationBuilder.DropIndex(
                name: "IX_URLs_UserId",
                table: "URLs");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("069ae840-e830-4f46-8a1c-f43d7d188499"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "URLs");

            migrationBuilder.AddColumn<string>(
                name: "Shortened_URLs",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Password", "Role", "Shortened_URLs" },
                values: new object[] { new Guid("6f01ccbd-ab8b-4bb5-89ca-73073a805e75"), new DateTime(2024, 5, 20, 17, 8, 55, 88, DateTimeKind.Local).AddTicks(73), "admin@example.com", "qN1CeE8F6Wy1MwGMzgdZXIah1zG/Jijls96jLN5rjFo=", "Admin", "[]" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6f01ccbd-ab8b-4bb5-89ca-73073a805e75"));

            migrationBuilder.DropColumn(
                name: "Shortened_URLs",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "URLs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Password", "Role" },
                values: new object[] { new Guid("069ae840-e830-4f46-8a1c-f43d7d188499"), new DateTime(2024, 5, 20, 17, 2, 17, 826, DateTimeKind.Local).AddTicks(7895), "admin@example.com", "qN1CeE8F6Wy1MwGMzgdZXIah1zG/Jijls96jLN5rjFo=", "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_URLs_UserId",
                table: "URLs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_URLs_Users_UserId",
                table: "URLs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
