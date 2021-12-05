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
            builder.Property(x => x.KundenNr).IsRequired();
        }
    }
}
