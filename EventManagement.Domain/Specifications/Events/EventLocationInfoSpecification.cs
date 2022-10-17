using System.Linq.Expressions;
using EventManagement.Domain.Entities.Events;

namespace EventManagement.Domain.Specifications.Events
{
    public class EventLocationInfoSpecification : IMapSpecification<Event>
    {
        public EventLocationInfoSpecification(Expression<Func<Event, bool>> predicate)
        {
            Predicate = predicate;
        }

        public Expression<Func<Event, Event>> Map
        {
            get
            {
                return e => new Event()
                {
                    Id = e.Id,
                    Address = e.Address,
                    ParticipantLimit = e.ParticipantLimit,
                    UserId = e.UserId,
                    Start = e.Start
                };
            }
        }

        public Expression<Func<Event, bool>> Predicate { get; }
    }
}