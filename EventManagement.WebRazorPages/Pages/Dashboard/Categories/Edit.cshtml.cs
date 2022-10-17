using EventManagement.WebRazorPages.Pages.Definitions;
using EventManagement.WebRazorPages.ServiceConfigurations;
using Microsoft.AspNetCore.Authorization;

namespace EventManagement.WebRazorPages.Pages.Categories
{
    [Authorize(Roles = "Admin")]
    public class EditModel : DefinitionEditModel
    {
        public EditModel(IHttpClientFactory httpClientFactory, IUserAccessor userAccessor) : base(httpClientFactory, userAccessor, "api/category")
        {
        }
    }
}
