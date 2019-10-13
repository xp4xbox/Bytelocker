namespace Bytelocker.UI
{
    partial class EncryptedFilesList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lvFilesList = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvFilesList
            // 
            this.lvFilesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chLocation});
            this.lvFilesList.HideSelection = false;
            this.lvFilesList.Location = new System.Drawing.Point(0, 0);
            this.lvFilesList.MultiSelect = false;
            this.lvFilesList.Name = "lvFilesList";
            this.lvFilesList.Scrollable = false;
            this.lvFilesList.Size = new System.Drawing.Size(548, 406);
            this.lvFilesList.TabIndex = 0;
            this.lvFilesList.UseCompatibleStateImageBehavior = false;
            this.lvFilesList.View = System.Windows.Forms.View.Details;
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 180;
            // 
            // chLocation
            // 
            this.chLocation.Text = "Location";
            this.chLocation.Width = 281;
            // 
            // EncryptedFilesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 406);
            this.Controls.Add(this.lvFilesList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "EncryptedFilesList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "List of Encrypted Files";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvFilesList;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chLocation;
    }
}