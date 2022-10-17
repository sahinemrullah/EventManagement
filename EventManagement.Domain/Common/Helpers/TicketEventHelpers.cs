using EventManagement.Domain.Common.CreateParameters;
using EventManagement.Domain.Entities.Events;
using EventManagement.Domain.Validations;
using EventManagement.Domain.Validations.Interfaces;

namespace EventManagement.Domain.Common.Helpers
{
    public static class TicketEventHelpers
    {
        public static IResult<TicketEvent> Create(TicketEventCreateParameters ticketEventCreateParameters, int userId)
        {
            TicketEvent ticketEvent = new()
            {
                Name = ticketEventCreateParameters.Name,
                Address = ticketEventCreateParameters.Address,
                ApplicationDeadline = ticketEventCreateParameters.ApplicationDeadline,
                CategoryId = ticketEventCreateParameters.CategoryId,
                CityId = ticketEventCreateParameters.CityId,
                Description = ticketEventCreateParameters.Description,
                ParticipantLimit = ticketEventCreateParameters.CityId,
                Start = ticketEventCreateParameters.Start,
                Price = ticketEventCreateParameters.Price,
                UserId = userId,
                ApprovedForListing = false,
            };

            IValidationResult validationResult = EventHelpers.Validate(ticketEvent);

            validationResult.ValidateGreaterThanZero(nameof(ticketEvent.Price), ticketEventCreateParameters.Price);

            if (!validationResult.IsSuccess)
                return ValidationResult<TicketEvent>.Failure(validationResult);

            return Result<TicketEvent>.Success(ticketEvent);
        }
    }
}