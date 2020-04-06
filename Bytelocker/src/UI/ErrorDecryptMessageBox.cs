using System.Windows.Forms;

namespace Bytelocker.UI
{
    internal class ErrorDecryptMessageBox
    {
        public static bool showMessageBoxDecryptError(string file_path)
        {
            var messageBoxInput = MessageBox.Show("Failed to decrypt a previously encrypted file: \"" + file_path +
                                                  "\".\n\n" +
                                                  "The file may be damaged/removed/locked or used by another process",
                "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

            if (messageBoxInput == DialogResult.Retry)
                return true;
            return false;
        }
    }
}