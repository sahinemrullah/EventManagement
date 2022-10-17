namespace EventManagement.Domain.Validations.Interfaces
{
    public interface IValidationResult : IResult
    {
        public Dictionary<string, List<string>> Errors { get; protected set; }
        public void AddError(string key, string message);
        void ValidateGreaterThanZero<T>(string key, T number) where T : struct, IComparable<T>;
        void ValidateRange<T>(string key, T value, T minimum, T maximum) where T : struct, IComparable<T>;
        void ValidateString(string key, string value, int maxLength);
        void ValidateString(string key, string value);
        void ValidateEmail(string value);
        void ValidatePassword(string password);
    }
    public interface IValidationResult<T> : IValidationResult, IResult<T>
    {

    }
}
