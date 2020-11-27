using System.Text.RegularExpressions;

namespace GameLoanManager.CrossCutting
{
    public static class StringExtensions
    {
        public static string FormatCellPhoneNumber(this string value) =>
            string.IsNullOrEmpty(value) ? value : Regex.Replace(value, @"[^0-9]", "");
    }
}
