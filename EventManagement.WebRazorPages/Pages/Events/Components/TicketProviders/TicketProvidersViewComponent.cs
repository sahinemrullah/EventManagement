using EventManagement.WebRazorPages.Pages.Events.Models;
using EventManagement.WebRazorPages.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EventManagement.WebRazorPages.Pages.Events.Components.PaginatedEvents
{
    public class TicketProvidersViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TicketProvidersViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("WebAPI");
            string accessToken = HttpContext.User.FindAll("Access-Token").First().Value;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{accessToken}");
            IEnumerable<IntegragionServiceModel> integrationServices =  await client.GetFromJsonAsync<List<IntegragionServiceModel>>("api/integrationServices/getintegrationservices");
            return View(integrationServices);
        }
    }
}
