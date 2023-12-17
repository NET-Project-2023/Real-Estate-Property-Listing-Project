

namespace Real_estate.Application.Features.Listings
{
    public class UserDto
    {
        public string UserId { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public List<string>? Roles { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
