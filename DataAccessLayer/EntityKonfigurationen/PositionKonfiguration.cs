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
    public class PositionKonfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.PositionNr).IsRequired().HasMaxLength(45);
            builder.Property(x => x.Menge).IsRequired().HasMaxLength(45);
        }
    }
}
