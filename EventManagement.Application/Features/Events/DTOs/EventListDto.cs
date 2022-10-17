namespace EventManagement.Application.Features.Events.DTOs
{
    public class EventListDto
    {
        public int EventId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Start { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public int ParticipantLimit { get; set; }
        public decimal? Price { get; set; }
    }
    public class TicketEventListDto
    {
        public int EventId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Start { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public int ParticipantLimit { get; set; }
        public decimal Price { get; set; }
    }
    public class EventApprovalListDto
    {
        public int EventId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime Start { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public int ParticipantLimit { get; set; }
        public decimal? Price { get; set; }
    }
}