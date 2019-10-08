using Bytelocker.Persistence;
using Bytelocker.Settings;
using Bytelocker.Tools;

namespace Bytelocker.Installer
{
    class Uninstall
    {
        public Uninstall()
        {
            this.DeleteRegChanges();
            this.RemoveFromStartup();
            this.CloseAndDeleteSelf();
        }

        private void DeleteRegChanges()
        {
            RegistryManager rm = new RegistryManager();
            rm.DeleteMainKey();
        }

        private void CloseAndDeleteSelf()
        {
            CommandManager cm = new CommandManager();
            cm.RunCommand("timeout 3 & del /f /q \"" + System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName + "\"");
            System.Environment.Exit(0);
        }

        private void RemoveFromStartup()
        {
            RegistryManager rm = new RegistryManager();
            rm.RemoveFromStartup(Melt.REGISTRY_STARTUP_VALUE_NAME);
        }
    }
}
