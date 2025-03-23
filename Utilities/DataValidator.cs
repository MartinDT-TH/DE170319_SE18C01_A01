using System;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Utilities
{
    public static class DataValidator
    {
        public static bool IsNullOrEmpty(string? value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            string phonePattern = @"^\+?[1-9]\d{1,14}$"; // Supports international format
            return Regex.IsMatch(phoneNumber, phonePattern);
        }
        public static bool IsValidLength(string value, int minLength, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return value.Length >= minLength && value.Length <= maxLength;
        }
        public static bool IsInRange(int value, int min, int max)
        {
            return value >= min && value <= max;
        }
        public static bool IsValidPastDate(DateTime date)
        {
            return date <= DateTime.Now;
        }
        public static bool IsValidFutureDate(DateTime date)
        {
            return date >= DateTime.Now;
        }
        public static bool IsAlphabetOnly(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return Regex.IsMatch(value, @"^[a-zA-Z]+$");
        }
        public static bool IsNumericOnly(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return Regex.IsMatch(value, @"^\d+$");
        }
        public static bool IsAlphaNumeric(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return Regex.IsMatch(value, @"^[a-zA-Z0-9]+$");
        }

        /// <summary>
        /// Checks if a password meets complexity requirements.
        /// - At least one uppercase letter
        /// - At least one lowercase letter
        /// - At least one number
        /// - At least one special character
        /// - Minimum length of 8 characters
        /// </summary>
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            string passwordPattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$";
            return Regex.IsMatch(password, passwordPattern);
        }

        /// <summary>
        /// Checks if a URL is valid.
        /// </summary>
        public static bool IsValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            string urlPattern = @"^(http|https)://[a-zA-Z0-9\-.]+\.[a-zA-Z]{2,}(/\S*)?$";
            return Regex.IsMatch(url, urlPattern);
        }

        /// <summary>
        /// Ensures a credit card number is valid using the Luhn algorithm.
        /// </summary>
        public static bool IsValidCreditCard(string creditCardNumber)
        {
            if (string.IsNullOrWhiteSpace(creditCardNumber) || !Regex.IsMatch(creditCardNumber, @"^\d+$"))
                return false;

            int sum = 0;
            bool alternate = false;
            for (int i = creditCardNumber.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(creditCardNumber[i].ToString());
                if (alternate)
                {
                    n *= 2;
                    if (n > 9) n -= 9;
                }
                sum += n;
                alternate = !alternate;
            }

            return (sum % 10 == 0);
        }

        /// <summary>
        /// Ensures an IBAN (International Bank Account Number) is valid.
        /// </summary>
        public static bool IsValidIBAN(string iban)
        {
            if (string.IsNullOrWhiteSpace(iban))
                return false;

            iban = iban.Replace(" ", "").ToUpper();
            if (!Regex.IsMatch(iban, @"^[A-Z0-9]+$"))
                return false;

            string rearrangedIban = iban.Substring(4) + iban.Substring(0, 4);
            string numericIban = string.Empty;
            foreach (char c in rearrangedIban)
            {
                numericIban += char.IsLetter(c) ? (c - 'A' + 10).ToString() : c.ToString();
            }

            return BigInteger.Parse(numericIban) % 97 == 1;
        }

        /// <summary>
        /// Ensures a ZIP/postal code is valid (supports international formats).
        /// </summary>
        public static bool IsValidZipCode(string zipCode, string countryCode)
        {
            if (string.IsNullOrWhiteSpace(zipCode))
                return false;

            var zipPatterns = new Dictionary<string, string>
            {
                { "US", @"^\d{5}(-\d{4})?$" }, // USA (ZIP + ZIP+4)
                { "CA", @"^[A-Za-z]\d[A-Za-z] ?\d[A-Za-z]\d$" }, // Canada (A1A 1A1)
                { "UK", @"^[A-Za-z\d]{1,4} ?\d[A-Za-z]{2}$" }, // UK
                { "DE", @"^\d{5}$" }, // Germany
                { "FR", @"^\d{5}$" } // France
            };

            return zipPatterns.ContainsKey(countryCode) && Regex.IsMatch(zipCode, zipPatterns[countryCode]);
        }
    }
}
