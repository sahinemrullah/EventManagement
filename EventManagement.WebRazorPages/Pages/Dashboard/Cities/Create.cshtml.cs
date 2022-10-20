using EventManagement.WebRazorPages.Pages.Definitions;
using Microsoft.AspNetCore.Authorization;

namespace EventManagement.WebRazorPages.Pages.Cities
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : DefinitionCreateModel
    {
        public CreateModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory, "city")
        {

        }
    }
}
