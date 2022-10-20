using EventManagement.WebRazorPages.Pages.Models;
using EventManagement.WebRazorPages.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventManagement.WebRazorPages.Pages.Definitions
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public class DefinitionIndexModel<TDefinition> : APIClientPageModelBase
        where TDefinition : class, IDefinitionDto
    {
        public DefinitionIndexModel(IHttpClientFactory httpClientFactory, string name) : base(httpClientFactory)
        {
            _name = name;
        }
        private int[] pageSizes = new int[] { 1, 5, 10, 25 };
        private string _name = "";
        public SelectList PageSizes => new(pageSizes, PageSize);
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IPaginatedList<TDefinition> Items { get; set; } = null!;
        public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 10)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            return await GetAsync(API.Definition.GetPaginated(_name, pageSize, pageNumber), OnGetAsyncHandler);
        }
        private async Task<IActionResult> OnGetAsyncHandler(HttpContent httpContent)
        {
            Items = await httpContent.ReadFromJsonAsync<PaginatedList<TDefinition>>();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync([FromForm] int id)
        {
            return await DeleteAsync(API.Definition.Delete(_name, id));
        }
    }
}
