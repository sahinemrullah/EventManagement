namespace EventManagement.Domain.Validations.Interfaces
{
    public interface IResultFactory
    {
        IResult Success();
        IResult Failure(Exception exception);
        IResult<T> Success<T>(T value);
        IResult<T> Failure<T>(Exception exception);
        IValidationResult<T> Failure<T>(IValidationResult validation);
        IValidationResult Validator();
    }
}
