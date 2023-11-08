namespace PropertyListing.Server.Domain.Entities
{
    public class User
    {
        public int UserId { get; private set; }
        public string Name { get; private set;}
        public string Email { get; private set;}
        public string Password { get; private set;}
        public Role UserRole { get; private set;}
        

        public User(int userId, string name, string email, string password, Role userRole)
        {
            UserId = userId;
            Name = name;
            Email = email;
            Password = password;
            UserRole = userRole;
        }
    }

    public enum Role
    {
        Customer,
        Owner,
        Admin
    }
}
