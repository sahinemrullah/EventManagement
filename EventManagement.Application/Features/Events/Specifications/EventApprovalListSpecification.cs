using EventManagement.Application.Features.Events.DTOs;
using EventManagement.Domain.Entities.Events;
using EventManagement.Domain.Specifications;
using System.Linq.Expressions;

namespace EventManagement.Application.Features.Events.Specifications
{
    public class EventApprovalListSpecification : IMapSpecification<Event, EventApprovalListDto>
    {
        public Expression<Func<Event, EventApprovalListDto>> Map => e => new EventApprovalListDto()
        {
            EventId = e.Id,
            Name = e.Name,
            Address = e.Address,
            Start = e.Start,
            ApplicationDeadline = e.ApplicationDeadline,
            ParticipantLimit = e.ParticipantLimit,
            Price = e is TicketEvent ? ((TicketEvent)e).Price : null
        };

        public Expression<Func<Event, bool>> Predicate => e => !e.ApprovedForListing;
    }
}
