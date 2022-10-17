using EventManagement.WebRazorPages.Extensions;
using EventManagement.WebRazorPages.Pages.Shared;
using EventManagement.WebRazorPages.ServiceConfigurations;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventManagement.WebRazorPages.Pages.Auth
{
    public class RegisterModel : APIClientPageModelBase
    {
        IValidator<RegisterModel> _validator;
        public RegisterModel(IHttpClientFactory httpClientFactory, IUserAccessor userAccessor, IValidator<RegisterModel> validator) : base(httpClientFactory, userAccessor)
        {
            _validator = validator;
        }

        [BindProperty]
        public string FirstName { get; set; } = null!;
        [BindProperty]
        public string LastName { get; set; } = null!;
        [BindProperty]
        public string Email { get; set; } = null!;
        [BindProperty]
        public string Password { get; set; } = null!;
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ValidationResult result = await _validator.ValidateAsync(this);
            if (!result.IsValid)
                result.AddToModelState(ModelState);

            return await PostAsync("api/users/register", new { Email, Password, LastName, FirstName }, OnPostAsyncHandler);
        }
        public async Task<IActionResult> OnPostAsyncHandler(HttpContent httpContent)
        {
            TempData.SetSuccessMessage("Successfully created user.");

            return RedirectToPage("./Login");
        }
    }
}
