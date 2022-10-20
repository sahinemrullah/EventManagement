using EventManagement.WebRazorPages.Pages.Shared;

namespace EventManagement.WebRazorPages.Pages.Categories
{
    public class CategoryDto : IDefinitionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}