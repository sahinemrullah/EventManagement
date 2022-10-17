namespace EventManagement.Domain.Entities.Events
{
    public sealed class TicketEvent : Event
    {
        internal TicketEvent()
        {

        }

        public decimal Price { get; internal set; }
    }
}
