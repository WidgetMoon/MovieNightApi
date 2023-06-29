using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieNight.Core.Managers.Interfaces;

namespace MovieNight.ApiServer.Controllers
{
    [Route("api/v{version:apiVersion}/movies")]
    [ApiController]
    [ApiVersion("1.0")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieManager movieManager;

        public MoviesController(IMovieManager movieManager)
        {
            this.movieManager = movieManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetMovie()
        {
            var result = movieManager.GetMovie();
            return Ok(result);
        }
    }
}
