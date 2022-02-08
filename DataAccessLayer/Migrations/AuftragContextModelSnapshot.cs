﻿// <auto-generated />
using System;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(AuftragContext))]
    partial class AuftragContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccessLayer.Entities.Adresse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("GueltigAb")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("GueltigBis")
                        .HasColumnType("datetime2");

                    b.Property<string>("HausNr")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("OrtschaftId")
                        .HasColumnType("int");

                    b.Property<string>("Strasse")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("OrtschaftId");

                    b.ToTable("Adressen");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Artikel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Aktiv")
                        .HasColumnType("bit");

                    b.Property<int>("ArtikelNr")
                        .HasColumnType("int");

                    b.Property<int>("ArtikelgruppeId")
                        .HasColumnType("int");

                    b.Property<string>("Bezeichnung")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("PreisNetto")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ArtikelgruppeId");

                    b.ToTable("Artikel");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Artikelgruppe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Artikelgruppe");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Auftrag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuftragsNr")
                        .HasMaxLength(45)
                        .HasColumnType("int");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int>("KundeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KundeId");

                    b.ToTable("Auftraege");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Kunde", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Firma")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("GueltigAb")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("GueltigBis")
                        .HasColumnType("datetime2");

                    b.Property<int>("KundenNr")
                        .HasColumnType("int");

                    b.Property<string>("Nachname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Passwort")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Vorname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Website")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Kunden");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.KundenAdresse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdresseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("GueltigAb")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("GueltigBis")
                        .HasColumnType("datetime2");

                    b.Property<int>("KundeId")
                        .HasColumnType("int");

                    b.Property<int>("KundenNr")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdresseId");

                    b.HasIndex("KundeId");

                    b.ToTable("KundenAdressen");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Ortschaft", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Aktiv")
                        .HasColumnType("bit");

                    b.Property<string>("Ort")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("PLZ")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Ortschaft");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArtikelId")
                        .HasColumnType("int");

                    b.Property<int>("AuftragId")
                        .HasColumnType("int");

                    b.Property<int>("Menge")
                        .HasMaxLength(45)
                        .HasColumnType("int");

                    b.Property<int>("PositionNr")
                        .HasMaxLength(45)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtikelId");

                    b.HasIndex("AuftragId");

                    b.ToTable("Positionen");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Adresse", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.Ortschaft", "Ortschaft")
                        .WithMany("Adressen")
                        .HasForeignKey("OrtschaftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ortschaft");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Artikel", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.Artikelgruppe", "Artikelgruppe")
                        .WithMany()
                        .HasForeignKey("ArtikelgruppeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artikelgruppe");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Auftrag", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.Kunde", "Kunde")
                        .WithMany()
                        .HasForeignKey("KundeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kunde");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.KundenAdresse", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.Adresse", "Adresse")
                        .WithMany()
                        .HasForeignKey("AdresseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entities.Kunde", "Kunde")
                        .WithMany()
                        .HasForeignKey("KundeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adresse");

                    b.Navigation("Kunde");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Position", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.Artikel", "Artikel")
                        .WithMany()
                        .HasForeignKey("ArtikelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entities.Auftrag", "Auftrag")
                        .WithMany()
                        .HasForeignKey("AuftragId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artikel");

                    b.Navigation("Auftrag");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Ortschaft", b =>
                {
                    b.Navigation("Adressen");
                });
#pragma warning restore 612, 618
        }
    }
}
