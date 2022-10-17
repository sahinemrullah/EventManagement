namespace EventManagement.Application.Features.Events.DTOs
{
    public class EventDto
    {
        public int EventId { get; init; }
        public string Name { get; init; } = null!;
        public string Address { get; set; } = null!;
        public string Description { get; init; } = null!;
        public DateTime Start { get; init; }
        public DateTime ApplicationDeadline { get; init; }
        public int ParticipantLimit { get; set; }
        public int UserId { get; set; }
        public string User { get; set; } = null!;
    }
}