using Real_estate.Application.Persistence;
using MediatR;
using Real_estate.Application.Features.Users.Queries.GetById;

namespace Real_estate.Application.Features.Listings.Queries.GetById
{
    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, GetByIdUserQueryResponse>
    {
        private readonly IUserManager userRepository;
        public GetByIdUserQueryHandler(IUserManager userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<GetByIdUserQueryResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Este in QUERY request username gol?: {request.Username}");
            var result = await userRepository.FindByUsernameAsync(request.Username);
            Console.WriteLine($"In QUERY am gasit username: {result.Value.Username}");
            if (!result.IsSuccess)
                return new GetByIdUserQueryResponse { Success = false, Message = result.Error };
            var userDto = result.Value;
            return new GetByIdUserQueryResponse
            {
                Success = true,
                User = new UserDto
                {
                    UserId = userDto.UserId,
                    Name = userDto.Name,
                    Username = userDto.Username,
                    Email = userDto.Email,
                    Roles = userDto.Roles,
                    PhoneNumber = userDto.PhoneNumber
                }
            };

        }
    }
}
