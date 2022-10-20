using EventManagement.WebRazorPages.Pages.Definitions;
using Microsoft.AspNetCore.Authorization;

namespace EventManagement.WebRazorPages.Pages.Cities
{
    [Authorize(Roles = "Admin")]
    public class EditModel : DefinitionEditModel
    {
        public EditModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory, "city")
        {
        }
    }
}
