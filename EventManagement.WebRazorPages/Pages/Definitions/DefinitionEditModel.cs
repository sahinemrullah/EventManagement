using EventManagement.WebRazorPages.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EventManagement.WebRazorPages.Pages.Definitions
{
    [Authorize(Roles = "Admin")]
    public class DefinitionEditModel : APIClientPageModelBase
    {
        private readonly string _name;
        public DefinitionEditModel(IHttpClientFactory httpClientFactory, string name) : base(httpClientFactory)
        {
            _name = name;
        }
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string Name { get; set; } = null!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            return await GetAsync(API.Definition.Get(_name, id), OnGetAsyncHandler);
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            return await PutAsync(API.Definition.Edit(_name), new { Id, Name });
        }
    }
}
