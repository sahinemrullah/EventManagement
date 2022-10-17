namespace EventManagement.Application.Features.Common.Exceptions
{
    public class EntityExistsException : Exception
    {
        private const string _format = "{0} already exists.";
        public EntityExistsException(string name) : base(string.Format(_format, name))
        {
            
        }
    }
}