using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AgendaE.Common.Extension
{
    public static class PasswordValidator
    {
        public static bool IsValidPassword(this string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                return false;

            bool hasUpperCase = Regex.IsMatch(password, "[A-Z]");
            bool hasLowerCase = Regex.IsMatch(password, "[a-z]");
            bool hasDigit = Regex.IsMatch(password, @"\d");
            bool hasSpecialChar = Regex.IsMatch(password, @"[\W_]"); // \W = qualquer coisa que não é letra ou número

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }
    }
}
