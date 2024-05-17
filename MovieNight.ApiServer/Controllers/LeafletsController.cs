using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieNight.Core.Handlers.Interfaces;
using MovieNight.Core.Models.LeafletsRequest;

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
        /// Gets leaflets for given products that are actually in offer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /leaflets
        ///     {
        ///         "products": [
        ///             "Orion",
        ///             "Uhorky",
        ///             "Rajo"
        ///         ]
        ///     }
        ///
        /// Sample response:
        ///
        ///     [
        /// {
        /// "id": 63,
        /// "name": "Uhorky nakladačky voľný predaj. 1 kg",
        /// "offPercent": -27,
        /// "oldPrice": 3.99,
        /// "newPrice": 2.89,
        /// "createdAt": "2024-05-17T07:35:04.2677482",
        /// "effectiveFrom": "2024-05-15T00:00:00",
        /// "effectiveTo": "2024-05-21T00:00:00"
        /// },
        /// {
        /// "id": 85,
        /// "name": "Orion Kofila 35 g",
        /// "offPercent": -26,
        /// "oldPrice": 0.75,
        /// "newPrice": 0.55,
        /// "createdAt": "2024-05-17T07:35:14.1203142",
        /// "effectiveFrom": "2024-05-15T00:00:00",
        /// "effectiveTo": "2024-05-21T00:00:00"
        /// },
        /// {
        /// "id": 113,
        /// "name": "Rajo Lakto Free Acidko kyslomliečny nápoj* viac druhov, 450 g",
        /// "offPercent": -31,
        /// "oldPrice": 1.45,
        /// "newPrice": 0.99,
        /// "createdAt": "2024-05-17T07:35:19.7850364",
        /// "effectiveFrom": "2024-05-15T00:00:00",
        /// "effectiveTo": "2024-05-21T00:00:00"
        /// },
        /// {
        /// "id": 114,
        /// "name": "Rajo smotana na šľahanie 33 % 250 ml",
        /// "offPercent": -41,
        /// "oldPrice": 1.79,
        /// "newPrice": 1.05,
        /// "createdAt": "2024-05-17T07:35:19.8154246",
        /// "effectiveFrom": "2024-05-15T00:00:00",
        /// "effectiveTo": "2024-05-21T00:00:00"
        /// }
        /// ]
        /// </remarks>
        /// <param name="products">Products names</param>
        /// <returns>Leaflets for products.</returns>
        /// <response code="200">Returns leaflets for products.</response>
        /// <response code="404">If the leaflets are not found.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLeaflets(LeafletsOfferRequest request)
        {
            var products = request.Products;
            var leaflets = await _movieHandler.GetOfferLeafletsAsync(products);
            if (leaflets == null || leaflets.Count == 0)
                return NotFound();
            return Ok(leaflets);
        }
    }
}
