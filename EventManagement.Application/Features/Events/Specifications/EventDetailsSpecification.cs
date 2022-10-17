using EventManagement.Application.Features.Events.DTOs;
using EventManagement.Domain.Entities.Events;
using EventManagement.Domain.Specifications;
using System.Linq.Expressions;

namespace EventManagement.Application.Features.Events.Specifications
{
    public class EventDetailsSpecification : IMapSpecification<Event, EventDto>
    {
        public EventDetailsSpecification(Expression<Func<Event, bool>> predicate)
        {
            Predicate = predicate;
        }
        public Expression<Func<Event, EventDto>> Map => e => new EventDto()
        {
            EventId = e.Id,
            Address = e.Address,
            ApplicationDeadline = e.ApplicationDeadline,
            Description = e.Description,
            Name = e.Name,
            ParticipantLimit = e.ParticipantLimit,
            Start = e.Start,
            UserId = e.UserId,
            User = e.User.GetDisplayName()
        };

        public Expression<Func<Event, bool>> Predicate { get; }
    }
}
