using System.IO;

namespace Bytelocker.UI
{
    internal class TruncateFilePath
    {
        // referenced from http://chadkuehn.com/shrink-file-paths-with-an-ellipsis-in-c/
        public static string ShrinkPath(string file_path, int max, string delim = "…")
        {
            var file_name = Path.GetFileName(file_path);
            var file_name_lenght = file_name.Length;
            var path_lenght = file_path.Length;
            var dir = file_path.Substring(0, path_lenght - file_name_lenght);

            var delim_lenght = delim.Length;
            var ideal_min_lenght = file_name_lenght + delim_lenght;

            var slash = file_path.IndexOf("/") > -1 ? "/" : "\\";

            if (max < ideal_min_lenght) return delim + file_name.Substring(0, max - 2 * delim_lenght) + delim;

            if (max == ideal_min_lenght) return delim + file_name;

            return dir.Substring(0, max - (ideal_min_lenght + 1)) + delim + slash + file_path;
        }
    }
}