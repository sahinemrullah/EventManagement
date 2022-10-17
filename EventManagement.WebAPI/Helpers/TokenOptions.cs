namespace EventManagement.WebAPI.Helpers
{
    public class TokenOptions
    {
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public string SecurityKey { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string Issuer { get; set; } = null!;
    }
}