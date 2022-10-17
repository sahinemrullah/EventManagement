using EventManagement.WebRazorPages.Pages.Models;
using EventManagement.WebRazorPages.Pages.Profile.Models;
using EventManagement.WebRazorPages.Pages.Shared;
using EventManagement.WebRazorPages.ServiceConfigurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventManagement.WebRazorPages.Pages.Profile
{
    public class MyOrganizationsModel : PaginatedPageModelBase<UserCreatedEventsListModel>
    {
        public MyOrganizationsModel(IHttpClientFactory httpClientFactory, IUserAccessor userAccessor) : base(httpClientFactory, userAccessor, "api/users/organizations")
        {
            DeleteRedirectPage = "./MyOrganizations";
        }

        public async Task<IActionResult> OnPostDeleteAsync([FromForm] int id)
        {
            return await DeleteAsync($"api/events/delete/{id}");
        }
    }
}
