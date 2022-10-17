using EventManagement.Domain.Common;
using EventManagement.Domain.Entities.Events;
using EventManagement.Domain.Security;
using EventManagement.Domain.Specifications;
using EventManagement.Domain.Validations;
using EventManagement.Domain.Validations.Interfaces;
using System.Linq.Expressions;

namespace EventManagement.Domain.Entities.Users
{
    public sealed class User : IEntity, ICredential
    {
        private ICollection<Event> _appliedEvents = null!;
        private ICollection<Event> _createdEvents = null!;
        private ICollection<Role> _roles = null!;

        private User()
        {
            _appliedEvents = new HashSet<Event>();
            _createdEvents = new HashSet<Event>();
        }
        internal User(string email, string firstname, string lastname) : this()
        {
            Email = email;
            FirstName = firstname;
            LastName = lastname;
        }
        public int Id { get; set; }

        public string Email { get; private init; } = null!;

        public string PasswordHash { get; private set; } = null!;

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public IEnumerable<Event> AppliedEvents
        {
            get => _appliedEvents;
            internal set => _appliedEvents = value.ToHashSet();
        }
        public IEnumerable<Event> CreatedEvents
        {
            get => _createdEvents;
            internal set => _createdEvents = value.ToHashSet();
        }
        public IEnumerable<Role> Roles
        {
            get => _roles;
            internal set => _roles = value.ToHashSet();
        }

        internal static IResult<User> FromId(int id)
        {
            if (id < 1)
                return Result<User>.Failure(new ArgumentException("Invalid user"));

            User user = new()
            {
                Id = id
            };

            return Result<User>.Success(user);
        }
        public static IResult<User> Create(string email, string firstName, string lastName)
        {
            ValidationResult<User> validationResult = new();

            validationResult.ValidateEmail(email);

            validationResult.ValidateString(nameof(firstName), firstName, 30);

            validationResult.ValidateString(nameof(lastName), lastName, 30);

            if (!validationResult.IsSuccess)
                return ValidationResult<User>.Failure(validationResult);

            User applicationUser = new(email, firstName, lastName);

            return Result<User>.Success(applicationUser);
        }

        public string GetDisplayName()
        {
            return $"{FirstName} {LastName}";
        }

        public void SetPassword(string password, IPasswordHasher passwordHasher)
        {
            PasswordHash = passwordHasher.HashPassword(password);
        }

        public static class Mapping
        {
            public const string AppliedEventsFieldName = nameof(_appliedEvents);
            public const string CreatedEventsFieldName = nameof(_createdEvents);
            public const string RoleFieldName = nameof(_roles);

            public static Expression<Func<User, User>> MapIdOnly => u => new User()
            {
                Id = u.Id
            };
        }
    }
}
