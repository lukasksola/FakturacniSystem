using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakturacniSystem.Migrations
{
    /// <inheritdoc />
    public partial class pridatDodavani : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Dodavatele",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NazevDodavatele = table.Column<string>(type: "TEXT", nullable: false),
                    PolozkaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dodavatele", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dodavatele_Polozky_PolozkaId",
                        column: x => x.PolozkaId,
                        principalTable: "Polozky",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dodavatele_PolozkaId",
                table: "Dodavatele",
                column: "PolozkaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dodavatele");

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
    }
}
