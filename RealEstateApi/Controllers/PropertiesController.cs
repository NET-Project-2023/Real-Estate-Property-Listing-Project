using GlobalBuyTicket.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Real_estate.Application.Features.Properties.Commands.CreateProperty;

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
    }
}
