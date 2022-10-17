using EventManagement.Domain.Collections;

namespace EventManagement.Application.Features.Common.Extensions
{
    public static class IQueryablePaginatedListExtensions
    {
        public static PaginatedList<T> ToPaginatedList<T>(this IQueryable<T> queryable, int pageSize, int pageNumber)
        {
            int itemCount = queryable.Count();

            IEnumerable<T> items = queryable.Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

            return new PaginatedList<T>(items, itemCount, pageNumber, pageSize);
        }
    }
}