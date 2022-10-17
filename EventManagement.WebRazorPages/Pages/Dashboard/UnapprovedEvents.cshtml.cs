using EventManagement.WebRazorPages.Extensions;
using EventManagement.WebRazorPages.Pages.Events.Models;
using EventManagement.WebRazorPages.Pages.Models;
using EventManagement.WebRazorPages.Pages.Shared;
using EventManagement.WebRazorPages.ServiceConfigurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventManagement.WebRazorPages.Pages.Dashboard
{
    public class UnapprovedEventsModel : PaginatedPageModelBase<EventApprovalListDto>
    {
        public UnapprovedEventsModel(IHttpClientFactory httpClientFactory, IUserAccessor userAccessor) : base(httpClientFactory, userAccessor, "api/events/getunapprovedevents")
        {
            DeleteRedirectPage = "./UnapprovedEvents";
            PatchRedirectPage = "./UnapprovedEvents";
        }

        public async Task<IActionResult> OnPostApproveAsync([FromForm] int id)
        {
            return await PatchAsync($"api/events/approveevent/{id}", string.Empty);
        }

        public async Task<IActionResult> OnPostDeclineAsync([FromForm] int id)
        {
            return await DeleteAsync($"api/events/declineevent/{id}");
        }
    }
}
