namespace EventManagement.Domain.Common
{
    public interface IOwnedEntity : IEntity
    {
        public int UserId { get; }
    }
}