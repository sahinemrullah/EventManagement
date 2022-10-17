using FluentValidation;

namespace EventManagement.WebRazorPages.Pages.Profile.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordValidator()
        {
            RuleFor(c => c.Password)
                                    .NotEmpty();

            RuleFor(c => c.NewPassword)
                                       .NotEmpty()
                                       .Must(c => c.Any(c => char.IsDigit(c)))
                                       .WithMessage("Passwords must contain at least 1 digit.")
                                       .Must(c => c.Any(c => char.IsLetter(c)))
                                       .WithMessage("Passwords must contain at least 1 letter.")
                                       .Length(8, 20);

            RuleFor(c => c.NewPasswordConfirmation)
                                                   .NotEmpty()
                                                   .Equal(c => c.NewPassword);
        }
    }
}
