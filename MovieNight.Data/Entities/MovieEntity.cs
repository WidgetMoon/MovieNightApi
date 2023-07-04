using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNight.Data.Entities
{
    public class MovieEntity
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public float? Rating { get; private set; }
        public DateTime? DateDownloaded { get; private set; }

        public MovieEntity(string title, float? rating, DateTime? dateDownloaded)
        {
            Title = title;
            Rating = rating;
            DateDownloaded = dateDownloaded;
        }
    }
}
