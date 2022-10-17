namespace EventManagement.Application.Features.Users.DTOs
{
    public class UpcomingEventListDto
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public string Address { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}