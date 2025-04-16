using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharamaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDrugRequestsTable06910 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "DrugRequests");

            migrationBuilder.DropColumn(
                name: "RequestDate",
                table: "DrugRequests");

            migrationBuilder.DropColumn(
                name: "RequestedBy",
                table: "DrugRequests");

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "DrugRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "RequestId",
                table: "DrugRequests",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DrugRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDate",
                table: "DrugRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RequestedBy",
                table: "DrugRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
