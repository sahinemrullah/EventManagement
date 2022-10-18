using EventManagement.Domain.Security;
using EventManagement.Persistence;
using System.Security.Authentication;
using EventManagement.Application.Features.Common;
using EventManagement.Application.Features.Common.Extensions;
using EventManagement.Domain.Entities.Users;
using System.Security.Claims;
using EventManagement.Application.Features.Credentials.DTOs;
using Microsoft.EntityFrameworkCore;
using EventManagement.Domain.Validations.Interfaces;
using EventManagement.Application.Features.Credentials;
using EventManagement.Domain.Collections;
using EventManagement.Application.Features.Events.DTOs;
using EventManagement.Domain.Specifications;
using EventManagement.Application.Features.Events.Specifications;
using EventManagement.Domain.Entities.Events;
using EventManagement.Application.Features.Users.DTOs;
using EventManagement.Application.Features.Users.Specifications;

namespace EventManagement.Application.Features.Users
{
    internal class UsersService : CredentialsServiceBase<User>, IUsersService
    {
        private readonly IUserAccessor _userAccessor;
        private readonly DbSet<UserRole> _roles;
        private readonly DbSet<Event> _events;
        private readonly DbSet<EventParticipant> _eventParticipant;
        public UsersService(EventManagementDbContext dbContext, IPasswordHasher passwordHasher, IUserAccessor userAccessor, IResultFactory resultFactory) 
            : base(dbContext, passwordHasher, resultFactory)
        {
            _userAccessor = userAccessor;
            _roles = dbContext.Set<UserRole>();
            _events = dbContext.Set<Event>();
            _eventParticipant = dbContext.Set<EventParticipant>();
        }

        public IResult ChangePassword(string password, string newPassword, string newPasswordConfirmation)
        {
            IValidationResult validationResult = ResultFactory.Validator();

            validationResult.ValidateString(nameof(password), password);
            
            validationResult.ValidateString(nameof(newPassword), newPassword);
            
            validationResult.ValidateString(nameof(newPasswordConfirmation), newPasswordConfirmation);

            if (!newPassword.Equals(newPasswordConfirmation))
                validationResult.AddError(string.Empty, "Passwords doesn't match");

            if (!validationResult.IsSuccess)
                return validationResult;

            _userAccessor.User.GetUserId(out int userId);

            User? user = CredentialsSet.Find(userId);

            if (user is null)
                return ResultFactory.Failure(new AuthenticationException("Invalid user."));

            if (!PasswordHasher.CheckPasswordHash(password, user.PasswordHash))
                return ResultFactory.Failure(new ArgumentException("Invalid credentials."));

            return SetPassword(user, newPassword);
        }

        public override IResult<ClaimsIdentity> Login(LoginDto loginDto, string authenticationScheme)
        {
            IResult<ClaimsIdentity> result = base.Login(loginDto, authenticationScheme);

            if(result.IsSuccess)
            {
                int userId = Convert.ToInt32(result.Value!.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value);
                IEnumerable<string> userRoles = _roles.WithMapSpecification(new UserRoleSpecification(userId))
                                                        .ToList();

                foreach (string role in userRoles)
                {
                    result.Value!.AddClaim(new Claim(ClaimTypes.Role, role));
                }

            }
            return result;
        }

        public IResult<IPaginatedList<UserCreatedEventsListDto>> Organizations(int pageSize, int pageNumber)
        {
            _userAccessor.User.GetUserId(out int userId);

            IPaginatedList<UserCreatedEventsListDto> result = _events
                                                                    .WithMapSpecification(new OrganizationsSpecification(userId))
                                                                    .OrderBy(e => e.Id)
                                                                    .ToPaginatedList(pageSize, pageNumber);

            return ResultFactory.Success(result);
        }

        public IResult<IPaginatedList<ParticipedEventListDto>> ParticipedEvents(int pageSize, int pageNumber)
        {
            _userAccessor.User.GetUserId(out int userId);

            IPaginatedList<ParticipedEventListDto> result = _eventParticipant
                                                                   .WithMapSpecification(new ParticipedEventsSpecification(userId))
                                                                   .OrderBy(e => e.Id)
                                                                   .ToPaginatedList(pageSize, pageNumber);

            return ResultFactory.Success(result);
        }

        public IResult<IPaginatedList<UpcomingEventListDto>> UpcomingEvents(int pageSize, int pageNumber)
        {
            _userAccessor.User.GetUserId(out int userId);

            IPaginatedList<UpcomingEventListDto> result = _eventParticipant
                                                                 .WithMapSpecification(new UpcomingEventsSpecification(userId))
                                                                 .OrderBy(e => e.Id)
                                                                 .ToPaginatedList(pageSize, pageNumber);

            return ResultFactory.Success(result);
        }
    }
}
