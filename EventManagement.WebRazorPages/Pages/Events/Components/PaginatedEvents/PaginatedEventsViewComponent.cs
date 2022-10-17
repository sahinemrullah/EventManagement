using EventManagement.WebRazorPages.Pages.Events.Models;
using EventManagement.WebRazorPages.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EventManagement.WebRazorPages.Pages.Events.Components.PaginatedEvents
{
    public class PaginatedEventsViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public PaginatedEventsViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int pageNumber, int pageSize, IEnumerable<int> categories, IEnumerable<int> cities)
        {
            var client = _httpClientFactory.CreateClient("WebAPI");
            string accessToken = HttpContext.User.FindAll("Access-Token").First().Value;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{accessToken}");
            categories ??= Array.Empty<int>();
            cities ??= Array.Empty<int>();
            StringBuilder stringBuilder = new("api/events/getpaginatedevents?");
            stringBuilder.AppendFormat("pageNumber={0}&pageSize={1}", pageNumber, pageSize);
            stringBuilder.AppendJoin(string.Empty, categories.Select(c => $"&categoryIds={c}"));
            stringBuilder.AppendJoin(string.Empty, cities.Select(c => $"&cityIds={c}"));
            string result = stringBuilder.ToString();
            IPaginatedList<EventListDto>? eventListDto = await client
                .GetFromJsonAsync<PaginatedList<EventListDto>>(result);
            return View(eventListDto);
        }
    }
}
