﻿using MovieNight.Domain.Domain;

namespace MovieNight.Domain.Interfaces
{
    public interface IMovieNightRepository
    {
        Task AddAsync(Movie movie);
        Task<Movie> GetAsync(int id);
        Task<IEnumerable<Movie>> GetRandomMoviesAsync(int count);
        Task<int> CountMoviesAsync();
        Task<List<Leaflet>> GetOfferLeafletsAsync(List<string> products);
        Task<List<Leaflet>> GetAllActiveOffers();
    }
}
