using EventManagement.WebRazorPages.Pages.Profile.Models;
using EventManagement.WebRazorPages.Pages.Shared;
using EventManagement.WebRazorPages.ServiceConfigurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventManagement.WebRazorPages.Pages.Profile
{

    public class UpcomingEventsModel : PaginatedPageModelBase<UpcomingEventListItemModel>
    {
        public UpcomingEventsModel(IHttpClientFactory httpClientFactory, IUserAccessor userAccessor) : base(httpClientFactory, userAccessor, "api/users/upcomingevents")
        {
        }
    }
}
