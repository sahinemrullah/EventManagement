using EventManagement.Application.Features.Events.DTOs;
using EventManagement.Domain.Collections;
using EventManagement.Domain.Common.CreateParameters;
using EventManagement.Domain.Entities.Events;
using EventManagement.Domain.Validations.Interfaces;

namespace EventManagement.Application.Features.Events
{
    public interface IEventService
    {
        public IResult<Event> CreateEvent(EventCreateParameters eventCreateDto);
        public IResult<TicketEvent> CreateTicketEvent(TicketEventCreateParameters eventCreateDto);
        public IResult<EventDto> GetById(int id);
        public IPaginatedList<EventListDto> GetPaginated(int pageNumber, int pageSize, IEnumerable<int>? categoryIds = null, IEnumerable<int>? cityIds = null);
        public IPaginatedList<EventApprovalListDto> GetPaginatedUnapprovedEvents(int pageNumber, int pageSize);
        public IPaginatedList<TicketEventListDto> GetPaginatedTicketEvents(int pageNumber, int pageSize, IEnumerable<int>? categoryIds = null, IEnumerable<int>? cityIds = null);
        public IResult Update(UpdateEventDto eventDto);
        public IResult ApplyForEvent(int eventId);
        public IResult Delete(int id);
        public IResult<string> Decline(int id);
        public IResult Approve(int id);
        public IResult<EventEditDto> GetEventForEditById(int id);
    }
}
