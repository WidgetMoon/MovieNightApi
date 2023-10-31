using Microsoft.EntityFrameworkCore;
using MovieNight.Data.Entities;
using MovieNight.Data.Entities.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MovieNight.Domain.Domain;

namespace MovieNight.Data.DbContexts
{
    public class MovieNightDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

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
