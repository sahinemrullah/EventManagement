namespace EventManagement.Domain.Common
{
    public interface IUnitOfWork
    {
        public int SaveChanges();
    }
}
