namespace EventManagement.Domain.Security
{
    public static class PasswordRules
    {
        public const int PasswordMinLength = 8;
        public const int PasswordDigitCount = 1;
        public const int PasswordLetterCount = 1;
        public readonly static string PasswordLengthErrorMessage = $"Password should be at least {PasswordMinLength} characters long.";
        public readonly static string PasswordDigitErrorMessage = $"Password should contain at least {PasswordDigitCount} digit.";
        public readonly static string PasswordLetterErrorMessage = $"Password should contain at least {PasswordLetterCount} letter.";
    }
}
