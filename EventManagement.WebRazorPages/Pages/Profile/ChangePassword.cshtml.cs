using EventManagement.WebRazorPages.Extensions;
using EventManagement.WebRazorPages.Pages.Shared;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.WebRazorPages.Pages.Profile
{
    [Authorize]
    public class ChangePasswordModel : APIClientPageModelBase
    {
        IValidator<ChangePasswordModel> _validator;
        public ChangePasswordModel(IHttpClientFactory httpClientFactory, IValidator<ChangePasswordModel> validator) : base(httpClientFactory)
        {
            _validator = validator;
        }

        [BindProperty]
        public string Password { get; set; } = null!;
        [BindProperty]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; } = null!;
        [BindProperty]
        [Display(Name = "Confirm Password")]
        public string NewPasswordConfirmation { get; set; } = null!;
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            FluentValidation.Results.ValidationResult result = _validator.Validate(this);

            if (!result.IsValid)
                result.AddToModelState(ModelState);

            return await PostAsync(API.User.ChangePassword, new { Password, NewPassword, NewPasswordConfirmation }, OnPostAsyncHandler);
        }

        public async Task<IActionResult> OnPostAsyncHandler(HttpContent httpContent)
        {
            TempData.SetSuccessMessage("Password successfully changed. Please enter your credentials again.");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Challenge();
        }
    }
}
