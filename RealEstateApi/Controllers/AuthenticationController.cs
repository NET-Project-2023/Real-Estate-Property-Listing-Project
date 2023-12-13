using Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Real_estate.Application.Contracts.Identity;
using Real_estate.Application.Contracts.Interfaces;
using Real_estate.Application.Features.Listings.Commands.CreateUser;
using Real_estate.Application.Models.Identity;
using RealEstate.API.Models;
using static Real_estate.Domain.Enums.Enums;

namespace RealEstate.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly ICurrentUserService currentUserService;
        private readonly IMediator _mediator;


        public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger, IMediator mediator, ICurrentUserService currentUserService)
        {
            _authService = authService;
            _logger = logger;
            _mediator = mediator;
            this.currentUserService = currentUserService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.Login(model);

                if (status == 0)
                {
                    return BadRequest(message);
                }

                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Invalid model state: {ModelState}");
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.Registeration(model, UserRoles.User);

                if (status == 0)
                {
                    return BadRequest(message);
                }

                var createUserCommand = new CreateUserCommand
                {
                    Name = model.Name,
                    UserName = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    UserRole = Role.User, 
                };

                var createUserResult = await _mediator.Send(createUserCommand);
                if (!createUserResult.Success)
                {
                    return BadRequest(createUserResult);
                }

                return CreatedAtAction(nameof(Register), model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _authService.Logout();
                return Ok("Logout successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet]
        [Route("currentuserinfo")]
        public CurrentUser CurrentUserInfo()
        {
            if (this.currentUserService.GetCurrentUserId() == null)
            {
                return new CurrentUser
                {
                    IsAuthenticated = false
                };
            }
            return new CurrentUser
            {
                IsAuthenticated = true,
                UserName = this.currentUserService.GetCurrentUserId(),
                Claims = this.currentUserService.GetCurrentClaimsPrincipal().Claims.ToDictionary(c => c.Type, c => c.Value)
            };
        }
    }
}
