using EventManagement.WebRazorPages.Pages.Definitions;
using EventManagement.WebRazorPages.ServiceConfigurations;
using Microsoft.AspNetCore.Authorization;

namespace EventManagement.WebRazorPages.Pages.Cities
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : DefinitionIndexModel<CityDto>
    {
        public IndexModel(IHttpClientFactory httpClientFactory, IUserAccessor userAccessor) : base(httpClientFactory, userAccessor, "api/city")
        {
        }

    }
}
