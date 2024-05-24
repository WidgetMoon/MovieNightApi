using Microsoft.EntityFrameworkCore;
using MovieNight.Data.Entities.Configurations;
using MovieNight.Domain.Domain;

namespace MovieNight.Data.DbContexts
{
    public class MovieNightDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Leaflet> Leaflets { get; set; }

        public MovieNightDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureModelBuilder(modelBuilder);
        }

        public static void ConfigureModelBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MovieEntityConfiguration());
        }
    }
}
