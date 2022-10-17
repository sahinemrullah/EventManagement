using FluentValidation;

namespace EventManagement.WebRazorPages.Pages.Events.Validators
{
    public class CreateEventValidator : AbstractValidator<CreateEventModel>
    {
        public CreateEventValidator()
        {
            RuleFor(c => c.Name).NotEmpty();

            RuleFor(c => c.Address).NotEmpty();

            RuleFor(c => c.Description).NotEmpty();

            RuleFor(c => c.ParticipantLimit).GreaterThan(0);

            RuleFor(c => c.CategoryId).GreaterThan(0);

            RuleFor(c => c.CityId).GreaterThan(0);

            RuleFor(c => c.Start).GreaterThan(DateTime.Now);

            RuleFor(c => c.ApplicationDeadline).GreaterThan(DateTime.Now);
        }
    }
}
