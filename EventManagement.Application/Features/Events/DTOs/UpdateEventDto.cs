namespace EventManagement.Application.Features.Events.DTOs
{
    public class UpdateEventDto
    {
        public int EventId { get; set; }
        public int ParticipantLimit { get; set; }
        public string Address { get; set; } = null!;
    }
}