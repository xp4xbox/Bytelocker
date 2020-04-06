using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Bytelocker.Settings;

namespace Bytelocker.UI
{
    public partial class EncryptedFilesList : Form
    {
        private readonly RegistryManager rm;

        public EncryptedFilesList()
        {
            InitializeComponent();
            rm = new RegistryManager();
        }

        private void EncryptedFilesList_Load(object sender, EventArgs e)
        {
            ListViewItem lvi;
            var icon_list = new ImageList();

            var files_list = rm.ReadAllValues(RegistryManager.FILES_KEY_NAME);

            if (!(files_list[0] == "null"))
            {
                foreach (var path in files_list)
                    try
                    {
                        icon_list.Images.Add(Icon.ExtractAssociatedIcon(path));
                    }
                    catch (Exception)
                    {
                        // if the file icon is not accessible
                    }

                lvFilesList.SmallImageList = icon_list;
                var i = 0;

                foreach (var path in rm.ReadAllValues(RegistryManager.FILES_KEY_NAME))
                {
                    lvi = new ListViewItem(Path.GetFileName(path), i);
                    lvi.SubItems.Add(new FileInfo(path).DirectoryName);
                    lvFilesList.Items.Add(lvi);
                    i++;
                }
            }
        }
    }
}