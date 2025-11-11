using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakturacniSystem.Migrations
{
    /// <inheritdoc />
    public partial class cena : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CenaZaKus",
                table: "Odebiratele",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CenaZaKus",
                table: "Odebiratele");
        }
    }
}
