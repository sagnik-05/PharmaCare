using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharamaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDrugTable999 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrugName",
                table: "Inventory");

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "Inventory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_SupplierId",
                table: "Inventory",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Suppliers_SupplierId",
                table: "Inventory",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Suppliers_SupplierId",
                table: "Inventory");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_SupplierId",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Inventory");

            migrationBuilder.AddColumn<string>(
                name: "DrugName",
                table: "Inventory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
