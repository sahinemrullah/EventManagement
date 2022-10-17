using EventManagement.Domain.Common.CreateParameters;
using EventManagement.Domain.Entities.Events;
using EventManagement.Domain.Validations;
using EventManagement.Domain.Validations.Interfaces;

namespace EventManagement.Domain.Common.Helpers
{
    public static class EventHelpers
    {
        public static IResult<Event> Create(EventCreateParameters eventCreateParameters, int userId)
        {
            Event @event = new()
            {
                Name = eventCreateParameters.Name,
                Address = eventCreateParameters.Address,
                ApplicationDeadline = eventCreateParameters.ApplicationDeadline,
                CategoryId = eventCreateParameters.CategoryId,
                CityId = eventCreateParameters.CityId,
                Description = eventCreateParameters.Description,
                ParticipantLimit = eventCreateParameters.ParticipantLimit,
                Start = eventCreateParameters.Start,
                UserId = userId,
                ApprovedForListing = false,
            };

            IValidationResult validationResult = Validate(@event);

            if (!validationResult.IsSuccess)
                return ValidationResult<Event>.Failure(validationResult);

            return Result<Event>.Success(@event);
        }

        internal static IValidationResult Validate(Event @event)
        {
            ValidationResult validationResult = new();

            validationResult.ValidateString(nameof(@event.Name), @event.Name, 255);

            validationResult.ValidateString(nameof(@event.Address), @event.Address, 255);

            validationResult.ValidateString(nameof(@event.Description), @event.Description, 4000);

            validationResult.ValidateRange(nameof(@event.Start), @event.Start, DateTime.Now, DateTime.MaxValue);

            validationResult.ValidateRange(nameof(@event.ApplicationDeadline), @event.ApplicationDeadline, DateTime.Now, DateTime.MaxValue);

            validationResult.ValidateGreaterThanZero(nameof(@event.ParticipantLimit), @event.ParticipantLimit);

            validationResult.ValidateGreaterThanZero(nameof(@event.CategoryId), @event.CategoryId);

            validationResult.ValidateGreaterThanZero(nameof(@event.CityId), @event.CityId);

            validationResult.ValidateGreaterThanZero(nameof(@event.UserId), @event.UserId);

            return validationResult;
        }
    }
}