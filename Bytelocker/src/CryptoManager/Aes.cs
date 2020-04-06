using System.Security.Cryptography;

namespace Bytelocker.CryptoManager
{
    internal class Aes
    {
        private const int SALT_BUFFER = 32;

        public byte[] GenerateRandomSalt()
        {
            var data = new byte[SALT_BUFFER];

            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                for (var i = 0; i < 10; i++) randomNumberGenerator.GetBytes(data);
            }

            return data;
        }
    }
}