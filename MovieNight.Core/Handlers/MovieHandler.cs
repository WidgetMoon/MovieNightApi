using MovieNight.Core.Handlers.Interfaces;
using MovieNight.Data.Entities;
using MovieNight.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNight.Core.Handlers
{
    public class MovieHandler : IMovieHandler
    {
        private readonly IMovieNightRepository _movieRepository;

        public MovieHandler(IMovieNightRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<MovieEntity> GetMovieById(int id)
        {
            return await _movieRepository.GetAsync(id);
        }
    }
}
