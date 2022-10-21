using EventManagement.WebRazorPages.Extensions;
using EventManagement.WebRazorPages.Pages.Events.Models;
using EventManagement.WebRazorPages.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EventManagement.WebRazorPages.Pages.Dashboard
{
    public class UnapprovedEventsModel : PaginatedPageModelBase<EventApprovalListDto>
    {
        public UnapprovedEventsModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory, API.Event.GetUnapprovedEvents)
        {

        }

        public async Task<IActionResult> OnPostApproveAsync([FromForm] int id)
        {
            HttpResponseMessage response = await HttpClient.PatchAsync(API.Event.ApproveEvent(id), null);

            if (!response.IsSuccessStatusCode)
                return await GetStatusCodeResultAsync(response);

            if (Request.IsAjaxRequest())
                return new OkObjectResult(new { Message = "Successfully Approved.", RedirectUrl = Url.Page("./UnapprovedEvents") });

            TempData.SetSuccessMessage("Successfully Approved.");

            return RedirectToPage("./UnapprovedEvents");
        }

        public async Task<IActionResult> OnPostDeclineAsync([FromForm] int id)
        {
            return await DeleteAsync(API.Event.DeclineEvent(id));
        }
    }
}
