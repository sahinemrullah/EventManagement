using EventManagement.Domain.Specifications;
using EventManagement.Domain.Entities.Events;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using EventManagement.Application.Features.Users.DTOs;

namespace EventManagement.Application.Features.Users.Specifications
{
    public class UpcomingEventsSpecification : IMapSpecification<EventParticipant, UpcomingEventListDto>
    {
        public UpcomingEventsSpecification(int userId)
        {
            Predicate = e => e.UserId == userId && EF.Functions.DateDiffDay(e.Event.Start, DateTime.Now) < 0;

            Map = e => new UpcomingEventListDto()
            {
                Id = e.Event.Id,
                Start = e.Event.Start,
                Address = e.Event.Address,
                Name = e.Event.Name
            };
        }
        public Expression<Func<EventParticipant, UpcomingEventListDto>> Map { get; }

        public Expression<Func<EventParticipant, bool>> Predicate { get; }
    }
}
