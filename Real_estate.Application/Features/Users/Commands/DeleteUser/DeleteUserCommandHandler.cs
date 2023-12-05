using MediatR;
using Real_estate.Application.Contracts;
using Real_estate.Domain.Entities;

namespace Real_estate.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserCommandResponse>
    {
        private readonly IAsyncRepository<User> userRepository;

        public DeleteUserCommandHandler(IAsyncRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<DeleteUserCommandResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var deleteResult = await userRepository.DeleteAsync(request.UserId);

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
