using Bytelocker.CryptoManager;

namespace Bytelocker.Installer
{
    class Uninstall
    {
        public Uninstall()
        {
            this.deleteRegChanges();
        }

        private void deleteRegChanges()
        {
            RegistryManager rm = new RegistryManager();
            rm.DeleteMainKey();
        }
    }
}
