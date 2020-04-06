using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace Bytelocker.Settings
{
    internal class RegistryManager
    {
        public static string FILES_KEY_NAME = "Files";
        public static string SETTINGS_KEY_NAME = "Config";
        public static string PWD_VALUE_NAME = "id";

        private readonly string SOFTWARE_NAME = @"Software\ByteLocker";

        public List<string> ReadAllValues(string subKey)
        {
            var regKey = Registry.CurrentUser.OpenSubKey(SOFTWARE_NAME + @"\" + subKey);
            List<string> files_list;

            try
            {
                files_list = new List<string>(regKey.GetValueNames());

                if (files_list.Count == 0)
                    return new List<string> {"null"};
                return files_list;
            }
            catch (NullReferenceException)
            {
                return new List<string> {"null"};
            }
        }

        public void CreateMainKey()
        {
            Registry.CurrentUser.CreateSubKey(SOFTWARE_NAME);
        }

        public void CreateSubKey(string name)
        {
            Registry.CurrentUser.CreateSubKey(SOFTWARE_NAME + @"\" + name);
        }

        public void DeleteSubKey(string name)
        {
            Registry.CurrentUser.DeleteSubKey(SOFTWARE_NAME + @"\" + name);
        }

        public void DeleteMainKey()
        {
            Registry.CurrentUser.DeleteSubKeyTree(SOFTWARE_NAME);
        }

        public void CreateBoolValue(string subKey, string valueName, bool value)
        {
            var regKey = Registry.CurrentUser.CreateSubKey(SOFTWARE_NAME + @"\" + subKey);
            regKey.SetValue(valueName, value ? 1 : 0);
        }

        public void DeleteValue(string subKey, string valueName)
        {
            var regKey = Registry.CurrentUser.CreateSubKey(SOFTWARE_NAME + @"\" + subKey);
            regKey.DeleteValue(valueName);
        }

        public void CreateStringValue(string subKey, string valueName, string value)
        {
            var regKey = Registry.CurrentUser.CreateSubKey(SOFTWARE_NAME + @"\" + subKey);
            regKey.SetValue(valueName, value);
        }

        public void AddItemToStartup(string valueName, string filepath, string args)
        {
            var regKey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");

            if (args == "")
                regKey.SetValue(valueName, "\"" + filepath + "\"");
            else
                regKey.SetValue(valueName, "\"" + filepath + "\" " + args);
        }

        public string getStartupPath(string valuename)
        {
            var regKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");

            try
            {
                return regKey.GetValue(valuename).ToString();
            }
            catch (Exception)
            {
                return "none";
            }
        }

        public void RemoveFromStartup(string valueName)
        {
            try
            {
                var regKey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                regKey.DeleteValue(valueName);
            }
            catch (Exception)
            {
            }
        }

        public string ReadStringValue(string subKey, string valueName)
        {
            var regKey = Registry.CurrentUser.OpenSubKey(SOFTWARE_NAME + @"\" + subKey);

            try
            {
                return regKey.GetValue(valueName).ToString();
            }
            catch (Exception)
            {
                return "none";
            }
        }

        public bool ReadBoolValue(string subKey, string valueName)
        {
            var regKey = Registry.CurrentUser.OpenSubKey(SOFTWARE_NAME + @"\" + subKey);

            try
            {
                return (int) regKey.GetValue(valueName) == 1 ? true : false;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }
    }
}