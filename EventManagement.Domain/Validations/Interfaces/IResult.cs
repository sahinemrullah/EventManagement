namespace EventManagement.Domain.Validations.Interfaces
{
    public interface IResult
    {
        public bool IsSuccess { get; }
        public Exception? Exception { get; }
    }

    public interface IResult<T> : IResult
    {
        T? Value { get; }
    }
}
