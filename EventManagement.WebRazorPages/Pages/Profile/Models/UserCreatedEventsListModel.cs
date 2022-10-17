namespace EventManagement.WebRazorPages.Pages.Profile.Models
{
    public class UserCreatedEventsListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public int ParticipantLimit { get; set; }
    }
}
