namespace EventManagement.Application.Features.Events.DTOs
{
    public class EventEditDto
    {
        public int EventId { get; set; }
        public string Address { get; set; }
        public int ParticipantLimit { get; set; }
        public DateTime Start { get; set; }
        public int UserId { get; set; }
    }
}