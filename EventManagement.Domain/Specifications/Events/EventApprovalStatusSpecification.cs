using System.Linq.Expressions;
using EventManagement.Domain.Entities.Events;

namespace EventManagement.Domain.Specifications.Events
{
    public class EventApprovalStatusSpecification : IMapSpecification<Event>
    {
        public EventApprovalStatusSpecification(Expression<Func<Event, bool>> predicate)
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
                    ApprovedForListing = e.ApprovedForListing
                };
            }
        }

        public Expression<Func<Event, bool>> Predicate { get; }
    }
}