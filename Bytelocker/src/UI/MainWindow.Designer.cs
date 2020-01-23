namespace Bytelocker.UI
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.btnNext = new System.Windows.Forms.Button();
            this.rtfInfo = new System.Windows.Forms.RichTextBox();
            this.lbTitle = new System.Windows.Forms.Label();
            this.pbShield = new System.Windows.Forms.PictureBox();
            this.lbTitleTime = new System.Windows.Forms.Label();
            this.lbTimeLeft = new System.Windows.Forms.Label();
            this.tmTimeLeftRefresher = new System.Windows.Forms.Timer(this.components);
            this.tbPassInput = new System.Windows.Forms.TextBox();
            this.BtnVerify = new System.Windows.Forms.Button();
            this.pbDecryptProgress = new System.Windows.Forms.ProgressBar();
            this.lbCurrentFileDecrypt = new System.Windows.Forms.Label();
            this.tmTimerDecrypt = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbShield)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(631, 381);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(120, 35);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "Next >>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // rtfInfo
            // 
            this.rtfInfo.Location = new System.Drawing.Point(234, 58);
            this.rtfInfo.Name = "rtfInfo";
            this.rtfInfo.ReadOnly = true;
            this.rtfInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtfInfo.Size = new System.Drawing.Size(517, 317);
            this.rtfInfo.TabIndex = 1;
            this.rtfInfo.Text = "";
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.Location = new System.Drawing.Point(280, 9);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(407, 29);
            this.lbTitle.TabIndex = 2;
            this.lbTitle.Text = "Your personal files are encrypted!";
            // 
            // pbShield
            // 
            this.pbShield.Image = ((System.Drawing.Image)(resources.GetObject("pbShield.Image")));
            this.pbShield.Location = new System.Drawing.Point(42, 58);
            this.pbShield.Name = "pbShield";
            this.pbShield.Size = new System.Drawing.Size(151, 180);
            this.pbShield.TabIndex = 3;
            this.pbShield.TabStop = false;
            // 
            // lbTitleTime
            // 
            this.lbTitleTime.AutoSize = true;
            this.lbTitleTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitleTime.Location = new System.Drawing.Point(77, 273);
            this.lbTitleTime.Name = "lbTitleTime";
            this.lbTitleTime.Size = new System.Drawing.Size(73, 20);
            this.lbTitleTime.TabIndex = 4;
            this.lbTitleTime.Text = "Time left:";
            // 
            // lbTimeLeft
            // 
            this.lbTimeLeft.AutoSize = true;
            this.lbTimeLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTimeLeft.Location = new System.Drawing.Point(62, 303);
            this.lbTimeLeft.Name = "lbTimeLeft";
            this.lbTimeLeft.Size = new System.Drawing.Size(104, 20);
            this.lbTimeLeft.TabIndex = 6;
            this.lbTimeLeft.Text = "00:00:00:00";
            // 
            // tmTimeLeftRefresher
            // 
            this.tmTimeLeftRefresher.Enabled = true;
            this.tmTimeLeftRefresher.Interval = 1000;
            this.tmTimeLeftRefresher.Tick += new System.EventHandler(this.UpdateTimeLeftEvent);
            // 
            // tbPassInput
            // 
            this.tbPassInput.Location = new System.Drawing.Point(259, 95);
            this.tbPassInput.Name = "tbPassInput";
            this.tbPassInput.Size = new System.Drawing.Size(341, 20);
            this.tbPassInput.TabIndex = 7;
            // 
            // BtnVerify
            // 
            this.BtnVerify.Location = new System.Drawing.Point(631, 381);
            this.BtnVerify.Name = "BtnVerify";
            this.BtnVerify.Size = new System.Drawing.Size(120, 35);
            this.BtnVerify.TabIndex = 9;
            this.BtnVerify.Text = "Verify";
            this.BtnVerify.UseVisualStyleBackColor = true;
            this.BtnVerify.Click += new System.EventHandler(this.BtnVerify_Click);
            // 
            // pbDecryptProgress
            // 
            this.pbDecryptProgress.Location = new System.Drawing.Point(259, 229);
            this.pbDecryptProgress.Name = "pbDecryptProgress";
            this.pbDecryptProgress.Size = new System.Drawing.Size(467, 21);
            this.pbDecryptProgress.TabIndex = 10;
            // 
            // lbCurrentFileDecrypt
            // 
            this.lbCurrentFileDecrypt.AutoSize = true;
            this.lbCurrentFileDecrypt.Location = new System.Drawing.Point(256, 204);
            this.lbCurrentFileDecrypt.Name = "lbCurrentFileDecrypt";
            this.lbCurrentFileDecrypt.Size = new System.Drawing.Size(0, 13);
            this.lbCurrentFileDecrypt.TabIndex = 11;
            // 
            // tmTimerDecrypt
            // 
            this.tmTimerDecrypt.Interval = 80;
            this.tmTimerDecrypt.Tick += new System.EventHandler(this.tmTimerDecrypt_Tick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 421);
            this.Controls.Add(this.lbCurrentFileDecrypt);
            this.Controls.Add(this.pbDecryptProgress);
            this.Controls.Add(this.BtnVerify);
            this.Controls.Add(this.tbPassInput);
            this.Controls.Add(this.lbTimeLeft);
            this.Controls.Add(this.lbTitleTime);
            this.Controls.Add(this.pbShield);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.rtfInfo);
            this.Controls.Add(this.btnNext);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.ShowInTaskbar = false;
            this.Text = "Bytelocker";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbShield)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.RichTextBox rtfInfo;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.PictureBox pbShield;
        private System.Windows.Forms.Label lbTitleTime;
        private System.Windows.Forms.Label lbTimeLeft;
        private System.Windows.Forms.Timer tmTimeLeftRefresher;
        private System.Windows.Forms.TextBox tbPassInput;
        private System.Windows.Forms.Button BtnVerify;
        private System.Windows.Forms.ProgressBar pbDecryptProgress;
        private System.Windows.Forms.Label lbCurrentFileDecrypt;
        private System.Windows.Forms.Timer tmTimerDecrypt;
    }
}