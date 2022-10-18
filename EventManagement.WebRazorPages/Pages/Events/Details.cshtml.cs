using EventManagement.WebRazorPages.Pages.Events.Models;
using EventManagement.WebRazorPages.Pages.Shared;
using EventManagement.WebRazorPages.ServiceConfigurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.WebRazorPages.Pages.Events
{
    [Authorize]
    public class DetailsModel : APIClientPageModelBase
    {
        public DetailsModel(IHttpClientFactory httpClientFactory, IUserAccessor userAccessor) : base(httpClientFactory, userAccessor)
        {
        }

        public EventModel EventModel { get; set; } = null!;
        public async Task<IActionResult> OnGet(int id)
        {
            return await GetAsync($"api/events/get/{id}", GetResult);
        }
        private async Task<IActionResult> GetResult(HttpContent httpContent)
        {
            EventModel = await httpContent.ReadFromJsonAsync<EventModel>();
            return Page();
        }
    }
}
