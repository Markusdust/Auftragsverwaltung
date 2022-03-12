using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccessLayer
{
    public class AuftragContext : DbContext
    {
        public DbSet<Kunde> Kunden { get; set; }
        public DbSet<Adresse> Adressen { get; set; }
        public DbSet<KundenAdresse> KundenAdressen { get; set; }
        public DbSet<Ortschaft> Ortschaften { get; set; }
        public DbSet<Artikel> Artikel { get; set; }
        public DbSet<Artikelgruppe> Artikelgruppe { get; set; }
        public DbSet<Auftrag> Auftraege { get; set; }
        public DbSet<Position> Positionen { get; set; }
        //Joel
        //public string connectionstring = "Data Source=localhost; Database=Auftragsverwaltung; Trusted_Connection=True";
        //Markus
        //public string connectionstring = "Data Source=localhost; Database=Auftragsverwaltung; Trusted_Connection=True";
        //Andy
        public string connectionstring ="Data Source=.\\SQLEXPRESS; Database=Auftragsverwaltung; Trusted_Connection=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionstring);
            //  optionsBuilder.UseLazyLoadingProxies();


            // install-package Microsoft.Extensions.Configuration.Json

            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //    .AddJsonFile("appsettings.json")
            //    .Build();
            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("EFCoreDemo"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //CustomerNumber gets automatic a number starting by 1000 increment 5
            modelBuilder.HasSequence<int>("KundenNummern", schema: "shared")
                .StartsAt(1500)
                .IncrementsBy(5);

            modelBuilder.Entity<Kunde>()
                .Property(c => c.KundenNr)
                .HasDefaultValueSql("NEXT VALUE FOR shared.KundenNummern");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuftragContext).Assembly);


            modelBuilder.HasSequence<int>("AuftragNr", schema: "shared")
                .StartsAt(2020)
                .IncrementsBy(5);

            modelBuilder.Entity<Auftrag>()
                .Property(c => c.AuftragsNr)
                .HasDefaultValueSql("NEXT VALUE FOR shared.AuftragNr");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuftragContext).Assembly);



            //BEISPIELDATEN
            
            #region Artikelgruppe
            modelBuilder.Entity<Artikelgruppe>().HasData(new Artikelgruppe()
            {
                Id = 1,
                Name = "Pflanzen",
                UebergeordneteGruppeId = null,
                Active = true
            });

            modelBuilder.Entity<Artikelgruppe>().HasData(new Artikelgruppe()
            {
                Id = 2,
                Name = "Werkzeug",
                UebergeordneteGruppeId = null,
                Active = true
            });

            modelBuilder.Entity<Artikelgruppe>().HasData(new Artikelgruppe()
            {
                Id = 3,
                Name = "Fahrzeug",
                UebergeordneteGruppeId = null,
                Active = true
            });

            modelBuilder.Entity<Artikelgruppe>().HasData(new Artikelgruppe()
            {
                Id = 4,
                Name = "Blume",
                UebergeordneteGruppeId = 1,
                Active = true
            });

            modelBuilder.Entity<Artikelgruppe>().HasData(new Artikelgruppe()
            {
                Id = 5,
                Name = "Feilchen",
                UebergeordneteGruppeId = 4,
                Active = true
            });

            modelBuilder.Entity<Artikelgruppe>().HasData(new Artikelgruppe()
            {
                Id = 6,
                Name = "Hammer",
                UebergeordneteGruppeId = 2,
                Active = true
            });

            modelBuilder.Entity<Artikelgruppe>().HasData(new Artikelgruppe()
            {
                Id = 7,
                Name = "Quad",
                UebergeordneteGruppeId = 3,
                Active = true
            });

            modelBuilder.Entity<Artikelgruppe>().HasData(new Artikelgruppe()
            {
                Id = 8,
                Name = "Velo",
                UebergeordneteGruppeId = 3,
                Active = true
            });
            #endregion

            #region Artikel

            modelBuilder.Entity<Artikel>().HasData(new Artikel()
            {
                Id = 1,
                ArtikelNr = 10,
                Bezeichnung = "Rose",
                PreisNetto = 1.1m,
                PreisBrutto = 1.2m,
                Mwst = 7.7m,
                Aktiv = true,
                ArtikelgruppeId = 4
            });


            modelBuilder.Entity<Artikel>().HasData(new Artikel()
            {
                Id = 2,
                ArtikelNr = 20,
                Bezeichnung = "Löwenzahn",
                PreisNetto = 2.50m,
                PreisBrutto = 3.00m,
                Mwst = 7.7m,
                Aktiv = true,
                ArtikelgruppeId = 1
            });
            modelBuilder.Entity<Artikel>().HasData(new Artikel()
            {
                Id = 3,
                ArtikelNr = 30,
                Bezeichnung = "Axt",
                PreisNetto = 50.0m,
                PreisBrutto = 52m,
                Mwst = 7.7m,
                Aktiv = false,
                ArtikelgruppeId = 2
            });
            modelBuilder.Entity<Artikel>().HasData(new Artikel()
            {
                Id = 4,
                ArtikelNr = 40,
                Bezeichnung = "Renault",
                PreisNetto = 70000m,
                PreisBrutto = 70200m,
                Mwst = 7.7m,
                Aktiv = true,
                ArtikelgruppeId = 3
            });
            modelBuilder.Entity<Artikel>().HasData(new Artikel()
            {
                Id = 5,
                ArtikelNr = 50,
                Bezeichnung = "Sonnenblume",
                PreisNetto = 2.50m,
                PreisBrutto = 2.70m,
                Mwst = 7.7m,
                Aktiv = true,
                ArtikelgruppeId = 1
            });
            modelBuilder.Entity<Artikel>().HasData(new Artikel()
            {
                Id = 6,
                ArtikelNr = 60,
                Bezeichnung = "Audi",
                PreisNetto = 20000m,
                PreisBrutto = 21540m,
                Mwst = 7.7m,
                Aktiv = true,
                ArtikelgruppeId = 3
            });
            modelBuilder.Entity<Artikel>().HasData(new Artikel()
            {
                Id = 7,
                ArtikelNr = 70,
                Bezeichnung = "BMW",
                PreisNetto = 28000m,
                PreisBrutto = 30156m,
                Mwst = 7.7m,
                Aktiv = true,
                ArtikelgruppeId = 3
            });
            modelBuilder.Entity<Artikel>().HasData(new Artikel()
            {
                Id = 8,
                ArtikelNr = 80,
                Bezeichnung = "Säge",
                PreisNetto = 3.2m,
                PreisBrutto = 3.3m,
                Mwst = 2.5m,
                Aktiv = true,
                ArtikelgruppeId = 2
            });
            modelBuilder.Entity<Artikel>().HasData(new Artikel()
            {
                Id = 9,
                ArtikelNr = 90,
                Bezeichnung = "Schlauch",
                PreisNetto = 2.2m,
                PreisBrutto = 2.35m,
                Mwst = 7.7m,
                Aktiv = true,
                ArtikelgruppeId = 8
            });
            modelBuilder.Entity<Artikel>().HasData(new Artikel()
            {
                Id = 10,
                ArtikelNr = 100,
                Bezeichnung = "Bremse",
                PreisNetto = 2m,
                PreisBrutto = 2m,
                Mwst = 0m,
                Aktiv = true,
                ArtikelgruppeId = 8
            });
            #endregion

            #region Kunden
            DateTime maxDateValue = DateTime.MaxValue;

            modelBuilder.Entity<Kunde>().HasData(new Kunde()
            {
                Id = 1,
                KundenNr = 1000,
                Vorname = "Markus",
                Nachname = "Staub",
                Firma = "Kellenberger",
                Email = "markusstaub1@gmail.com",
                Passwort = "1234",
                Website = "Kellenberger.com",
                GueltigAb = new DateTime(2022, 03, 07, 16, 26, 11),
                GueltigBis = maxDateValue
            });

            modelBuilder.Entity<Kunde>().HasData(new Kunde()
            {
                Id = 2,
                KundenNr = 1005,
                Vorname = "Andy",
                Nachname = "Steingruber",
                Firma = "Safran",
                Email = "a.Safran@safran.com",
                Passwort = "Safisupi",
                Website = "Safran.com",
                GueltigAb = new DateTime(2022, 03, 07, 16, 27, 03),
                GueltigBis = maxDateValue
            });

            modelBuilder.Entity<Kunde>().HasData(new Kunde()
            {
                Id = 3,
                KundenNr = 1010,
                Vorname = "Pascal",
                Nachname = "Gerard",
                Firma = "Hardinge",
                Email = "P.Gerard@H.com",
                Passwort = "hadiou88",
                Website = "Hardigne.com",
                GueltigAb = new DateTime(2022, 03, 07, 16, 27, 51),
                GueltigBis = new DateTime(2022, 03, 07, 16, 28, 50)
            });

            modelBuilder.Entity<Kunde>().HasData(new Kunde()
            {
                Id = 4,
                KundenNr = 1010,
                Vorname = "Pascal",
                Nachname = "Türliman",
                Firma = "Hardinge",
                Email = "P.Gerard@H.com",
                Passwort = "hadiou88",
                Website = "Hardigne.com",
                GueltigAb = new DateTime(2022, 03, 07, 16, 28, 51),
                GueltigBis = maxDateValue
            });
            #endregion

            #region KundenAdressen

            modelBuilder.Entity<KundenAdresse>().HasData(new KundenAdresse()
            {
                Id = 1,
                KundenNr = 1000,
                GueltigAb = new DateTime(2022, 03, 07, 16, 26, 12),
                GueltigBis = new DateTime(2022, 03, 07, 16, 30, 12),
                KundeId = 1,
                AdresseId = 1

            });

            modelBuilder.Entity<KundenAdresse>().HasData(new KundenAdresse()
            {
                Id = 2,
                KundenNr = 1005,
                GueltigAb = new DateTime(2022, 03, 07, 16, 27, 03),
                GueltigBis = new DateTime(2022, 03, 07, 16, 28, 15),
                KundeId = 2,
                AdresseId = 2

            });

            modelBuilder.Entity<KundenAdresse>().HasData(new KundenAdresse()
            {
                Id = 3,
                KundenNr = 1010,
                GueltigAb = new DateTime(2022, 03, 07, 16, 27, 51),
                GueltigBis = new DateTime(2022, 03, 07, 16, 28, 50),
                KundeId = 3,
                AdresseId = 3

            });

            modelBuilder.Entity<KundenAdresse>().HasData(new KundenAdresse()
            {
                Id = 4,
                KundenNr = 1005,
                GueltigAb = new DateTime(2022, 03, 07, 16, 28, 15),
                GueltigBis = new DateTime(2022, 03, 07, 16, 29, 47),
                KundeId = 2,
                AdresseId = 4

            });

            modelBuilder.Entity<KundenAdresse>().HasData(new KundenAdresse()
            {
                Id = 5,
                KundenNr = 1010,
                GueltigAb = new DateTime(2022, 03, 07, 16, 28, 50),
                GueltigBis = maxDateValue,
                KundeId = 4,
                AdresseId = 3

            });

            modelBuilder.Entity<KundenAdresse>().HasData(new KundenAdresse()
            {
                Id = 6,
                KundenNr = 1005,
                GueltigAb = new DateTime(2022, 03, 07, 16, 29, 47),
                GueltigBis = maxDateValue,
                KundeId = 2,
                AdresseId = 5

            });

            modelBuilder.Entity<KundenAdresse>().HasData(new KundenAdresse()
            {
                Id = 7,
                KundenNr = 1000,
                GueltigAb = new DateTime(2022, 03, 07, 16, 30, 12),
                GueltigBis = maxDateValue,
                KundeId = 1,
                AdresseId = 6

            });
            #endregion

            #region Adresse

            modelBuilder.Entity<Adresse>().HasData(new Adresse()
            {
                Id = 1,
                Strasse = "Bahnhofstrasse",
                HausNr = "23",
                OrtschaftId = 1,
                GueltigAb = new DateTime(2022, 03, 07, 16, 26, 12),
                GueltigBis = new DateTime(2022, 03, 07, 16, 30, 13)
            });

            modelBuilder.Entity<Adresse>().HasData(new Adresse()
            {
                Id = 2,
                Strasse = "Safranstrasse",
                HausNr = "Safranstrasse",
                OrtschaftId = 2,
                GueltigAb = new DateTime(2022, 03, 07, 16, 27, 03),
                GueltigBis = new DateTime(2022, 03, 07, 16, 28, 15)
            });

            modelBuilder.Entity<Adresse>().HasData(new Adresse()
            {
                Id = 3,
                Strasse = "Tunnelstrasse",
                HausNr = "848",
                OrtschaftId = 3,
                GueltigAb = new DateTime(2022, 03, 07, 16, 27, 51),
                GueltigBis = maxDateValue
            });

            modelBuilder.Entity<Adresse>().HasData(new Adresse()
            {
                Id = 4,
                Strasse = "Kürbistrasse",
                HausNr = "23",
                OrtschaftId = 4,
                GueltigAb = new DateTime(2022, 03, 07, 16, 28, 15),
                GueltigBis = new DateTime(2022, 03, 07, 16, 29, 47)
            });

            modelBuilder.Entity<Adresse>().HasData(new Adresse()
            {
                Id = 5,
                Strasse = "Helvetiastrasse",
                HausNr = "11",
                OrtschaftId = 5,
                GueltigAb = new DateTime(2022, 03, 07, 16, 29, 47),
                GueltigBis = maxDateValue
            });

            modelBuilder.Entity<Adresse>().HasData(new Adresse()
            {
                Id = 6,
                Strasse = "Markplatz",
                HausNr = "5",
                OrtschaftId = 1,
                GueltigAb = new DateTime(2022, 03, 07, 16, 30, 12),
                GueltigBis = maxDateValue
            });
            #endregion

            #region Ortschaft

            modelBuilder.Entity<Ortschaft>().HasData(new Ortschaft()
            {
                Id = 1,
                PLZ = 9000,
                Ort = "St.Gallen",
                Aktiv = true,
            });

            modelBuilder.Entity<Ortschaft>().HasData(new Ortschaft()
            {
                Id = 2,
                PLZ = 5811,
                Ort = "Bern",
                Aktiv = true,
            });

            modelBuilder.Entity<Ortschaft>().HasData(new Ortschaft()
            {
                Id = 3,
                PLZ = 2114,
                Ort = "Genf",
                Aktiv = true,
            });

            modelBuilder.Entity<Ortschaft>().HasData(new Ortschaft()
            {
                Id = 4,
                PLZ = 6585,
                Ort = "Basel",
                Aktiv = true,
            });

            modelBuilder.Entity<Ortschaft>().HasData(new Ortschaft()
            {
                Id = 5,
                PLZ = 6437,
                Ort = "Zug",
                Aktiv = true,
            });
            #endregion

            #region Auftrag
            modelBuilder.Entity<Auftrag>().HasData(new Auftrag()
            {
                Id = 1,
                AuftragsNr = 2000,                
                Datum = new DateTime(2022, 03, 08, 00, 00, 00),
                KundeId = 1005
            });

            modelBuilder.Entity<Auftrag>().HasData(new Auftrag()
            {
                Id = 2,
                AuftragsNr = 2005,
                Datum = new DateTime(2022, 03, 04, 00, 00, 00),
                KundeId = 1000
            });

            modelBuilder.Entity<Auftrag>().HasData(new Auftrag()
            {
                Id = 3,
                AuftragsNr = 2010,
                Datum = new DateTime(2022, 02, 15, 00, 00, 00),
                KundeId = 1010
            });

            modelBuilder.Entity<Auftrag>().HasData(new Auftrag()
            {
                Id = 4,
                AuftragsNr = 2015,
                Datum = new DateTime(2022, 03, 22, 00, 00, 00),
                KundeId = 1005
            });
            #endregion

            #region Position
            modelBuilder.Entity<Position>().HasData(new Position()
            {
                Id = 1,
                PositionNr = 10,
                Menge = 8,
                AuftragId = 2000,
                ArtikelId = 10
            });

            modelBuilder.Entity<Position>().HasData(new Position()
            {
                Id = 2,
                PositionNr = 20,
                Menge = 4,
                AuftragId = 2000,
                ArtikelId = 100
            });

            modelBuilder.Entity<Position>().HasData(new Position()
            {
                Id = 3,
                PositionNr = 30,
                Menge = 6,
                AuftragId = 2000,
                ArtikelId = 80
            });

            modelBuilder.Entity<Position>().HasData(new Position()
            {
                Id = 4,
                PositionNr = 10,
                Menge = 2,
                AuftragId = 2005,
                ArtikelId = 70
            });

            modelBuilder.Entity<Position>().HasData(new Position()
            {
                Id = 5,
                PositionNr = 20,
                Menge = 3,
                AuftragId = 2005,
                ArtikelId = 40
            });

            modelBuilder.Entity<Position>().HasData(new Position()
            {
                Id = 6,
                PositionNr = 30,
                Menge = 3,
                AuftragId = 2005,
                ArtikelId = 10
            });

            modelBuilder.Entity<Position>().HasData(new Position()
            {
                Id = 7,
                PositionNr = 10,
                Menge = 15,
                AuftragId = 2010,
                ArtikelId = 20
            });

            modelBuilder.Entity<Position>().HasData(new Position()
            {
                Id = 8,
                PositionNr = 20,
                Menge = 4,
                AuftragId = 2010,
                ArtikelId = 30
            });

            modelBuilder.Entity<Position>().HasData(new Position()
            {
                Id = 9,
                PositionNr = 30,
                Menge = 100,
                AuftragId = 2010,
                ArtikelId = 50
            });

            modelBuilder.Entity<Position>().HasData(new Position()
            {
                Id = 10,
                PositionNr = 40,
                Menge = 3,
                AuftragId = 2010,
                ArtikelId = 100
            });

            modelBuilder.Entity<Position>().HasData(new Position()
            {
                Id = 11,
                PositionNr = 50,
                Menge = 2,
                AuftragId = 2010,
                ArtikelId = 90
            });

            modelBuilder.Entity<Position>().HasData(new Position()
            {
                Id = 12,
                PositionNr = 10,
                Menge = 12,
                AuftragId = 2015,
                ArtikelId = 60
            });
            #endregion
        }
    }
}
