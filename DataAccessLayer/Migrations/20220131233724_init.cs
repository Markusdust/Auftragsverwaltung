using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artikelgruppe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikelgruppe", x => x.Id);
                });

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
                name: "Artikel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bezeichnung = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ArtikelNr = table.Column<int>(type: "int", nullable: false),
                    PreisNetto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Aktiv = table.Column<bool>(type: "bit", nullable: false),
                    ArtikelgruppeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artikel_Artikelgruppe_ArtikelgruppeId",
                        column: x => x.ArtikelgruppeId,
                        principalTable: "Artikelgruppe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auftraege",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuftragsNr = table.Column<int>(type: "int", maxLength: 45, nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KundeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auftraege", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auftraege_Kunden_KundeId",
                        column: x => x.KundeId,
                        principalTable: "Kunden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Adressen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Strasse = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HausNr = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    GueltigAb = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GueltigBis = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrtschaftId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adressen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adressen_Ortschaft_OrtschaftId",
                        column: x => x.OrtschaftId,
                        principalTable: "Ortschaft",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Positionen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionNr = table.Column<int>(type: "int", maxLength: 45, nullable: false),
                    Menge = table.Column<int>(type: "int", maxLength: 45, nullable: false),
                    AuftragId = table.Column<int>(type: "int", nullable: false),
                    ArtikelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positionen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positionen_Artikel_ArtikelId",
                        column: x => x.ArtikelId,
                        principalTable: "Artikel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Positionen_Auftraege_AuftragId",
                        column: x => x.AuftragId,
                        principalTable: "Auftraege",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KundenAdressen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KundenNr = table.Column<int>(type: "int", nullable: false),
                    GueltigAb = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GueltigBis = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KundeId = table.Column<int>(type: "int", nullable: false),
                    AdresseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KundenAdressen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KundenAdressen_Adressen_AdresseId",
                        column: x => x.AdresseId,
                        principalTable: "Adressen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KundenAdressen_Kunden_KundeId",
                        column: x => x.KundeId,
                        principalTable: "Kunden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adressen_OrtschaftId",
                table: "Adressen",
                column: "OrtschaftId");

            migrationBuilder.CreateIndex(
                name: "IX_Artikel_ArtikelgruppeId",
                table: "Artikel",
                column: "ArtikelgruppeId");

            migrationBuilder.CreateIndex(
                name: "IX_Auftraege_KundeId",
                table: "Auftraege",
                column: "KundeId");

            migrationBuilder.CreateIndex(
                name: "IX_KundenAdressen_AdresseId",
                table: "KundenAdressen",
                column: "AdresseId");

            migrationBuilder.CreateIndex(
                name: "IX_KundenAdressen_KundeId",
                table: "KundenAdressen",
                column: "KundeId");

            migrationBuilder.CreateIndex(
                name: "IX_Positionen_ArtikelId",
                table: "Positionen",
                column: "ArtikelId");

            migrationBuilder.CreateIndex(
                name: "IX_Positionen_AuftragId",
                table: "Positionen",
                column: "AuftragId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KundenAdressen");

            migrationBuilder.DropTable(
                name: "Positionen");

            migrationBuilder.DropTable(
                name: "Adressen");

            migrationBuilder.DropTable(
                name: "Artikel");

            migrationBuilder.DropTable(
                name: "Auftraege");

            migrationBuilder.DropTable(
                name: "Ortschaft");

            migrationBuilder.DropTable(
                name: "Artikelgruppe");

            migrationBuilder.DropTable(
                name: "Kunden");
        }
    }
}
