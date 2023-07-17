using MovieNight.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNight.Data.Interfaces
{
    public interface IMovieNightRepository
    {
        Task AddAsync(MovieEntity entity);
        Task<MovieEntity> GetAsync(int id);
        Task<IEnumerable<MovieEntity>> GetAllAsync();
        Task<IEnumerable<MovieEntity>> GetRandomMovies(int count);
    }
}
