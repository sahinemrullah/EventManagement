namespace EventManagement.Application.Features.IntegrationServices.DTOs
{
    public class RegisterIntegrationServiceDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string WebDomain { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
    }
}
