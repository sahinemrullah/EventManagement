using EventManagement.WebRazorPages.Extensions;
using EventManagement.WebRazorPages.Pages.Events.Models;
using EventManagement.WebRazorPages.Pages.Shared;
using EventManagement.WebRazorPages.ServiceConfigurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.WebRazorPages.Pages.Events
{
    [Authorize]
    public class EditModel : APIClientPageModelBase
    {
        public EditModel(IHttpClientFactory httpClientFactory, IUserAccessor userAccessor) : base(httpClientFactory, userAccessor)
        {
        }
        [BindProperty]
        public EventDto EventDto { get; set; } = null!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            return await GetAsync($"api/events/get/{id}", OnGetAsyncHandler);
        }
        private async Task<IActionResult> OnGetAsyncHandler(HttpContent httpContent)
        {
            EventDto = await httpContent.ReadFromJsonAsync<EventDto>();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return await PostAsync($"api/events/edit", new { EventDto.EventId, EventDto.Address, EventDto.ParticipantLimit }, OnPostAsyncHandler);
        }

        public async Task<IActionResult> OnPostAsyncHandler(HttpContent httpContent)
        {
            TempData.SetSuccessMessage("Successfully edited event.");
            return RedirectToPage("./Index");
        }
    }
}
