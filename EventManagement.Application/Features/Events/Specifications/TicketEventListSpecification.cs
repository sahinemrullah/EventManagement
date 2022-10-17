using EventManagement.Application.Features.Events.DTOs;
using EventManagement.Domain.Entities.Events;
using EventManagement.Domain.Specifications;
using System.Linq.Expressions;

namespace EventManagement.Application.Features.Events.Specifications
{
    public class TicketEventListSpecification : IMapSpecification<TicketEvent, TicketEventListDto>
    {
        public Expression<Func<TicketEvent, TicketEventListDto>> Map => e => new TicketEventListDto()
        {
            EventId = e.Id,
            Name = e.Name,
            Description = e.Description.Substring(0, 300),
            Start = e.Start,
            ApplicationDeadline = e.ApplicationDeadline,
            ParticipantLimit = e.ParticipantLimit,
            Price = e.Price,
        };

        public Expression<Func<TicketEvent, bool>> Predicate => e => e.ApprovedForListing;
    }

}
