using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieNight.Data.DbContexts;
using MovieNight.Data.Interfaces;
using MovieNight.Data.Repositories;

namespace MovieNight.Data
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection PersistenceServiceRegistrations<TDbContext>(this IServiceCollection services,
            IConfiguration configuration) 
            where TDbContext : MovieNightDbContext
        {
            services.AddDbContext<MovieNightDbContext>(db=>
                                            db.UseSqlServer(configuration.GetConnectionString("DbConnection")));
            var asd = configuration.GetConnectionString("DbConnection");
            services.AddScoped<IMovieNightRepository, MovieNightRepository>();

            return services;
        }
    }
}
