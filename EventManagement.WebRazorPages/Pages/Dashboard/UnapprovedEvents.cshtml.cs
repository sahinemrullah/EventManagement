using EventManagement.WebRazorPages.Pages.Events.Models;
using EventManagement.WebRazorPages.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.WebRazorPages.Pages.Dashboard
{
    public class UnapprovedEventsModel : PaginatedPageModelBase<EventApprovalListDto>
    {
        public UnapprovedEventsModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory, API.Event.GetUnapprovedEvents)
        {
            DeleteRedirectPage = "./UnapprovedEvents";
            PatchRedirectPage = "./UnapprovedEvents";
        }

        public async Task<IActionResult> OnPostApproveAsync([FromForm] int id)
        {
            return await PatchAsync(API.Event.ApproveEvent(id), string.Empty);
        }

        public async Task<IActionResult> OnPostDeclineAsync([FromForm] int id)
        {
            return await DeleteAsync(API.Event.DeclineEvent(id));
        }
    }
}
