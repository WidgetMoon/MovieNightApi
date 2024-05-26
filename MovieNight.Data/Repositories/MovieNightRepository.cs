using Microsoft.EntityFrameworkCore;
using MovieNight.Data.DbContexts;
using MovieNight.Domain.Domain;
using MovieNight.Domain.Interfaces;

namespace MovieNight.Data.Repositories
{
    public class MovieNightRepository : IMovieNightRepository
    {
        private readonly MovieNightDbContext _dbContext;

        public MovieNightRepository(MovieNightDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Movie movie)
        {
            if (movie == null) return;
            await _dbContext.Movies.AddAsync(movie);
            _dbContext.SaveChanges();
        }

        public async Task<Movie> GetAsync(int id)
        {
            return await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Movie>> GetRandomMoviesAsync(int count)
        {
            return await _dbContext.Movies.OrderBy(r => Guid.NewGuid()).Take(count).ToListAsync();
        }

        public async Task<int> CountMoviesAsync()
        {
            return await _dbContext.Movies.CountAsync();
        }

        public async Task<List<Leaflet>> GetOfferLeafletsAsync(List<string> productsName)
        {
            var products = await GetAllActiveOffers();

            //return products that contain the productsName
            var leaflets = products.Where(p => productsName
                .Any(name => p.FullPlainText.Contains(name))).ToList();
            return leaflets;
        }

        public async Task<List<Leaflet>> GetAllActiveOffers()
        {
            var products = await _dbContext.Leaflets
                .Where(l => l.EffectiveTo >= DateTime.Now)
                .OrderBy(p => p.Name)
                .ToListAsync();
            return products;
        }
    }
}
