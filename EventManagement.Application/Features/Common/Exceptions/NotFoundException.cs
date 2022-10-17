namespace EventManagement.Application.Features.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string key, string name) : base($"{name} not found with given id '{key}'.")
        {

        }
    }
}