using System;

namespace Bytelocker.Tools
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

        public void RunCommand(String command)
        {
            this.pssi.Arguments += command;
            this.StartCommand();
        }

        private void StartCommand()
        {
            this.ps.StartInfo = pssi;
            this.ps.Start();
        }
    }
}
