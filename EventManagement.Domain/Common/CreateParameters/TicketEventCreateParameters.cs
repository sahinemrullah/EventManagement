namespace EventManagement.Domain.Common.CreateParameters
{
    public class TicketEventCreateParameters
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Start { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public int ParticipantLimit { get; set; }
        public int CategoryId { get; set; }
        public int CityId { get; set; }
        public decimal Price { get; set; }
    }
}
