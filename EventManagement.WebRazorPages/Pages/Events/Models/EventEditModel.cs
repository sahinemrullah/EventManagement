namespace EventManagement.WebRazorPages.Pages.Events.Models
{
    public class EventEditModel
    {
        public int EventId { get; set; }
        public string Address { get; set; } = null!;
        public int ParticipantLimit { get; set; }
    }
}