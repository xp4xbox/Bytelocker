using System;
using System.IO;

namespace Bytelocker.CryptoManager
{
    class FileManager
    {
        private String file_path;
        private FileEncrypter fe;

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
            this.fe = new FileEncrypter();

            this.fe.FetchPassword();
            this.fe.PinPasswordToMemory();
            this.fe.GenerateRandomSalt();
        }

        public void DecryptFile()
        {
            if (!(FileManager.IsFileLocked(this.file_path)) && (Path.GetExtension(this.file_path) == FileEncrypter.FILE_EXTENSION_ENCRYPT))
            {
                this.fe.ChooseFile(this.file_path);
                this.fe.Decrypt();
            }

            this.fe.ClearPasswordFromMemory();
        }

        public void EncryptFile()
        {
            String file_extension = Path.GetExtension(this.file_path);

            if (!(IsFileLocked(this.file_path)) && file_extension != FileEncrypter.FILE_EXTENSION_ENCRYPT && FileEncrypter.FILE_EXTENSIONS_TO_ENCRYPT.Contains(file_extension))
            {
                this.fe.ChooseFile(this.file_path);
                this.fe.Encrypt();
            }

            this.fe.ClearPasswordFromMemory();
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
