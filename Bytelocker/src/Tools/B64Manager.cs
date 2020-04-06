using System;
using System.Text;

namespace Bytelocker.Tools
{
    internal class B64Manager
    {
        public static string ToBase64(string text)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
        }

        public static string Base64ToString(string b64_text)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(b64_text));
        }
    }
}