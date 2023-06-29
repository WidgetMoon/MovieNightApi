using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieNight.ApiServer.Controllers
{
    [Route("api/v{version:apiVersion}/movies")]
    [ApiController]
    [ApiVersion("1.0")]
    public class MoviesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetMovie()
        {
            var movie = "Movie";
            return Ok(movie);
        }
    }
}
