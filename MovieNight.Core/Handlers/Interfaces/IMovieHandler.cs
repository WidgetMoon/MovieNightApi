﻿using MovieNight.Domain.Domain;

namespace MovieNight.Core.Handlers.Interfaces
{
    public interface IMovieHandler
    {
        Task<Movie> GetMovieById(int id);
        Task GetTop100Movies();
        Task<IEnumerable<Movie>> GetRandomMovies(int count);
        Task GetTop250ImdbMovies();
        Task<Dictionary<string, List<Leaflet>>> GetOfferLeafletsAsync(List<string> products);
        Task<List<Leaflet>> GetAllActiveOffers();
    }
}
