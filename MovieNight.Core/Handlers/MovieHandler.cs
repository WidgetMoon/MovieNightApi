using Microsoft.Extensions.Configuration;
using MovieNight.Core.Handlers.Interfaces;
using MovieNight.Core.Mappers;
using MovieNight.Core.Models.ImdbResponseModel;
using MovieNight.Data.Entities;
using MovieNight.Data.Interfaces;
using System.Net.Http.Json;

namespace MovieNight.Core.Handlers
{
    public class MovieHandler : IMovieHandler
    {
        private readonly IMovieNightRepository _movieRepository;
        private readonly IConfiguration _configuration;

        public MovieHandler(IMovieNightRepository movieRepository, IConfiguration configuration)
        {
            _movieRepository = movieRepository;
            _configuration = configuration;
        }

        public async Task<MovieEntity> GetMovieById(int id)
        {
            return await _movieRepository.GetAsync(id);
        }

        public async Task<MovieEntity> GetTop100Movies()
        {
            if (await _movieRepository.CountMoviesAsync() >= 100)
            {
                return await _movieRepository.GetAsync(1);
            }

            var client = new HttpClient();
            var rapidKey = _configuration["XRapidKey:ImdbTop100"];
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
                Headers =
                         {
                             { "X-RapidAPI-Key", rapidKey },
                             { "X-RapidAPI-Host", "imdb-top-100-movies.p.rapidapi.com" },
                         },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadFromJsonAsync<IEnumerable<ImdbMovieModel>>();

                var result = ImdbMovieMapper.Map(body);

                Console.WriteLine(body);

                foreach (var item in result)
                {
                    await _movieRepository.AddAsync(item);
                }
            }

            return await _movieRepository.GetAsync(1);
        }

        public async Task<IEnumerable<MovieEntity>> GetRandomMovies(int count)
        {
            return await _movieRepository.GetRandomMoviesAsync(count);
        }

        public Task GetTop250ImdbMovies()
        {
            throw new NotImplementedException();
        }
    }
}
