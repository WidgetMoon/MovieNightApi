using MovieNight.Core.Models.ImdbResponseModel;
using MovieNight.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNight.Core.Mappers
{
    public static class ImdbMovieMapper
    {
        public static IEnumerable<MovieEntity> Map(IEnumerable<ImdbMovieModel>? from)
        {
            if(from is null) return Enumerable.Empty<MovieEntity>();

            var result = new List<MovieEntity>();

            foreach (var item in from)
            {
                var mappedItem = MapMovie(item);
                result.Add(mappedItem);
            }

            return result;
        }

        public static MovieEntity MapMovie(ImdbMovieModel from)
        {
            return new MovieEntity(
                title: from.Title,
                dateDownloaded: DateTime.UtcNow,
                rank: from.Rank,
                thumbnail: from.Thumbnail,
                rating: from.Rating,
                year: from.Year,
                image: from.Image,
                description: from.Description,
                trailer: from.Trailer,
                genres: from.Genres,
                directors: from.Directors,
                writers: from.Writers,
                imdbId: from.ImdbId
                );
        }
    }
}
