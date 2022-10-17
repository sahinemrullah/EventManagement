
using EventManagement.Domain.Exceptions;
using EventManagement.Domain.Security;
using EventManagement.Domain.Validations.Interfaces;
using System.Text.RegularExpressions;

namespace EventManagement.Domain.Validations
{
    internal class ValidationResult : IValidationResult
    {
        private Dictionary<string, List<string>> _errors;

        public ValidationResult()
        {
            _errors = new Dictionary<string, List<string>>();
        }

        public bool IsSuccess => !_errors.Any();
        public Exception? Exception => IsSuccess ? null : new ModelValidationException(_errors);
        public Dictionary<string, List<string>> Errors { get => _errors; set => _errors = value; }

        public void AddError(string key, string message)
        {
            if (!_errors.ContainsKey(key))
                _errors.Add(key, new List<string>());

            _errors[key].Add(message);
        }

        public void ValidateString(string key, string value, int maxLength)
        {
            ValidateString(key, value);

            if (value.Length > maxLength)
                AddError(key, $"{key} length is {value.Length} it should be less than or equal to {maxLength}.");
        }

        public void ValidateString(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                AddError(key, $"{key} should not be empty.");
        }

        public void ValidateRange<T>(string key, T value, T minimum, T maximum) where T : struct, IComparable<T>
        {
            if (minimum.CompareTo(maximum) > 0)
                throw new InvalidOperationException("Minimum cannot be greater than maximum.");

            if (value.CompareTo(minimum) < 0)
                AddError(key, $"Value should be greater than or equal to '{minimum}'.");
            else if (value.CompareTo(maximum) > 0)
                AddError(key, $"Value should be less than or equal to '{maximum}'.");
        }

        public void ValidateGreaterThanZero<T>(string key, T number) where T : struct, IComparable<T>
        {
            var r = default(T).CompareTo(number);
            if (default(T).CompareTo(number) >= 0)
                AddError(key, "Value should be greater than 0.");
        }

        public void ValidateEmail(string value)
        {
            const string emailKey = "Email";
            ValidateString(emailKey, value, 320);

            if (!Regex.IsMatch(value, @"^(.+)@(.+)$"))
                AddError(emailKey, "Invalid email.");
        }

        public void ValidatePassword(string password)
        {
            const string passwordKey = "Password";
            if (password.Length < PasswordRules.PasswordMinLength)
                 AddError(passwordKey, PasswordRules.PasswordLengthErrorMessage);

            if (password.Count(c => char.IsDigit(c)) < PasswordRules.PasswordDigitCount)
                AddError(passwordKey, PasswordRules.PasswordDigitErrorMessage);

            if (password.Count(c => char.IsLetter(c)) < PasswordRules.PasswordLetterCount)
                AddError(passwordKey, PasswordRules.PasswordLetterErrorMessage);
        }
    }

    internal class ValidationResult<T> : ValidationResult, IValidationResult<T>
    {
        public ValidationResult() : base()
        {

        }
        private ValidationResult(IValidationResult validationResult) : base()
        {
            Errors = validationResult.Errors;
        }

        private ValidationResult(T? value) : base()
        {
            Value = value;
        }

        public T? Value { get; }

        public static IValidationResult<T> Failure(IValidationResult validationResult)
        {
            return new ValidationResult<T>(validationResult);
        }

        public static IValidationResult<T> Success(T value)
        {
            return new ValidationResult<T>(value);
        }
    }
}
