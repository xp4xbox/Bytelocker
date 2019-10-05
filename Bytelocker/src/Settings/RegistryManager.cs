using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace Bytelocker.CryptoManager
{
    class RegistryManager
    {

        private String SOFTWARE_NAME = @"Software\ByteLocker";
        public static String FOLDER_KEY_NAME = "Folders";
        public static String FILES_KEY_NAME = "Files";
        public static String SETTINGS_KEY_NAME = "Config";

        public List<String> ReadAllValues(String subKey)
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(SOFTWARE_NAME + @"\" + subKey);
            return new List<String>(regKey.GetValueNames());
        }

        public void CreateMainKey()
        {
            Registry.CurrentUser.CreateSubKey(SOFTWARE_NAME);
        }

        public void CreateSubKey(String name)
        {
            Registry.CurrentUser.CreateSubKey(SOFTWARE_NAME + @"\" + name);
        }

        public void DeleteSubKey(string name)
        {
            Registry.CurrentUser.DeleteSubKey(SOFTWARE_NAME + @"\" + name);
        }

        public void CreateBoolValue(String subKey, String valueName, bool value)
        {
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey(SOFTWARE_NAME + @"\" + subKey);
            regKey.SetValue(valueName, value ? 1:0);
        }

        public void DeleteValue(String subKey, String valueName)
        {
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey(SOFTWARE_NAME + @"\" + subKey);
            regKey.DeleteValue(valueName);
        }

        public void CreateStringValue(String subKey, String valueName, String value)
        {
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey(SOFTWARE_NAME + @"\" + subKey);
            regKey.SetValue(valueName, value);
        }

        public void AddItemToStartup(String valuename, String filepath)
        {
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            regKey.SetValue(valuename, filepath);
        }

        public bool ReadBoolValue(String subKey, String valueName)
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(SOFTWARE_NAME + @"\" + subKey);

            try
            {
                return ((int)(regKey.GetValue(valueName)) == 1 ? true : false);

            } catch (System.NullReferenceException) {

                return false;
            }
        }
    }
}
