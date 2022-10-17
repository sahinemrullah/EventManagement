using EventManagement.Domain.Specifications;
using EventManagement.Domain.Entities.Events;
using System.Linq.Expressions;
using EventManagement.Application.Features.Users.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Application.Features.Users.Specifications
{
    public class ParticipedEventsSpecification : IMapSpecification<Event, ParticipedEventListDto>
    {
        public ParticipedEventsSpecification(int userId)
        {
            Predicate = e => e.UserId == userId && EF.Functions.DateDiffDay(e.Start, DateTime.Now) > 0;

            Map = e => new ParticipedEventListDto()
            {
                Id = e.Id,
                Start = e.Start,
                Address = e.Address,
                Name = e.Name
            };
        }
        public Expression<Func<Event, ParticipedEventListDto>> Map { get; }

        public Expression<Func<Event, bool>> Predicate { get; }
    }
}
