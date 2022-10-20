using EventManagement.WebRazorPages.Pages.Definitions;
using Microsoft.AspNetCore.Authorization;

namespace EventManagement.WebRazorPages.Pages.Cities
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : DefinitionIndexModel<CityDto>
    {
        public IndexModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory, "city")
        {
        }

    }
}
