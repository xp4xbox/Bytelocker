using System;
using System.Windows.Forms;

namespace Bytelocker.UI
{
    class ErrorDecryptMessageBox
    {
        public static bool showMessageBoxDecryptError(String file_path)
        {
            DialogResult messageBoxInput = MessageBox.Show("Failed to decrypt a previously encrypted file: \"" + file_path + "\".\n\n" +
                "The file may be damaged/removed/locked or used by another process", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

            if (messageBoxInput == DialogResult.Retry)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
