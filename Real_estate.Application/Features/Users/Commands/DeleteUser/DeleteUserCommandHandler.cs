using MediatR;
using Real_estate.Application.Persistence;
using Real_estate.Domain.Entities;
using Real_estate.Domain.Common; // Make sure you include the namespace for the Result<T> class.

namespace Real_estate.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserByUsernameCommandHandler : IRequestHandler<DeleteUserByUsernameCommand, DeleteUserCommandResponse>
    {
        private readonly IAsyncRepository<User> userRepository;

        public DeleteUserByUsernameCommandHandler(IAsyncRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<DeleteUserCommandResponse> Handle(DeleteUserByUsernameCommand request, CancellationToken cancellationToken)
        {
            // Find user by username
            var userResult = await userRepository.FindByConditionAsync(u => u.Name == request.Username);
            if (!userResult.IsSuccess || userResult.Value == null) // Check for success and if value is not null
            {
                return new DeleteUserCommandResponse
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            // At this point, it's safe to assume userResult.Value is not null.
            var deleteResult = await userRepository.DeleteAsync(userResult.Value.UserId);

            if (!deleteResult.IsSuccess)
            {
                return new DeleteUserCommandResponse
                {
                    Success = false,
                    Message = deleteResult.Error
                };
            }

            return new DeleteUserCommandResponse
            {
                Success = true,
                Message = "User deleted successfully."
            };
        }
    }
}