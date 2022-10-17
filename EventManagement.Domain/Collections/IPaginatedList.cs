namespace EventManagement.Domain.Collections
{
    public interface IPaginatedList
    {
        public bool HasNext { get; }
        public bool HasPrevious { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }
        public int CurrentPage { get; }
        public int PageSize { get; }
    }

    public interface IPaginatedList<T> : IPaginatedList
    {
        public HashSet<T> Items { get; }
    }
}