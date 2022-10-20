using EventManagement.WebRazorPages.Pages.Profile.Models;
using EventManagement.WebRazorPages.Pages.Shared;

namespace EventManagement.WebRazorPages.Pages.Profile
{

    public class UpcomingEventsModel : PaginatedPageModelBase<UpcomingEventListItemModel>
    {
        public UpcomingEventsModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory, API.User.UpcomingEvents)
        {
        }
    }
}
