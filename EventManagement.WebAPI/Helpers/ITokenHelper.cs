using System.Security.Claims;

namespace EventManagement.WebAPI.Helpers
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(ClaimsPrincipal claimsPrincipal);
    }
}
