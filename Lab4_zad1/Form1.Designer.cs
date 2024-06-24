namespace Lab4_zad1
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnSelectFiles;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Button btnUploadFiles;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.Button btnLoadConfig;
        private System.Windows.Forms.ListBox lstFiles;
        private System.Windows.Forms.TextBox txtEncryptionKey;
        private System.Windows.Forms.TextBox txtIV;
        private System.Windows.Forms.ProgressBar progressBar;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnSelectFiles = new Button();
            btnEncrypt = new Button();
            btnDecrypt = new Button();
            btnUploadFiles = new Button();
            btnSaveConfig = new Button();
            btnLoadConfig = new Button();
            lstFiles = new ListBox();
            txtEncryptionKey = new TextBox();
            txtIV = new TextBox();
            generateKeyIVButton = new Button();
            progressBar = new ProgressBar();
            SuspendLayout();
            // 
            // btnSelectFiles
            // 
            btnSelectFiles.Location = new Point(12, 12);
            btnSelectFiles.Name = "btnSelectFiles";
            btnSelectFiles.Size = new Size(100, 23);
            btnSelectFiles.TabIndex = 0;
            btnSelectFiles.Text = "Select Files";
            btnSelectFiles.UseVisualStyleBackColor = true;
            btnSelectFiles.Click += btnSelectFiles_Click;
            // 
            // btnEncrypt
            // 
            btnEncrypt.Location = new Point(12, 69);
            btnEncrypt.Name = "btnEncrypt";
            btnEncrypt.Size = new Size(100, 23);
            btnEncrypt.TabIndex = 1;
            btnEncrypt.Text = "Encrypt";
            btnEncrypt.UseVisualStyleBackColor = true;
            btnEncrypt.Click += btnEncrypt_Click;
            // 
            // btnDecrypt
            // 
            btnDecrypt.Location = new Point(12, 98);
            btnDecrypt.Name = "btnDecrypt";
            btnDecrypt.Size = new Size(100, 23);
            btnDecrypt.TabIndex = 2;
            btnDecrypt.Text = "Decrypt";
            btnDecrypt.UseVisualStyleBackColor = true;
            btnDecrypt.Click += btnDecrypt_Click;
            // 
            // btnUploadFiles
            // 
            btnUploadFiles.Location = new Point(12, 127);
            btnUploadFiles.Name = "btnUploadFiles";
            btnUploadFiles.Size = new Size(100, 23);
            btnUploadFiles.TabIndex = 3;
            btnUploadFiles.Text = "Upload Files";
            btnUploadFiles.UseVisualStyleBackColor = true;
            btnUploadFiles.Click += btnUploadFiles_Click;
            // 
            // btnSaveConfig
            // 
            btnSaveConfig.Location = new Point(12, 156);
            btnSaveConfig.Name = "btnSaveConfig";
            btnSaveConfig.Size = new Size(100, 23);
            btnSaveConfig.TabIndex = 4;
            btnSaveConfig.Text = "Save Config";
            btnSaveConfig.UseVisualStyleBackColor = true;
            btnSaveConfig.Click += btnSaveConfig_Click;
            // 
            // btnLoadConfig
            // 
            btnLoadConfig.Location = new Point(12, 185);
            btnLoadConfig.Name = "btnLoadConfig";
            btnLoadConfig.Size = new Size(100, 23);
            btnLoadConfig.TabIndex = 5;
            btnLoadConfig.Text = "Load Config";
            btnLoadConfig.UseVisualStyleBackColor = true;
            btnLoadConfig.Click += btnLoadConfig_Click;
            // 
            // lstFiles
            // 
            lstFiles.FormattingEnabled = true;
            lstFiles.ItemHeight = 15;
            lstFiles.Location = new Point(120, 12);
            lstFiles.Name = "lstFiles";
            lstFiles.Size = new Size(456, 94);
            lstFiles.TabIndex = 6;
            // 
            // txtEncryptionKey
            // 
            txtEncryptionKey.Location = new Point(120, 128);
            txtEncryptionKey.Name = "txtEncryptionKey";
            txtEncryptionKey.Size = new Size(456, 23);
            txtEncryptionKey.TabIndex = 7;
            txtEncryptionKey.Text = "Enter encryption key";
            // 
            // txtIV
            // 
            txtIV.Location = new Point(120, 157);
            txtIV.Name = "txtIV";
            txtIV.Size = new Size(456, 23);
            txtIV.TabIndex = 8;
            txtIV.Text = "Enter IV";
            // 
            // generateKeyIVButton
            // 
            generateKeyIVButton.Location = new Point(12, 40);
            generateKeyIVButton.Name = "generateKeyIVButton";
            generateKeyIVButton.Size = new Size(100, 23);
            generateKeyIVButton.TabIndex = 9;
            generateKeyIVButton.Text = "Generate Key";
            generateKeyIVButton.UseVisualStyleBackColor = true;
            generateKeyIVButton.Click += generateKeyIVButton_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(12, 228);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(564, 23);
            progressBar.TabIndex = 7;
            // 
            // MainForm
            // 
            ClientSize = new Size(588, 272);
            Controls.Add(generateKeyIVButton);
            Controls.Add(txtIV);
            Controls.Add(txtEncryptionKey);
            Controls.Add(lstFiles);
            Controls.Add(btnLoadConfig);
            Controls.Add(btnSaveConfig);
            Controls.Add(btnUploadFiles);
            Controls.Add(btnDecrypt);
            Controls.Add(btnEncrypt);
            Controls.Add(btnSelectFiles);
            Controls.Add(progressBar);
            Name = "MainForm";
            Text = "Txt File Encryptor App";
            ResumeLayout(false);
            PerformLayout();
        }

        private Button generateKeyIVButton;
    }
}