using EventManagement.Domain.Specifications;
using EventManagement.Domain.Entities.Events;
using System.Linq.Expressions;
using EventManagement.Application.Features.Users.DTOs;

namespace EventManagement.Application.Features.Users.Specifications
{
    public class OrganizationsSpecification : IMapSpecification<Event, UserCreatedEventsListDto>
    {
        public OrganizationsSpecification(int userId)
        {
            Predicate = e => e.UserId == userId;

            Map = e => new UserCreatedEventsListDto()
            {
                Id = e.Id,
                Start = e.Start,
                ApplicationDeadline = e.ApplicationDeadline,
                ParticipantLimit = e.ParticipantLimit,
                Name = e.Name
            };
        }
        public Expression<Func<Event, UserCreatedEventsListDto>> Map { get; }

        public Expression<Func<Event, bool>> Predicate { get; }
    }
}
