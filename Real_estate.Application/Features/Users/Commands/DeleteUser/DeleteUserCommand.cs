using MediatR;

namespace Real_estate.Application.Features.Listings.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<DeleteUserCommandResponse>
    {
        public Guid UserId { get; set; }
    }
}
