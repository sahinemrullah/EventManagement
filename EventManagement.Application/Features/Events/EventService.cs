using EventManagement.Application.Features.Events.DTOs;
using EventManagement.Application.Features.Common;
using EventManagement.Domain.Entities.Events;
using EventManagement.Persistence;
using EventManagement.Domain.Collections;
using EventManagement.Application.Features.Common.Extensions;
using System.Security.Authentication;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using EventManagement.Domain.Specifications;
using EventManagement.Domain.Common.Helpers;
using EventManagement.Domain.Specifications.Events;
using EventManagement.Domain.Entities.Users;
using EventManagement.Application.Features.Common.Exceptions;
using EventManagement.Domain.Exceptions;
using EventManagement.Application.Features.Events.Specifications;
using EventManagement.Domain.Validations.Interfaces;
using EventManagement.Domain.Common.CreateParameters;
using System.Security.Claims;

namespace EventManagement.Application.Features.Events
{
    internal class EventService : IEventService
    {
        private readonly EventManagementDbContext _dbContext;
        private readonly IUserAccessor _userAccessor;
        private readonly IResultFactory _resultFactory;

        public EventService(EventManagementDbContext dbContext, IUserAccessor userAccessor, IResultFactory resultFactory)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
            _resultFactory = resultFactory;
        }

        public IResult<Event> CreateEvent(EventCreateParameters eventCreateDto)
        {
            _userAccessor.User.GetUserId(out int userId);

            IResult<Event> newEventResult = EventHelpers.Create(eventCreateDto, userId);

            if (newEventResult.IsSuccess)
                _dbContext.Events.Add(newEventResult.Value!);

            return newEventResult;
        }

        public IResult<TicketEvent> CreateTicketEvent(TicketEventCreateParameters ticketEventCreateParameters)
        {
            _userAccessor.User.GetUserId(out int userId);

            IResult<TicketEvent> newTicketEventResult = TicketEventHelpers.Create(ticketEventCreateParameters, userId);

            if (newTicketEventResult.IsSuccess)
                _dbContext.TicketEvents.Add(newTicketEventResult.Value!);

            return newTicketEventResult;
        }

        public IResult ApplyForEvent(int eventId)
        {
            if (!_userAccessor.User.GetUserId(out int userId))
                return _resultFactory.Failure(new AuthenticationException("Invalid user"));

            Event? @event = _dbContext.Events
                                            .WithMapSpecification(new EventApplicationStatusSpecification(e => e.Id == eventId))
                                            .FirstOrDefault();


            if (@event is TicketEvent)
                return _resultFactory.Failure(new BusinessRuleException("Please select a valid ticket issuer."));

            if (@event is null)
                return _resultFactory.Failure(new NotFoundException(eventId.ToString(), nameof(Event)));
            
            _dbContext.Events.Attach(@event);

            IResult<User> result = @event.AddUser(userId);

            if (result.IsSuccess)
                _dbContext.Users.Attach(result.Value!);

            return result;
        }

        public IResult Delete(int id)
        {
            Event? @event = _dbContext.Events.Find(id);

            if (@event == null)
                return _resultFactory.Failure(new NotFoundException(id.ToString(), nameof(Event)));

            ClaimsPrincipal user = _userAccessor.User;

            bool isAdmin = user.IsInRole("Admin");

            if (!isAdmin && !user.OwnsEntity(@event))
                return _resultFactory.Failure(new AuthorizationException());

            if (!isAdmin && (@event.Start - DateTime.Now).TotalDays < 5)
                return _resultFactory.Failure(new BusinessRuleException("You can't delete event after 5 days left."));

            _dbContext.Events.Remove(@event);

            return _resultFactory.Success();
        }

        public IResult Update(UpdateEventDto eventDto)
        {
            Event? @event = _dbContext.Events
                                            .WithMapSpecification(new EventLocationInfoSpecification(e => e.Id == eventDto.EventId))
                                            .SingleOrDefault();

            if (@event is null)
                return _resultFactory.Failure(new NotFoundException(eventDto.EventId.ToString(), nameof(Event)));

            ClaimsPrincipal user = _userAccessor.User;

            bool isAdmin = user.IsInRole("Admin");

            if (!isAdmin && !user.OwnsEntity(@event))
                return _resultFactory.Failure(new AuthorizationException());

            if (!isAdmin && (@event.Start - DateTime.Now).TotalDays < 5)
                return _resultFactory.Failure(new BusinessRuleException("You can't update event after 5 days left."));

            EntityEntry<Event> entry = _dbContext.Events.Attach(@event);

            IResult result = @event.ChangeLocation(eventDto.Address, eventDto.ParticipantLimit);

            return result;
        }

