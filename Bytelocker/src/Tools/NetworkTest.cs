using System.Net;

namespace Bytelocker.src.Tools
{
    class NetworkTest
    {
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("https://www.blockchain.info/"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
