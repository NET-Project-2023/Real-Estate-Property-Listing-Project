using Real_estate.Application.Persistence;
using Real_estate.Domain.Entities;
using MediatR;
using System.Runtime.InteropServices;

namespace Real_estate.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
    {
        private readonly IUserRepository repository;

        public CreateUserCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateUserCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new CreateUserCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            var userResult = User.Create(request.Name, request.Email, request.Password, request.UserRole, request.PhoneNumber);


            System.Console.WriteLine(request.UserRole);
            if (!userResult.IsSucces)
            {
                return new CreateUserCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { userResult.Error }
                };
            }

            await repository.AddAsync(userResult.Value);

            return new CreateUserCommandResponse
            {
                Success = true,
                User = new CreateUserDto
                {
                    UserId = userResult.Value.UserId,
                    Name = userResult.Value.Name,
                    Email = userResult.Value.Email,
                    Password = userResult.Value.Password,
                    PhoneNumber = userResult.Value.PhoneNumber,
                    UserRole = userResult.Value.UserRole
                }
            };
        }
    }
}