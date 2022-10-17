using EventManagement.Domain.Common;
using EventManagement.Domain.Security;
using EventManagement.Domain.Validations;
using EventManagement.Domain.Validations.Interfaces;

namespace EventManagement.Domain.Entities
{
    public sealed class IntegrationService : IEntity, ICredential
    {
        private IntegrationService()
        {

        }

        public int Id { get; private set; }
        public string WebDomain { get; private set; } = null!;
        public string CompanyName { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;

        public static IResult<IntegrationService> Create(string email, string webDomain, string companyName)
        {
            ValidationResult<IntegrationService> validationResult = new ();

            validationResult.ValidateEmail(email);

            validationResult.ValidateString(nameof(companyName), companyName, 255);

            validationResult.ValidateString(nameof(webDomain), webDomain, 255);

            if (!(Uri.TryCreate(webDomain, UriKind.Absolute, out Uri? uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)))
                validationResult.AddError(nameof(webDomain), "Please enter a valid URL.");

            if (!validationResult.IsSuccess)
                return validationResult;

            IntegrationService integrationService = new()
            {
                Email = email,
                WebDomain = webDomain,
                CompanyName = companyName
            };

            return Result<IntegrationService>.Success(integrationService);
        }

        public void SetPassword(string password, IPasswordHasher passwordHasher)
        {
            PasswordHash = passwordHasher.HashPassword(password);
        }
    }
}
