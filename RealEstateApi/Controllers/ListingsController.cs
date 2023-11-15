using Microsoft.AspNetCore.Mvc;
using Real_estate.Application.Features.Listings.Commands.CreateListing;

namespace RealEstate.API.Controllers
{
    public class ListingsController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateListingCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
