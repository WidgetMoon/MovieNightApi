using MovieNight.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNight.Core.Handlers.Interfaces
{
    public interface IMovieHandler
    {
        Task<MovieEntity> GetMovieById(int id);
        Task<MovieEntity> GetTop100Movies();
        Task<IEnumerable<MovieEntity>> GetRandomMovies(int count);
    }
}
