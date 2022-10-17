using EventManagement.Domain.Security;

namespace EventManagement.Domain.Common
{
    public interface ICredential
    {
        public string Email { get; }
        public string PasswordHash { get; }
        public void SetPassword(string password, IPasswordHasher passwordHasher);
    }
}
