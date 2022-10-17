using Microsoft.AspNetCore.Mvc;
using IResult = EventManagement.Domain.Validations.Interfaces.IResult;
using EventManagement.Application.Features.Common.Exceptions;
using System.Security.Authentication;
using EventManagement.Domain.Exceptions;
using Microsoft.AspNetCore.Authentication;
using EventManagement.Application.Features.Events;
using Microsoft.AspNetCore.Authorization;

namespace EventManagement.WebAPI.Helpers
{
    public static class IResultIActionResultExtensions
    {
        public static IActionResult GetExceptionStatusResult(this IResult result)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException();

            return result.Exception switch
            {
                NotFoundException notFoundException => new NotFoundObjectResult(notFoundException.Message),
                BusinessRuleException businessRuleException => new BadRequestObjectResult(ToBadRequestErrorDict(businessRuleException.Message)),
                ArgumentException argumentException => new BadRequestObjectResult(ToBadRequestErrorDict(argumentException.Message)),
                EntityExistsException entityExistsException => new BadRequestObjectResult(ToBadRequestErrorDict(entityExistsException.Message)),
                ModelValidationException modelValidationException => new BadRequestObjectResult(new { modelValidationException.Errors }),
                AuthenticationException => new ChallengeResult(),
                AuthorizationException => new ForbidResult(),
                _ => new StatusCodeResult(StatusCodes.Status500InternalServerError),
            };
        }

        private static object ToBadRequestErrorDict(string message)
        {
            return new { Errors = new Dictionary<string, string[]> { { string.Empty, new string[] { message } } } };
        }
    }
}
