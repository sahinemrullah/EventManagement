using EventManagement.WebRazorPages.Pages.Events.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.WebRazorPages.Pages.Events.Components.CityFilter
{
    public class CityFilterViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CityFilterViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<int> selectedCities, string? name = null)
        {
            selectedCities ??= Array.Empty<int>();
            HttpClient client = _httpClientFactory.CreateClient("WebAPI");
            string accessToken = HttpContext.User.FindAll("Access-Token").First().Value;

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{accessToken}");

            List<CityFilterDto>? result = await client.GetFromJsonAsync<List<CityFilterDto>>("api/city/getall");

            if (result is not null && selectedCities.Any())
                result.ForEach(c => c.Selected = selectedCities.Contains(c.Id));

            if (name != null)
                return View(name, result);

            return View(result);
        }
    }
}
