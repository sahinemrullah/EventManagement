using EventManagement.Domain.Entities.Users;

namespace EventManagement.Domain.Entities.Events
{
    public class EventParticipant
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
