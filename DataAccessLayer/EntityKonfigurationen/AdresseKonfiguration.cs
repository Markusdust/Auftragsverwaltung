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
   public class AdresseKonfiguration : IEntityTypeConfiguration<Adresse>
    {
        public void Configure(EntityTypeBuilder<Adresse> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Strasse).IsRequired().HasMaxLength(255);
            builder.Property(x => x.HausNr).IsRequired().HasMaxLength(255);
            builder.Property(x => x.GueltigAb).IsRequired();
            builder.Property(x => x.GueltigBis).IsRequired();
        }
    }
}
