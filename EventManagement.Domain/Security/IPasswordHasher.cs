namespace EventManagement.Domain.Security
{
    public interface IPasswordHasher
    {
        public string HashPassword(string password);

        public bool CheckPasswordHash(string password, string hashedPassword);
    }
}
