namespace EventManagement.Domain.Exceptions
{
    public class ModelValidationException : Exception
    {
        public Dictionary<string, List<string>> Errors { get; }
        public ModelValidationException(Dictionary<string, List<string>> errors)
        {
            Errors = errors;
        }
    }
}