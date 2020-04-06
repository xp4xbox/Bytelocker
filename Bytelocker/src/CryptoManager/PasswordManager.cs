using System;
using System.Text;
using Bytelocker.Settings;
using Bytelocker.Tools;

namespace Bytelocker.CryptoManager
{
    /*
     * NOTE: storing password in registry and encoding/decoding using b64 is very insecure
     * Using a custom server to generate and send/receive would be optimal.
     */

    internal class PasswordManager
    {
        protected string password;

        // password lenght must NOT be 4 as "none" used in RegistryManager
        public int PASSWORD_LENGHT = 30;
        private readonly RegistryManager rm;
        public string VALID_CHAR = "abcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?{}[]().,;:";

        public PasswordManager()
        {
            rm = new RegistryManager();
        }

        public void FetchPassword()
        {
            // tmp solution for password
            var pwd = rm.ReadStringValue("", RegistryManager.PWD_VALUE_NAME);

            if (pwd == "none")
            {
                UploadPassword();
                FetchPassword();
            }
            else
            {
                password = B64Manager.Base64ToString(pwd);
            }
        }

        public string returnPassword()
        {
            return password;
        }

        private void UploadPassword()
        {
            // tmp solution for password
            rm.CreateStringValue("", RegistryManager.PWD_VALUE_NAME, B64Manager.ToBase64(GenerateRandomPassword()));
        }

        private string GenerateRandomPassword()
        {
            var sb = new StringBuilder();
            var rand = new Random();
            char randChar;

            while (0 < PASSWORD_LENGHT--)
            {
                randChar = VALID_CHAR[rand.Next(VALID_CHAR.Length)];
                if (char.IsLetter(randChar))
                    // if char is letter, do rand bool to determine uppercase or lowercase
                    randChar = (rand.Next(100) <= 20 ? true : false) ? char.ToUpper(randChar) : randChar;
                sb.Append(randChar);
            }

            return sb.ToString();
        }
    }
}