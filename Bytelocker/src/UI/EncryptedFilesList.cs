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
        }

        private void EncryptedFilesList_Load(object sender, EventArgs e)
        {
            ListViewItem lvi;
            ImageList icon_list = new ImageList();

            List<String> files_list = rm.ReadAllValues(RegistryManager.FILES_KEY_NAME);

            if (!(files_list[0] == "null"))
            {
                foreach (String path in files_list)
                {
                    try
                    {
                        icon_list.Images.Add(Icon.ExtractAssociatedIcon(path));
                    } catch (Exception)
                    {
                        // if the file icon is not accessible
                    }
                    
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
}
