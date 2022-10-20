using EventManagement.WebRazorPages.Extensions;
using EventManagement.WebRazorPages.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.WebRazorPages.Pages.Definitions
{
    public abstract class DefinitionCreateModel : APIClientPageModelBase
    {
        private readonly string _name;
        protected DefinitionCreateModel(IHttpClientFactory httpClientFactory, string name) : base(httpClientFactory)
        {
            _name = name;
        }

        [BindProperty]
        public string Name { get; set; } = null!;
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return await PostAsync(API.Definition.Create(_name), new { Name }, OnPostAsyncHandler);
        }

        private async Task<IActionResult> OnPostAsyncHandler(HttpContent httpContent)
        {
            TempData.SetSuccessMessage("Successfully created.");
            return Page();
        }
    }
}
