using EventManagement.Application.Features.Credentials.DTOs;
using EventManagement.Application.Features.Events;
using EventManagement.Application.Features.Events.DTOs;
using EventManagement.Application.Features.IntegrationServices;
using EventManagement.Application.Features.IntegrationServices.DTOs;
using EventManagement.Domain.Collections;
using EventManagement.Domain.Common;
using EventManagement.Domain.Entities;
using EventManagement.Domain.Validations.Interfaces;
using EventManagement.WebAPI.Helpers;
using EventManagement.WebAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using IResult = EventManagement.Domain.Validations.Interfaces.IResult;

namespace EventManagement.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IntegrationServicesController : ControllerBase
    {
        private readonly IIntegrationServicesService _integrationServiceServices;
        private readonly IEventService _eventService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenHelper _tokenHelper;

        public IntegrationServicesController(IIntegrationServicesService integrationService, IEventService eventService, IUnitOfWork unitOfWork, ITokenHelper tokenHelper)
        {
            _integrationServiceServices = integrationService;
            _eventService = eventService;
            _unitOfWork = unitOfWork;
            _tokenHelper = tokenHelper;
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterIntegrationServiceDto registerDto)
        {
            IResult<IntegrationService> createResult = IntegrationService.Create(registerDto.Email, registerDto.WebDomain, registerDto.CompanyName);

            if (!createResult.IsSuccess)
                return createResult.GetExceptionStatusResult();

            IResult registerResult = _integrationServiceServices.Register(createResult.Value!, registerDto.Password);

            if (!registerResult.IsSuccess)
                return registerResult.GetExceptionStatusResult();

            _unitOfWork.SaveChanges();

            return CreatedAtAction(nameof(Login), new { id = createResult.Value!.Id });
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDto loginIntegrationServiceDto)
        {
            IResult<ClaimsIdentity> claimsIdentityResult = _integrationServiceServices.Login(loginIntegrationServiceDto, JwtBearerDefaults.AuthenticationScheme);

            if (!claimsIdentityResult.IsSuccess)
                return claimsIdentityResult.GetExceptionStatusResult();

            AccessToken token = _tokenHelper.CreateToken(new ClaimsPrincipal(claimsIdentityResult.Value!));

            return Ok(token);
        }

        [HttpGet]
        [Consumes("application/json", "application/xml")]
        [Produces("application/json", "application/xml")]
        [ProducesDefaultResponseType(typeof(PaginatedList<EventListDto>))]
        [Authorize(Roles = nameof(IntegrationService))]
        public IActionResult GetPaginatedEvents([FromQuery] EventPagingRequest eventPagingRequest)
        {
            IPaginatedList<TicketEventListDto> events = _eventService.GetPaginatedTicketEvents(eventPagingRequest.PageNumber, eventPagingRequest.PageSize, eventPagingRequest.CategoryIds, eventPagingRequest.CityIds);

            return Ok(events);
        }

        [HttpGet]
        [Authorize(Roles = nameof(Domain.Entities.Users.User))]
        public IActionResult GetIntegrationServices()
        {
            IEnumerable<IntegrationServiceListDto> integrationServices = _integrationServiceServices.GetIntegrationServices();
            
            return Ok(integrationServices);
        }
    }
}
