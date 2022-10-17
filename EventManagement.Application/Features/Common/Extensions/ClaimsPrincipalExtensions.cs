using EventManagement.Domain.Common;
using System.Security.Claims;

namespace EventManagement.Application.Features.Common.Extensions
{
    internal static class ClaimsPrincipalExtensions
    {
        internal static bool OwnsEntity(this ClaimsPrincipal claimsPrincipal, IOwnedEntity ownedEntity) => 
            claimsPrincipal.GetUserId(out int parsedUserId) && parsedUserId == ownedEntity.UserId;
        internal static bool GetUserId(this ClaimsPrincipal claimsPrincipal, out int userId) => 
            int.TryParse(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out userId);
    }
}
