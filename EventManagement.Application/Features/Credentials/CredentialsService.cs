using EventManagement.Domain.Security;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using EventManagement.Persistence;
using EventManagement.Application.Features.Common.Exceptions;
using EventManagement.Domain.Common;
using EventManagement.Application.Features.Credentials.DTOs;
using EventManagement.Domain.Exceptions;
using EventManagement.Domain.Validations.Interfaces;

namespace EventManagement.Application.Features.Credentials
{
    internal class CredentialsServiceBase<TCredential> : ICredentialsService<TCredential> where TCredential : class, ICredential, IEntity
    {
        public CredentialsServiceBase(EventManagementDbContext dbContext, IPasswordHasher passwordHasher, IResultFactory resultFactory)
        {
            PasswordHasher = passwordHasher;
            CredentialsSet = dbContext.Set<TCredential>();
            ResultFactory = resultFactory;
        }
        protected IResultFactory ResultFactory { get; }
        protected DbSet<TCredential> CredentialsSet { get; }
        protected IPasswordHasher PasswordHasher { get; }
        public IResult Register(TCredential credential, string password)
        {
            TCredential? existingCredential = GetByEmail(credential.Email);

            if (existingCredential is not null)
                return ResultFactory.Failure(new EntityExistsException(nameof(credential.Email)));

            IResult setPasswordResult = SetPassword(credential, password);

            if (!setPasswordResult.IsSuccess)
                return setPasswordResult;

            CredentialsSet.Add(credential);

            return ResultFactory.Success();
        }

        public virtual IResult<ClaimsIdentity> Login(LoginDto loginDto, string authenticationScheme)
        {
            TCredential? credential = GetByEmail(loginDto.Email);

            if (credential is null || !PasswordHasher.CheckPasswordHash(loginDto.Password, credential.PasswordHash))
                return ResultFactory.Failure<ClaimsIdentity>(new BusinessRuleException("Invalid credentials."));

            ICollection<Claim> claims = ClaimsHelper.GetClaimsForCredential(credential);

            ClaimsIdentity claimsIdentity = new(claims, authenticationScheme);

            return ResultFactory.Success(claimsIdentity);
        }

        private TCredential? GetByEmail(string email)
        {
            return CredentialsSet.SingleOrDefault(u => u.Email.ToUpper() == email.ToUpper());
        }

        protected IResult SetPassword(TCredential credential, string password)
        {
            IValidationResult validation = ResultFactory.Validator();

            validation.ValidatePassword(password);

            if (!validation.IsSuccess)
                return validation;

            credential.SetPassword(password, PasswordHasher);

            return ResultFactory.Success();
        }
    }
}
