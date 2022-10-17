using EventManagement.WebRazorPages.Pages.Models;
using EventManagement.WebRazorPages.Pages.Profile;
using EventManagement.WebRazorPages.ServiceConfigurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventManagement.WebRazorPages.Pages.Shared
{
    public abstract class PaginatedPageModelBase<T> : APIClientPageModelBase
    {
        private readonly int[] pageSizes = { 1, 5, 10, 25 };
        private readonly string _baseUri;
        protected PaginatedPageModelBase(IHttpClientFactory httpClientFactory, IUserAccessor userAccessor, string baseUri) : base(httpClientFactory, userAccessor)
        {
            _baseUri = baseUri;
        }
        public SelectList PageSizes => new(pageSizes, PageSize);
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IPaginatedList<T> PaginatedList { get; set; } = null!; 
        public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 10)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            return await GetAsync($"{_baseUri}?pageNumber={pageNumber}&pageSize={pageSize}", OnGetAsyncHandler);
        }

        private async Task<IActionResult> OnGetAsyncHandler(HttpContent httpContent)
        {
            PaginatedList = await httpContent.ReadFromJsonAsync<PaginatedList<T>>();
            return Page();
        }
    }
}
