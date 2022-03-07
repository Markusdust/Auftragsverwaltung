using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
        public string connectionstring = "Data Source=localhost; Database=Auftragsverwaltung; Trusted_Connection=True";
        //Markus
        //public string connectionstring = "Data Source=localhost; Database=Auftragsverwaltung; Trusted_Connection=True";
        //Andy
        //public string connectionstring ="Data Source=.\\SQLEXPRESS; Database=Auftragsverwaltung; Trusted_Connection=True";
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
                .StartsAt(1000)
                .IncrementsBy(5);

            modelBuilder.Entity<Kunde>()
                .Property(c => c.KundenNr)
                .HasDefaultValueSql("NEXT VALUE FOR shared.KundenNummern");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuftragContext).Assembly);


            modelBuilder.HasSequence<int>("AuftragNr", schema: "shared")
                .StartsAt(2000)
                .IncrementsBy(5);

            modelBuilder.Entity<Auftrag>()
                .Property(c => c.AuftragsNr)
                .HasDefaultValueSql("NEXT VALUE FOR shared.AuftragNr");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuftragContext).Assembly);
        }
    }
}
