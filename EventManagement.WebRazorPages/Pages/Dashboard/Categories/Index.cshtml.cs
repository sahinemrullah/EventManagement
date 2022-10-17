using EventManagement.WebRazorPages.Pages.Definitions;
using EventManagement.WebRazorPages.ServiceConfigurations;

namespace EventManagement.WebRazorPages.Pages.Categories
{
    public class IndexModel : DefinitionIndexModel<CategoryDto>
    {
        public IndexModel(IHttpClientFactory httpClientFactory, IUserAccessor userAccessor) : base(httpClientFactory, userAccessor, "api/category")
        {
        }

    }
}
