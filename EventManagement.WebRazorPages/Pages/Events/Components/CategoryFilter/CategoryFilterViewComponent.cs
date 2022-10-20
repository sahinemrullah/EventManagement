using EventManagement.WebRazorPages.Extensions;
using EventManagement.WebRazorPages.Pages.Events.Models;
using EventManagement.WebRazorPages.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.WebRazorPages.Pages.Events.Components.CategoryFilter
{
    public class CategoryFilterViewComponent : ViewComponent
    {
        private readonly HttpClient _client;
        public CategoryFilterViewComponent(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.GetAPIClient();
        }

        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<int> selectedCategories, string? name = null)
        {
            selectedCategories ??= Array.Empty<int>();

            List<CategoryFilterDto>? result = await _client.GetFromJsonAsync<List<CategoryFilterDto>>(API.Definition.GetAll("category"));
            if (result is not null)
                result.ForEach(c => c.Selected = selectedCategories.Contains(c.Id));

            if (name != null)
                return View(name, result);

            return View(result);
        }
    }
}
