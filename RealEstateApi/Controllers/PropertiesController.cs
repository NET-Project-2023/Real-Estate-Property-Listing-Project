using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Real_estate.Application.Features.Properties.Commands.CreateProperty;
using Real_estate.Application.Features.Properties.Commands.DeleteProperty;
using Real_estate.Application.Features.Properties.Commands.UpdateProperty;
using Real_estate.Application.Features.Properties.Queries.GetAll;
using Real_estate.Application.Features.Properties.Queries.GetById;
using Real_estate.Application.Features.Properties.Queries.GetByName;
using Real_estate.Application.Features.Properties.Queries.GetByOwnerUsername;
using RealEstate.API.Utility;


namespace RealEstate.API.Controllers
{

    public class PropertiesController : ApiControllerBase
    {
        private readonly ILogger<PropertiesController> _logger;
        public PropertiesController(ILogger<PropertiesController> logger)
        {
            _logger = logger;
        }

        //[Authorize(Roles = "User")]
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromForm] CreatePropertyCommand command)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (command.ImagesFiles != null) 
            {
                command.Images = await UtilityFunctions.ConvertToByteArrayAsync(command.ImagesFiles);
            }
            _logger.LogInformation($"{command.Images} result:");


            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        //[Authorize(Roles = "User")]
        [HttpPut("update/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromForm] UpdatePropertyCommand command)
        {
            if (string.IsNullOrEmpty(command.Title))
            {
                return BadRequest("Title is required for property update.");
            }

            // Check if ImagesFiles is empty or null
            if (command.ImagesFiles != null && command.ImagesFiles.Any())
            {
                command.Images = await UtilityFunctions.ConvertToByteArrayAsync(command.ImagesFiles);
            }
            else
            {
                // Set Images to null
                command.Images = null;
            }

            var result = await Mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(new { Message = "Property updated successfully." });
        }


        //[Authorize(Roles = "User")]
        //[Authorize(Roles = "Admin")]
        [HttpGet]
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

        //[Authorize(Roles = "User")]
        //[Authorize(Roles = "Admin")]
        [HttpDelete("{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string title)
        {
            var command = new DeletePropertyCommand { PropertyTitle = title };
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Message);
        }


        //[Authorize(Roles = "User")]
        //[Authorize(Roles = "Admin")]
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