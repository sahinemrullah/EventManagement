using System.Security.Claims;

namespace EventManagement.WebRazorPages.ServiceConfigurations
{
    public interface IUserAccessor
    {
        ClaimsPrincipal User { get; }
    }
}