using Microsoft.Extensions.Configuration;
using MovieNight.Core.Handlers.Interfaces;
using MovieNight.Core.Helpers;
using MovieNight.Core.Mappers;
using MovieNight.Core.Models.ImdbResponseModel;
using MovieNight.Domain.Domain;
using MovieNight.Domain.Interfaces;
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

        public async Task<Dictionary<string, List<Leaflet>>> GetOfferLeafletsAsync(List<string> products)
        {
            //insert all products to lower with linq
            products = products.Select(p => p.RemoveDiacriticsSpacesLower()).ToList();
            var offers = await _movieRepository.GetAllActiveOffers();
            var result = new Dictionary<string, List<Leaflet>>();
            foreach (var product in products)
            {
                var offered = offers.Where(o => o.FullPlainText.RemoveDiacriticsSpacesLower().Contains(product)).ToList();
                if (offered.Any())
                {
                    result.Add(product, offered);
                }
            }

            return result;
        }

        //TODO: Just commit message bro
        public async Task<List<Leaflet>> GetAllActiveOffers()
        {
            return await _movieRepository.GetAllActiveOffers();
        }
    }
}
