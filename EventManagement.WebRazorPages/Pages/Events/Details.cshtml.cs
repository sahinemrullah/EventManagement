using EventManagement.WebRazorPages.Pages.Events.Models;
using EventManagement.WebRazorPages.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.WebRazorPages.Pages.Events
{
    [Authorize]
    public class DetailsModel : APIClientPageModelBase
    {
        public DetailsModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public EventModel EventModel { get; set; } = null!;
        public async Task<IActionResult> OnGet(int id)
        {
            return await GetAsync(API.Event.Get(id), GetResult);
        }
        private async Task<IActionResult> GetResult(HttpContent httpContent)
        {
            EventModel = await httpContent.ReadFromJsonAsync<EventModel>();
            return Page();
        }
    }
}
