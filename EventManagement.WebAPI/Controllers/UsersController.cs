using Microsoft.AspNetCore.Mvc;
using EventManagement.Domain.Common;
using System.Security.Claims;
using EventManagement.Application.Features.Users;
using EventManagement.Application.Features.Users.DTOs;
using EventManagement.WebAPI.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IResult = EventManagement.Domain.Validations.Interfaces.IResult;
using EventManagement.Application.Features.Credentials.DTOs;
using EventManagement.Domain.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using EventManagement.Domain.Validations.Interfaces;
using EventManagement.WebAPI.Models;
using EventManagement.Domain.Collections;

namespace EventManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenHelper _tokenHelper;

        public UsersController(IUsersService userService, IUnitOfWork unitOfWork, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _tokenHelper = tokenHelper;
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterUserDto registerUserDto)
        {
            IResult<User> userCreateResult = Domain.Entities.Users.User.Create(registerUserDto.Email, registerUserDto.FirstName, registerUserDto.LastName);

            if (!userCreateResult.IsSuccess)
                return userCreateResult.GetExceptionStatusResult();

            IResult registerResult = _userService.Register(userCreateResult.Value!, registerUserDto.Password);

            if (!registerResult.IsSuccess)
                return registerResult.GetExceptionStatusResult();

            _unitOfWork.SaveChanges();

            return CreatedAtAction(nameof(Login), new { id = userCreateResult.Value!.Id });
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDto loginUserDto)
        {
            IResult<ClaimsIdentity> claimsIdentityResult = _userService.Login(loginUserDto, JwtBearerDefaults.AuthenticationScheme);

            if (!claimsIdentityResult.IsSuccess)
                return claimsIdentityResult.GetExceptionStatusResult();

            AccessToken token = _tokenHelper.CreateToken(new ClaimsPrincipal(claimsIdentityResult.Value!));

            return Ok(token);
        }

        [HttpPost]
        [Authorize(Roles = nameof(Domain.Entities.Users.User))]
        public IActionResult ChangePassword([FromBody] PasswordChangeDto passwordChangeDto)
        {
            IResult result = _userService.ChangePassword(passwordChangeDto.Password, passwordChangeDto.NewPassword, passwordChangeDto.NewPasswordConfirmation);

            if (!result.IsSuccess)
                return result.GetExceptionStatusResult();

            _unitOfWork.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = nameof(Domain.Entities.Users.User))]
        public IActionResult Organizations([FromQuery] PaginatedRequest request)
        {
            IResult<IPaginatedList<UserCreatedEventsListDto>> result = _userService.Organizations(request.PageSize, request.PageNumber);

            if (!result.IsSuccess)
                return result.GetExceptionStatusResult();

            return Ok(result.Value!);
        }

        [HttpGet]
        [Authorize(Roles = nameof(Domain.Entities.Users.User))]
        public IActionResult ParticipedEvents([FromQuery] PaginatedRequest request)
        {
            IResult<IPaginatedList<ParticipedEventListDto>> result = _userService.ParticipedEvents(request.PageSize, request.PageNumber);

            if (!result.IsSuccess)
                return result.GetExceptionStatusResult();

            return Ok(result.Value!);
        }

        [HttpGet]
        [Authorize(Roles = nameof(Domain.Entities.Users.User))]
        public IActionResult UpcomingEvents([FromQuery] PaginatedRequest request)
        {
            IResult<IPaginatedList<UpcomingEventListDto>> result = _userService.UpcomingEvents(request.PageSize, request.PageNumber);

            if (!result.IsSuccess)
                return result.GetExceptionStatusResult();

            return Ok(result.Value!);
        }
    }
}
