using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webprogramlama_odev.Migrations
{
    /// <inheritdoc />
    public partial class Pediatri6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BolumAcilDurum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BolumId = table.Column<int>(type: "int", nullable: true),
                    AcilDurumId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BolumAcilDurum", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BolumAcilDurum_AcilDurumlar_AcilDurumId",
                        column: x => x.AcilDurumId,
                        principalTable: "AcilDurumlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BolumAcilDurum_Bolumler_BolumId",
                        column: x => x.BolumId,
                        principalTable: "Bolumler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BolumAcilDurum_AcilDurumId",
                table: "BolumAcilDurum",
                column: "AcilDurumId");

            migrationBuilder.CreateIndex(
                name: "IX_BolumAcilDurum_BolumId",
                table: "BolumAcilDurum",
                column: "BolumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BolumAcilDurum");
        }
    }
}
