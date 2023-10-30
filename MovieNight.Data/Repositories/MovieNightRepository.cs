using Microsoft.EntityFrameworkCore;
using MovieNight.Data.DbContexts;
using MovieNight.Data.Entities;
using MovieNight.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNight.Data.Repositories
{
    public class MovieNightRepository : IMovieNightRepository
    {
        private readonly MovieNightDbContext _dbContext;

        public MovieNightRepository(MovieNightDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(MovieEntity entity)
        {
            if (entity == null) return;
            await _dbContext.Movies.AddAsync(entity);
            _dbContext.SaveChanges();
        }
        
        public async Task<MovieEntity> GetAsync(int id)
        {
            return await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<MovieEntity>> GetRandomMoviesAsync(int count)
        {
            return await _dbContext.Movies.OrderBy(r => Guid.NewGuid()).Take(count).ToListAsync();
        }

        public async Task<int> CountMoviesAsync()
        {
            return await _dbContext.Movies.CountAsync();
        }
    }
}
