using System.Security.Cryptography;

namespace Bytelocker.CryptoManager
{
    class Aes
    {
        private const int SALT_BUFFER = 32;

        public byte[] GenerateRandomSalt()
        {
            byte[] data = new byte[SALT_BUFFER];

            using (RNGCryptoServiceProvider randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    randomNumberGenerator.GetBytes(data);
                }
            }

            return data;
        }
    }
}
