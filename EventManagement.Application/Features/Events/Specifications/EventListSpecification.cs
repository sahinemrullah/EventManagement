using EventManagement.Application.Features.Events.DTOs;
using EventManagement.Domain.Entities.Events;
using EventManagement.Domain.Specifications;
using System.Linq.Expressions;

namespace EventManagement.Application.Features.Events.Specifications
{
    public class EventListSpecification : IMapSpecification<Event, EventListDto>
    {
        public Expression<Func<Event, EventListDto>> Map => e => new EventListDto()
        {
            EventId = e.Id,
            Name = e.Name,
            Description = e.Description.Substring(0, 300),
            Start = e.Start,
            ApplicationDeadline = e.ApplicationDeadline,
            ParticipantLimit = e.ParticipantLimit,
            Price = e is TicketEvent ? ((TicketEvent)e).Price : null
        };

        public Expression<Func<Event, bool>> Predicate => e => e.ApprovedForListing;
    }
}
