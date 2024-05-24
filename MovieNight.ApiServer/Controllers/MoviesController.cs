using Microsoft.AspNetCore.Mvc;
using MovieNight.Core.Handlers.Interfaces;

namespace MovieNight.ApiServer.Controllers
{
    /// <summary>
    /// Gets Movies and downloads top 100 from IMDB. Also what am I supposed to write here?
    /// </summary>
    [Route("api/movies")]
    [ApiController]
    [Produces("application/json")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieHandler _movieHandler;

        /// <inheritdoc />
        public MoviesController(IMovieHandler movieHandler)
        {
            _movieHandler = movieHandler;
        }

        /// <summary>
        /// Gets one movie by id.
        /// </summary>
        /// <remarks>
        /// Sample response:
        ///
        ///     GET /Movie
        ///     {
        ///         "id": 2,
        ///         "title": "The Godfather",
        ///         "dateDownloaded": "2023-10-30T10:51:10.6885646",
        ///         "rank": 2,
        ///         "thumbnail": "https://m.media-amazon.com/images/M/MV5BM2MyNjYxNmUtYTAwNi00MTYxLWJmNWYtYzZlODY3ZTk3OTFlXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_UY67_CR1,0,45,67_AL_.jpg",
        ///         "rating": 9.2,
        ///         "year": 1972,
        ///         "image": "https://m.media-amazon.com/images/M/MV5BM2MyNjYxNmUtYTAwNi00MTYxLWJmNWYtYzZlODY3ZTk3OTFlXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_QL75_UY562_CR8,0,380,562_.jpg",
        ///         "description": "The aging patriarch of an organized crime dynasty in postwar New York City transfers control of his clandestine empire to his reluctant youngest son.",
        ///         "trailer": null,
        ///         "genres": [
        ///         "Crime",
        ///         "Drama"
        ///         ],
        ///         "directors": null,
        ///         "writers": null,
        ///         "imdbId": "tt0068646"
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>A movie by its id.</returns>
        /// <response code="200">Returns a movie by it's id.</response>
        /// <response code="404">If the movie is not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetMovie(int id)
        {
            if (id < 1 || id > 100)
            {
                return BadRequest("Id must be between 1 and 100");
            }

            var result = await _movieHandler.GetMovieById(id);

            if (result is not null)
            {
                return Ok(result);
            }

            return NotFound("Movie with given ID does not exist.");
        }

        /// <summary>
        /// Gets random movies in range 1-100.
        /// </summary>
        /// <param name="count">Range from 1-100</param>
        /// <returns>A list of random movies.</returns>
        /// <response code="200">A list of random movies.</response>
        /// <response code="404">When [count] is out of range.</response>
        [HttpGet("random/{count}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetRandomMovies(int count)
        {
            if (count < 1 || count > 100)
            {
                return BadRequest("Count must be between 1 and 100");
            }
            var result = await _movieHandler.GetRandomMovies(count);
            return Ok(result);
        }

        /// <summary>
        /// Downloads top 100 movies from a RapidApi.
        /// </summary>
        /// <returns></returns>
        [HttpGet("downloadTop100")]
        public async Task<ActionResult> DownloadTopMovies()
        {
            try
            {
                await _movieHandler.GetTop100Movies();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}
