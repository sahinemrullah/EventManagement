using EventManagement.Domain.Common;
using System.Security.Claims;

namespace EventManagement.Domain.Security
{
    public static class ClaimsHelper
    {
        public static ICollection<Claim> GetClaimsForCredential<TCredential>(TCredential credential) where TCredential : ICredential, IEntity
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, credential.Email),
                new Claim(ClaimTypes.NameIdentifier, credential.Id.ToString()),
                new Claim(ClaimTypes.Role, typeof(TCredential).Name),
            };
            return claims;
        }
    }
}
