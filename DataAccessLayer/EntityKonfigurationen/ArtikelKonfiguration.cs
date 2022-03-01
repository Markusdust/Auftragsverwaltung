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
    public class ArtikelKonfiguration : IEntityTypeConfiguration<Artikel>
    {
        public void Configure(EntityTypeBuilder<Artikel> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd(); // .IsRequired()
            builder.Property(x => x.Bezeichnung).IsRequired().HasMaxLength(255);
            builder.Property(x => x.PreisNetto).IsRequired();
            builder.Property(x => x.Aktiv).IsRequired();
           

        }
    }
}