using EventManagement.WebRazorPages.Pages.Events.Models;
using EventManagement.WebRazorPages.Pages.Shared;
using EventManagement.WebRazorPages.ServiceConfigurations;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.WebRazorPages.Pages.Events
{
    public class DetailsModel : APIClientPageModelBase
    {
        public DetailsModel(IHttpClientFactory httpClientFactory, IUserAccessor userAccessor) : base(httpClientFactory, userAccessor)
        {
        }

        public EventDto EventDto { get; set; } = null!;
        public async Task<IActionResult> OnGet(int id)
        {
            return await GetAsync($"api/events/get/{id}", GetResult);
        }
        private async Task<IActionResult> GetResult(HttpContent httpContent)
        {
            EventDto = await httpContent.ReadFromJsonAsync<EventDto>();
            return Page();
        }
    }
}
