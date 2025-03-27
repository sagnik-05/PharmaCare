using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PharamaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDrugTable4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4682f03-1513-4c21-9bb9-35f3a52f01de");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6c5487a-cc1d-41d9-b804-f3f3b2af6598");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Admin", "ADMIN" },
                    { "2", null, "Doctor", "DOCTOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b4682f03-1513-4c21-9bb9-35f3a52f01de", null, "Doctor", "DOCTOR" },
                    { "f6c5487a-cc1d-41d9-b804-f3f3b2af6598", null, "Admin", "ADMIN" }
                });
        }
    }
}
