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
        /// Gets offerings for given products that are actually in offer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /leaflets
        ///     {
        ///         "products": [
        ///             "Captain",
        ///             "Tesco"
        ///         ]
        ///     }
        ///
        /// Sample response:
        ///
        ///     {
        ///         "captain": [
        ///           {
        ///               "id": 462,
        ///               "name": "Captain Jack viac druhov, 330 ml",
        ///               "fullPlainText": "captainjackviacdruhov,330ml",
        ///               "offPercent": -27,
        ///               "oldPrice": 1.09,
        ///               "newPrice": 0.79,
        ///               "createdAt": "2024-05-17T19:16:40.9454282",
        ///               "effectiveFrom": "2024-05-15T00:00:00",
        ///               "effectiveTo": "2024-05-28T00:00:00"
        ///           },
        ///           {
        ///               "id": 466,
        ///               "name": "Captain Morgan original spiced gold 35 % 0.71",
        ///               "fullPlainText": "captainmorganoriginalspicedgold35%0.71",
        ///               "offPercent": -26,
        ///               "oldPrice": 14.99,
        ///               "newPrice": 10.99,
        ///               "createdAt": "2024-05-17T19:16:40.9776342",
        ///               "effectiveFrom": "2024-05-15T00:00:00",
        ///               "effectiveTo": "2024-05-28T00:00:00"
        ///           }
        ///         ],
        ///         "tesco": [
        ///           {
        ///               "id": 467,
        ///               "name": "SIM karta Tesco mobile Trio 15 GB",
        ///               "fullPlainText": "simkartatescomobiletrio15gb",
        ///               "offPercent": -58,
        ///               "oldPrice": 11.99,
        ///               "newPrice": 4.99,
        ///               "createdAt": "2024-05-17T19:16:40.9865201",
        ///               "effectiveFrom": "2024-05-15T00:00:00",
        ///               "effectiveTo": "2024-05-28T00:00:00"
        ///           },
        ///           {
        ///               "id": 498,
        ///               "name": "Tesco kapsuly viac druhov, 112 - 256 g",
        ///               "fullPlainText": "tescokapsulyviacdruhov,112-256g",
        ///               "offPercent": -17,
        ///               "oldPrice": 4.09,
        ///               "newPrice": 3.39,
        ///               "createdAt": "2024-05-17T19:16:44.7283397",
        ///               "effectiveFrom": "2024-05-15T00:00:00",
        ///               "effectiveTo": "2024-05-28T00:00:00"
        ///           }
        ///         ]
        ///     }
        /// </remarks>
        /// <param name="products">Products names</param>
        /// <returns>Leaflets for products.</returns>
        /// <response code="200">Returns offers for products.</response>
        /// <response code="404">If no offers are found.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOffers(LeafletsOfferRequest request)
        {
            var products = request.Products;
            var leaflets = await _movieHandler.GetOfferLeafletsAsync(products);
            if (leaflets == null || leaflets.Count == 0)
                return NotFound("No offers were found for the given products.");
            return Ok(leaflets);
        }

        /// <summary>
        /// Returns all active products where [EffectiveTo] is in the future and [EffectiveFrom] is in the past.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns all active offers.</response>
        /// <response code="404">If no active offers were found.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllActiveOffers()
        {
            var leaflets = await _movieHandler.GetAllActiveOffers();
            if (leaflets == null || leaflets.Count == 0)
                return NotFound("No active offers were found. Please wait for new leaflets.");
            return Ok(leaflets);
        }
    }
}
