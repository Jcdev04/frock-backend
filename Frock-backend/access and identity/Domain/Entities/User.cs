using Frock_backend.access_and_identity.Domain.ValueObjects;

namespace Frock_backend.access_and_identity.Domain.Entities
{
    public class User
    {
        public userID Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Email Email { get; private set; }
        public string PasswordHash { get; private set; }
        public UserRole Role { get; private set; }
        public DateTime CreatedAt { get; private set; }

        // Constructor para Entity Framework
        private User() { }

        public User(string firstName, string lastName, string email, string password, UserRole role)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be null or empty");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be null or empty");

            Id = new userID(firstName, lastName);
            FirstName = firstName;
            LastName = lastName;
            Email = new Email(email);
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            Role = role;
            CreatedAt = DateTime.UtcNow;
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }

        // ✅ Método para devolver datos seguros (sin PasswordHash)
        public object ToSafeObject()
        {
            return new
            {
                Id = Id.Value,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email.Value,
                Role = Role,
                CreatedAt = CreatedAt
            };
        }
    }
}
