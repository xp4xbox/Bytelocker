using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bytelocker.src
{
    class CommandManager
    {
        private System.Diagnostics.Process ps;
        private System.Diagnostics.ProcessStartInfo pssi;

        public CommandManager()
        {
            this.ps = new System.Diagnostics.Process();
            this.pssi = new System.Diagnostics.ProcessStartInfo();

            this.pssi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            this.pssi.FileName = "cmd.exe";
            this.pssi.Arguments = "/c ";
        }

        public void CopyToClipboard(String text)
        {
            this.pssi.Arguments += ("echo " + text + " | clip");
            this.StartCommand();
        }

        private void StartCommand()
        {
            this.ps.StartInfo = pssi;
            this.ps.Start();
        }
    }
}
