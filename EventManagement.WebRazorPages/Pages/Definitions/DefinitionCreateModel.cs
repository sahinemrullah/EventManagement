using EventManagement.WebRazorPages.Extensions;
using EventManagement.WebRazorPages.Pages.Shared;
using EventManagement.WebRazorPages.ServiceConfigurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.WebRazorPages.Pages.Definitions
{
    public abstract class DefinitionCreateModel : APIClientPageModelBase
    {
        private readonly string _baseUri;
        protected DefinitionCreateModel(IHttpClientFactory httpClientFactory, IUserAccessor userAccessor, string baseUri) : base(httpClientFactory, userAccessor)
        {
            _baseUri = baseUri;
        }

        [BindProperty]
        public string Name { get; set; } = null!;
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return await PostAsync($"{_baseUri}/create", new { Name }, OnPostAsyncHandler);
        }

        private async Task<IActionResult> OnPostAsyncHandler(HttpContent httpContent)
        {
            TempData.SetSuccessMessage("Successfully created.");
            return Page();
        }
    }
}
