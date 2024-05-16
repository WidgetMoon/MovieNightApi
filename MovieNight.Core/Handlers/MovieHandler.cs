using Microsoft.Extensions.Configuration;
using MovieNight.Core.Handlers.Interfaces;
using MovieNight.Core.Mappers;
using MovieNight.Core.Models.ImdbResponseModel;
using MovieNight.Data.Entities;
using System.Net.Http.Json;
using MovieNight.Domain.Domain;
using MovieNight.Domain.Interfaces;

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

        public async Task<Movie> GetMovieById(int id)
        {
            return await _movieRepository.GetAsync(id);
        }

        public async Task GetTop100Movies()
        {
            if (await _movieRepository.CountMoviesAsync() >= 100)
            {
                return;
            }

            var client = new HttpClient();
            var rapidKey = _configuration["XRapidKey:ImdbTop100"];

            if (rapidKey is null)
            {
                throw new ArgumentNullException(nameof(rapidKey));
            }

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

                if (body is null)
                {
                    throw new HttpRequestException("Response body is null.");
                }

                var result = ImdbMovieMapper.Map(body);

                foreach (var item in result)
                {
                    await _movieRepository.AddAsync(item);
                }
            }
        }

        public async Task<IEnumerable<Movie>> GetRandomMovies(int count)
        {
            return await _movieRepository.GetRandomMoviesAsync(count);
        }

        public Task GetTop250ImdbMovies()
        {
            throw new NotImplementedException();
        }

        public Task<List<Leaflet>> GetOfferLeafletsAsync(List<string> products)
        {
            return _movieRepository.GetOfferLeafletsAsync(products);
        }
    }
}
