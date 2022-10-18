using EventManagement.Application.Features.Events.DTOs;
using EventManagement.Domain.Entities.Events;
using EventManagement.Domain.Specifications;
using System.Linq.Expressions;

namespace EventManagement.Application.Features.Events.Specifications
{
    public class EventEditSpecification : IMapSpecification<Event, EventEditDto>
    {
        public EventEditSpecification(Expression<Func<Event, bool>> predicate)
        {
            Predicate = predicate;
        }
        public Expression<Func<Event, EventEditDto>> Map => e => new EventEditDto()
        {
            EventId = e.Id,
            Address = e.Address,
            ParticipantLimit = e.ParticipantLimit,
            Start = e.Start,
            UserId = e.UserId
        };

        public Expression<Func<Event, bool>> Predicate { get; }
    }
}
