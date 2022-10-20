using EventManagement.WebRazorPages.Extensions;
using EventManagement.WebRazorPages.Pages.Shared;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.WebRazorPages.Pages.Auth
{
    public class RegisterModel : APIClientPageModelBase
    {
        IValidator<RegisterModel> _validator;
        public RegisterModel(IHttpClientFactory httpClientFactory, IValidator<RegisterModel> validator) : base(httpClientFactory)
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

            PartialViewName = "RegisterForm";

            return await PostAsync(API.User.Register, new { Email, Password, LastName, FirstName }, OnPostAsyncHandler);
        }
        public async Task<IActionResult> OnPostAsyncHandler(HttpContent httpContent)
        {
            if(Request.IsAjaxRequest())
                return new OkObjectResult(new { Message = "Successfully created user.", RedirectUrl = Url.Page("./Login") });

            TempData.SetSuccessMessage("Successfully created user.");

            return RedirectToPage("./Login");
        }
    }
}
