using EventManagement.WebRazorPages.Extensions;
using EventManagement.WebRazorPages.Pages.Events.Models;
using EventManagement.WebRazorPages.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.WebRazorPages.Pages.Events
{
    [Authorize]
    public class EditModel : APIClientPageModelBase
    {
        public EditModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }
        [BindProperty]
        public EventEditModel EventEditModel { get; set; } = null!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            return await GetAsync(API.Event.GetEventForEdit(id), OnGetAsyncHandler);
        }
        private async Task<IActionResult> OnGetAsyncHandler(HttpContent httpContent)
        {
            EventEditModel = await httpContent.ReadFromJsonAsync<EventEditModel>();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return await PostAsync(API.Event.Edit, new { EventEditModel.EventId, EventEditModel.Address, EventEditModel.ParticipantLimit }, OnPostAsyncHandler);
        }

        public async Task<IActionResult> OnPostAsyncHandler(HttpContent httpContent)
        {
            TempData.SetSuccessMessage("Successfully edited event.");
            return RedirectToPage("./Index");
        }
    }
}
