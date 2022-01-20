using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EntityKonfigurationen
{
    public class AuftragKonfiguration : IEntityTypeConfiguration<Auftrag>
    {
        public void Configure(EntityTypeBuilder<Auftrag> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.AuftragsNr).IsRequired().HasMaxLength(45);
            builder.Property(x => x.Datum).IsRequired();
        }
    }
}
