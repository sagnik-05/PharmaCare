using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PharamaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDrugTable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Drug_DrugId",
                table: "Inventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drug",
                table: "Drug");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c2e221fd-2ef9-4096-a3e6-04e480a7d34c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f39a5a40-b959-4bca-abe0-266d5468555a");

            migrationBuilder.RenameTable(
                name: "Drug",
                newName: "Drugs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drugs",
                table: "Drugs",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b4682f03-1513-4c21-9bb9-35f3a52f01de", null, "Doctor", "DOCTOR" },
                    { "f6c5487a-cc1d-41d9-b804-f3f3b2af6598", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Drugs_DrugId",
                table: "Inventory",
                column: "DrugId",
                principalTable: "Drugs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Drugs_DrugId",
                table: "Inventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drugs",
                table: "Drugs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4682f03-1513-4c21-9bb9-35f3a52f01de");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6c5487a-cc1d-41d9-b804-f3f3b2af6598");

            migrationBuilder.RenameTable(
                name: "Drugs",
                newName: "Drug");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drug",
                table: "Drug",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c2e221fd-2ef9-4096-a3e6-04e480a7d34c", null, "Admin", "ADMIN" },
                    { "f39a5a40-b959-4bca-abe0-266d5468555a", null, "Doctor", "DOCTOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Drug_DrugId",
                table: "Inventory",
                column: "DrugId",
                principalTable: "Drug",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
