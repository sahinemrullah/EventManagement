namespace EventManagement.WebAPI.Models
{
    public class PasswordChangeDto
    {
        public string NewPasswordConfirmation { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}