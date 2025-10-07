using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakturacniSystem.Migrations
{
    /// <inheritdoc />
    public partial class dodavatele : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Polozky",
                table: "Polozky");

            migrationBuilder.RenameTable(
                name: "Polozky",
                newName: "Polozka");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Polozka",
                table: "Polozka",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Polozka",
                table: "Polozka");

            migrationBuilder.RenameTable(
                name: "Polozka",
                newName: "Polozky");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Polozky",
                table: "Polozky",
                column: "Id");
        }
    }
}
