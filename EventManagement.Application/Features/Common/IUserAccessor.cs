using System.Security.Claims;

namespace EventManagement.Application.Features.Common
{
    public interface IUserAccessor
    {
        public ClaimsPrincipal User { get; }
    }
}
