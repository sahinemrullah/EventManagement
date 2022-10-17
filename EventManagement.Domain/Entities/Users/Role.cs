namespace EventManagement.Domain.Entities.Users
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private HashSet<User> _users = new();
        public IEnumerable<User> Users
        {
            get => _users;
            internal set => _users = value.ToHashSet();
        }
    }
}
