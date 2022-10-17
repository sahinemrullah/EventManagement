using EventManagement.WebRazorPages.Pages.Shared;
using EventManagement.WebRazorPages.ServiceConfigurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EventManagement.WebRazorPages.Pages.Definitions
{
    [Authorize(Roles = "Admin")]
    public class DefinitionEditModel : APIClientPageModelBase
    {
        private readonly string _baseUri;
        public DefinitionEditModel(IHttpClientFactory httpClientFactory, IUserAccessor userAccessor, string baseUri) : base(httpClientFactory, userAccessor)
        {
            _baseUri = baseUri;
        }
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string Name { get; set; } = null!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            return await GetAsync($"{_baseUri}/get/{id}", OnGetAsyncHandler);
        }

        public async Task<IActionResult> OnGetAsyncHandler(HttpContent httpContent)
        {
            JsonDocument jsonDocument = await httpContent.ReadFromJsonAsync<JsonDocument>();
            if(jsonDocument is not null)
            {
                Name = jsonDocument.RootElement.GetProperty("name").GetString();
                Id = jsonDocument.RootElement.GetProperty("id").GetInt32();
            }
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return await PutAsync($"{_baseUri}/edit/", new { Id, Name });
        }
    }
}
