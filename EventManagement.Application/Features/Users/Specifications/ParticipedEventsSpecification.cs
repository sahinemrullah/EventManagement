using EventManagement.Domain.Specifications;
using EventManagement.Domain.Entities.Events;
using System.Linq.Expressions;
using EventManagement.Application.Features.Users.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Application.Features.Users.Specifications
{
    public class ParticipedEventsSpecification : IMapSpecification<EventParticipant, ParticipedEventListDto>
    {
        public ParticipedEventsSpecification(int userId)
        {
            Predicate = e => e.UserId == userId && EF.Functions.DateDiffDay(e.Event.Start, DateTime.Now) > 0;

            Map = e => new ParticipedEventListDto()
            {
                Id = e.Event.Id,
                Start = e.Event.Start,
                Address = e.Event.Address,
                Name = e.Event.Name
            };
        }
        public Expression<Func<EventParticipant, ParticipedEventListDto>> Map { get; }

        public Expression<Func<EventParticipant, bool>> Predicate { get; }
    }
}
