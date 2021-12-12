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
    public class KundenAdresseKonfiguration : IEntityTypeConfiguration<KundenAdresse>
    {

        public void Configure(EntityTypeBuilder<KundenAdresse> builder)
        {
            builder.Property(x => x.Id).IsRequired();
        }
    }
}
