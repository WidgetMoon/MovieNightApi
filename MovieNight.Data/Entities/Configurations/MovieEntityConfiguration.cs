using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieNight.Domain.Domain;

namespace MovieNight.Data.Entities.Configurations
{
    public class MovieEntityConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movie");
            builder.HasKey(x => x.Id);

            builder.Property(m => m.Writers).HasConversion(v => string.Join(',', v!),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
            builder.Property(m => m.Directors).HasConversion(v => string.Join(',', v!),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
            builder.Property(m => m.Genres).HasConversion(v => string.Join(',', v!),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

        }
    }
}
