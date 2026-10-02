using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ZAFMC
{
    // Validation rules for the Congregation screen. Pure logic (no UI, no DB) so it can be tested
    // without the page. Every Validate* method returns null when valid, otherwise a user-facing message.
    public static class CongregationRules
    {
        public const int SearchMaxLength = 20;

        // Format used by <input type="date"> when posting.
        private const string DateFormat = "yyyy-MM-dd";

        private static readonly Regex MembershipNumberPattern = new Regex("^ZAFMC[0-9]{8}$", RegexOptions.CultureInvariant);

        // Date Of Birth is optional, but when given it must be a valid date and not later than today.
        public static string ValidateDateOfBirth(string text, DateTime today, out DateTime? dob)
        {
            string error = ValidateOptionalDate(text, "Date Of Birth", out dob);
            if (error != null)
            {
                return error;
            }
            if (dob.HasValue && dob.Value.Date > today.Date)
            {
                dob = null;
                return "Date Of Birth cannot be later than today.";
            }
            return null;
        }

        // Empty -> valid with a null value; otherwise must be a valid yyyy-MM-dd date.
        public static string ValidateOptionalDate(string text, string label, out DateTime? value)
        {
            value = null;
            string trimmed = (text ?? string.Empty).Trim();
            if (trimmed.Length == 0)
            {
                return null;
            }
            DateTime parsed;
            if (!DateTime.TryParseExact(trimmed, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed))
            {
                return label + " is not a valid date.";
            }
            value = parsed.Date;
            return null;
        }

        // Exactly one of Male / Female must be selected.
        public static string ValidateGender(string selectedValue)
        {
            if (string.Equals(selectedValue, "Male", StringComparison.Ordinal) ||
                string.Equals(selectedValue, "Female", StringComparison.Ordinal))
            {
                return null;
            }
            return "Please select Male or Female.";
        }

        public static string ValidateRequired(string value, string label)
        {
            return string.IsNullOrWhiteSpace(value) ? label + " is required." : null;
        }

        public static string ValidateMaxLength(string value, int maxLength, string label)
        {
            return (value ?? string.Empty).Length > maxLength
                ? label + " cannot be longer than " + maxLength + " characters."
                : null;
        }

        // "ZAFMC" followed by exactly 8 digits, e.g. ZAFMC04718263.
        public static bool IsMembershipNumber(string value)
        {
            return value != null && MembershipNumberPattern.IsMatch(value);
        }

        // Turns a search term into a LIKE "starts with" pattern, escaping LIKE wildcards.
        public static string ToLikePrefix(string term)
        {
            string t = (term ?? string.Empty).Trim();
            t = t.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");
            return t + "%";
        }
    }
}
