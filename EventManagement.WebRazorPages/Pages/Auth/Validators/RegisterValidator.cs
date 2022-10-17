using FluentValidation;

namespace EventManagement.WebRazorPages.Pages.Auth.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterModel>
    {
        public RegisterValidator()
        {
            RuleFor(e => e.FirstName).NotEmpty().Length(1, 20);
            RuleFor(e => e.LastName).NotEmpty().Length(1, 20);
            RuleFor(e => e.Email)
                                .NotEmpty()
                                .EmailAddress();
            RuleFor(c => c.Password)
                                   .NotEmpty()
                                   .Must(c => c.Any(c => char.IsDigit(c)))
                                   .WithMessage("Passwords must contain at least 1 digit.")
                                   .Must(c => c.Any(c => char.IsLetter(c)))
                                   .WithMessage("Passwords must contain at least 1 letter.")
                                   .Length(8, 20);
        }
    }
}
