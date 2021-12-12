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
   public class OrtschaftKonfiguration : IEntityTypeConfiguration<Ortschaft>
    {
        public void Configure(EntityTypeBuilder<Ortschaft> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.PLZ).IsRequired();
            builder.Property(x => x.Ort).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Aktiv).IsRequired();

        }
    }
}
