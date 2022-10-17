using EventManagement.WebRazorPages.Pages.Events.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.WebRazorPages.Pages.Events.Components.CategoryFilter
{
    public class CategoryFilterViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoryFilterViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<int> selectedCategories, string? name = null)
        {
            selectedCategories ??= Array.Empty<int>();

            HttpClient client = _httpClientFactory.CreateClient("WebAPI");

            string accessToken = HttpContext.User.FindAll("Access-Token").First().Value;

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{accessToken}");

            List<CategoryFilterDto>? result = await client.GetFromJsonAsync<List<CategoryFilterDto>>("api/category/getall");
            if (result is not null)
                result.ForEach(c => c.Selected = selectedCategories.Contains(c.Id));

            if (name != null)
                return View(name, result);

            return View(result);
        }
    }
}
