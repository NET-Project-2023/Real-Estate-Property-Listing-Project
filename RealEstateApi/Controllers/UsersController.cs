using Microsoft.AspNetCore.Mvc;
using Real_estate.Application.Features.Listings.Commands.CreateUser;
using Real_estate.Application.Features.Listings.Queries.GetAll;
using Real_estate.Application.Features.Listings.Queries.GetById;
using Real_estate.Application.Features.Listings.Commands.DeleteUser;
using Real_estate.Application.Features.Users.Commands.UpdateUser;

namespace RealEstate.API.Controllers
{

    public class UsersController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllUserQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdUserQuery(id));
            return Ok(result);
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid userId)
        {
            var command = new DeleteUserCommand { UserId = userId };
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Message);
        }
        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid userId, UpdateUserCommand command)
        {
            if (userId != command.UserId)
            {
                return BadRequest("User ID mismatch.");
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
