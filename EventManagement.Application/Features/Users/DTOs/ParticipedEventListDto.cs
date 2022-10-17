namespace EventManagement.Application.Features.Users.DTOs
{
    public class ParticipedEventListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime Start { get; set; }
    }
}
