using EventManagement.WebRazorPages.Pages.Profile.Models;
using EventManagement.WebRazorPages.Pages.Shared;

namespace EventManagement.WebRazorPages.Pages.Profile
{
    public class ParticipedEventsModel : PaginatedPageModelBase<ParticipedEventListItemModel>
    {
        public ParticipedEventsModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory, API.User.ParticipedEvents)
        {
        }
    }
}
