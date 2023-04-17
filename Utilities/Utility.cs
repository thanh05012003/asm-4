using System.Text.RegularExpressions;

namespace ZestWeb.Utilities
{
    public class Utility
    {
        public bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password)||password.Length < 7)
            {
                return false;
            }

            if (!Regex.IsMatch(password, @"^[a-zA-Z0-9]+$"))
            {
                return false;
            }

            return true;
        }
    }
}
