using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakturacniSystem.Migrations
{
    /// <inheritdoc />
    public partial class foreignKeyPolozka : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dodavatele_Polozky_PolozkaId",
                table: "Dodavatele");

            migrationBuilder.DropIndex(
                name: "IX_Dodavatele_PolozkaId",
                table: "Dodavatele");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Dodavatele_PolozkaId",
                table: "Dodavatele",
                column: "PolozkaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dodavatele_Polozky_PolozkaId",
                table: "Dodavatele",
                column: "PolozkaId",
                principalTable: "Polozky",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
