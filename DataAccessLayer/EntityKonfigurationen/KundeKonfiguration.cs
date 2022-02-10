using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EntityKonfigurationen
{
    public class KundeKonfiguration : IEntityTypeConfiguration<Kunde>
    {

        public void Configure(EntityTypeBuilder<Kunde> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            //Autoinkrement implementieren ab 1000 in 5er schritten
            //builder.Property(x => x.KundenNr).IsRequired();

            builder.Property(x => x.Nachname).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Vorname).IsRequired().HasMaxLength(255);

            builder.Property(x => x.Firma).HasMaxLength(255);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Passwort).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Website).HasMaxLength(255);

            builder.Property(x => x.GueltigAb).IsRequired();
            builder.Property(x => x.GueltigBis).IsRequired();

            
        }
    }
}
