

namespace Real_estate.Application.Models
{
    public class EmailSettings
    {
        public string ApiKey { get; init; } = default!;
        public string FromAddress { get; init; } = default!;
        public string FromName { get; init; } = default!;
    }
}
