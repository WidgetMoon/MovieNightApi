using MovieNight.Core.Models.ImdbResponseModel;
using MovieNight.Domain.Domain;

namespace MovieNight.Core.Mappers
{
    public static class ImdbMovieMapper
    {
        public static IEnumerable<Movie> Map(IEnumerable<ImdbMovieModel>? from)
        {
            if(from is null) return Enumerable.Empty<Movie>();

            var result = new List<Movie>();

            foreach (var item in from)
            {
                var mappedItem = MapMovie(item);
                result.Add(mappedItem);
            }

            return result;
        }

        public static Movie MapMovie(ImdbMovieModel from)
        {
            return new Movie(
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
