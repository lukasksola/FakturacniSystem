using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakturacniSystem.Migrations
{
    /// <inheritdoc />
    public partial class odebirani : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Odebiratele",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NazevOdberatele = table.Column<string>(type: "TEXT", nullable: false),
                    PolozkaId = table.Column<int>(type: "INTEGER", nullable: false),
                    pocetPolozekOdebrano = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odebiratele", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Odebiratele");
        }
    }
}
