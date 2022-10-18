using EventManagement.WebRazorPages.Extensions;
using EventManagement.WebRazorPages.Pages.Shared;
using EventManagement.WebRazorPages.ServiceConfigurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventManagement.WebRazorPages.Pages.Events
{
    [Authorize(Roles = "User")]
    public class IndexModel : APIClientPageModelBase
    {
        private readonly int[] pageSizes = { 1, 5, 10, 25 };

        public IndexModel(IHttpClientFactory httpClientFactory, IUserAccessor userAccessor) : base(httpClientFactory, userAccessor)
        {
        }

        public SelectList PageSizes => new(pageSizes, PageSize);
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<int> Cities { get; set; } = null!;
        public IEnumerable<int> Categories { get; set; } = null!;
        public IActionResult OnGet(IEnumerable<int> cities, IEnumerable<int> categories, int pageNumber = 1, int pageSize = 10)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Cities = cities;
            Categories = categories;
            return Page();
        }

        public IActionResult OnPost()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostApplyAsync([FromForm] int id)
        {
            return await PostAsync($"api/events/apply/{id}", string.Empty, OnPostApplyAsyncHandler);
        }

        private async Task<IActionResult> OnPostApplyAsyncHandler(HttpContent httpContent)
        {
            return RedirectToPage("/Profile/UpcomingEvents");
        }
    }
}
