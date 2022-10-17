using EventManagement.Domain.Entities.Users;
using EventManagement.Domain.Specifications;
using System.Linq.Expressions;

namespace EventManagement.Application.Features.Users.Specifications
{
    public class UserRoleSpecification : IMapSpecification<UserRole, string>
    {
        public UserRoleSpecification(int userId)
        {
            Map = r => r.Role.Name;
            Predicate = r => r.UserId == userId;
        }
        public Expression<Func<UserRole, string>> Map { get; }

        public Expression<Func<UserRole, bool>> Predicate { get; }
    }
}
