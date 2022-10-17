using EventManagement.Application.Features.Credentials.DTOs;
using EventManagement.Domain.Common;
using EventManagement.Domain.Validations.Interfaces;
using System.Security.Claims;

namespace EventManagement.Application.Features.Credentials
{
    public interface ICredentialsService<TCredential> where TCredential : ICredential
    {
        public IResult Register(TCredential credential, string password);

        public IResult<ClaimsIdentity> Login(LoginDto loginDto, string authenticationScheme);
    }
}
