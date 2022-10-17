using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventManagement.WebRazorPages.Pages.Events
{
    [Authorize(Roles = "User")]
    public class IndexModel : PageModel
    {
        private readonly int[] pageSizes = { 1, 5, 10, 25 };
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
    }
}
