using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        // De customizat, de ex cu proprietatile din entitatea User
        // IdentityUser: are majoritatea propr de care am putea avea nevoie (email, phoneNumber, etc.)
        public string? Name { get; set; }
    }
}
