using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharamaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDrugRequestsTable011 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrugRequests_AspNetUsers_DoctorId",
                table: "DrugRequests");

            migrationBuilder.DropIndex(
                name: "IX_DrugRequests_DoctorId",
                table: "DrugRequests");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "DrugRequests");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "DrugRequests",
                newName: "RequestedBy");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "DrugRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "DrugRequests");

            migrationBuilder.RenameColumn(
                name: "RequestedBy",
                table: "DrugRequests",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                table: "DrugRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_DrugRequests_DoctorId",
                table: "DrugRequests",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrugRequests_AspNetUsers_DoctorId",
                table: "DrugRequests",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
