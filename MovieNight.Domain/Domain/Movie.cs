using System.ComponentModel.DataAnnotations;

namespace MovieNight.Domain.Domain
{
    public class Movie
    {
        [Key]
        public int Id { get; private set; }
        public string? Title { get; private set; }
        public DateTime? DateDownloaded { get; set; }
        public int? Rank { get; private set; }
        public Uri? Thumbnail { get; set; }
        public float? Rating { get; set; }
        public int? Year { get; set; }
        public Uri? Image { get; set; }
        public string? Description { get; set; }
        public Uri? Trailer { get; set; }
        public IEnumerable<string>? Genres { get; set; }
        public IEnumerable<string>? Directors { get; set; }
        public IEnumerable<string>? Writers { get; set; }
        public string? ImdbId { get; set; }

        private Movie() { }

        public Movie(string title, DateTime? dateDownloaded, int rank, Uri thumbnail, float rating, int year, Uri image, string description, Uri trailer, IEnumerable<string> genres, IEnumerable<string> directors, IEnumerable<string> writers, string imdbId)
        {
            Title = title;
            DateDownloaded = dateDownloaded;
            Rank = rank;
            Thumbnail = thumbnail;
            Rating = rating;
            Year = year;
            Image = image;
            Description = description;
            Trailer = trailer;
            Genres = genres;
            Directors = directors;
            Writers = writers;
            ImdbId = imdbId;
        }
    }
}
