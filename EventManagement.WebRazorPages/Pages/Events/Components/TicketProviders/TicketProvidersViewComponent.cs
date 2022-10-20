using EventManagement.WebRazorPages.Extensions;
using EventManagement.WebRazorPages.Pages.Events.Models;
using EventManagement.WebRazorPages.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.WebRazorPages.Pages.Events.Components.PaginatedEvents
{
    public class TicketProvidersViewComponent : ViewComponent
    {
        private readonly HttpClient _client;
        public TicketProvidersViewComponent(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.GetAPIClient();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<IntegragionServiceModel> integrationServices =  await _client.GetFromJsonAsync<List<IntegragionServiceModel>>(API.IntegrationService.GetAll);
            return View(integrationServices);
        }
    }
}
