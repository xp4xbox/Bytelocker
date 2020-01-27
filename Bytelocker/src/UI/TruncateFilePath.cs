using System;
using System.IO;

namespace Bytelocker.UI
{
    class TruncateFilePath
    {
        // referenced from http://chadkuehn.com/shrink-file-paths-with-an-ellipsis-in-c/
        public static String ShrinkPath(string file_path, int max, String delim = "…")
        {
            String file_name = Path.GetFileName(file_path);
            int file_name_lenght = file_name.Length;
            int path_lenght = file_path.Length;
            String dir = file_path.Substring(0, path_lenght - file_name_lenght);

            int delim_lenght = delim.Length;
            int ideal_min_lenght = file_name_lenght + delim_lenght;

            String slash = (file_path.IndexOf("/") > -1 ? "/" : "\\");

            if (max < ideal_min_lenght)
            {
                return delim + file_name.Substring(0, (max - (2 * delim_lenght))) + delim;
            }

            if (max == ideal_min_lenght)
            {
                return delim + file_name;
            }

            return dir.Substring(0, (max - (ideal_min_lenght + 1))) + delim + slash + file_path;
        }
    }
}
