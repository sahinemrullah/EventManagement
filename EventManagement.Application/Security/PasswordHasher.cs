using EventManagement.Domain.Security;

namespace EventManagement.Application.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public bool CheckPasswordHash(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }
    }
}
