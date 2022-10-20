namespace EventManagement.WebRazorPages.Pages.Models
{
    public class AccessToken
    {
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
