using Bytelocker.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bytelocker.UI
{
    public partial class EncryptedFilesList : Form
    {
        private RegistryManager rm;

        public EncryptedFilesList()
        {
            InitializeComponent();
            rm = new RegistryManager();
            this.Load += new System.EventHandler(this.EncryptedFilesList_Load);
        }

        private void EncryptedFilesList_Load(object sender, EventArgs e)
        {
            ListViewItem lvi;
            ImageList icon_list = new ImageList();

            // get all icons for each file
            foreach (String path in rm.ReadAllValues(RegistryManager.FILES_KEY_NAME))
            {
                icon_list.Images.Add(Icon.ExtractAssociatedIcon(path));
            }

            lvFilesList.SmallImageList = icon_list;
            int i = 0;

            foreach (String path in rm.ReadAllValues(RegistryManager.FILES_KEY_NAME))
            {
                lvi = new ListViewItem(Path.GetFileName(path), i);
                lvi.SubItems.Add(new FileInfo(path).DirectoryName);
                lvFilesList.Items.Add(lvi);
                i++;
            }
        }
    }
}
