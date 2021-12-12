using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class ArtikelArtikelGruppe : Migration
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
                    Strasse = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HausNr = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    GueltigAb = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GueltigBis = table.Column<DateTime>(type: "datetime2", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Artikelgruppe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ArtikelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikelgruppe", x => x.Id);
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
                    mwst = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    preisBrutto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ArtikelgruppeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artikel_Artikelgruppe_ArtikelgruppeId",
                        column: x => x.ArtikelgruppeId,
                        principalTable: "Artikelgruppe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adresse_OrtschaftId",
                table: "Adresse",
                column: "OrtschaftId");

            migrationBuilder.CreateIndex(
                name: "IX_Artikel_ArtikelgruppeId",
                table: "Artikel",
                column: "ArtikelgruppeId");

            migrationBuilder.CreateIndex(
                name: "IX_Artikelgruppe_ArtikelId",
                table: "Artikelgruppe",
                column: "ArtikelId");

            migrationBuilder.CreateIndex(
                name: "IX_KundenAdresse_AdresseId",
                table: "KundenAdresse",
                column: "AdresseId");

            migrationBuilder.CreateIndex(
                name: "IX_KundenAdresse_KundeId",
                table: "KundenAdresse",
                column: "KundeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artikelgruppe_Artikel_ArtikelId",
                table: "Artikelgruppe",
                column: "ArtikelId",
                principalTable: "Artikel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artikel_Artikelgruppe_ArtikelgruppeId",
                table: "Artikel");

            migrationBuilder.DropTable(
                name: "KundenAdresse");

            migrationBuilder.DropTable(
                name: "Adresse");

            migrationBuilder.DropTable(
                name: "Kunden");

            migrationBuilder.DropTable(
                name: "Ortschaft");

            migrationBuilder.DropTable(
                name: "Artikelgruppe");

            migrationBuilder.DropTable(
                name: "Artikel");
        }
    }
}
