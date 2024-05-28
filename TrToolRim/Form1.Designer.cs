namespace TrToolRim
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckedListBox checkedListBoxFolders;
        private System.Windows.Forms.Button btnUpdateFiles;

        private void InitializeComponent()
        {
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.checkedListBoxFolders = new System.Windows.Forms.CheckedListBox();
            this.btnUpdateFiles = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(13, 13);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(100, 23);
            this.btnOpenFolder.TabIndex = 0;
            this.btnOpenFolder.Text = "Open Folder";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(713, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 384);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(775, 211);
            this.dataGridView.TabIndex = 2;
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.Location = new System.Drawing.Point(12, 283);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(775, 95);
            this.listBoxFiles.TabIndex = 4;
            this.listBoxFiles.SelectedIndexChanged += new System.EventHandler(this.listBoxFiles_SelectedIndexChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(10, 598);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(192, 13);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Join our Discord community for support!";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // checkedListBoxFolders
            // 
            this.checkedListBoxFolders.FormattingEnabled = true;
            this.checkedListBoxFolders.Location = new System.Drawing.Point(12, 42);
            this.checkedListBoxFolders.Name = "checkedListBoxFolders";
            this.checkedListBoxFolders.Size = new System.Drawing.Size(774, 229);
            this.checkedListBoxFolders.TabIndex = 5;
            // 
            // btnUpdateFiles
            // 
            this.btnUpdateFiles.Location = new System.Drawing.Point(120, 13);
            this.btnUpdateFiles.Name = "btnUpdateFiles";
            this.btnUpdateFiles.Size = new System.Drawing.Size(100, 23);
            this.btnUpdateFiles.TabIndex = 6;
            this.btnUpdateFiles.Text = "Update Files";
            this.btnUpdateFiles.UseVisualStyleBackColor = true;
            this.btnUpdateFiles.Click += new System.EventHandler(this.btnUpdateFiles_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 620);
            this.Controls.Add(this.btnUpdateFiles);
            this.Controls.Add(this.checkedListBoxFolders);
            this.Controls.Add(this.listBoxFiles);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.linkLabel1);
            this.Name = "Form1";
            this.Text = "RimWorld Mod Translator";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
