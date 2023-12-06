using MediatR;
using Real_estate.Application.Persistence;
using Real_estate.Domain.Entities;

namespace Real_estate.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserCommandResponse>
    {
        private readonly IAsyncRepository<User> userRepository;

        public UpdateUserCommandHandler(IAsyncRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateUserCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new UpdateUserCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var userResult = await userRepository.FindByIdAsync(request.UserId);
            if (!userResult.IsSuccess)
            {
                return new UpdateUserCommandResponse
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            var user = userResult.Value;
            if (request.Name != null)
            {
                user.UpdateName(request.Name);
            }
            if (request.Email != null)
            {
                user.UpdateEmail(request.Email);
            }
            if (request.Password != null)
            {
                user.UpdatePassword(request.Password); // Ensure you hash the password if necessary
            }
            if (request.UserRole.HasValue)
            {
                user.UpdateUserRole(request.UserRole.Value);
            }
            if (request.PhoneNumber != null)
            {
                user.UpdatePhoneNumber(request.PhoneNumber);
            }

         

            var updateResult = await userRepository.UpdateAsync(user);

            if (!updateResult.IsSuccess)
            {
                return new UpdateUserCommandResponse
                {
                    Success = false,
                    Message = "Failed to update user."
                };
            }

            return new UpdateUserCommandResponse
            {
                Success = true,
                Message = "User updated successfully."
            };
        }
    }
}
