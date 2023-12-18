using Microsoft.AspNetCore.Mvc;
using Real_estate.Application.Features.Listings.Commands.CreateListing;
using Real_estate.Application.Features.Listings.Commands.DeleteListing;
using Real_estate.Application.Features.Listings.Queries.GetAll;
using Real_estate.Application.Features.Listings.Queries.GetById;
using Real_estate.Application.Features.Listings.Commands.UpdateListing;
using Microsoft.AspNetCore.Authorization;

namespace RealEstate.API.Controllers
{
    public class ListingsController : ApiControllerBase
    {
        [Authorize(Roles = "User")]
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

        [Authorize(Roles = "User")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllListingQuery());
            return Ok(result);
        }

        [HttpGet("ByTitle/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string title)
        {
            var result = await Mediator.Send(new GetByIdListingQuery(title));
            return Ok(result);
        }

        [HttpDelete("/delete/{listingId}")]
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
        [HttpPut("/update/{listingTitle}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(string listingTitle, UpdateListingCommand command)
        {
            if (listingTitle != command.Title)
            {
                return BadRequest("Listing ID mismatch.");
            }

            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }
    }
}
