using EventManagement.Application.Features.Events;
using EventManagement.Application.Features.Events.DTOs;
using EventManagement.Application.Features.Mail;
using EventManagement.Domain.Collections;
using EventManagement.Domain.Common;
using EventManagement.Domain.Common.CreateParameters;
using EventManagement.Domain.Entities.Events;
using EventManagement.Domain.Validations.Interfaces;
using EventManagement.WebAPI.Helpers;
using EventManagement.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using IResult = EventManagement.Domain.Validations.Interfaces.IResult;

namespace EventManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = nameof(Domain.Entities.Users.User))]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        public EventsController(IEventService eventService, IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _eventService = eventService;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            IResult<EventDto> result = _eventService.GetById(id);

            if (!result.IsSuccess)
                return result.GetExceptionStatusResult();

            return Ok(result.Value!);
        }

        [HttpGet]
        public IActionResult GetPaginatedEvents([FromQuery] EventPagingRequest eventPagingRequest)
        {
            IPaginatedList<EventListDto> events = _eventService.GetPaginated(eventPagingRequest.PageNumber, eventPagingRequest.PageSize, eventPagingRequest.CategoryIds, eventPagingRequest.CityIds);

            return Ok(events);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUnapprovedEvents([FromQuery] PaginatedRequest eventPagingRequest)
        {
            IPaginatedList<EventApprovalListDto> events = _eventService.GetPaginatedUnapprovedEvents(eventPagingRequest.PageNumber, eventPagingRequest.PageSize);

            return Ok(events);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult ApproveEvent([FromRoute] int id)
        {
            IResult result = _eventService.Approve(id);

            if (!result.IsSuccess)
                return result.GetExceptionStatusResult();

            _unitOfWork.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeclineEvent([FromRoute] int id)
        {
            IResult<string> result = _eventService.Decline(id);

            if (!result.IsSuccess)
                return result.GetExceptionStatusResult();

            _unitOfWork.SaveChanges();

            _emailService.SendEmailAsync(result.Value!, "Your event listing request declined by the Admin.");

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            IResult result = _eventService.Delete(id);

            if (!result.IsSuccess)
                return result.GetExceptionStatusResult();

            _unitOfWork.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody] EventCreateParameters eventCreateDto)
        {
            IResult<Event> result = _eventService.CreateEvent(eventCreateDto);

            if (!result.IsSuccess)
                return result.GetExceptionStatusResult();

            _unitOfWork.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = result.Value!.Id }, result.Value);
        }

        [HttpPost]
        public IActionResult CreateTicketEvent([FromBody] TicketEventCreateParameters eventCreateDto)
        {
            IResult<TicketEvent> result = _eventService.CreateTicketEvent(eventCreateDto);

            if (!result.IsSuccess)
                return result.GetExceptionStatusResult();

            _unitOfWork.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = result.Value!.Id }, result.Value);
        }

        [HttpPost("{id}")]
        public IActionResult Apply([FromRoute] int id)
        {
            IResult result = _eventService.ApplyForEvent(id);

            if (!result.IsSuccess)
                return result.GetExceptionStatusResult();

            _unitOfWork.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult Edit([FromBody] UpdateEventDto eventDto)
        {
            IResult result = _eventService.Update(eventDto);

            if (!result.IsSuccess)
                return result.GetExceptionStatusResult();

            _unitOfWork.SaveChanges();

            return Ok();
        }
    }
}
