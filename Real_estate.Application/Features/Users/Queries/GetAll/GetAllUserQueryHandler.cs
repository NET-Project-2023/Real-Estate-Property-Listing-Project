using MediatR;
using Real_estate.Application.Persistence;

namespace Real_estate.Application.Features.Listings.Queries.GetAll
{

    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, GetAllUserResponse>
    {
        private readonly IUserManager repository;

        public GetAllUserQueryHandler(IUserManager repository)
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
                    Username = c.Username,
                    Email = c.Email,
                    //Password = c.Password,
                    Roles = c.Roles,
                    PhoneNumber = c.PhoneNumber
                }).ToList();
            }
            return response;
        }
    }

}
