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
        Task<MovieEntity> GetAsync(int id);
        Task<IEnumerable<MovieEntity>> GetAllAsync();
    }
}
