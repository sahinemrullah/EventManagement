using EventManagement.Domain.Common;
using EventManagement.Domain.Entities.Users;
using EventManagement.Domain.Exceptions;
using EventManagement.Domain.Validations;
using EventManagement.Domain.Validations.Interfaces;

namespace EventManagement.Domain.Entities.Events
{
    public class Event : IEntity, IOwnedEntity
    {
        private ICollection<User> _appliedUsers;
        public const string AppliedUsersFieldName = nameof(_appliedUsers);

        internal Event()
        {
            _appliedUsers = new HashSet<User>();
        }
        
        public int Id { get; set; }
        public string Name { get; internal set; } = null!;
        public string Address { get; internal set; } = null!;
        public string Description { get; internal set; } = null!;
        public DateTime Start { get; internal set; }
        public DateTime ApplicationDeadline { get; internal set; }
        public bool ApprovedForListing { get; internal set; }
        public int ParticipantLimit { get; internal set; }
        public int CategoryId { get; internal set; }
        public int CityId { get; internal set; }
        public int UserId { get; internal set; }
        public User User { get; internal set; } = null!;
        public Category Category { get; internal set; } = null!;
        public City City { get; internal set; } = null!;
        public IEnumerable<User> AppliedUsers 
        { 
            get => _appliedUsers; 
            internal set
            {
                _appliedUsers = value.ToHashSet();
            }
        }

        public void Approve()
        {
            ApprovedForListing = true;
        }

        public IResult ChangeLocation(string address, int participantLimit)
        {
            if ((Start - DateTime.Now).TotalDays <= 5)
                return Result.Failure(new BusinessRuleException("You can't update the event after 5 days left to start."));

            ValidationResult validationResult = new();

            validationResult.ValidateString(nameof(Address), address);

            if (participantLimit < ParticipantLimit)
                validationResult.AddError(nameof(ParticipantLimit), $"Value cannot be less than old value('{ParticipantLimit}').");

            if (validationResult.IsSuccess)
            {
                Address = address;
                ParticipantLimit = participantLimit;
            }

            return validationResult;
        }

        public IResult<User> AddUser(int userId)
        {
            IResult<User> result = User.FromId(userId);

            if(result.IsSuccess)
                return AddUser(result.Value!);
            
            return result;
        }

        public IResult<User> AddUser(User user)
        {
            if (!ApprovedForListing)
                throw new InvalidOperationException("You cannot add user to an unapproved event.");

            if ((ApplicationDeadline - DateTime.Now).TotalDays < 0)
                return Result<User>.Failure(new BusinessRuleException("Applications are closed."));

            User? existingUser = _appliedUsers.SingleOrDefault(u => u.Id == user.Id);

            if (existingUser is not null)
                return Result<User>.Failure(new BusinessRuleException("You are already applied for the event."));

            if (ParticipantLimit <= AppliedUsers.Count())
                return Result<User>.Failure(new BusinessRuleException("You cannot apply to the event, the quota is full."));

            _appliedUsers.Add(user);

            return Result<User>.Success(user);
        }

        public IResult RemoveUser(User appliedUser)
        {
            User? user = _appliedUsers.SingleOrDefault(u => u.Id == appliedUser.Id);

            if (user is null)
                return Result.Success();

            _appliedUsers.Remove(user);

            return Result.Success();
        }
    }
}
