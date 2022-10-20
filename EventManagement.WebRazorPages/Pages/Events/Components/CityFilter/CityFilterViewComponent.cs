using EventManagement.WebRazorPages.Extensions;
using EventManagement.WebRazorPages.Pages.Events.Models;
using EventManagement.WebRazorPages.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.WebRazorPages.Pages.Events.Components.CityFilter
{
    public class CityFilterViewComponent : ViewComponent
    {
        private readonly HttpClient _client;
        public CityFilterViewComponent(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.GetAPIClient();
        }

        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<int> selectedCities, string? name = null)
        {
            selectedCities ??= Array.Empty<int>();

            List<CityFilterDto>? result = await _client.GetFromJsonAsync<List<CityFilterDto>>((API.Definition.GetAll("city")));

            if (result is not null && selectedCities.Any())
                result.ForEach(c => c.Selected = selectedCities.Contains(c.Id));

            if (name != null)
                return View(name, result);

            return View(result);
        }
    }
}
