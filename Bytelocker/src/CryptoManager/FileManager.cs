using System;
using System.IO;

namespace Bytelocker.CryptoManager
{
    class FileManager
    {
        private String file_path;
        private FileEncrypter fe = new FileEncrypter();
        private RegistryManager rm = new RegistryManager();

        public void ChooseFile(String file_path)
        {
            this.file_path = file_path;
        }

        public void DeleteFile()
        {
            try
            {
                File.Delete(this.file_path);
            } catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
            }
            
        }

        public void SetupFileEncrypter()
        {
            this.fe.FetchPassword();
            this.fe.PinPasswordToMemory();
            this.fe.GenerateRandomSalt();
        }

        public bool DecryptFile()
        {
            bool success = false;
            
            if (File.Exists(this.file_path))
            {
                if (!(FileManager.IsFileLocked(this.file_path)) && (Path.GetExtension(this.file_path) == FileEncrypter.FILE_EXTENSION_ENCRYPT))
                {
                    success = true;
                    this.fe.ChooseFile(this.file_path);
                    this.fe.Decrypt();
                    try
                    {
                        rm.DeleteValue(RegistryManager.FILES_KEY_NAME, this.file_path.Substring(0, this.file_path.Length - FileEncrypter.FILE_EXTENSION_ENCRYPT.Length));
                    } catch (System.ArgumentException)
                    {
                        // if the value does not exist in registry
                    }
                    
                }
            }

            this.fe.ClearPasswordFromMemory();

            return success;
        }

        public bool EncryptFile()
        {
            bool success = true;

            String file_extension = Path.GetExtension(this.file_path);

            if (!(IsFileLocked(this.file_path)) && file_extension != FileEncrypter.FILE_EXTENSION_ENCRYPT && FileEncrypter.FILE_EXTENSIONS_TO_ENCRYPT.Contains(file_extension))
            {
                this.fe.ChooseFile(this.file_path);
                this.fe.Encrypt();
                rm.CreateBoolValue(RegistryManager.FILES_KEY_NAME, this.file_path, true);
            } else
            {
                success = false;
            }

            this.fe.ClearPasswordFromMemory();

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
