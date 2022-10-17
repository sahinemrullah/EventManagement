using EventManagement.Application.Features.Credentials;
using EventManagement.Application.Features.IntegrationServices.DTOs;
using EventManagement.Domain.Entities;

namespace EventManagement.Application.Features.IntegrationServices
{
    public interface IIntegrationServicesService : ICredentialsService<IntegrationService>
    {
        IEnumerable<IntegrationServiceListDto> GetIntegrationServices();
    }
}
