using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieNight.Core.Handlers.Interfaces;

namespace MovieNight.ApiServer.Controllers
{
    [Route("api/leaflets")]
    [ApiController]
    [Produces("application/json")]
    public class LeafletsController : ControllerBase
    {
        private readonly IMovieHandler _movieHandler;

        public LeafletsController(IMovieHandler movieHandler)
        {
            _movieHandler = movieHandler;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLeaflets([FromBody] List<string> products)
        {
            var leaflets = await _movieHandler.GetOfferLeafletsAsync(products);
            if (leaflets == null || leaflets.Count == 0)
                return NotFound();
            return Ok(leaflets);
        }
    }
}
