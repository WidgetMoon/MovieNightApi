using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieNight.Core.Handlers.Interfaces;

namespace MovieNight.ApiServer.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieHandler _movieHandler;

        public MoviesController(IMovieHandler movieHandler)
        {
            _movieHandler = movieHandler;
        }

        [HttpGet]
        public async Task<ActionResult> GetMovie()
        {
            var result = await _movieHandler.GetMovieById(1);

            if (result is not null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult> GetDirector()
        {
            return Empty;
        }
    }
}
