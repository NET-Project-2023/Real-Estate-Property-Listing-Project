using MediatR;
using Real_estate.Application.Persistence;

namespace Real_estate.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserByUsernameCommandHandler : IRequestHandler<DeleteUserByUsernameCommand, DeleteUserCommandResponse>
    {
        private readonly IUserManager userRepository;

        public DeleteUserByUsernameCommandHandler(IUserManager userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<DeleteUserCommandResponse> Handle(DeleteUserByUsernameCommand request, CancellationToken cancellationToken)
        {
            // Attempt to retrieve the user's information using the provided username
            var userResult = await userRepository.FindByUsernameAsync(request.Username);

            // Check if the operation was successful and the user was found
            if (!userResult.IsSuccess || userResult.Value == null)
            {
                // The operation was not successful, so return an error response
                return new DeleteUserCommandResponse { Success = false, Message = "User not found" };
            }

            // The operation was successful, and the user was found, now retrieve the user's GUID
            var userDto = userResult.Value; // This assumes that Result<T> has a property named Value that holds the data

            // Proceed to delete the user using the GUID
            var deleteResult = await userRepository.DeleteAsync(Guid.Parse(userDto.UserId)); // Assuming userDto.UserId is a string that contains the GUID

            // Check if the delete operation was successful
            if (!deleteResult.IsSuccess)
            {
                // The deletion was not successful, return an error response
                return new DeleteUserCommandResponse { Success = false, Message = deleteResult.Error };
            }

            // The deletion was successful, return a success response
            return new DeleteUserCommandResponse { Success = true };
        }
    }
}
