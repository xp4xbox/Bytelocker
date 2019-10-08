using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bytelocker.src.Tools
{
    class B64Manager
    {
        public static String ToBase64(String text)
        {
            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(text));
        }

        public static String Base64ToString(String b64_text)
        {
            return System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(b64_text));
        }
    }
}
