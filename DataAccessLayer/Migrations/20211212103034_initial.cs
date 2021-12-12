using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kunden",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KundenNr = table.Column<int>(type: "int", nullable: false),
                    Vorname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Nachname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Firma = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Passwort = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GueltigAb = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GueltigBis = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kunden", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ortschaft",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PLZ = table.Column<int>(type: "int", nullable: false),
                    Ort = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Aktiv = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ortschaft", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Adresse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Strasse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HausNr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrtschaftId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adresse_Ortschaft_OrtschaftId",
                        column: x => x.OrtschaftId,
                        principalTable: "Ortschaft",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KundenAdresse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KundenNr = table.Column<int>(type: "int", nullable: false),
                    GueltigAb = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GueltigBis = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KudenId = table.Column<int>(type: "int", nullable: false),
                    KundeId = table.Column<int>(type: "int", nullable: true),
                    AdresseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KundenAdresse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KundenAdresse_Adresse_AdresseId",
                        column: x => x.AdresseId,
                        principalTable: "Adresse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KundenAdresse_Kunden_KundeId",
                        column: x => x.KundeId,
                        principalTable: "Kunden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adresse_OrtschaftId",
                table: "Adresse",
                column: "OrtschaftId");

            migrationBuilder.CreateIndex(
                name: "IX_KundenAdresse_AdresseId",
                table: "KundenAdresse",
                column: "AdresseId");

            migrationBuilder.CreateIndex(
                name: "IX_KundenAdresse_KundeId",
                table: "KundenAdresse",
                column: "KundeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KundenAdresse");

            migrationBuilder.DropTable(
                name: "Adresse");

            migrationBuilder.DropTable(
                name: "Kunden");

            migrationBuilder.DropTable(
                name: "Ortschaft");
        }
    }
}
