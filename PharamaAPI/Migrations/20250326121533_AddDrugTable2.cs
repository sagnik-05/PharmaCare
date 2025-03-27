using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PharamaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDrugTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4404d659-89f8-4462-a519-bb8483a3dd6d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae32e8c3-5a88-4c19-bb5a-51d408d63956");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c2e221fd-2ef9-4096-a3e6-04e480a7d34c", null, "Admin", "ADMIN" },
                    { "f39a5a40-b959-4bca-abe0-266d5468555a", null, "Doctor", "DOCTOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c2e221fd-2ef9-4096-a3e6-04e480a7d34c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f39a5a40-b959-4bca-abe0-266d5468555a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4404d659-89f8-4462-a519-bb8483a3dd6d", null, "Doctor", "DOCTOR" },
                    { "ae32e8c3-5a88-4c19-bb5a-51d408d63956", null, "Admin", "ADMIN" }
                });
        }
    }
}
