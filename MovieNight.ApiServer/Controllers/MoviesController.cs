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

        [HttpGet("{id}")]
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

        [HttpGet("random/{count}")]
        public async Task<ActionResult> GetRandomMovies(int count)
        {
            if(count < 1 || count > 100)
            {
                return BadRequest("Count must be between 1 and 100");
            }
            var result = await _movieHandler.GetRandomMovies(count);
            return Ok(result);
        }

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
