namespace EventManagement.Domain.Specifications
{
    public static class SpecificationExtensions
    {
        public static IQueryable<T> WithSpecification<T>(this IQueryable<T> query, ISpecification<T> specification)
        {
            return query.Where(specification.Predicate);
        }
        public static IQueryable<T> WithMapSpecification<T>(this IQueryable<T> query, IMapSpecification<T> specification)
        {
            return query
                    .WithSpecification(specification)
                    .Select(specification.Map);
        }
        public static IQueryable<U> WithMapSpecification<T, U>(this IQueryable<T> query, IMapSpecification<T, U> specification)
        {
            return query
                    .WithSpecification(specification)
                    .Select(specification.Map);
        }
    }
}