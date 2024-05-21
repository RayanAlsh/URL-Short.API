using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URL_Short.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6f01ccbd-ab8b-4bb5-89ca-73073a805e75"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Password", "Role", "Shortened_URLs" },
                values: new object[] { new Guid("1265046e-7203-4f33-ada4-83749432e4a7"), new DateTime(2024, 5, 21, 11, 17, 16, 394, DateTimeKind.Local).AddTicks(114), "admin@example.com", "qN1CeE8F6Wy1MwGMzgdZXIah1zG/Jijls96jLN5rjFo=", "Admin", "[]" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1265046e-7203-4f33-ada4-83749432e4a7"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Password", "Role", "Shortened_URLs" },
                values: new object[] { new Guid("6f01ccbd-ab8b-4bb5-89ca-73073a805e75"), new DateTime(2024, 5, 20, 17, 8, 55, 88, DateTimeKind.Local).AddTicks(73), "admin@example.com", "qN1CeE8F6Wy1MwGMzgdZXIah1zG/Jijls96jLN5rjFo=", "Admin", "[]" });
        }
    }
}
