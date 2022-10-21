using EventManagement.WebRazorPages.Extensions;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using EventManagement.WebRazorPages.Pages.Shared;

namespace EventManagement.WebRazorPages.Pages.Events.Validators
{
    public class CreateTicketEventModel : APIClientPageModelBase
    {
        IValidator<CreateTicketEventModel> _validator;
        public CreateTicketEventModel(IHttpClientFactory httpClientFactory, IValidator<CreateTicketEventModel> validator) : base(httpClientFactory)
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
        [BindProperty]
        public decimal Price { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            FluentValidation.Results.ValidationResult result = _validator.Validate(this);

            if (!result.IsValid)
                result.AddToModelState(ModelState);

            return await PostAsync(API.Event.CreateTicketEvent, new { Name, Address, Description, Start, ApplicationDeadline, ParticipantLimit, CategoryId, CityId, Price }, OnPostAsyncHandler);
        }

        public async Task<IActionResult> OnPostAsyncHandler(HttpContent httpContent)
        {
            TempData.SetSuccessMessage("Event successfully created.");
            return Page();
        }
    }
}
