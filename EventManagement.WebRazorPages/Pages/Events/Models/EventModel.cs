namespace EventManagement.WebRazorPages.Pages.Events.Models
{
    public class EventModel
    {
        public int EventId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Start { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public int ParticipantLimit { get; set; }
        public int UserId { get; set; }
        public string User { get; set; } = null!;
    }
}