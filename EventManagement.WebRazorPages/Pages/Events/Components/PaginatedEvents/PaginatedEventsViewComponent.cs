using EventManagement.WebRazorPages.Extensions;
using EventManagement.WebRazorPages.Pages.Events.Models;
using EventManagement.WebRazorPages.Pages.Models;
using EventManagement.WebRazorPages.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EventManagement.WebRazorPages.Pages.Events.Components.PaginatedEvents
{
    public class PaginatedEventsViewComponent : ViewComponent
    {
        private readonly HttpClient _client;
        public PaginatedEventsViewComponent(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.GetAPIClient();
        }

        public async Task<IViewComponentResult> InvokeAsync(int pageNumber, int pageSize, IEnumerable<int> categories, IEnumerable<int> cities)
        {
            categories ??= Array.Empty<int>();
            cities ??= Array.Empty<int>();
            IPaginatedList<EventListDto>? eventListDto = await _client
                .GetFromJsonAsync<PaginatedList<EventListDto>>(API.Event.GetPaginated(pageNumber, pageSize, categories, cities));
            return View(eventListDto);
        }
    }
}
