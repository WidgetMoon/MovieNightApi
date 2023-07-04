using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNight.Data.Entities.Configurations
{
    public class MovieEntityConfiguration : IEntityTypeConfiguration<MovieEntity>
    {
        public void Configure(EntityTypeBuilder<MovieEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(m => m.Title).IsRequired().HasMaxLength(255);
            builder.Property(m => m.Rating).IsRequired(false);
        }
    }
}
