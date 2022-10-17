using EventManagement.Application.Features.Credentials;
using EventManagement.Application.Features.Events.DTOs;
using EventManagement.Application.Features.Users.DTOs;
using EventManagement.Domain.Collections;
using EventManagement.Domain.Entities.Users;
using EventManagement.Domain.Validations.Interfaces;

namespace EventManagement.Application.Features.Users
{
    public interface IUsersService : ICredentialsService<User>
    {
        public IResult ChangePassword(string password, string newPassword, string newPasswordConfirmation);
        public IResult<IPaginatedList<UserCreatedEventsListDto>> Organizations(int pageSize, int pageNumber);
        public IResult<IPaginatedList<ParticipedEventListDto>> ParticipedEvents(int pageSize, int pageNumber);
        public IResult<IPaginatedList<UpcomingEventListDto>> UpcomingEvents(int pageSize, int pageNumber);
    }
}
