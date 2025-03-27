using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharamaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDrugTable5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DrugName",
                table: "Inventory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrugName",
                table: "Inventory");
        }
    }
}
