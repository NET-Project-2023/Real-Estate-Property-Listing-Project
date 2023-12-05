using Microsoft.AspNetCore.Mvc;
using Real_estate.Application.Features.Listings.Commands.CreateListing;
using Real_estate.Application.Features.Listings.Commands.DeleteListing;
using Real_estate.Application.Features.Listings.Queries.GetAll;
using Real_estate.Application.Features.Listings.Queries.GetById;
using Real_estate.Application.Features.Properties.Queries.GetAll;
using Real_estate.Application.Features.Properties.Queries.GetById;
using Real_estate.Application.Features.Properties.Queries.GetByName;

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

        [HttpGet("GetAllListings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllListingQuery());
            return Ok(result);
        }

        [HttpGet("ById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdListingQuery(id));
            return Ok(result);
        }

        [HttpDelete("{listingId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid listingId)
        {
            var command = new DeleteListingCommand { ListingId = listingId };
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Message);
        }

    }
}
