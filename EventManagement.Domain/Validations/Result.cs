using EventManagement.Domain.Validations.Interfaces;

namespace EventManagement.Domain.Validations
{
    internal class Result : IResult
    {
        protected Result()
        {
        }
        protected Result(Exception? exception) : this()
        {
            Exception = exception;
        }
        public bool IsSuccess => Exception is null;
        public Exception? Exception { get; }

        public static IResult Failure(Exception exception)
        {
            return new Result(exception);
        }
        public static IResult Success()
        {
            return new Result();
        }
    }
    internal class Result<T> : Result, IResult<T>
    {
        public T? Value { get; }
        private Result(Exception? exception) : base(exception)
        {

        }
        private Result(T? value) : base()
        {
            Value = value;
        }
        public static new IResult<T> Failure(Exception exception)
        {
            return new Result<T>(exception);
        }
        public static IResult<T> Success(T value)
        {
            return new Result<T>(value);
        }
    }
}
