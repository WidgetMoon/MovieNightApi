using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieNight.Core.Handlers.Interfaces;

namespace MovieNight.ApiServer.Controllers
{
    /// <summary>
    /// Endpoints for leaflets.
    /// </summary>
    [Route("api/leaflets")]
    [ApiController]
    [Produces("application/json")]
    public class LeafletsController : ControllerBase
    {
        private readonly IMovieHandler _movieHandler;

        /// <inheritdoc />
        public LeafletsController(IMovieHandler movieHandler)
        {
            _movieHandler = movieHandler;
        }

        /// <summary>
        /// Gets leaflets for the given products.
        /// </summary>
        /// <param name="products">Products names</param>
        /// <returns>Leaflets for products.</returns>
        /// <response code="200">Returns leaflets for products.</response>
        /// <response code="404">If the leaflets are not found.</response>
        [HttpPost()]
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
