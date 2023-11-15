using Real_estate.Application.Persistence;
using MediatR;



namespace Real_estate.Application.Features.Users.Queries.GetById
{
    public class GetByIdCategoryHandler : IRequestHandler<GetByIdUserQuery, UserDto>
    {
        private readonly IUserRepository repository;

        public GetByIdCategoryHandler(IUserRepository repository)
        {
            this.repository = repository;
        }
        public async Task<UserDto> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            var c = await repository.FindByIdAsync(request.Id);
            if (c.IsSuccess)
            {
                return new UserDto
                {
                    UserId = c.Value.UserId,
                    Name = c.Value.Name,
                    Email = c.Value.Email,
                    Password = c.Value.Password,
                    UserRole = c.Value.UserRole,
                    PhoneNumber = c.Value.PhoneNumber
                };
            }
            return new UserDto();
        }
    }
}
