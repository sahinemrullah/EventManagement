using EventManagement.WebRazorPages.Pages.Shared;

namespace EventManagement.WebRazorPages.Pages.Cities
{
    public class CityDto : IDefinitionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}