using EventManagement.Domain.Specifications;
using EventManagement.Domain.Entities.Events;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using EventManagement.Application.Features.Users.DTOs;

namespace EventManagement.Application.Features.Users.Specifications
{
    public class UpcomingEventsSpecification : IMapSpecification<Event, UpcomingEventListDto>
    {
        public UpcomingEventsSpecification(int userId)
        {
            Predicate = e => e.UserId == userId && EF.Functions.DateDiffDay(e.Start, DateTime.Now) < 0;

            Map = e => new UpcomingEventListDto()
            {
                Id = e.Id,
                Start = e.Start,
                Address = e.Address,
                Name = e.Name
            };
        }
        public Expression<Func<Event, UpcomingEventListDto>> Map { get; }

        public Expression<Func<Event, bool>> Predicate { get; }
    }
}
