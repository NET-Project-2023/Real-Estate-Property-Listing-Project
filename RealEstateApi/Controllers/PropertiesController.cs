using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly RealEstateContext _context;
        private readonly ILogger<PropertiesController> _logger;
        public PropertiesController(ILogger<PropertiesController> logger, RealEstateContext context)
        {
            _logger = logger;
            _context = context;

        }

        [Authorize(Roles = "User")]
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
                _logger.LogInformation($"CUMVA APELEZ AICI?");
                _logger.LogInformation($"DUMNEZEUUUUUUUUUUUU10 {command.ImagesFiles} .");

                command.Images = await UtilityFunctions.ConvertToByteArrayAsync(command.ImagesFiles,_logger);
            }
            _logger.LogInformation($"{command.Images} result:");


            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [Authorize(Roles = "User")]
        [HttpPut("update/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromForm] UpdatePropertyCommand command)
        {
            _logger.LogInformation("Received update request for property: {Title}", command.Title);

            _logger.LogInformation($"DUMNEZEUUUUUUUUUUUU {command.Images} .");
            _logger.LogInformation($"DUMNEZEUUUUUUUUUUUU2 {command.ImagesFiles} .");



            if (string.IsNullOrEmpty(command.Title))
            {
                return BadRequest("Title is required for property update.");
            }
            _logger.LogInformation($"DUMNEZEUUUUUUUUUUUU2 {command.ImagesFiles} .");


            // Update property details except images
            var updateResult = await Mediator.Send(command);
            if (!updateResult.Success)
            {
                return BadRequest(updateResult.Message);
            }
            _logger.LogInformation($"DUMNEZEUUUUUUUUUUUU3333 {command.ImagesFiles} .");

            // Fetch the property again to update images
            var propertyToUpdate = await _context.Properties
                              .FirstOrDefaultAsync(p => p.Title == command.Title);

            if (propertyToUpdate == null)
            {
                return BadRequest("Property not found.");
            }

            _logger.LogInformation($"DUMNEZEUUUUUUUUUUUU334444444444443 {command.ImagesFiles} .");
            _logger.LogInformation($"DUMNEZEUUUUUUUUUUUU334444444444443 {command.ImagesFiles} .");
            if (command.ImagesFiles != null && command.ImagesFiles.Any())
            {
                _logger.LogInformation($"Received {command.ImagesFiles.Count} image files.");
                var newImages = await UtilityFunctions.ConvertImagesToByteArrayAsync(command.ImagesFiles, _logger);
                _logger.LogInformation($"Converted {newImages.Count} new images.");

                // Your logic to handle these byte arrays
                // For example, assigning them to your property's Images field
                propertyToUpdate.Images.AddRange(newImages);

            }
            else
            {
                _logger.LogInformation("No new images to add.");
            }

            _logger.LogInformation($"Received {command.Images?.Count ?? 0} image files for conversion.");

            // Clear existing images and add new ones
            _logger.LogInformation($"Received {command.ImagesFiles?.Count ?? 0} image files for conversion.");

            propertyToUpdate.Images.Clear();
          

            await _context.SaveChangesAsync();
            _logger.LogInformation($"Saved {propertyToUpdate.Images.Count} images to the database.");


            await _context.SaveChangesAsync();

            return Ok(new { Message = "Property updated successfully." });
        }


        [Authorize(Roles = "User")]
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

        [Authorize(Roles = "User")]
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


        [Authorize(Roles = "User")]
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