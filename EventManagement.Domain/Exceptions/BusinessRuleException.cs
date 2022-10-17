using System.Runtime.Serialization;

namespace EventManagement.Domain.Exceptions
{
    [Serializable]
    public class BusinessRuleException : ApplicationException
    {
        public BusinessRuleException()
        {
        }

        public BusinessRuleException(string? message) : base(message)
        {
        }

        public BusinessRuleException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BusinessRuleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}