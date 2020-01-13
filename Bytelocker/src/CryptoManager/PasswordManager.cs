using Bytelocker.Settings;
using Bytelocker.src.Tools;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Bytelocker.CryptoManager
{
    /*
     * NOTE: storing password in registry and encoding/decoding using b64 is very insecure
     * Using a custom server to generate and send/receive would be optimal.
     */

    class PasswordManager
    {
        protected String password;

        // password lenght must NOT be 4 as "none" used in RegistryManager
        public int PASSWORD_LENGHT = 30;
        public String VALID_CHAR = "abcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?{}[]().,;:";
        RegistryManager rm;

        public PasswordManager()
        {
            this.rm = new RegistryManager();
        }


        public void FetchPassword()
        {
            // tmp solution for password
            String pwd = this.rm.ReadStringValue("", RegistryManager.PWD_VALUE_NAME);

            if (pwd == "none")
            {
                this.UploadPassword();
                this.FetchPassword();
            } else
            {
                this.password = B64Manager.Base64ToString(pwd);
            }
        }

        public String returnPassword()
        {
            return this.password;
        }

        private void UploadPassword()
        {
            // tmp solution for password
            this.rm.CreateStringValue("", RegistryManager.PWD_VALUE_NAME, B64Manager.ToBase64(this.GenerateRandomPassword()));
        }

        private String GenerateRandomPassword()
        {
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();
            char randChar;

            while (0 < PASSWORD_LENGHT--)
            {
                randChar = VALID_CHAR[rand.Next(VALID_CHAR.Length)];
                if (char.IsLetter(randChar))
                {
                    // if char is letter, do rand bool to determine uppercase or lowercase
                   randChar = (rand.Next(100) <= 20 ? true : false) ? char.ToUpper(randChar) : randChar;
                }
                sb.Append(randChar);
            }
            return sb.ToString();
        }
    }
}
