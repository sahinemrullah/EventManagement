using EventManagement.Domain.Entities.Events;
using Microsoft.EntityFrameworkCore;
using EventManagement.Domain.Specifications;
using System.Linq.Expressions;
using LinqKit;

namespace EventManagement.Application.Features.Events.Specifications
{
    public class CategoryAndCityFilterSpecification<TEvent> : ISpecification<TEvent>
        where TEvent : Event
    {
        public CategoryAndCityFilterSpecification(IEnumerable<int>? categoryIds = null, IEnumerable<int>? cityIds = null)
        {
            Expression<Func<TEvent, bool>> expression = PredicateBuilder.New<TEvent>(true);

            if (categoryIds is not null && categoryIds.Any())
                expression = expression.And(e => categoryIds.Contains(e.CategoryId));

            if (cityIds is not null && cityIds.Any())
                expression = expression.And(e => cityIds.Contains(e.CityId));

            Predicate = expression;
        }
        public Expression<Func<TEvent, bool>> Predicate { get; }
    }

}
