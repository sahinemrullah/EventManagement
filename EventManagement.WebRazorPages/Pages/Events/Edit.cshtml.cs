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
        public EventEditModel EventEditModel { get; set; } = null!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            return await GetAsync($"api/events/geteventforedit/{id}", OnGetAsyncHandler);
        }
        private async Task<IActionResult> OnGetAsyncHandler(HttpContent httpContent)
        {
            EventEditModel = await httpContent.ReadFromJsonAsync<EventEditModel>();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return await PostAsync($"api/events/edit", new { EventEditModel.EventId, EventEditModel.Address, EventEditModel.ParticipantLimit }, OnPostAsyncHandler);
        }

        public async Task<IActionResult> OnPostAsyncHandler(HttpContent httpContent)
        {
            TempData.SetSuccessMessage("Successfully edited event.");
            return RedirectToPage("./Index");
        }
    }
}
