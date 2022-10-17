using EventManagement.Domain.Validations.Interfaces;

namespace EventManagement.Domain.Validations
{
    internal class ResultFactory : IResultFactory
    {
        public IResult Failure(Exception exception)
        {
            return Result.Failure(exception);
        }

        public IResult<T> Failure<T>(Exception exception)
        {
            return Result<T>.Failure(exception);
        }

        public IValidationResult<T> Failure<T>(IValidationResult validation)
        {
            return ValidationResult<T>.Failure(validation);
        }

        public IResult Success()
        {
            return Result.Success();
        }

        public IResult<T> Success<T>(T value)
        {
            return Result<T>.Success(value);
        }

        public IValidationResult Validator()
        {
            return new ValidationResult();
        }
    }
}
