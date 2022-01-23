using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class AuftragContext : DbContext
    {
        public DbSet<Kunde> Kunden { get; set; }
        public DbSet<Adresse> Adressen { get; set; }
        public DbSet<KundenAdresse> KundenAdressen { get; set; }
        public DbSet<Artikel> Artikel { get; set; }
        public DbSet<Artikelgruppe> Artikelgruppe { get; set; }
        public DbSet<Auftrag> Auftraege { get; set; }
        public DbSet<Position> Positionen { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Markus
            //optionsBuilder.UseSqlServer("Data Source=localhost; Database=Auftragsverwaltung; Trusted_Connection=True");
            // Andy
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS; Database=Auftragsverwaltung; Trusted_Connection=True");
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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuftragContext).Assembly);
        }
    }
}
