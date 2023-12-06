using Microsoft.AspNetCore.Mvc;
using Real_estate.Application.Features.Listings.Commands.CreateProperty;
using Real_estate.Application.Features.Listings.Commands.DeleteProperty;
using Real_estate.Application.Features.Listings.Queries.GetAll;
using Real_estate.Application.Features.Listings.Queries.GetById;
using Real_estate.Application.Features.Listings.Queries.GetByName;
using Real_estate.Application.Features.Properties.Commands.UpdateProperty;

namespace RealEstate.API.Controllers
{
    public class PropertiesController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreatePropertyCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("GetAllProperties")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllPropertyQuery());
            return Ok(result);
        }

        [HttpGet("ById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdPropertyQuery(id));
            return Ok(result);
        }
        [HttpGet("ByName/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string name)
        {
            var result = await Mediator.Send(new GetByNamePropertyQuery(name));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeletePropertyCommand { PropertyId = id };
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Message);
        }
        [HttpPut("{propertyId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid propertyId, UpdatePropertyCommand command)
        {
            if (propertyId != command.PropertyId)
            {
                return BadRequest("Property ID mismatch.");
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
