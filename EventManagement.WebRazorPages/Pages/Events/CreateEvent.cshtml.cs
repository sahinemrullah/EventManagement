using EventManagement.WebRazorPages.Extensions;
using EventManagement.WebRazorPages.Pages.Shared;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.WebRazorPages.Pages.Events
{
    [Authorize]
    public class CreateEventModel : APIClientPageModelBase
    {
        IValidator<CreateEventModel> _validator;
        public CreateEventModel(IHttpClientFactory httpClientFactory, IValidator<CreateEventModel> validator) : base(httpClientFactory)
        {
            _validator = validator;
        }
        [BindProperty]
        public string Name { get; set; } = null!;
        [BindProperty]
        public string Address { get; set; } = null!;
        [BindProperty]
        public string Description { get; set; } = null!;
        [BindProperty]
        public DateTime Start { get; set; }
        [BindProperty]
        [Display(Name = "Application Deadline")]
        public DateTime ApplicationDeadline { get; set; }
        [BindProperty]
        [Display(Name = "Participant Limit")]
        public int ParticipantLimit { get; set; }
        [BindProperty]
        public int CategoryId { get; set; }
        [BindProperty]
        public int CityId { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            FluentValidation.Results.ValidationResult result = _validator.Validate(this);

            if (!result.IsValid)
                result.AddToModelState(ModelState);

            return await PostAsync(API.Event.CreateEvent, new { Name, Address, Description, Start, ApplicationDeadline, ParticipantLimit, CategoryId, CityId }, OnPostAsyncHandler);
        }

        public async Task<IActionResult> OnPostAsyncHandler(HttpContent httpContent)
        {
            TempData.SetSuccessMessage("Event successfully created.");
            return Page();
        }
    }
}