        public IResult Approve(int id)
        {
            Event? @event = _dbContext.Events
                                            .WithMapSpecification(new EventApprovalStatusSpecification(e => e.Id == id))
                                            .SingleOrDefault();

            if (@event is null)
                return _resultFactory.Failure(new NotFoundException(id.ToString(), nameof(Event)));

            if (@event.ApprovedForListing)
                return _resultFactory.Success();

            EntityEntry<Event> entry = _dbContext.Events.Attach(@event);

            @event.Approve();

            return _resultFactory.Success();
        }

        public IPaginatedList<EventListDto> GetPaginated(int pageNumber, int pageSize, IEnumerable<int>? categoryIds = default, IEnumerable<int>? cityIds = default)
        {
            return _dbContext.Events
                                    .WithSpecification(new CategoryAndCityFilterSpecification<Event>(categoryIds, cityIds))
                                    .WithMapSpecification(new EventListSpecification())
                                    .ToPaginatedList(pageSize, pageNumber);
        }
        
        public IPaginatedList<EventApprovalListDto> GetPaginatedUnapprovedEvents(int pageNumber, int pageSize)
        {
            return _dbContext.Events
                                    .WithMapSpecification(new EventApprovalListSpecification())
                                    .ToPaginatedList(pageSize, pageNumber);
        }

        public IPaginatedList<TicketEventListDto> GetPaginatedTicketEvents(int pageNumber, int pageSize, IEnumerable<int>? categoryIds = null, IEnumerable<int>? cityIds = null)
        {
            return _dbContext.TicketEvents
                                          .WithSpecification(new CategoryAndCityFilterSpecification<TicketEvent>(categoryIds, cityIds))
                                          .WithMapSpecification(new TicketEventListSpecification())
                                          .ToPaginatedList(pageSize, pageNumber);
        }

        public IResult<EventDto> GetById(int id)
        {
            EventDto? @event = _dbContext.Events
                                                .WithMapSpecification(new EventDetailsSpecification(e => e.Id == id && e.ApprovedForListing))
                                                .FirstOrDefault();

            if (@event is null)
                return _resultFactory.Failure<EventDto>(new NotFoundException(id.ToString(), nameof(Event)));

            return _resultFactory.Success(@event);
        }

        public IResult<string> Decline(int id)
        {
            Event? @event = _dbContext.Events.Find(id);

            if (@event == null)
                return _resultFactory.Failure<string>(new NotFoundException(id.ToString(), nameof(Event)));

            ClaimsPrincipal user = _userAccessor.User;

            bool isAdmin = user.IsInRole("Admin");

            if (!isAdmin && !user.OwnsEntity(@event))
                return _resultFactory.Failure<string>(new AuthorizationException());

            if (!isAdmin && (@event.Start - DateTime.Now).TotalDays < 5)
                return _resultFactory.Failure<string>(new BusinessRuleException("You can't delete event after 5 days left."));

            string email = _dbContext.Users.Where(u => u.Id == @event.UserId).Select(u => u.Email).First();

            _dbContext.Events.Remove(@event);

            return _resultFactory.Success(email);
        }

        public IResult<EventEditDto> GetEventForEditById(int id)
        {
            _userAccessor.User.GetUserId(out int userId);

            EventEditDto? editDto = _dbContext.Events
                                                     .WithMapSpecification(new EventEditSpecification(e => e.Id == id && e.ApprovedForListing))
                                                     .FirstOrDefault();

            if (editDto == null)
                return _resultFactory.Failure<EventEditDto>(new NotFoundException(id.ToString(), nameof(Event)));

            if (editDto.UserId != userId)
                return _resultFactory.Failure<EventEditDto>(new AuthorizationException());

            if ((editDto.Start - DateTime.Now).TotalDays < 5)
                return _resultFactory.Failure<EventEditDto>(new BusinessRuleException("You can't edit event after 5 days left."));

            return _resultFactory.Success(editDto);
        }
    }
}
