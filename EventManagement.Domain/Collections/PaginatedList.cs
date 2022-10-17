using System.Xml.Serialization;

namespace EventManagement.Domain.Collections
{
    [XmlRoot(ElementName = "PaginatedList")]
    public class PaginatedList<T> : IPaginatedList<T>
    {
        public PaginatedList()
        {

        }
        public PaginatedList(IEnumerable<T> items, int itemCount, int pageNumber = 1, int pageSize = 10)
        {
            TotalCount = itemCount;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(itemCount / (double)pageSize);
            Items = new HashSet<T>(items);
        }
        [XmlElement(ElementName = nameof(HasNext))]
        public bool HasNext => CurrentPage < TotalPages;

        [XmlElement(ElementName = nameof(HasPrevious))]
        public bool HasPrevious => CurrentPage > 1;

        [XmlElement(ElementName = nameof(TotalCount))]
        public int TotalCount { get; set; }

        [XmlElement(ElementName = nameof(CurrentPage))]
        public int CurrentPage { get; set; }

        [XmlElement(ElementName = nameof(PageSize))]
        public int PageSize { get; set; }

        [XmlElement(ElementName = nameof(TotalPages))]
        public int TotalPages { get; set; }

        [XmlElement(ElementName = nameof(Items))]
        public HashSet<T> Items { get; set; }
    }
}