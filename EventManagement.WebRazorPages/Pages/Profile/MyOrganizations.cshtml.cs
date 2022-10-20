using EventManagement.WebRazorPages.Pages.Profile.Models;
using EventManagement.WebRazorPages.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.WebRazorPages.Pages.Profile
{
    public class MyOrganizationsModel : PaginatedPageModelBase<UserCreatedEventsListModel>
    {
        public MyOrganizationsModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory, API.User.Organizations)
        {
            DeleteRedirectPage = "./MyOrganizations";
        }

        public async Task<IActionResult> OnPostDeleteAsync([FromForm] int id)
        {
            return await DeleteAsync(API.Event.Delete(id));
        }
    }
}
