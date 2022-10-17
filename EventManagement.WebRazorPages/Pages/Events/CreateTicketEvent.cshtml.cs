using EventManagement.WebRazorPages.Extensions;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using EventManagement.WebRazorPages.Pages.Shared;
using EventManagement.WebRazorPages.ServiceConfigurations;

namespace EventManagement.WebRazorPages.Pages.Events.Validators
{
    public class CreateTicketEventModel : APIClientPageModelBase
    {
        IValidator<CreateTicketEventModel> _validator;
        public CreateTicketEventModel(IHttpClientFactory httpClientFactory, IUserAccessor userAccessor, IValidator<CreateTicketEventModel> validator) : base(httpClientFactory, userAccessor)
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

            return await PostAsync($"api/events/createticketevent", new { Name, Address, Description, Start, ApplicationDeadline, ParticipantLimit, CategoryId, CityId, Price }, OnPostAsyncHandler);
        }

        public async Task<IActionResult> OnPostAsyncHandler(HttpContent httpContent)
        {
            TempData.SetSuccessMessage("Event successfully created.");
            return RedirectToPage("./CreateTicketEvent");
        }
    }
}
