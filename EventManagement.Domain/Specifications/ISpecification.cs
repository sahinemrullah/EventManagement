using System.Linq.Expressions;

namespace EventManagement.Domain.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Predicate { get; }
    }
    public interface IMapSpecification<T> : ISpecification<T>
    {
        Expression<Func<T, T>> Map { get; }
    }
    public interface IMapSpecification<T, U> : ISpecification<T>
    {
        Expression<Func<T, U>> Map { get; }
    }
}