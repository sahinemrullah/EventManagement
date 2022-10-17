using EventManagement.WebRazorPages.Pages.Models;
using EventManagement.WebRazorPages.Pages.Shared;
using EventManagement.WebRazorPages.ServiceConfigurations;
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
        public DefinitionIndexModel(IHttpClientFactory httpClientFactory, IUserAccessor userAccessor, string baseUrl) : base(httpClientFactory, userAccessor)
        {
            _baseUrl = baseUrl;
        }
        private int[] pageSizes = new int[] { 1, 5, 10, 25 };
        private string _baseUrl = "";
        public SelectList PageSizes => new(pageSizes, PageSize);
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IPaginatedList<TDefinition> Dtos { get; set; } = null!;
        public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 10)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            return await GetAsync($"{_baseUrl}/getpaginated/?pageSize={pageSize}&pageNumber={pageNumber}", GetResult);
        }
        private async Task<IActionResult> GetResult(HttpContent httpContent)
        {
            Dtos = await httpContent.ReadFromJsonAsync<PaginatedList<TDefinition>>();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync([FromForm] int id)
        {
            return await DeleteAsync($"{_baseUrl}/delete/{id}");
        }
    }
}
