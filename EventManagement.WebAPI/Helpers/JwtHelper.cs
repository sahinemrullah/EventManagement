using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventManagement.WebAPI.Helpers
{
    public class JwtHelper : ITokenHelper
    {
        private readonly TokenOptions _tokenOptions;

        public JwtHelper(IConfiguration configuration)
        {
            _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateToken(ClaimsPrincipal claimsPrincipal)
        {

            DateTime tokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            DateTime refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration);
            
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
            
            SigningCredentials signingCredentials = new (securityKey, SecurityAlgorithms.HmacSha512Signature);
            
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            
            JwtSecurityToken accessTokenJWT = CreateJwtSecurityToken(_tokenOptions, tokenExpiration, signingCredentials, claimsPrincipal);
            string? token = jwtSecurityTokenHandler.WriteToken(accessTokenJWT);
            
            JwtSecurityToken refreshTokenJWT = CreateJwtSecurityToken(_tokenOptions, refreshTokenExpiration, signingCredentials, claimsPrincipal);
            string? refreshToken = jwtSecurityTokenHandler.WriteToken(refreshTokenJWT);

            return new AccessToken
            {
                Token = token,
                RefreshToken = refreshToken,
            };
        }

        private static JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions,
                                                       DateTime? tokenExpiration,
                                                       SigningCredentials signingCredentials,
                                                       ClaimsPrincipal claimsPrincipal)
        {
            JwtSecurityToken jwt = new(
                tokenOptions.Issuer,
                tokenOptions.Audience,
                claims: claimsPrincipal.Claims,
                notBefore: DateTime.Now,
                expires: tokenExpiration,
                signingCredentials: signingCredentials
            );
            return jwt;
        }
    }
}
