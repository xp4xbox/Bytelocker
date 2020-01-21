using System.Text;
using System.Runtime.InteropServices;
using System;

namespace Bytelocker.src.UI
{
    class FilePathTruncate
    {
        // http://www.csharp411.com/truncate-file-path-with-ellipsis/
        [DllImport("shlwapi.dll")]
        static extern bool PathCompactPathEx([Out] StringBuilder pszOut, string szPath, int cchMax, int dwFlags);

        public static string TruncatePath(string path, int length)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                PathCompactPathEx(sb, path, length, 0);
                return sb.ToString();
            } catch (Exception)
            {
                return path;
            }
           
        }

    }
}
