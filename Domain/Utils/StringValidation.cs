using System;
using System.Text.RegularExpressions;

namespace Domain.Utils
{
    public static class StringValidation
    {
        public static bool IsValid(this string value)
        {
            return !string.IsNullOrEmpty(value) &&
                !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsValidEmail(this string value)
        {
            return value.IsValid() &&
                Regex.IsMatch(value,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }

        public static bool IsValidPassword(this string value)
        {
            return value.IsValid() &&
                Regex.IsMatch(value,
                @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[~`!@#\$%\^&\*\(\)_\-\+=\{\[\}\]\|\\:;'<,>\.\?\/]).{8,}$",
                RegexOptions.None,
                TimeSpan.FromMilliseconds(250));
        }
    }
}
