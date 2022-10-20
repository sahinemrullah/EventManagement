using EventManagement.WebRazorPages.Pages.Definitions;

namespace EventManagement.WebRazorPages.Pages.Categories
{
    public class IndexModel : DefinitionIndexModel<CategoryDto>
    {
        public IndexModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory, "category")
        {
        }

    }
}
