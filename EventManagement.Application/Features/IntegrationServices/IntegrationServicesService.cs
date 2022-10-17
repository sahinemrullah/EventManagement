using EventManagement.Application.Features.Credentials;
using EventManagement.Application.Features.IntegrationServices.DTOs;
using EventManagement.Domain.Entities;
using EventManagement.Domain.Security;
using EventManagement.Domain.Validations.Interfaces;
using EventManagement.Persistence;

namespace EventManagement.Application.Features.IntegrationServices
{
    internal class IntegrationServicesService : CredentialsServiceBase<IntegrationService>, IIntegrationServicesService
    {
        public IntegrationServicesService(EventManagementDbContext dbContext, IPasswordHasher passwordHasher, IResultFactory resultFactory) 
            : base(dbContext, passwordHasher, resultFactory)
        {
        }

        public IEnumerable<IntegrationServiceListDto> GetIntegrationServices()
        {
            return CredentialsSet.Select(i => new IntegrationServiceListDto()
            {
                CompanyName = i.CompanyName,
                WebDomain = i.WebDomain,
            }).ToList();
        }
    }
}
