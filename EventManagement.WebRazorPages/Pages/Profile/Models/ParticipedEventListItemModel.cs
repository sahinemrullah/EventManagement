namespace EventManagement.WebRazorPages.Pages.Profile.Models
{
    public class ParticipedEventListItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime Start { get; set; }
    }
}
