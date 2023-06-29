using Microsoft.Extensions.Configuration;
using MovieNight.Core.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNight.Core.Managers
{
    public class MovieManager : IMovieManager
    {
        private readonly IConfiguration configuration;

        public MovieManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetMovie()
        {
            Console.WriteLine(configuration.GetSection("DbConnectionString"));
            var sad = configuration.GetSection("DbConnectionString");
            return string.Empty;
        }
    }
}
