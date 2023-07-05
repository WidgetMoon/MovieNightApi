using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MovieNight.Core.Models.ImdbResponseModel
{
    public class ImdbMovieModel
    {
        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("thumbnail")]
        public Uri Thumbnail { get; set; }

        [JsonPropertyName("rating")]
        public float Rating { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("image")]
        public Uri Image { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("trailer")]
        public Uri Trailer { get; set; }

        [JsonPropertyName("genre")]
        public IEnumerable<string> Genres { get; set; }

        [JsonPropertyName("director")]
        public IEnumerable<string> Directors { get; set; }

        [JsonPropertyName("writers")]
        public IEnumerable<string> Writers { get; set; }

        [JsonPropertyName("imdbid")]
        public string ImdbId { get; set; }
    }
}
