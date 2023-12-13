using MediatR;
using Real_estate.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserByUsernameCommand : IRequest<DeleteUserCommandResponse>
{
    public string Username { get; set; }
}