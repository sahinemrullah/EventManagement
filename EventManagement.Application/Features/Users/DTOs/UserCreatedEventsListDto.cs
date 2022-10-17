namespace EventManagement.Application.Features.Users.DTOs
{
    public class UserCreatedEventsListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Start { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public int ParticipantLimit { get; set; }
    }
}