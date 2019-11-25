using System;

namespace Orient.Base.Net.Core.Api.Core.Common.Extensions
{
    public static class StringExtension
    {
        public static string[] SplitToArray(this string text, string separator)
        {
            string[] separators = null;
            if (string.IsNullOrEmpty(separator))
            {
                separators = new string[] { "," };
            }
            else
            {
                separators = new string[] { separator };
            }
            return text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string CustomString(string str)
        {
            str = str.Trim();
            var arrayStr = str.Split(" ");

            string result = "";

            foreach (var text in arrayStr)
            {
                if (!text.Equals(""))
                {
                    result += text + " ";
                }
            }

            return result.TrimEnd();
        }
    }
}
