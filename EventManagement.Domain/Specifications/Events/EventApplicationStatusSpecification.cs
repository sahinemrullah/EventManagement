using EventManagement.Domain.Entities.Events;
using EventManagement.Domain.Entities.Users;
using System.Linq.Expressions;

namespace EventManagement.Domain.Specifications.Events
{
    public class EventApplicationStatusSpecification : IMapSpecification<Event>
    {
        public EventApplicationStatusSpecification(Expression<Func<Event, bool>> predicate)
        {
            Predicate = predicate;
        }
        public Expression<Func<Event, Event>> Map
        {
            get
            {
                return e =>
                    e is TicketEvent ?
                    new TicketEvent() { Id = e.Id } : new Event()
                    {
                        Id = e.Id,
                        ApplicationDeadline = e.ApplicationDeadline,
                        ParticipantLimit = e.ParticipantLimit,
                        AppliedUsers = e.AppliedUsers.AsQueryable().Select(User.Mapping.MapIdOnly),
                        ApprovedForListing = e.ApprovedForListing
                    };
            }
        }

        public Expression<Func<Event, bool>> Predicate { get; }
    }
}