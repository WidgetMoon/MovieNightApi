﻿using Microsoft.Extensions.Configuration;
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
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
                Headers =
                         {
                             { "X-RapidAPI-Key", "0aa776835emsh0fa5a8145b88371p1a6cb6jsncc22a8fe7c7d" },
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
            return await _movieRepository.GetRandomMovies(count);
        }
    }
}
