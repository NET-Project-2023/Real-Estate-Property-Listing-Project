using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Real_estate.Application.Features.Properties.Commands.CreateProperty;
using Real_estate.Application.Features.Properties.Commands.DeleteProperty;
using Real_estate.Application.Features.Properties.Commands.UpdateProperty;
using Real_estate.Application.Features.Properties.Queries.GetAll;
using Real_estate.Application.Features.Properties.Queries.GetById;
using Real_estate.Application.Features.Properties.Queries.GetByName;
using Real_estate.Application.Features.Properties.Queries.GetByOwnerUsername;

namespace RealEstate.API.Controllers
{

    public class PropertiesController : ApiControllerBase
    {
        private readonly ILogger<PropertiesController> _logger;
        public PropertiesController(ILogger<PropertiesController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = "User")]
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

        [Authorize(Roles = "User")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllPropertyQuery());
            return Ok(result);
        }

        [Authorize(Roles = "User")]
        [HttpGet("ById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdPropertyQuery(id));
            return Ok(result);
        }


        [Authorize(Roles = "User")]
        [HttpGet("ByName/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string name)
        {
            var result = await Mediator.Send(new GetByNamePropertyQuery(name));
            return Ok(result);
        }

        [Authorize(Roles = "User")]
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
        [Authorize(Roles = "User")]
        [HttpPut("update/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(string title, UpdatePropertyCommand command)
        {

            if (title != command.Title)
            {
                _logger.LogWarning("Property ID mismatch. Provided title: {ProvidedTitle}, Command title: {CommandTitle}", title, command.Title);
                return BadRequest("Property ID mismatch.");
            }

            var result = await Mediator.Send(command);

            _logger.LogInformation("Update command result: Success = {Success}, Message = {Message}", result.Success, result.Message);

            if (!result.Success)
            {
                //_logger.LogWarning("Update command failed: {Message}", result.Message);
                return BadRequest(result.Message);
            }

            //return Ok(result.Message);
            return Ok(new { Message = "Property updated successfully." });

        }

        [Authorize(Roles = "User")]
        [HttpGet("ByCurrentUser/{ownerUsername}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCurrentUser(string ownerUsername)
        {
            //var username = User.FindFirst(ClaimTypes.Name)?.Value;
            //Console.WriteLine("Username extracted: ", username);
            var result = await Mediator.Send(new GetByOwnerUsernamePropertyQuery(ownerUsername));
            return Ok(result);
        }
    }
}