namespace EventManagement.WebAPI.Models
{
    public class EventPagingRequest
    {
        public IEnumerable<int>? CityIds { get; set; }
        public IEnumerable<int>? CategoryIds { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}