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

        public Task<IEnumerable<MovieEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<MovieEntity> GetAsync(int id)
        {
            return await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
