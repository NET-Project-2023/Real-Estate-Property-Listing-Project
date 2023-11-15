using Microsoft.AspNetCore.Mvc;
using Real_estate.Application.Features.Properties.Commands.CreateProperty;
using Real_estate.Application.Features.Properties.Queries.GetAll;
using Real_estate.Application.Features.Properties.Queries.GetById;
using Real_estate.Application.Features.Properties.Queries.GetByName;

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
    }
}
