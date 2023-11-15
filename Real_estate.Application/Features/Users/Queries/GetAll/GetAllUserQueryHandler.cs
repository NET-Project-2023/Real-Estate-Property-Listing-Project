using MediatR;
using Real_estate.Application.Persistence;

namespace Real_estate.Application.Features.Users.Queries.GetAll
{

    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, GetAllUserResponse>
    {
        private readonly IUserRepository repository;

        public GetAllUserQueryHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllUserResponse> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            GetAllUserResponse response = new();
            var result = await repository.GetAllAsync(); 
            if (result.IsSuccess)
            {
                response.Users = result.Value.Select(c => new UserDto
                {
                    UserId = c.UserId,
                    Name = c.Name,
                    Email = c.Email,
                    Password = c.Password,
                    UserRole = c.UserRole,
                    PhoneNumber = c.PhoneNumber
                }).ToList();
            }
            return response;
        }
    }

}
